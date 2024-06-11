using System;
using System.Collections.Generic;

namespace project_itasty.Models;

public partial class SeasonalIngredient
{
    public int Id { get; set; }

    public int MonthId { get; set; }

    public string? CommonName { get; set; }

    public byte[]? IngredientsImg { get; set; }

    public bool? IsActive { get; set; }
}
