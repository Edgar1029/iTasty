using System;
using System.Collections.Generic;

namespace project_itasty.Models;

public partial class SeasonalIngredient
{
    public int IngredientsId { get; set; }

    public int MonthId { get; set; }

    public string IngredientsName { get; set; } = null!;

    public byte[]? IngredientsImg { get; set; }
}
