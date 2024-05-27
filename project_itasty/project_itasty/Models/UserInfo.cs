using System;
using System.Collections.Generic;

namespace project_itasty.Models;

public partial class UserInfo
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public byte[]? UserPhoto { get; set; }

    public byte[]? UserBanner { get; set; }

    public string? UserIntro { get; set; }

    public int UserPermissions { get; set; }

    public DateTime CreateTime { get; set; }

    public virtual ICollection<CustomRecipeFolder> CustomRecipeFolders { get; set; } = new List<CustomRecipeFolder>();

    public virtual ICollection<EditedRecipe> EditedRecipes { get; set; } = new List<EditedRecipe>();

    public virtual ICollection<FavoritesRecipe> FavoritesRecipes { get; set; } = new List<FavoritesRecipe>();

    public virtual ICollection<HelpForm> HelpForms { get; set; } = new List<HelpForm>();

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    public virtual ICollection<ShoppingReceipe> ShoppingReceipes { get; set; } = new List<ShoppingReceipe>();

    public virtual ICollection<UserFollower> UserFollowerFollowers { get; set; } = new List<UserFollower>();

    public virtual ICollection<UserFollower> UserFollowerUsers { get; set; } = new List<UserFollower>();
}
