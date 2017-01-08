
using System;

namespace PrettyCats.DAL.Entities
{
	public class Log: IEntity
	{
		public int ID { get; set; }
		public DateTime Logged { get; set; }
		public string Level { get; set; }
		public string Message { get; set; }
		public string Url { get; set; }
		public string Logger { get; set; }
		public string CallSite { get;set; }
		public string Exception { get; set; }
	}
}
