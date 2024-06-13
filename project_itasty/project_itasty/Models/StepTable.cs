using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace project_itasty.Models;

public partial class StepTable
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

    public int RecipeId { get; set; }

    public string StepText { get; set; } = null!;

    public byte[]? StepImg { get; set; }

	[NotMapped]
	public string? StepBase64 { get; set; }

	public virtual RecipeTable Recipe { get; set; } = null!;
}
