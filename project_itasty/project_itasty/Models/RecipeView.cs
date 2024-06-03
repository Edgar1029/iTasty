using System;
using System.Collections.Generic;

namespace project_itasty.Models;

public partial class RecipeView
{
    public int RecipeId { get; set; }

    public DateOnly ViewDate { get; set; }

    public int? ViewNum { get; set; }

    public virtual RecipeTable Recipe { get; set; } = null!;
}
