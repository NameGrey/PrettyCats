﻿namespace PrettyCats.Models
{
	public class BreedModelView
	{
		public BreedModelView(int id, string name)
		{
			this.ID = id;
			this.Name = name;
		}

		public int ID { get; set; }
		public string Name { get; set; }
	}
}