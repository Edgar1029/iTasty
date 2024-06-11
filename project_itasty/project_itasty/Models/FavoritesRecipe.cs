using System;
using System.Collections.Generic;

namespace project_itasty.Models;

public partial class FavoritesRecipe
{
    public int UserId { get; set; }

    public int FavoriteRecipeId { get; set; }

    public int RecipeId { get; set; }

    public virtual ICollection<FavoritesCheck> FavoritesChecks { get; set; } = new List<FavoritesCheck>();

    public virtual RecipeTable Recipe { get; set; } = null!;

    public virtual UserInfo User { get; set; } = null!;
}
