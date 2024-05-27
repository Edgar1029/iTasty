using System;
using System.Collections.Generic;

namespace project_itasty.Models;

public partial class Recipe
{
    public int RecipeId { get; set; }

    public int? UserId { get; set; }

    public string? RecipeName { get; set; }

    public int? IngredientsTableId { get; set; }

    public byte[]? StepNimage { get; set; }

    public string? StepNtext { get; set; }

    public int? Views { get; set; }

    public int? Favorites { get; set; }

    public int? ParentRecipeId { get; set; }

    public int? IngredientsTable1Id { get; set; }

    public int? CommentsBoardId { get; set; }

    public byte[]? RecipeCoverImage { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public string? RecipeIntroduction { get; set; }

    public string? RecipeStatus { get; set; }

    public string? PublicPrivate { get; set; }

    public virtual ICollection<CustomRecipeFolder> CustomRecipeFolders { get; set; } = new List<CustomRecipeFolder>();

    public virtual ICollection<EditedRecipe> EditedRecipes { get; set; } = new List<EditedRecipe>();

    public virtual ICollection<FavoritesRecipe> FavoritesRecipes { get; set; } = new List<FavoritesRecipe>();

    public virtual ICollection<Recipe> InverseParentRecipe { get; set; } = new List<Recipe>();

    public virtual Recipe? ParentRecipe { get; set; }

    public virtual ICollection<ShoppingReceipe> ShoppingReceipes { get; set; } = new List<ShoppingReceipe>();

    public virtual UserInfo? User { get; set; }
}
