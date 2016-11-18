using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using PrettyCats.DAL;
using PrettyCats.DAL.Entities;
using PrettyCats.DAL.Repositories;
using PrettyCats.DAL.Repositories.DbRepositories;
using PrettyCats.Helpers;
using PrettyCats.Services;
using PrettyCats.Services.Interfaces;

namespace PrettyCats.Controllers
{
	[RoutePrefix("api/pictures")]
	public class PicturesApiController: ApiController
	{
		private readonly IPicturesRepository _picturesRepository;
		private readonly IPictureLinksConstructor _picturesLinksConstructor;
		private ImageWorker _imageWorker;

		public PicturesApiController(IPicturesRepository picturesRepository, IPictureLinksConstructor picturesLinksConstructor)
		{
			_picturesRepository = picturesRepository;
			_picturesLinksConstructor = picturesLinksConstructor;
			_imageWorker = new ImageWorker(_picturesRepository, _picturesLinksConstructor, HttpContext.Current.Server);
		}

		[Route("{kittenId}")]
		public IEnumerable<Pictures> GetCollectionByKittenId(int kittenId)
		{
			return _picturesRepository.GetCollection().Where(i => i.PetID == kittenId).OrderBy(i => i.Order);
		}

		[Route("main-picture/{kittenId}")]
		public Pictures GetMainPicture(int kittenId)
		{
			return _picturesRepository.GetCollection().FirstOrDefault(i=>i.IsMainPicture && i.PetID == kittenId);
		}

		[HttpDelete]
		[Route("{id}")]
		public void RemovePicture([FromUri] int id)
		{
			if (id > 0)
			{
				_picturesRepository.Delete(id);
				_picturesRepository.Save();
			}
		}

		[HttpPost]
		[Route("add")]
		public async void AddNewPicture()
		{
			if (!Request.Content.IsMimeMultipartContent())
			{
				//TODO: Handle the error (Research ways to handle exception Web aPI)
				throw  new NotImplementedException();
			}

			var provider = new MultipartMemoryStreamProvider();

			Request.Content.LoadIntoBufferAsync().Wait();
			await Request.Content.ReadAsMultipartAsync(provider);

			byte[] picture = await provider.Contents[0].ReadAsByteArrayAsync();
			string kittenName = await provider.Contents[1].ReadAsStringAsync();
			string kittenIdString = await provider.Contents[2].ReadAsStringAsync();

			int kittenId;

			if (!int.TryParse(kittenIdString, out kittenId))
			{
				//TODO: Handle the error (Research ways to handle exception Web aPI)
				throw new NotImplementedException();
			}

			_imageWorker.AddPhoto(new MemoryStream(picture), kittenName, false, kittenId);
		}

		[HttpPost]
		[Route("main-picture/add")]
		public async Task<Pictures> SetMainPicture()
		{
			Pictures result = null;
			if (!Request.Content.IsMimeMultipartContent())
			{
				//TODO: Handle the error (Research ways to handle exception Web aPI)
				throw new NotImplementedException();
			}
			
			var provider = new MultipartMemoryStreamProvider();

			await Request.Content.ReadAsMultipartAsync(provider);

			byte[] mainPicture = await provider.Contents[0].ReadAsByteArrayAsync();
			string kittenName = await provider.Contents[1].ReadAsStringAsync();
			int kittenId;

			if (!int.TryParse(await provider.Contents[2].ReadAsStringAsync(), out kittenId))
			{
				//TODO: Handle the error (Research ways to handle exception Web aPI)
				throw new NotImplementedException();
			}

			//try
			//{
				result = SavePicture(mainPicture, kittenName, kittenId);
			//}
			//catch (Exception ex)
			//{
			//	//TODO: Handle the error (Research ways to handle exception Web aPI)
			//}

			return result;
		}

		private Pictures SavePicture(byte[] file, string kittenName, int kittenId)
		{
			Pictures result = null;
			var fileStream = new MemoryStream(file);
			var copy = new MemoryStream(file);

			string path = _imageWorker.SaveMainPicture(kittenName, fileStream);

			if (!string.IsNullOrEmpty(path))
			{
				var oldMainPicture = _picturesRepository.GetCollection()
					.FirstOrDefault(i => i.IsMainPicture && i.PetID == kittenId);

				if (oldMainPicture != null)
					_picturesRepository.Delete(oldMainPicture.ID);
				
				//Save main photo for kittens main page.
				result = _imageWorker.AddPhoto(copy, kittenName, true, kittenId);
			}

			return result;
		}
	}
}