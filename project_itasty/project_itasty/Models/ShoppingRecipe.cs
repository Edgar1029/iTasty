using System;
using System.Collections.Generic;

namespace project_itasty.Models;

public partial class ShoppingRecipe
{
    public int UserId { get; set; }

    public int ShoppingReceipeId { get; set; }

    public int RecipeId { get; set; }

    public string RecipeName { get; set; } = null!;

    public byte[]? RecipeCoverImage { get; set; }

    public string FolderName { get; set; } = null!;

    public string? ShoppingIngredientsName { get; set; }

    public float? ShoppingIngredientsNumber { get; set; }

    public string? ShoppingIngredientsUnit { get; set; }

    public bool? Checkbox { get; set; }

    public DateTime IngredientTime { get; set; }

    public virtual RecipeTable Recipe { get; set; } = null!;

    public virtual UserInfo User { get; set; } = null!;
}
