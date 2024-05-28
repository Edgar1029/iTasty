using System;
using System.Collections.Generic;

namespace project_itasty.Models;

public partial class RecipeTable
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public byte[] RecipeCover { get; set; } = null!;

    public string? RecipeIntroduction { get; set; }

    public int? TimesWatched { get; set; }

    public int? Collections { get; set; }

    public int? OriginalRecipeid { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime ChangeTime { get; set; }

    public string? RecipeState { get; set; }

    public string PublicOrnot { get; set; } = null!;

    public string? ProteinUsed { get; set; }

    public string? MealType { get; set; }

    public string? CuisineStyle { get; set; }

    public string? HealthyOptions { get; set; }

    public virtual ICollection<IngredientsTable> IngredientsTables { get; set; } = new List<IngredientsTable>();

    public virtual ICollection<MessageTable> MessageTables { get; set; } = new List<MessageTable>();

    public virtual ICollection<StepTable> StepTables { get; set; } = new List<StepTable>();
}
