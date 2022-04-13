using System;
using System.ComponentModel.DataAnnotations;

namespace Task45.ViewModels
{
	public class MessageViewModel
	{

		[Required(ErrorMessage = "This field is required.")]
		public string Recipient { get; set; }

		[Required(ErrorMessage = "This field is required.")]
		public string Theme { get; set; }

		[Required(ErrorMessage = "This field is required.")]
		public string Text { get; set; }

	}
}

