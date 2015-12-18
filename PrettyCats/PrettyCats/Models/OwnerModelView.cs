using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrettyCats.Models
{
	public class OwnerModelView
	{
		public OwnerModelView(int id, string name)
		{
			this.ID = id;
			this.Name = name;
		}

		public int ID { get; set; }
		public string Name { get; set; }
	}
}