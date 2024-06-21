using System.ComponentModel.DataAnnotations.Schema;

namespace project_itasty.Models
{
	public class StepForEdit
	{
		public int StepId { get; set; }

		public int StepRecipeId { get; set; }

		public string StepText { get; set; } = null!;

		public byte[]? StepImg { get; set; }

		[NotMapped]
		public string? StepBase64 { get; set; }

	}
}
