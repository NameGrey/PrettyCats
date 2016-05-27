using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PrettyCats.DAL;
using PrettyCats.DAL.Entities;
using PrettyCats.DAL.Repositories;
using PrettyCats.DAL.Repositories.DbRepositories;
using PrettyCats.DAL.Services;
using PrettyCats.Helpers;
using PrettyCats.Models;

namespace PrettyCats.Controllers
{
	public class AdminController : Controller
	{
		enum PictureSizes
		{
			MainPicture,
			StandartSliderPicture,
			SmallSliderPicture
		}

		readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		static object lockObj = new object();

		private IKittensRepository kittensRepository;
		private IPicturesRepository picturesRepository;
		private IKittenBreedRepository breedsRepository;
		private IKittenOwnerRepository ownersRepository;
		private IKittenDisplayPlaceRepository displayPlacesRepository;

		private PicturesLinksConstructor picturesLinksConstructor;

		public AdminController()
		{
			var newContext = new StorageContext();
			kittensRepository = new DBKittensRepository(newContext);
			picturesRepository = new DbPicturesRepository(newContext);
			breedsRepository = new DbBreedsRepository(newContext);
			ownersRepository = new DbOwnersRepository(newContext);
			displayPlacesRepository = new DbDisplayPlacesRepository(newContext);

			picturesLinksConstructor = new PicturesLinksConstructor();
			CustomizeAutomapper();
		}

		private void CustomizeAutomapper()
		{
			AutoMapper.Mapper.Initialize(cfg =>
			{
				cfg.CreateMap<Pets, KittenOnTheAdminPageModelView>()
					.ForMember(i => i.PlaceOfDisplaying, i => i.MapFrom(m => m.DisplayPlace.PlaceOfDisplaying))
					.ForMember(i => i.ImageUrl,
						i => i.MapFrom(
							src => src.Pictures.FirstOrDefault(el => el.IsMainPicture) != null ? src.Pictures.First(el => el.IsMainPicture).Image : string.Empty))
					.ForMember(i => i.AllParents, i => i.UseValue(kittensRepository.GetCollection().Where(el => el.IsParent)))
					.ForMember(i => i.DisplayPlaces, i => i.UseValue(displayPlacesRepository.GetCollection()))
					.ForMember(i => i.Breeds, i => i.UseValue(breedsRepository.GetCollection()))
					.ForMember(i => i.Owners, i => i.UseValue(ownersRepository.GetCollection()))
					.ForMember(i => i.PictureID,
						i => i.MapFrom(
							src => src.Pictures.FirstOrDefault(el => el.IsMainPicture) != null ? (int?) src.Pictures.First(el => el.IsMainPicture).ID : null));

				cfg.CreateMap<Pets, AddKittenModelView>()
					.ForMember(i => i.BirthDate, i => i.MapFrom(src => src.BirthDate != null ? ((DateTime)src.BirthDate).ToString("dd.MM.yyyy") : string.Empty))
					.ForMember(i => i.BreedName, i => i.MapFrom(src => src.PetBreeds != null ? src.PetBreeds.RussianName : string.Empty))
					.ForMember(i => i.FatherName, i => i.MapFrom(src => src.Father != null ? src.Father.RussianName : string.Empty))
					.ForMember(i => i.MotherName, i => i.MapFrom(src => src.Mother != null ? src.Mother.RussianName : string.Empty))
					.ForMember(i => i.OwnerName, i => i.MapFrom(src => src.Owners != null ? src.Owners.Name : string.Empty))
					.ForMember(i => i.OwnerPhone, i => i.MapFrom(src => src.Owners != null ? src.Owners.Phone : string.Empty))
					.ForMember(i => i.Owners, i=>i.UseValue(ownersRepository.GetCollection()))
					.ForMember(i => i.AllParents, i => i.UseValue(kittensRepository.GetCollection().Where(el => el.IsParent)))
					.ForMember(i => i.DisplayPlaces, i => i.UseValue(displayPlacesRepository.GetCollection()))
					.ForMember(i => i.Breeds, i => i.UseValue(breedsRepository.GetCollection()));
			});

		}

		protected override void OnException(ExceptionContext filterContext)
		{
			LogHelper.WriteLog(Server.MapPath("~/App_Data/" + Settings.LogFileName), filterContext.Exception.ToString());

			if (filterContext.HttpContext.IsCustomErrorEnabled)
			{
				filterContext.ExceptionHandled = true;
				this.View("Error").ExecuteResult(this.ControllerContext);
			}
		}

		// GET: Admin
		public ActionResult Index()
		{
			return View("Admin");
		}

		[Authorize]
		public ActionResult AdminPanel()
		{
			return View();
		}

		[Authorize]
		public ActionResult AdminChangeKittens()
		{
			return GetKittensCollectionView(false, false);
		}

		[Authorize]
		public ActionResult AdminChangeKittensArchive()
		{
			return GetKittensCollectionView(false, true);
		}

		private ActionResult GetKittensCollectionView(bool isParent, bool isInArchive)
		{
			var kittens = kittensRepository.GetCollection().Where(e => e.IsParent == isParent && e.IsInArchive == isInArchive);
			var kittensModelViews = AutoMapper.Mapper.Map<IEnumerable<Pets>, IEnumerable<KittenOnTheAdminPageModelView>>(kittens);

			return View(isParent ? "AdminChangeParents" : "AdminChangeKittens", kittensModelViews);
		}

		[Authorize]
		public ActionResult AdminChangeParents()
		{
			return GetKittensCollectionView(true, false);
		}

		public ActionResult KittenOnTheAdminPageHtml()
		{
			return View();
		}

		public ActionResult _KittenPicture()
		{
			return View();
		}

		[Authorize]
		public ActionResult KittenPictures(int id)
		{
			Pets kitten = kittensRepository.GetByID(id);

			return View(kitten.Pictures.OrderBy(i=>i.Order).ToList());
		}

		[Authorize]
		[HttpPost]
		public int KittenPictureChangeOrder(int id, int newOrder)
		{
			picturesRepository.SetNewOrderForPicture(id, newOrder);
			picturesRepository.Save();

			return newOrder;
		}

		[Authorize]
		[HttpPost]
		public ActionResult ChangePicturesOrder(List<Pictures> json)
		{
			var result = new List<Pictures>();

			foreach (var pict in json)
			{
				picturesRepository.SetNewOrderForPicture(pict.ID, pict.Order);
				picturesRepository.Save();

				result.Add(picturesRepository.GetByID(pict.ID));
			}
			
			return View("KittenPictures", result);
		}

		public ActionResult LogIn(string username, string password)
		{
			if (SecurityHelper.LogInAdmin(username, password))
				return RedirectToAction("AdminPanel");
			else
				return View("Admin", (object) "Неверный пароль!");
		}

		public ActionResult Error(string errorText)
		{
			return View("Error", (object)errorText);
		}

		#region Work with kitten

		[Authorize]
		[HttpPost]
		public ActionResult AddKitten(Pets newKitten, HttpPostedFileBase[] files)
		{
			logger.Info("Add kittten method");
			if (kittensRepository.IsKittenExistsWithAnotherId(newKitten))
			{
				return Error("Котенок с таким именем уже есть!!!");
			}

			// Initialize addition fields
			InsertNewKittenIntoRepository(newKitten, false);

			return RedirectToAction("AdminChangeKittens");
		}

		[Authorize]
		[HttpPost]
		public ActionResult AddParentCat(Pets newKitten, HttpPostedFileBase[] files)
		{
			logger.Info("Add parent method");

			if (kittensRepository.IsKittenExistsWithAnotherId(newKitten))
			{
				return Error("Кошка с таким именем уже есть!!!");
			}

			InsertNewKittenIntoRepository(newKitten, true);

			return RedirectToAction("AdminChangeParents");
		}

		private void InsertNewKittenIntoRepository(Pets newKitten, bool isParent)
		{
			// Initialize addition fields
			newKitten.Owners = ownersRepository.GetByID(newKitten.OwnerID);
			newKitten.PetBreeds = breedsRepository.GetByID(newKitten.BreedID);
			newKitten.IsParent = isParent;

			kittensRepository.Insert(newKitten);
			kittensRepository.Save();
		}

		[Authorize]
		[HttpGet]
		public ActionResult AddKitten()
		{
			return View(CreateAddKittenModelView(false));
		}

		[Authorize]
		[HttpGet]
		public ActionResult AddParentCat()
		{
			return View(CreateAddKittenModelView(true));
		}

		private AddKittenModelView CreateAddKittenModelView(bool isParent)
		{
			return new AddKittenModelView()
			{
				Breeds = breedsRepository.GetCollection().ToList(),
				Owners = ownersRepository.GetCollection().ToList(),
				AllParents = kittensRepository.GetCollection().Where(i => i.IsParent == isParent).ToList(),
				DisplayPlaces = displayPlacesRepository.GetCollection().ToList()
			};
		}

		[Authorize]
		[HttpGet]
		public ActionResult EditKitten(int id)
		{
			Pets kitten = kittensRepository.GetByID(id);

			if (kitten == null)
				return RedirectToAction("AdminChangeKittens");
			
			return View(GetModelViewByKittenId(id));
		}

		private AddKittenModelView GetModelViewByKittenId(int id)
		{
			var pet = kittensRepository.GetByID(id);
			return AutoMapper.Mapper.Map<Pets, AddKittenModelView>(pet);
		}

		[Authorize]
		public ActionResult RemoveKitten(int id)
		{
			Pets kitten = kittensRepository.GetByID(id);
			bool isParent = kitten.IsParent;
			string redirectTo = isParent ? "AdminChangeParents" : "AdminChangeKittens";

			logger.Info("Remove kittten id=" + kitten.ID);
			if (!kittensRepository.IsKittenExists(kitten))
			{
				return Error("Котенка с таким именем не существует!!!");
			}

			if (isParent && kittensRepository.IsKittenExistsWithParent(kitten))
			{
				return Error("Родитель не может быть удален, так как есть котята с таким родителем!!!");
			}

			RemoveAllPictures(kitten.Pictures.ToList());

			kittensRepository.Delete(kitten.ID);
			kittensRepository.Save();

			return RedirectToAction(redirectTo);
		}

		[Authorize]
		[HttpPost]
		public ActionResult EditKitten(Pets kitten)
		{
			logger.Info("Edit kittten id=" + kitten.ID);
			if (kittensRepository.IsKittenExistsWithAnotherId(kitten))
			{
				return Error("Котенок с таким именем уже есть!!!");
			}

			bool isParent = kitten.IsParent;
			string redirectTo = isParent ? "AdminChangeParents" : "AdminChangeKittens";
			Pets oldKitten = kittensRepository.GetByID(kitten.ID);

			if(oldKitten.IsInArchive)
				redirectTo = "AdminChangeKittensArchive";

			// Initialize addition fields
			kitten.Owners = ownersRepository.GetByID(kitten.OwnerID);
			kitten.PetBreeds = breedsRepository.GetByID(kitten.BreedID);

			kittensRepository.Update(kitten);
			kittensRepository.Save();

			return RedirectToAction(redirectTo);
		}

		#endregion

		#region Work with images

		public void RemoveAllPictures(List<Pictures> pictures)
		{
			foreach (var pic in pictures)
			{
				RemovePicture(pic);
			}
		}

		[Authorize]
		[HttpPost]
		public int RemovePicture(int id)
		{
			logger.Info("Remove picture id=" + id);
			Pictures picture = picturesRepository.GetByID(id);

			RemovePicture(picture);
			
			return id;
		}

		[Authorize]
		public int RemovePicture(Pictures picture)
		{
			if (picture != null)
			{
				picturesRepository.Delete(picture.ID);
				picturesRepository.Save();

				var smallKittenPath = picturesLinksConstructor.GetSmallKittenImageFileName(picture.Image);

				RemoveFile(Server.MapPath(picture.Image));
				RemoveFile(Server.MapPath(smallKittenPath));
			}

			return picture.ID;
		}

		[Authorize]
		[HttpPost]
		public string AddImage(HttpPostedFileBase file, string kittenName)
		{
			logger.Info("Add picture kittenName=" + kittenName);

			var mStream = new MemoryStream();
			file.InputStream.CopyTo(mStream);

			AddPhoto(mStream, kittenName);

			return kittenName;
		}

		/// <summary>
		/// Add a picture of a kitten on the site (Create two photo: first is standart for slider and second is small for slider)
		/// </summary>
		/// <param name="file">Memory stream of image</param>
		/// <param name="kittenName">Name of kitten which picture we want to add</param>
		private void AddPhoto(MemoryStream file, string kittenName)
		{
			lock (lockObj)
			{
				var smallPictureStream = new MemoryStream();
				string kittenNameNumbered = picturesRepository.GetNewNumberOfImage(kittenName);
				string kittenNameNumberedSmall = picturesRepository.GetNewNumberOfImage(kittenName, true);
				string dirPath = Server.MapPath(PicturesLinksConstructor.KittensImageDirectoryPath + "/" + kittenName);
				string linkPath = PicturesLinksConstructor.KittensImageDirectoryPath + "/" + kittenName + "/" + kittenNameNumbered;
				string smallLinkPath = PicturesLinksConstructor.KittensImageDirectoryPath + "/" + kittenName + "/" + kittenNameNumberedSmall;

				if (!Directory.Exists(dirPath))
				{
					Directory.CreateDirectory(dirPath);
				}

				file.Position = 0;
				file.CopyTo(smallPictureStream);

				WebImage savedImage = SaveImage(dirPath + "\\" + kittenNameNumbered, file, PictureSizes.StandartSliderPicture);
				WebImage savedSmallImage = SaveImage(dirPath + "\\" + kittenNameNumberedSmall, smallPictureStream, PictureSizes.SmallSliderPicture);

				if (savedImage != null && savedSmallImage!= null)
				{
					kittensRepository.AddPictureForTheKitten(kittenName, new Pictures()
					{
						Image = linkPath,
						ImageSmall = smallLinkPath,
						CssClass = savedImage.Width > savedImage.Height ? DBKittensRepository.SmallImageHorizontal : DBKittensRepository.SmallImageVertical
					});

					kittensRepository.Save();
				}
			}
		}

		public static bool ResizeImage(string orgFile, string resizedFile, ImageFormat format, int width, int height)
		{
			try
			{
				using (Image img = Image.FromFile(orgFile))
				{
					Image thumbNail = new Bitmap(width, height, img.PixelFormat);
					Graphics g = Graphics.FromImage(thumbNail);
					g.CompositingQuality = CompositingQuality.HighQuality;
					g.SmoothingMode = SmoothingMode.HighQuality;
					g.InterpolationMode = InterpolationMode.HighQualityBicubic;
					Rectangle rect = new Rectangle(0, 0, width, height);
					g.DrawImage(img, rect);
					thumbNail.Save(resizedFile, format);
				}

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		[Authorize]
		public ActionResult AddMainFoto(HttpPostedFileBase f, string kittenName)
		{
			logger.Info("Add main photo for kittenName=" + kittenName);

			var copy = new MemoryStream();
			f.InputStream.CopyTo(copy);

			string path = SaveMainPicture(kittenName, f.InputStream);
			string redirectTo = "AdminChangeKittens";

			if (!String.IsNullOrEmpty(path))
			{
				Pets kitten = kittensRepository.GetKittenByName(kittenName);

				picturesRepository.Insert(new Pictures() { Image = path, IsMainPicture = true, Pet = kitten, PetID = kitten.ID});
				picturesRepository.Save();
				
				redirectTo = kitten.IsParent ? "AdminChangeParents" : "AdminChangeKittens";

				kittensRepository.Update(kitten);
				kittensRepository.Save();

				//Save main photo for kittens main page.
				AddPhoto(copy, kittenName);
			}

			return RedirectToAction(redirectTo);
		}

		private string SaveMainPicture(string kittenName, Stream file)
		{
			string path = String.Empty;
			
			// Verify that the user selected a file
			if (file != null && file.Length > 0 && !String.IsNullOrEmpty(kittenName))
			{
				var sizeImage = new WebImage(file).Crop(1,1).Resize(300, 300, false, true);

				path = picturesLinksConstructor.GetKittenImagePath(kittenName);
				RemoveFile(Server.MapPath(path));
				sizeImage.Save(Server.MapPath(path));
			}

			return path;
		}

		/// <summary>
		/// Save Image in specified picture size
		/// </summary>
		/// <param name="filename">Local file path</param>
		/// <param name="file">Image stream</param>
		/// <param name="pictureSize">Size of image</param>
		/// <returns></returns>
		private WebImage SaveImage(string filename, Stream file, PictureSizes pictureSize)
		{
			WebImage result = null;
			
			try
			{
				result = new WebImage(file);
				int width = result.Width;
				int height = result.Height;
				Size newSize = GetImageSize(pictureSize, width, height);

				result.Resize(newSize.Width, newSize.Height);

				RemoveFile(filename);
				// save the file.
				result.Save(filename);

				result.FileName = filename;
			}
			catch (Exception)
			{
				result = null;
			}

			return result;
		}

		private Size GetImageSize(PictureSizes size, int width, int height)
		{
			int newWidth = 0;
			int newHeight = 0;

			switch (size)
			{
				case PictureSizes.MainPicture:
					newWidth = 300;
					newHeight = 300;
					break;

				case PictureSizes.StandartSliderPicture:
					if (width > height)
					{
						newWidth = 600;
						newHeight = newWidth * height / width;
						newHeight = newHeight > 400 ? 400 : newHeight;
					}
					else
					{
						newHeight = 400;
						newWidth = newHeight * width / height;
					}
					break;

				case PictureSizes.SmallSliderPicture:
					if (width > height)
					{
						newWidth = 72;
						newHeight = newWidth * height / width;
					}
					else
					{
						newHeight = 72;
						newWidth = newHeight * width / height;
					}
					break;
			}

			return new Size(newWidth, newHeight); ;
		}

		private string SaveImage(string filename, WebImage image)
		{
			try
			{
				RemoveFile(filename);
				// save the file.
				image.Save(filename);
			}
			catch (Exception)
			{
				filename = String.Empty;
			}

			return filename;
		}

		private void RemoveFile(string path)
		{
			if (System.IO.File.Exists(path))
			{
				System.IO.File.Delete(path);
			}
		}

		#endregion
	}
}