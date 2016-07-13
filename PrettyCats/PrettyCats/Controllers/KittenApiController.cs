using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
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

			var provider = new MultipartMemoryStreamProvider();
			await Request.Content.ReadAsMultipartAsync(provider);

			byte[] kitten = await provider.Contents[0].ReadAsByteArrayAsync();
			string jsonString = Encoding.UTF8.GetString(kitten);
			var newKitten = JsonConvert.DeserializeObject<Pets>(jsonString);

			if (_kittensRepository.IsKittenExists(newKitten))
			{
				throw new NotImplementedException();
				//TODO: define how to handle exceptions as for server side as for client side
			}

			_kittensRepository.Insert(newKitten);
			_kittensRepository.Save();

			return new HttpResponseMessage(HttpStatusCode.OK);
		}

		[HttpGet]
		[Route("remove/{id:int}")]
		public async Task<HttpResponseMessage> RemoveKitten(int id)
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
		public void EditKitten(Pets kitten)
		{
			if (_kittensRepository.IsKittenExistsWithAnotherId(kitten))
			{
				throw new NotImplementedException();
				//TODO: define how to handle exceptions as for server side as for client side
			}

			_kittensRepository.Update(kitten);
			_kittensRepository.Save();
		}

		[Route("")]
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