using System;
using System.Collections.Generic;

namespace Final_project_test.Models;

public partial class Ingredient
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? Commonname { get; set; }

    public float? Kcalg { get; set; }
}
