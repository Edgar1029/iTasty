using System;
using System.Collections.Generic;

namespace Final_project_test.Models;

public partial class FavoritesCheck
{
    public int FavoriteRecipeId { get; set; }

    public int Id { get; set; }

    public bool? Checkbox { get; set; }

    public virtual FavoritesRecipe FavoriteRecipe { get; set; } = null!;

    public virtual IngredientsTable IdNavigation { get; set; } = null!;
}
