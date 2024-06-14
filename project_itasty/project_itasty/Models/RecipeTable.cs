﻿using System;
using System.Collections.Generic;

namespace project_itasty.Models;

public partial class RecipeTable
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

    public int? CookingTime { get; set; }

    public int? Servings { get; set; }

    public int? Calories { get; set; }

    public virtual ICollection<EditedRecipe> EditedRecipes { get; set; } = new List<EditedRecipe>();

    public virtual ICollection<IngredientsTable> IngredientsTables { get; set; } = new List<IngredientsTable>();

    public virtual ICollection<RecipeTable> InverseParentRecipe { get; set; } = new List<RecipeTable>();

    public virtual ICollection<MessageTable> MessageTables { get; set; } = new List<MessageTable>();

    public virtual RecipeTable? ParentRecipe { get; set; }

    public virtual ICollection<ShoppingRecipe> ShoppingRecipes { get; set; } = new List<ShoppingRecipe>();

    public virtual ICollection<StepTable> StepTables { get; set; } = new List<StepTable>();

    public virtual UserInfo User { get; set; } = null!;
}
