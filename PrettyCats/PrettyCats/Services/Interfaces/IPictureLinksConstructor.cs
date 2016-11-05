
namespace PrettyCats.Services.Interfaces
{
	public interface IPictureLinksConstructor
	{
		string BaseServerUrl { get; }
		string GetKittenPicturesFolder(string kittenName, PathFullness pathFullness = PathFullness.AbsolutePath);

		string GetKittenImagePath(string kittenNameNumbered, bool withExtension = true, bool withNamedFolder = false, PathFullness pathFullness = PathFullness.AbsolutePath);

		string GetSmallKittenImageFileName(string imagePath);
	}
}
