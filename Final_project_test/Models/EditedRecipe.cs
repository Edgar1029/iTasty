using System;
using System.Collections.Generic;

namespace Final_project_test.Models;

public partial class EditedRecipe
{
    public int UserId { get; set; }

    public int EditedRecipeId { get; set; }

    public int RecipeId { get; set; }

    public virtual RecipeTable Recipe { get; set; } = null!;

    public virtual UserInfo User { get; set; } = null!;
}
