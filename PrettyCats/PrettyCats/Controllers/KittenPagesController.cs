using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using PrettyCats.DAL;
using PrettyCats.DAL.Entities;
using PrettyCats.DAL.Repositories;
using PrettyCats.DAL.Repositories.DbRepositories;
using PrettyCats.Models;

namespace PrettyCats.Controllers
{
	public class KittenPagesController : BaseController
	{
		private readonly IKittensRepository _kittensRepository;

		public KittenPagesController(IKittensRepository kittensRepository)
		{
			_kittensRepository = kittensRepository;

			CustomizeMapper();
		}

		private void CustomizeMapper()
		{
			//TODO: Try to find better mapper to avoid so complex expressions
			Mapper.Initialize(cfg=>
			{
				cfg.CreateMap<Pets, KittenModelView>()
					.ForMember(i => i.BirthDate, i => i.MapFrom(src => src.BirthDate != null ? ((DateTime) src.BirthDate).ToString("dd.MM.yyyy") : string.Empty))
					.ForMember(i => i.BreedName, i => i.MapFrom(src => src.PetBreeds != null ? src.PetBreeds.ShortName : string.Empty))
					.ForMember(i => i.FatherName, i => i.MapFrom(src => src.Father != null ? src.Father.RussianName : string.Empty))
					.ForMember(i => i.MotherName, i => i.MapFrom(src => src.Mother != null ? src.Mother.RussianName : string.Empty))
					.ForMember(i => i.OwnerName, i => i.MapFrom(src => src.Owners != null ? src.Owners.Name : string.Empty))
					.ForMember(i => i.OwnerPhone, i => i.MapFrom(src => src.Owners != null ? src.Owners.Phone : string.Empty))
					.ForMember(i => i.MainImageSmallSizeUrl, 
						i=>i.MapFrom(src=>src.Pictures.FirstOrDefault(el=>el.IsMainPicture) != null ? src.Pictures.First(el=>el.IsMainPicture).ImageSmall : string.Empty))
					.ForMember(i => i.MainImageStandartSizeUrl,
						i => i.MapFrom(src => src.Pictures.FirstOrDefault(el => el.IsMainPicture) != null ? src.Pictures.First(el => el.IsMainPicture).Image : string.Empty))
					.ForMember(i=>i.Pictures, i=>i.MapFrom(src=>src.Pictures));

				cfg.CreateMap<Pets, KittenShortModelView>()
					.ForMember(i=>i.PictureID, 
						i=>i.MapFrom(src => src.Pictures.FirstOrDefault(el => el.IsMainPicture) != null ? (int?)src.Pictures.First(el => el.IsMainPicture).ID : null))
					.ForMember(i => i.ImageUrl, 
						i => i.MapFrom(src => src.Pictures.FirstOrDefault(el => el.IsMainPicture) != null ? src.Pictures.First(el => el.IsMainPicture).Image : string.Empty));

			});
		}

		[Route("kitten-page/{id}")]
		public ActionResult KittenMainPage_old(int id)
		{
			return View("KittenMainPage", GetModelViewByKittenId(id)); // TODO: if there is no such kitten show common site error for this
		}

		private KittenModelView GetModelViewByKittenId(int id)
		{
			var kitten = _kittensRepository.GetByID(id);

			return Mapper.Map<Pets, KittenModelView>(kitten);
		}

		private KittenShortModelView GetShortModelViewByKittenId(int id)
		{
			var kitten = _kittensRepository.GetByID(id);

			return Mapper.Map<Pets, KittenShortModelView>(kitten);
		}

		private IEnumerable<KittenShortModelView> ConvertToShortKittenModelView(IEnumerable<Pets> pets)
		{
			return Mapper.Map<IEnumerable<Pets>, IEnumerable<KittenShortModelView>>(pets);
		}

		[Route("parent-kitten-page/{id}")]
		public ActionResult ParentCatMainPage_old(int id)
		{
			ViewBag.BackLink = Request.UrlReferrer?.AbsoluteUri ?? "";
			return View("ParentCatMainPage", GetModelViewByKittenId(id));
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
			var parents = _kittensRepository.GetCollection().Where(i => i.IsParent && !i.IsHidden).ToList();

			return View("AllParents", ConvertToShortKittenModelView(parents).ToList());
		}

		[Route("bengal-kittens")]
		public ActionResult BengalKittens_old()
		{
			var v = _kittensRepository.GetKittensByBreed(3);
			ViewBag.PreviousPage = "NotArchive";
			ViewBag.Title = "Котята бенгальской породы";
			return View("CategoryKittens", ConvertToShortKittenModelView(v).ToList());
		}

		[Route("scotland-kittens")]
		public ActionResult BritishKittens_old()
		{
			var v = _kittensRepository.GetKittensByBreed(1).ToList();
			ViewBag.PreviousPage = "NotArchive";
			ViewBag.Title = "Шотландские котята";
			return View("CategoryKittens", ConvertToShortKittenModelView(v).ToList());
		}

		[Route("scotland-kittens-archive")]
		public ActionResult BritishKittens_Archive_old()
		{
			var v = _kittensRepository.GetKittensByBreed(1, true).ToList();
			ViewBag.PreviousPage = "Archive";
			ViewBag.Title = "Шотландские котята (Архив)";
			return View("CategoryKittens", ConvertToShortKittenModelView(v).ToList());
		}

		[Route("mainkun-kittens")]
		public ActionResult MainKunKittens_old()
		{
			var v = _kittensRepository.GetKittensByBreed(2);
			ViewBag.PreviousPage = "NotArchive";
			ViewBag.Title = "Котята породы Мейн-кун";
			return View("CategoryKittens", ConvertToShortKittenModelView(v).ToList());
		}

		[Route("archive")]
		public ActionResult Archive_old()
		{
			return View("Archive");
		}

		[Route("bengal-kittens-archive")]
		public ActionResult BengalKittens_Archive_old()
		{
			var v = _kittensRepository.GetKittensByBreed(3, true);
			ViewBag.PreviousPage = "Archive";
			ViewBag.Title = "Котята бенгальской породы (Архив)";
			return View("CategoryKittens", ConvertToShortKittenModelView(v).ToList());
		}

		[Route("mainkun-kittens-archive")]
		public ActionResult MainKunKittens_Archive_old()
		{
			var v = _kittensRepository.GetKittensByBreed(2, true);
			ViewBag.PreviousPage = "Archive";
			ViewBag.Title = "Котята породы Мейн-кун (Архив)";
			return View("CategoryKittens", ConvertToShortKittenModelView(v).ToList());
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