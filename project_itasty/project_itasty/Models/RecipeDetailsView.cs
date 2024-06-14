namespace project_itasty.Models
{
    public class RecipeDetailsView
    {
        public RecipeTable? Recipe { get; set; }
        public List<IngredientsTable>? IngredientsTables { get; set; }
        public List<StepTable>? StepTables { get; set; }
		public UserInfo? User { get; set; }
		public List<MessageTable>? MessageTables { get; set; }

	}
}
