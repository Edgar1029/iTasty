using System;
using System.Collections.Generic;

namespace project_itasty.Models;

public partial class CustomRecipeFolder
{
    public int UserId { get; set; }

    public int CustomFolderId { get; set; }

    public string CustomFolderName { get; set; } = null!;

    public int RecipeId { get; set; }

    public virtual Recipe Recipe { get; set; } = null!;

    public virtual UserInfo User { get; set; } = null!;
}
