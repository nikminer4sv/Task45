using System;
using System.ComponentModel.DataAnnotations;

namespace Task45.Models
{
	public class Message
	{

		public int Id { get; set; }

		public string Recipient { get; set; }

		public string Sender { get; set; }

		public string Theme { get; set; }

		public string Text { get; set; }

		[DataType(DataType.Date)]
		public DateTime Date { get; set; }
	}
}

