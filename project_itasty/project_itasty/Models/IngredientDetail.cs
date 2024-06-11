using System;
using System.Collections.Generic;

namespace project_itasty.Models;

public partial class IngredientDetail
{
    public string IngredientId { get; set; } = null!;

    public string? IngredientName { get; set; }

    public string? CommonName { get; set; }

    public float? Kcalg { get; set; }

    public virtual ICollection<IngredientsTable> IngredientsTables { get; set; } = new List<IngredientsTable>();
}
