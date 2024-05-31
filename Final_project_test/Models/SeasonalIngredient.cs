using System;
using System.Collections.Generic;

namespace Final_project_test.Models;

public partial class SeasonalIngredient
{
    public int Id { get; set; }

    public int MonthId { get; set; }

    public string? SeasonalIngredientId { get; set; }

    public string? CommonName { get; set; }

    public virtual IngredientDetail? SeasonalIngredientNavigation { get; set; }
}
