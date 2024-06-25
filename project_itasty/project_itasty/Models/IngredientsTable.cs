using System;
using System.Collections.Generic;

namespace project_itasty.Models;

public partial class IngredientsTable
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int RecipeId { get; set; }

    public string? TitleName { get; set; }

    public int? TitleId { get; set; }

    public string? IngredientsId { get; set; }

    public string? IngredientsName { get; set; }

    public float? IngredientsNumber { get; set; }

    public string? IngredientsUnit { get; set; }

    public float? Kcalg { get; set; }

    public virtual ICollection<FavoritesCheck> FavoritesChecks { get; set; } = new List<FavoritesCheck>();

    public virtual IngredientDetail? Ingredients { get; set; }

    public virtual RecipeTable Recipe { get; set; } = null!;

    public virtual UserInfo User { get; set; } = null!;
}
