using System;
using System.Collections.Generic;

namespace Final_project_test.Models;

public partial class CustomReceipeFolder
{
    public int MemberId { get; set; }

    public int CustomFolderId { get; set; }

    public string CustomFolderName { get; set; } = null!;

    public int RecipeId { get; set; }
}
