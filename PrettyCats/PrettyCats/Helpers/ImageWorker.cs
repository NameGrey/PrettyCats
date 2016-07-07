using System;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Helpers;
using PrettyCats.DAL.Entities;
using PrettyCats.DAL.Repositories;
using PrettyCats.Services;
using PrettyCats.Services.Interfaces;

namespace PrettyCats.Helpers
{
	public class ImageWorker
	{
		enum PictureSizes
		{
			MainPicture,
			StandartSliderPicture,
			SmallSliderPicture
		}

		static readonly object _lockObj = new object();
		private IPicturesRepository _picturesRepository;
		private IPictureLinksConstructor _pictureLinksConstructor;
		private HttpServerUtility _server;

		public ImageWorker(IPicturesRepository picturesRepository, IPictureLinksConstructor pictureLinksConstructor, HttpServerUtility server)
		{
			_picturesRepository = picturesRepository;
			_pictureLinksConstructor = pictureLinksConstructor;
			_server = server;
		}

		public Pictures AddPhoto(MemoryStream file, string kittenName)
		{
			Pictures result = null;

			lock (_lockObj)
			{
				var smallPictureStream = new MemoryStream();
				string kittenNameNumbered = _picturesRepository.GetNewNumberOfImage(kittenName);
				string kittenNameNumberedSmall = _picturesRepository.GetNewNumberOfImage(kittenName, true);
				string dirPath = _server.MapPath(_pictureLinksConstructor.GetKittenPicturesFolder(kittenName, PathFullness.RelativePath));
				string linkPath = _pictureLinksConstructor.GetKittenImagePath(kittenNameNumbered, true, true);//_pictureLinksConstructor.KittensImageDirectoryPath + "/" + kittenName + "/" + kittenNameNumbered;
				string smallLinkPath = _pictureLinksConstructor.GetKittenImagePath(kittenNameNumbered, true, true);//_pictureLinksConstructor.KittensImageDirectoryPath + "/" + kittenName + "/" + kittenNameNumberedSmall;

				if (!Directory.Exists(dirPath))
				{
					Directory.CreateDirectory(dirPath);
				}

				file.CopyTo(smallPictureStream);

				result = new Pictures()
				{
					Image = linkPath,
					ImageSmall = smallLinkPath
				};

				_picturesRepository.Insert(result);

				SaveImage(dirPath + "\\" + kittenNameNumbered, file, PictureSizes.StandartSliderPicture);
				SaveImage(dirPath + "\\" + kittenNameNumberedSmall, smallPictureStream, PictureSizes.SmallSliderPicture);
			}

			return result;
		}

		public string SaveMainPicture(string kittenName, Stream file)
		{
			var result = String.Empty;

			// Verify that the user selected a file
			if (file != null && file.Length > 0 && !String.IsNullOrEmpty(kittenName))
			{
				var sizeImage = new WebImage(file).Crop(1, 1).Resize(300, 300, false, true);
				result = _pictureLinksConstructor.GetKittenImagePath(kittenName);

				var path = _server.MapPath(_pictureLinksConstructor.GetKittenImagePath(kittenName, true, false, PathFullness.RelativePath));

				RemoveFile(path);
				sizeImage.Save(path, "jpg");
			}
			return result;
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
				result.Save(filename, "jpg");

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

		public void RemoveFile(string path)
		{
			if (System.IO.File.Exists(path))
			{
				System.IO.File.Delete(path);
			}
		}
	}
}