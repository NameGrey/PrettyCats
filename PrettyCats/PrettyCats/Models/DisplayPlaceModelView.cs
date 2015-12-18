using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrettyCats.Models
{
	public class DisplayPlaceModelView
	{
		public DisplayPlaceModelView(int id, string place)
		{
			ID = id;
			Place = place;
		}

		public int ID { get; set; }
		public string Place { get; set; }
	}
}