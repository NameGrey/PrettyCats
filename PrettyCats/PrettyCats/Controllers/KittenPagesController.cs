using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PrettyCats.Database;
using PrettyCats.Helpers;
using PrettyCats.Models;

namespace PrettyCats.Controllers
{
	public class KittenPagesController : Controller
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

		[Route("kitten-page/{id}")]
		public ActionResult KittenMainPage_old(int id)
		{
            return View("KittenMainPage", GetModelViewByKittenId(id)); // TODO: if there is no such kitten show common site error for this
		}

	    private KittenModelView GetModelViewByKittenId(int id)
	    {
		    var list = from pet in DbStorage.Pets
			    join mother in DbStorage.Pets on pet.MotherID equals mother.ID into outerMother
			    from leftOuterMother in outerMother.DefaultIfEmpty()
			    join father in DbStorage.Pets on pet.FatherID equals father.ID into outerFather
			    from leftOuterFather in outerFather.DefaultIfEmpty()
				join mainPicture in DbStorage.Pictures on pet.PictureID equals mainPicture.ID into outerMainPicture
				from leftOuterMainPicture in outerMainPicture
			    join breed in DbStorage.PetBreeds on pet.BreedID equals breed.ID
			    join owner in DbStorage.Owners on pet.OwnerID equals owner.ID
				where pet.ID == id
			    select
				    new KittenModelView()
				    {
					    BirthDate = pet.BirthDate?.ToString("dd.MM.yyyy"),
					    BreedID = pet.BreedID,
					    BreedName = breed.RussianName,
					    Color = pet.Color,
					    FatherID = pet.FatherID,
					    FatherName = leftOuterFather != null ? leftOuterFather.RussianName : string.Empty,
					    IsInArchive = pet.IsInArchive,
					    MotherID = pet.MotherID,
					    MotherName = leftOuterMother != null ? leftOuterMother.RussianName : string.Empty,
					    OwnerName = owner.Name,
					    OwnerPhone = owner.Phone,
					    UnderThePictureText = pet.UnderThePictureText,
					    VideoUrl = pet.VideoUrl,
					    Price = pet.Price,
						IsParent = pet.IsParent,
					    Status = pet.Status,
						MainImageCssClass = leftOuterMainPicture != null ? leftOuterMainPicture.CssClass : string.Empty,
						MainImageSmallSizeUrl = leftOuterMainPicture != null ? leftOuterMainPicture.Image : string.Empty,
						MainImageStandartSizeUrl = leftOuterMainPicture != null ? leftOuterMainPicture.ImageSmall : string.Empty,
						Pictures = pet.Pictures
				    };

			return list.FirstOrDefault();
	    }

		private KittenShortModelView GetShortModelViewByKittenId(int id)
		{
			return (from pet in DbStorage.Pets
					join picture in DbStorage.Pictures on pet.PictureID equals picture.ID into outerPicture
					from leftOuterPicture in outerPicture.DefaultIfEmpty()
					join breed in DbStorage.PetBreeds on pet.BreedID equals breed.ID
					join owner in DbStorage.Owners on pet.OwnerID equals owner.ID
					where pet.ID == id
					select
						new KittenShortModelView()
						{
							ID = pet.ID,
							PictureID = pet.PictureID,
							ImageUrl = leftOuterPicture.Image,
							IsParent = pet.IsParent,
							RussianName = pet.RussianName,
							Status = pet.Status
						}).FirstOrDefault();
		}

		private IEnumerable<KittenShortModelView> ConvertToShortKittenModelView(IEnumerable<Pets> pets)
		{
			return (from pet in pets
					join picture in DbStorage.Pictures on pet.PictureID equals picture.ID into outerPicture
					from leftOuterPicture in outerPicture.DefaultIfEmpty()
					join breed in DbStorage.PetBreeds on pet.BreedID equals breed.ID
					join owner in DbStorage.Owners on pet.OwnerID equals owner.ID
					select
						new KittenShortModelView()
						{
							ID = pet.ID,
							PictureID = pet.PictureID,
							ImageUrl = leftOuterPicture.Image,
							IsParent = pet.IsParent,
							RussianName = pet.RussianName,
							Status = pet.Status
						});
		}

		[Route("parent-kitten-page/{id}")]
		public ActionResult ParentCatMainPage_old(int id)
		{
			ViewBag.BackLink = Request.UrlReferrer?.AbsoluteUri ?? "";
			return View("ParentCatMainPage", GetModelViewByKittenId(id));
		}

		public ActionResult GetKittenHtml(int id)
		{
			return View(DbStorage.Pets.Find(i=>i.ID == id));
		}

		public ActionResult KittenOnTheMainPageHtml(int id)
		{
			return View(GetShortModelViewByKittenId(id));
		}

		#region Display kittens

		[Route("kittens")]
		public ActionResult AllAvailableKittens_old()
		{
			return View("AllAvailableKittens");
		}

		[Route("parent-kittens")]
		public ActionResult AllParents_old()
		{
			var v = DbStorage.Pets.Where(i => i.IsParent && i.WhereDisplay != 3).ToList();
			return View("AllParents", v);
		}

		[Route("bengal-kittens")]
		public ActionResult BengalKittens_old()
		{
			var v = DbStorage.GetKittensByBreed(2);
			ViewBag.PreviousPage = "NotArchive";
			return View("BengalKittens", ConvertToShortKittenModelView(v).ToList());
		}

		[Route("scotland-kittens")]
		public ActionResult BritishKittens_old()
		{
			var v = DbStorage.GetKittensByBreed(3).ToList();
			v.AddRange(DbStorage.GetKittensByBreed(4));
			ViewBag.PreviousPage = "NotArchive";
			return View("BritishKittens", ConvertToShortKittenModelView(v).ToList());
		}

		[Route("mainkun-kittens")]
		public ActionResult MainKunKittens_old()
		{
			var v = DbStorage.GetKittensByBreed(1);
			ViewBag.PreviousPage = "NotArchive";
			return View("MainKunKittens", ConvertToShortKittenModelView(v).ToList());
		}

		[Route("archive")]
		public ActionResult Archive_old()
		{
			return View("Archive");
		}

		[Route("bengal-kittens-archive")]
		public ActionResult BengalKittens_Archive_old()
		{
			var v = DbStorage.GetKittensByBreed(2, true);
			ViewBag.PreviousPage = "Archive";
			return View("BengalKittens", ConvertToShortKittenModelView(v).ToList());
		}

		[Route("scotland-kittens-archive")]
		public ActionResult BritishKittens_Archive_old()
		{
			var v = DbStorage.GetKittensByBreed(3, true).ToList();
			v.AddRange(DbStorage.GetKittensByBreed(4, true));
			ViewBag.PreviousPage = "Archive";
			
			return View("BritishKittens", ConvertToShortKittenModelView(v).ToList());
		}

		[Route("mainkun-kittens-archive")]
		public ActionResult MainKunKittens_Archive_old()
		{
			var v = DbStorage.GetKittensByBreed(1, true);
			ViewBag.PreviousPage = "Archive";
			return View("MainKunKittens", ConvertToShortKittenModelView(v).ToList());
		}
		#endregion

		//These pages should be removed when search system find new urls for them
		#region Redirect pages
		
		public ActionResult KittenMainPage(int id)
		{
			return RedirectPermanent($"/kitten-page/{id}");
		}

		public ActionResult ParentCatMainPage(int id)
		{
			return RedirectPermanent($"/parent-kitten-page/{id}");
		}

		public ActionResult AllAvailableKittens()
		{
			return RedirectPermanent("/kittens");
		}
		
		public ActionResult AllParents()
		{
			return RedirectPermanent("/parent-kittens");
		}
		
		public ActionResult BengalKittens()
		{
			return RedirectPermanent("/bengal-kittens");
		}
		
		public ActionResult BritishKittens()
		{
			return RedirectPermanent("/scotland-kittens");
		}

		public ActionResult MainKunKittens()
		{
			return RedirectPermanent("/mainkun-kittens");
		}

		public ActionResult Archive()
		{
			return RedirectPermanent("/archive");
		}

		public ActionResult BengalKittens_Archive()
		{
			return RedirectPermanent("/bengal-kittens-archive");
		}

		public ActionResult BritishKittens_Archive()
		{
			return RedirectPermanent("/scotland-kittens-archive");
		}

		public ActionResult MainKunKittens_Archive()
		{
			return RedirectPermanent("/mainkun-kittens-archive");
		}
		#endregion
	}
}