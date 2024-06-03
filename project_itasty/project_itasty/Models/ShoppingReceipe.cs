using System;
using System.Collections.Generic;

namespace project_itasty.Models;

public partial class ShoppingReceipe
{
    public int UserId { get; set; }

    public int ShoppingReceipeId { get; set; }

    public int RecipeId { get; set; }

    public virtual RecipeTable Recipe { get; set; } = null!;

    public virtual UserInfo User { get; set; } = null!;
}
