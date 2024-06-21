namespace project_itasty.Models
{
	public class IngredientsForEdit
	{
		public int IngredientsTableId { get; set; }

		public int IngredientUserId { get; set; }

		public int IngredientRecipeId { get; set; }

		public string? TitleName { get; set; }

		public int? TitleId { get; set; }

		public string? IngredientsId { get; set; }

		public string? IngredientsName { get; set; }

		public float? IngredientsNumber { get; set; }

		public string? IngredientsUnit { get; set; }

		public float? IngredientKcalg { get; set; }

	}
}
