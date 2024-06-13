using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace project_itasty.Models;

public partial class IngredientsTable
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

    public int UserId { get; set; }

    public int RecipeId { get; set; }

    public string? TitleName { get; set; }

    public int? TitleId { get; set; }

    public string? IngredientsId { get; set; }

    public string? IngredientsName { get; set; }

    public float? IngredientsNumber { get; set; }

    public string? IngredientsUnit { get; set; }

    public virtual ICollection<FavoritesCheck> FavoritesChecks { get; set; } = new List<FavoritesCheck>();

    public virtual IngredientDetail? Ingredients { get; set; }

    public virtual RecipeTable Recipe { get; set; } = null!;

    public virtual UserInfo User { get; set; } = null!;
}
