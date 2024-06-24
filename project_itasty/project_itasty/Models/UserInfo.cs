using System;
using System.Collections.Generic;

namespace project_itasty.Models;

public partial class UserInfo
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string? UserEmail { get; set; }

    public string UserPassword { get; set; } = null!;

    public byte[]? UserPhoto { get; set; }

    public byte[]? UserBanner { get; set; }

    public string? UserIntro { get; set; }

    public int UserPermissions { get; set; }

    public DateTime UserCreateTime { get; set; }

    public virtual ICollection<CustomRecipeFolder> CustomRecipeFolders { get; set; } = new List<CustomRecipeFolder>();

    public virtual ICollection<EditedRecipe> EditedRecipes { get; set; } = new List<EditedRecipe>();

    public virtual ICollection<FavoritesRecipe> FavoritesRecipes { get; set; } = new List<FavoritesRecipe>();

    public virtual ICollection<HelpForm> HelpForms { get; set; } = new List<HelpForm>();

    public virtual ICollection<IngredientsTable> IngredientsTables { get; set; } = new List<IngredientsTable>();

    public virtual ICollection<MessageTable> MessageTables { get; set; } = new List<MessageTable>();

    public virtual ICollection<RecipeTable> RecipeTables { get; set; } = new List<RecipeTable>();

    public virtual ICollection<ReportTable> ReportTables { get; set; } = new List<ReportTable>();

    public virtual ICollection<ShoppingRecipe> ShoppingRecipes { get; set; } = new List<ShoppingRecipe>();

    public virtual ICollection<UserFollower> UserFollowerFollowers { get; set; } = new List<UserFollower>();

    public virtual ICollection<UserFollower> UserFollowerUsers { get; set; } = new List<UserFollower>();
}
