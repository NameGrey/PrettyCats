using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PrettyCats.Database;
using PrettyCats.Helpers;
using PrettyCats.Models;

namespace PrettyCats.Controllers
{
	public class AdminController : Controller
	{
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

		public ActionResult AdminPanel()
		{
			return View();
		}

		public ActionResult AdminChangeKittens()
		{
			var v = DbStorage.Instance.Pets.ToList();
			return View("AdminChangeKittens", v);
		}

		public ActionResult KittenOnTheAdminPageHtml()
		{
			return View();
		}

		public ActionResult _KittenPicture()
		{
			return View();
		}

		public ActionResult KittenPictures(int id)
		{
			Pets kitten = DbStorage.GetKittenByID(id);

			return View(kitten.Pictures.ToList());
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

		[HttpPost]
		public ActionResult AddKitten(Pets newKitten, HttpPostedFileBase[] files)
		{

			int ss = Convert.ToInt16("sds");

			if (DbStorage.IsKittenExistsWithAnotherId(newKitten))
			{
				return Error("Котенок с таким именем уже есть!!!");
			}

			// Initialize addition fields
			newKitten.Owners = DbStorage.Instance.Owners.First(i => i.ID == newKitten.OwnerID);
			newKitten.PetBreeds = DbStorage.Instance.PetBreeds.First(i => i.ID == newKitten.BreedID);

			DbStorage.Instance.Pets.Add(newKitten);
			DbStorage.Instance.SaveChanges();

			return RedirectToAction("AdminChangeKittens");
		}

		[HttpGet]
		public ActionResult AddKitten()
		{
			KittenModelView newKitten = new KittenModelView
			{
				Name = "newName",
				RussianName = "dff",
				DisplayPlaceId = 1,
				BreedId = 1,
				OwnerId = 1,
				BirthDate = DateTime.Now,
				UnderThePictureText = "Text under the picture",
				Breeds = from el in DbStorage.Instance.PetBreeds.AsEnumerable() select new BreedModelView(el.ID, el.RussianName),
				DisplayPlaces =
					from el in DbStorage.Instance.DisplayPlaces.AsEnumerable()
					select new DisplayPlaceModelView(el.ID, el.PlaceOfDisplaying),
				Owners = from el in DbStorage.Instance.Owners.AsEnumerable() select new OwnerModelView(el.ID, el.Name),
			};


			return View(new Pets());
		}

		[HttpGet]
		public ActionResult EditKitten(int id)
		{
			Pets kitten = DbStorage.GetKittenByID(id);

			if (kitten == null)
				return RedirectToAction("AdminChangeKittens");
			
			return View(kitten);
		}

		public ActionResult RemoveKitten(int id)
		{
			Pets kitten = DbStorage.GetKittenByID(id);
			if (!DbStorage.IsKittenExists(kitten))
			{
				return Error("Котенка с таким именем не существует!!!");
			}

			//List<Pictures> listRemoveImages = (from i in DbStorage.Instance.Pictures where i.Pets.Contains(kitten) select i).ToList();
			//DbStorage.Instance.Pictures.RemoveRange(kitten.Pictures);
			//Pictures mainPicture = DbStorage.Instance.Pictures.Find(kitten.PictureID);
			//DbStorage.Instance.Pictures.Remove(mainPicture);
			RemoveAllPictures(kitten.Pictures.ToList());

			DbStorage.Instance.Pets.Remove(kitten);
			DbStorage.Instance.SaveChanges();

			return RedirectToAction("AdminChangeKittens");
		}

		[HttpPost]
		public ActionResult EditKitten(Pets kitten)
		{
			if (DbStorage.IsKittenExistsWithAnotherId(kitten))
			{
				return Error("Котенок с таким именем уже есть!!!");
			}

			// Initialize addition fields
			kitten.Owners = DbStorage.Instance.Owners.First(i => i.ID == kitten.OwnerID);
			kitten.PetBreeds = DbStorage.Instance.PetBreeds.First(i => i.ID == kitten.BreedID);
			DbStorage.Instance.Pets.AddOrUpdate(kitten); 

			DbStorage.Instance.SaveChanges();

			return RedirectToAction("AdminChangeKittens");
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
		[HttpPost]
		public int RemovePicture(int id)
		{
			Pictures picture = DbStorage.Instance.Pictures.Find(id);

			RemovePicture(picture);
			
			return id;
		}

		public int RemovePicture(Pictures picture)
		{
			if (picture.Pets.Count > 0)
			{
				picture.Pets.First().Pictures.Remove(picture);

				DbStorage.Instance.Pictures.Remove(picture);
				DbStorage.Instance.SaveChanges();

				var v = DbStorage.GetSmallKittenImageFileName(picture.Image);

				RemoveFile(Server.MapPath(picture.Image));
				RemoveFile(Server.MapPath(v));
			}

			return picture.ID;
		}

		[HttpPost]
		public string AddImage(HttpPostedFileBase file, string kittenName)
		{
			var length = file.ContentLength;
			var bytes = new byte[length];
			file.InputStream.Read(bytes, 0, length);

			string kittenNameNumbered = DbStorage.GetNumberedImage(kittenName);
			string kittenNameNumberedSmall = DbStorage.GetNumberedImage(kittenName, true);
			string dirPath = Server.MapPath(DbStorage.KittensImageDirectoryPath + "/" + kittenName);
			string linkPath = DbStorage.KittensImageDirectoryPath + "/" + kittenName + "/" + kittenNameNumbered;

			if (!Directory.Exists(dirPath))
			{
				Directory.CreateDirectory(dirPath);
			}

			string saveImagePath = SaveImage(dirPath + "\\" + kittenNameNumbered, bytes);

			var image = new WebImage(bytes);

			int width = image.Width;
			int height = image.Height;

			int newWidth = 0;
			int newHeight = 0;

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

			image.Resize(newWidth, newHeight);

			SaveImage(dirPath + "\\" + kittenNameNumberedSmall, image.Crop(2, 2).Resize(newWidth, newHeight, false));
			//ResizeImage(dirPath + "\\" + kittenNameNumbered, dirPath + "\\" + kittenNameNumberedSmall, ImageFormat.Jpeg, newWidth,
			//	newHeight);

			if (saveImagePath != String.Empty)
			{
				DbStorage.Instance.Pets.First(i => i.Name == kittenName)
					.Pictures.Add(
						new Pictures()
						{
							Image = linkPath,
							ImageSmall = DbStorage.GetSmallKittenImageFileName(linkPath),
							CssClass = newWidth > newHeight ? DbStorage.SmallImageHorizontal : DbStorage.SmallImageVertical
						});
				DbStorage.Instance.SaveChanges();
			}

			return kittenName;
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

		public ActionResult AddMainFoto(HttpPostedFileBase f, string kittenName)
		{
			string path = SaveImage(kittenName, f);

			if (!String.IsNullOrEmpty(path))
			{
				var pict = DbStorage.Instance.Pictures.Add(new Pictures() { Image = path });
				DbStorage.Instance.SaveChanges();

				Pets kitten = DbStorage.GetKittenByName(kittenName);
				kitten.PictureID = pict.ID;
				DbStorage.Instance.SaveChanges();
			}

			return RedirectToAction("AdminChangeKittens");
		}

		private string SaveImage(string kittenName, HttpPostedFileBase file)
		{
			string path = String.Empty;
			
			// Verify that the user selected a file
			if (file != null && file.ContentLength > 0 && !String.IsNullOrEmpty(kittenName))
			{
				var sizeImage = new WebImage(file.InputStream).Crop(1,1).Resize(300, 300, false, true);

				path = DbStorage.GetKittenImagePath(kittenName);
				RemoveFile(Server.MapPath(path));
				sizeImage.Save(Server.MapPath(path));
			}

			return path;
		}

		private string SaveImage(string filename, byte[] file)
		{
			try
			{
				var sizeImage = new WebImage(file);
				int width = sizeImage.Width;
				int height = sizeImage.Height;

				int newWidth = 0;
				int newHeight = 0;

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

				sizeImage.Resize(newWidth, newHeight);

				RemoveFile(filename);
				// save the file.
				sizeImage.Save(filename);
				
				//var fileStream = new FileStream(Server.MapPath(path), FileMode.Create, FileAccess.ReadWrite);
				//fileStream.Write(file, 0, file.Length);
				//fileStream.Close();
			}
			catch (Exception)
			{
				filename = String.Empty;
			}

			return filename;
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