using System;
using System.Collections.Generic;

namespace project_itasty.Models;

public partial class IngredientsTable
{
    public int Id { get; set; }

    public int RecipeId { get; set; }

    public string? TitleName { get; set; }

    public int? TitleId { get; set; }

    public string? IngredientsId { get; set; }

    public string? IngredientsName { get; set; }

    public float? IngredientsNumber { get; set; }

    public string? IngredientsUnit { get; set; }

    public int? IngredientsPrice { get; set; }

    public bool? Checkbox { get; set; }

    public virtual ICollection<IngredientsTable> InverseTitle { get; set; } = new List<IngredientsTable>();

    public virtual RecipeTable Recipe { get; set; } = null!;

    public virtual IngredientsTable? Title { get; set; }
}
