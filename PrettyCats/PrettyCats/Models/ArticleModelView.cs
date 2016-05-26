namespace PrettyCats.Models
{
	public class ArticleModelView
	{
		public string ImageUrl { get; set;}
		public string ArticleName { get; set; }
		public string MainArticleText { get; set; }
		public string ArticleUrl { get; set; }

		public ArticleModelView(string imageUrl, string articleName, string mainArticleText, string articleUrl)
		{
			this.ArticleName = articleName;
			this.ImageUrl = imageUrl;
			this.MainArticleText = mainArticleText;
			this.ArticleUrl = articleUrl;
		}
	}
}