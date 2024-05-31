using System;
using System.Collections.Generic;

namespace Final_project_test.Models;

public partial class StepTable
{
    public int Id { get; set; }

    public int RecipeId { get; set; }

    public string StepText { get; set; } = null!;

    public byte[]? StepImg { get; set; }

    public virtual RecipeTable Recipe { get; set; } = null!;
}
