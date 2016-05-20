using System;
using System.IO;
using System.Text.RegularExpressions;

namespace PrettyCats.DAL.Services
{
	public class PicturesLinksConstructor
	{
		public const string KittensImageDirectoryPath = "~/Resources/Kittens";

		public string GetKittenImagePath(string kittenName, bool withExtension = true, bool withNamedFolder = false)
		{
			// extract only the filename
			var fileName = kittenName + (withNamedFolder ? "\\" + kittenName + "\\" : String.Empty) +
							(withExtension ? ".jpg" : String.Empty);
			// store the file inside /Resources/Kittens folder
			var path = Path.Combine(KittensImageDirectoryPath + (withNamedFolder ? "\\" + kittenName + "\\" : String.Empty), fileName);

			return path;
		}
		
		public string GetSmallKittenImageFileName(string imagePath)
		{
			string result = String.Empty;
			string name = Path.GetFileNameWithoutExtension(imagePath);

			if (name != null)
			{
				result = Regex.Match(name, @"\d+").Value;
				result = Regex.Replace(imagePath, result, "_" + result);
			}

			return result;
		}
	}
}