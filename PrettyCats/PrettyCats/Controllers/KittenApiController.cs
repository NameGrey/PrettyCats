using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using PrettyCats.DAL.Entities;
using PrettyCats.DAL.Repositories;
using PrettyCats.Helpers;
using PrettyCats.Services.Interfaces;

namespace PrettyCats.Controllers
{
	[RoutePrefix("api/kittens")]
	public class KittenApiController: ApiController
	{
		private readonly IKittensRepository _kittensRepository;
		private readonly IPicturesRepository _picturesRepository;
		private readonly IPictureLinksConstructor _picturesLinksConstructor;

		private ImageWorker _imageWorker;

		public KittenApiController(IKittensRepository kittensRepository, IPicturesRepository picturesRepository, IPictureLinksConstructor picturesLinksConstructor)
		{
			_kittensRepository = kittensRepository;
			_picturesRepository = picturesRepository;
			_picturesLinksConstructor = picturesLinksConstructor;

			_imageWorker = new ImageWorker(_picturesRepository, _picturesLinksConstructor, HttpContext.Current.Server);
		}

		[Route("{id:int}")]
		public Pets GetById(int id)
		{
			return _kittensRepository.GetByID(id);
		}

		[Route("kittensByPath/{pathName}")]
		public IEnumerable<Pets> GetKittens(string pathName)
		{
			pathName = "/" + pathName;

			return _kittensRepository.GetCollection().Where(i => i.PetBreeds.LinkPage == pathName).ToList();
		}

		[HttpPost]
		[Route("add")]
		public async Task<HttpResponseMessage> AddKitten()
		{
			if (!Request.Content.IsMimeMultipartContent())
			{
			}

			var newKitten = await GetKittenFromRequest(Request);

			if (_kittensRepository.IsKittenExists(newKitten))
			{
				throw new NotImplementedException();
				//TODO: define how to handle exceptions as for server side as for client side
			}

			_kittensRepository.Insert(newKitten);
			_kittensRepository.Save();

			return new HttpResponseMessage(HttpStatusCode.OK);
		}

		private async Task<Pets> GetKittenFromRequest(HttpRequestMessage request)
		{
			var provider = new MultipartMemoryStreamProvider();
			await request.Content.ReadAsMultipartAsync(provider);

			byte[] kitten = await provider.Contents[0].ReadAsByteArrayAsync();
			string jsonString = Encoding.UTF8.GetString(kitten);

			return JsonConvert.DeserializeObject<Pets>(jsonString); ;
		}

		[HttpGet]
		[Route("remove/{id:int}")]
		public HttpResponseMessage RemoveKitten(int id)
		{
			Pets kitten = _kittensRepository.GetByID(id);
			bool isParent = kitten.IsParent;
			
			if (!_kittensRepository.IsKittenExists(kitten))
			{
				throw new NotImplementedException();
				//TODO: define how to handle exceptions as for server side as for client side
			}

			if (isParent && _kittensRepository.IsKittenExistsWithParent(kitten))
			{
				throw new NotImplementedException();
				//TODO: define how to handle exceptions as for server side as for client side
				//return Error("Родитель не может быть удален, так как есть котята с таким родителем!!!");
			}
			
			RemoveAllPictures(kitten.Pictures.ToList());
			_imageWorker.RemoveMainPicture(kitten.Name);

			_kittensRepository.Delete(kitten.ID);
			_kittensRepository.Save();

			return new HttpResponseMessage(HttpStatusCode.OK);
		}

		public void RemoveAllPictures(List<Pictures> pictures)
		{
			foreach (var pic in pictures)
			{
				_imageWorker.RemovePicture(pic);
			}
		}

		[HttpPost]
		[Route("edit")]
		public async void EditKitten()
		{
			if (!Request.Content.IsMimeMultipartContent())
			{
			}

			var editKitten = await GetKittenFromRequest(Request);

			if (editKitten != null)
			{
				if (_kittensRepository.IsKittenExistsWithAnotherId(editKitten))
				{
					throw new NotImplementedException();
					//TODO: define how to handle exceptions as for server side as for client side
				}

				_kittensRepository.Update(editKitten);
				_kittensRepository.Save();
			}
		}

		[Route("")]
		[HttpGet]
		public IEnumerable<Pets> GetKittens()
		{
			return _kittensRepository.GetCollection();
		}

		[Route("parents")]
		public IEnumerable<Pets> GetParents()
		{
			return _kittensRepository.GetCollection().Where(i => i.IsParent && !i.IsInArchive);
		}
	}
}