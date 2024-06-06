using System;
using System.Collections.Generic;

namespace Final_project_test.Models;

public partial class Recipe
{
    public int RecipeId { get; set; }

    public int UserId { get; set; }

    public string RecipeName { get; set; } = null!;

    public byte[]? RecipeCoverImage { get; set; }

    public string? RecipeIntroduction { get; set; }

    public int Views { get; set; }

    public int Favorites { get; set; }

    public int? ParentRecipeId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public string? RecipeStatus { get; set; }

    public string? PublicPrivate { get; set; }

    public string? ProteinUsed { get; set; }

    public string? MealType { get; set; }

    public string? CuisineStyle { get; set; }

    public string? HealthyOptions { get; set; }

    public virtual ICollection<CustomRecipeFolder> CustomRecipeFolders { get; set; } = new List<CustomRecipeFolder>();

    public virtual ICollection<EditedRecipe> EditedRecipes { get; set; } = new List<EditedRecipe>();

    public virtual ICollection<FavoritesRecipe> FavoritesRecipes { get; set; } = new List<FavoritesRecipe>();

    public virtual ICollection<IngredientsTable> IngredientsTables { get; set; } = new List<IngredientsTable>();

    public virtual ICollection<Recipe> InverseParentRecipe { get; set; } = new List<Recipe>();

    public virtual ICollection<MessageTable> MessageTables { get; set; } = new List<MessageTable>();

    public virtual Recipe? ParentRecipe { get; set; }

    public virtual ICollection<ShoppingReceipe> ShoppingReceipes { get; set; } = new List<ShoppingReceipe>();

    public virtual ICollection<StepTable> StepTables { get; set; } = new List<StepTable>();

    public virtual UserInfo User { get; set; } = null!;
}
