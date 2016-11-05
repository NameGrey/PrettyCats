﻿using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Http;
using PrettyCats.Helpers;
using PrettyCats.Services.Interfaces;

namespace PrettyCats.Services
{

	public enum PathFullness
	{
		AbsolutePath,
		RelativePath
	}

	public class PicturesLinksConstructor: IPictureLinksConstructor
	{
		private readonly string _baseServerUrl;
		public const string KittensImageDirectoryPath = "\\Resources\\Kittens";

		public string BaseServerUrl => _baseServerUrl;

		public PicturesLinksConstructor(string baseServerUrl)
		{
			_baseServerUrl = baseServerUrl;
		}

		public PicturesLinksConstructor() : this(GlobalAppConfiguration.BaseServerUrl)
		{
			
		}

		public string GetKittenPicturesFolder(string kittenName, PathFullness pathFullness = PathFullness.AbsolutePath)
		{
			return KittensImageDirectoryPath + "\\" + kittenName;
		}

		public string GetKittenImagePath(string kittenNameNumbered, bool withExtension = true, bool withNamedFolder = false, PathFullness pathFullness = PathFullness.AbsolutePath)
		{
			string clearName = Regex.Match(kittenNameNumbered, @"[^\d]*").Value;

			// extract only the filename
			var fileName = (withNamedFolder ? "\\" + clearName + "\\" : String.Empty) + kittenNameNumbered +
			               (withExtension ? ".jpg" : String.Empty);
			// store the file inside /Resources/Kittens folder
			var path = KittensImageDirectoryPath + "\\" + fileName;

			if (pathFullness == PathFullness.AbsolutePath)
				path = _baseServerUrl + path;

			return path;
		}
		
		public string GetSmallKittenImageFileName(string imagePath)
		{
			string result = String.Empty;
			string name = Path.GetFileNameWithoutExtension(imagePath);

			if (name != null)
			{
				result = Regex.Match(name, @"\d+").Value;
				string clearName = Regex.Match(name, @"[^\d]*").Value;

				result = Regex.Replace(imagePath, name, clearName + "_" + result);
			}

			return result;
		}
	}
}