namespace project_itasty.Models
{
    public class RecipeDetailsView
    {
        public RecipeTable? Recipe { get; set; }
        public List<IngredientsTable>? IngredientsTables { get; set; }
        public List<StepTable>? StepTables { get; set; }
		public UserInfo? User { get; set; }
		public List<MessageTable>? MessageTables { get; set; }

        public List<UserInfo>? Message_users { get; set; }

		public string MessageContent { get; set; }

		public int? FatherMessage { get; set; }

		public UserInfo? LoginUser { get; set; }

	}
}
