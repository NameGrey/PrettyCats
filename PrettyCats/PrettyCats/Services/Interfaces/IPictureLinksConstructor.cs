
namespace PrettyCats.Services.Interfaces
{
	public interface IPictureLinksConstructor
	{
		string BaseServerUrl { get; }
		string GetKittenPicturesFolder(string kittenName, PathFullness pathFullness = PathFullness.AbsolutePath);

		string GetKittenImagePath(string kittenName, bool withExtension = true, bool withNamedFolder = false, PathFullness pathFullness = PathFullness.AbsolutePath);

		string GetSmallKittenImageFileName(string imagePath, PathFullness pathFullness = PathFullness.AbsolutePath);
	}
}
