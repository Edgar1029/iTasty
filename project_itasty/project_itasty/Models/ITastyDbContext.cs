﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace project_itasty.Models;

public partial class ITastyDbContext : DbContext
{
    public ITastyDbContext()
    {
    }

    public ITastyDbContext(DbContextOptions<ITastyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CustomRecipeFolder> CustomRecipeFolders { get; set; }

    public virtual DbSet<EditedRecipe> EditedRecipes { get; set; }

    public virtual DbSet<FavoritesCheck> FavoritesChecks { get; set; }

    public virtual DbSet<FavoritesRecipe> FavoritesRecipes { get; set; }

    public virtual DbSet<HelpForm> HelpForms { get; set; }

    public virtual DbSet<IngredientDetail> IngredientDetails { get; set; }

    public virtual DbSet<IngredientsTable> IngredientsTables { get; set; }

    public virtual DbSet<MessageTable> MessageTables { get; set; }

    public virtual DbSet<RecipeTable> RecipeTables { get; set; }

    public virtual DbSet<RecipeView> RecipeViews { get; set; }

    public virtual DbSet<SeasonalIngredient> SeasonalIngredients { get; set; }

    public virtual DbSet<ShoppingRecipe> ShoppingRecipes { get; set; }

    public virtual DbSet<StepTable> StepTables { get; set; }

    public virtual DbSet<UserFollower> UserFollowers { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=itasty.database.windows.net;Initial Catalog=iTastyDB;Persist Security Info=True;User ID=dbu;PassWord=P@ssw0rd;Encrypt=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomRecipeFolder>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.CustomFolderId }).HasName("PK__customRe__2001186CC91BDA34");

            entity.ToTable("customRecipeFolder");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CustomFolderId)
                .ValueGeneratedOnAdd()
                .HasColumnName("customFolderId");
            entity.Property(e => e.CustomFolderName)
                .HasMaxLength(50)
                .HasColumnName("customFolderName");
            entity.Property(e => e.RecipeId).HasColumnName("recipeId");

            entity.HasOne(d => d.Recipe).WithMany(p => p.CustomRecipeFolders)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK__customRec__recip__671F4F74");

            entity.HasOne(d => d.User).WithMany(p => p.CustomRecipeFolders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__customRec__userI__662B2B3B");
        });

        modelBuilder.Entity<EditedRecipe>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.EditedRecipeId }).HasName("PK__editedRe__45BD02F7B0A58D22");

            entity.ToTable("editedRecipe");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.EditedRecipeId)
                .ValueGeneratedOnAdd()
                .HasColumnName("editedRecipeId");
            entity.Property(e => e.RecipeId).HasColumnName("recipeId");

            entity.HasOne(d => d.Recipe).WithMany(p => p.EditedRecipes)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__editedRec__recip__1F63A897");

            entity.HasOne(d => d.User).WithMany(p => p.EditedRecipes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__editedRec__userI__1E6F845E");
        });

        modelBuilder.Entity<FavoritesCheck>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.FavoriteRecipeId }).HasName("PK__favorite__6CF7028125728E2C");

            entity.ToTable("favoritesCheck");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FavoriteRecipeId).HasColumnName("favoriteRecipeId");
            entity.Property(e => e.Checkbox).HasColumnName("checkbox");

            entity.HasOne(d => d.FavoriteRecipe).WithMany(p => p.FavoritesChecks)
                .HasForeignKey(d => d.FavoriteRecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__favorites__favor__2F9A1060");

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.FavoritesChecks)
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__favoritesChe__id__2EA5EC27");
        });

        modelBuilder.Entity<FavoritesRecipe>(entity =>
        {
            entity.HasKey(e => e.FavoriteRecipeId).HasName("PK__favorite__EE4EABE4C1121B3E");

            entity.ToTable("favoritesRecipe");

            entity.Property(e => e.FavoriteRecipeId).HasColumnName("favoriteRecipeId");
            entity.Property(e => e.RecipeId).HasColumnName("recipeId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Recipe).WithMany(p => p.FavoritesRecipes)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__favorites__recip__2334397B");

            entity.HasOne(d => d.User).WithMany(p => p.FavoritesRecipes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__favorites__userI__22401542");
        });

        modelBuilder.Entity<HelpForm>(entity =>
        {
            entity.HasKey(e => e.FormId).HasName("PK__helpForm__51BCB72B3D456AB4");

            entity.ToTable("helpForm");

            entity.Property(e => e.FormId).HasColumnName("formId");
            entity.Property(e => e.QuestionContent)
                .HasMaxLength(50)
                .HasColumnName("questionContent");
            entity.Property(e => e.QuestionImage).HasColumnName("questionImage");
            entity.Property(e => e.QuestionType)
                .HasMaxLength(200)
                .HasColumnName("questionType");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.HelpForms)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__helpForm__userId__32767D0B");
        });

        modelBuilder.Entity<IngredientDetail>(entity =>
        {
            entity.HasKey(e => e.IngredientId).HasName("PK__Ingredie__2753A5270210F161");

            entity.ToTable("IngredientDetail");

            entity.Property(e => e.IngredientId)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ingredientId");
            entity.Property(e => e.CommonName).HasColumnName("commonName");
            entity.Property(e => e.IngredientName)
                .HasMaxLength(20)
                .HasColumnName("ingredientName");
            entity.Property(e => e.Kcalg).HasColumnName("kcalg");
        });

        modelBuilder.Entity<IngredientsTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ingredie__3213E83FE14DBDBC");

            entity.ToTable("ingredientsTable");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IngredientsId)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ingredientsID");
            entity.Property(e => e.IngredientsName)
                .HasMaxLength(20)
                .HasColumnName("ingredientsName");
            entity.Property(e => e.IngredientsNumber).HasColumnName("ingredientsNumber");
            entity.Property(e => e.IngredientsUnit)
                .HasMaxLength(50)
                .HasColumnName("ingredientsUnit");
            entity.Property(e => e.RecipeId).HasColumnName("recipeId");
            entity.Property(e => e.TitleId).HasColumnName("titleId");
            entity.Property(e => e.TitleName)
                .HasMaxLength(20)
                .HasColumnName("titleName");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Ingredients).WithMany(p => p.IngredientsTables)
                .HasForeignKey(d => d.IngredientsId)
                .HasConstraintName("FK__ingredien__ingre__2BC97F7C");

            entity.HasOne(d => d.Recipe).WithMany(p => p.IngredientsTables)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ingredien__recip__2AD55B43");

            entity.HasOne(d => d.User).WithMany(p => p.IngredientsTables)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ingredien__userI__29E1370A");
        });

        modelBuilder.Entity<MessageTable>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__messageT__4808B993840DB719");

            entity.ToTable("messageTable");

            entity.Property(e => e.MessageId).HasColumnName("messageId");
            entity.Property(e => e.ChangeTime)
                .HasColumnType("smalldatetime")
                .HasColumnName("changeTime");
            entity.Property(e => e.CreateTime)
                .HasColumnType("smalldatetime")
                .HasColumnName("createTime");
            entity.Property(e => e.MessageContent)
                .HasMaxLength(150)
                .HasColumnName("messageContent");
            entity.Property(e => e.RecipeId).HasColumnName("recipeId");
            entity.Property(e => e.TopMessageid).HasColumnName("topMessageid");
            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.ViolationStatus)
                .HasMaxLength(20)
                .HasColumnName("violationStatus");

            entity.HasOne(d => d.Recipe).WithMany(p => p.MessageTables)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__messageTa__recip__7FEAFD3E");

            entity.HasOne(d => d.User).WithMany(p => p.MessageTables)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__messageTa__viola__7EF6D905");
        });

        modelBuilder.Entity<RecipeTable>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("PK__recipeTa__C114EE8322C106FC");

            entity.ToTable("recipeTable");

            entity.Property(e => e.RecipeId)
                .ValueGeneratedNever()
                .HasColumnName("recipeId");
            entity.Property(e => e.Calories).HasColumnName("calories");
            entity.Property(e => e.CookingTime).HasColumnName("cookingTime");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("createdDate");
            entity.Property(e => e.CuisineStyle)
                .HasMaxLength(50)
                .HasColumnName("cuisineStyle");
            entity.Property(e => e.Favorites).HasColumnName("favorites");
            entity.Property(e => e.HealthyOptions)
                .HasMaxLength(50)
                .HasColumnName("healthyOptions");
            entity.Property(e => e.LastModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("lastModifiedDate");
            entity.Property(e => e.MealType)
                .HasMaxLength(50)
                .HasColumnName("mealType");
            entity.Property(e => e.ParentRecipeId).HasColumnName("parentRecipeId");
            entity.Property(e => e.ProteinUsed)
                .HasMaxLength(50)
                .HasColumnName("proteinUsed");
            entity.Property(e => e.PublicPrivate)
                .HasMaxLength(10)
                .HasColumnName("publicPrivate");
            entity.Property(e => e.RecipeCoverImage).HasColumnName("recipeCoverImage");
            entity.Property(e => e.RecipeIntroduction).HasColumnName("recipeIntroduction");
            entity.Property(e => e.RecipeName)
                .HasMaxLength(255)
                .HasColumnName("recipeName");
            entity.Property(e => e.RecipeStatus)
                .HasMaxLength(50)
                .HasColumnName("recipeStatus");
            entity.Property(e => e.Servings).HasColumnName("servings");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Views).HasColumnName("views");

            entity.HasOne(d => d.ParentRecipe).WithMany(p => p.InverseParentRecipe)
                .HasForeignKey(d => d.ParentRecipeId)
                .HasConstraintName("FK__recipeTab__paren__634EBE90");

            entity.HasOne(d => d.User).WithMany(p => p.RecipeTables)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__recipeTab__userI__625A9A57");
        });

        modelBuilder.Entity<RecipeView>(entity =>
        {
            entity.HasKey(e => new { e.RecipeId, e.ViewDate }).HasName("PK__recipeVi__BD2D12EE8E9DAA1F");

            entity.ToTable("recipeView");

            entity.Property(e => e.RecipeId).HasColumnName("recipeId");
            entity.Property(e => e.ViewDate).HasColumnName("viewDate");
            entity.Property(e => e.ViewNum).HasColumnName("viewNum");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeViews)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__recipeVie__recip__0880433F");
        });

        modelBuilder.Entity<SeasonalIngredient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__seasonal__3213E83F88A8C15B");

            entity.ToTable("seasonalIngredients");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommonName).HasColumnName("commonName");
            entity.Property(e => e.IngredientsImg).HasColumnName("ingredientsImg");
            entity.Property(e => e.MonthId).HasColumnName("monthId");
        });

        modelBuilder.Entity<ShoppingRecipe>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ShoppingReceipeId }).HasName("PK__shopping__3C59E2389CCB37F9");

            entity.ToTable("shoppingRecipe");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ShoppingReceipeId)
                .ValueGeneratedOnAdd()
                .HasColumnName("shoppingReceipeId");
            entity.Property(e => e.Checkbox).HasColumnName("checkbox");
            entity.Property(e => e.FolderName)
                .HasMaxLength(50)
                .HasColumnName("folderName");
            entity.Property(e => e.IngredientTime)
                .HasColumnType("smalldatetime")
                .HasColumnName("ingredientTime");
            entity.Property(e => e.RecipeCoverImage).HasColumnName("recipeCoverImage");
            entity.Property(e => e.RecipeId).HasColumnName("recipeId");
            entity.Property(e => e.RecipeName)
                .HasMaxLength(255)
                .HasColumnName("recipeName");
            entity.Property(e => e.ShoppingIngredientsName)
                .HasMaxLength(20)
                .HasColumnName("shoppingIngredientsName");
            entity.Property(e => e.ShoppingIngredientsNumber).HasColumnName("shoppingIngredientsNumber");
            entity.Property(e => e.ShoppingIngredientsUnit)
                .HasMaxLength(50)
                .HasColumnName("shoppingIngredientsUnit");

            entity.HasOne(d => d.Recipe).WithMany(p => p.ShoppingRecipes)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__shoppingR__recip__2704CA5F");

            entity.HasOne(d => d.User).WithMany(p => p.ShoppingRecipes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__shoppingR__userI__2610A626");
        });

        modelBuilder.Entity<StepTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__stepTabl__3213E83F2B03F157");

            entity.ToTable("stepTable");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RecipeId).HasColumnName("recipeId");
            entity.Property(e => e.StepImg).HasColumnName("stepImg");
            entity.Property(e => e.StepText)
                .HasMaxLength(150)
                .HasColumnName("stepText");

            entity.HasOne(d => d.Recipe).WithMany(p => p.StepTables)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__stepTable__recip__02C769E9");
        });

        modelBuilder.Entity<UserFollower>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.FollowerId, e.FollowDate }).HasName("PK__userFoll__4265C32DB9359E08");

            entity.ToTable("userFollower");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.FollowerId).HasColumnName("followerId");
            entity.Property(e => e.FollowDate).HasColumnName("followDate");
            entity.Property(e => e.UnfollowDate).HasColumnName("unfollowDate");

            entity.HasOne(d => d.Follower).WithMany(p => p.UserFollowerFollowers)
                .HasForeignKey(d => d.FollowerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__userFollo__follo__0C50D423");

            entity.HasOne(d => d.User).WithMany(p => p.UserFollowerUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__userFollo__userI__0B5CAFEA");
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__userInfo__CB9A1CFFE3B4B211");

            entity.ToTable("userInfo");

            entity.HasIndex(e => e.UserEmail, "UQ__userInfo__D54ADF55FA16E371").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.UserBanner).HasColumnName("userBanner");
            entity.Property(e => e.UserCreateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("userCreateTime");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(30)
                .HasColumnName("userEmail");
            entity.Property(e => e.UserIntro)
                .HasMaxLength(50)
                .HasColumnName("userIntro");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .HasColumnName("userName");
            entity.Property(e => e.UserPassword).HasColumnName("userPassword");
            entity.Property(e => e.UserPermissions).HasColumnName("userPermissions");
            entity.Property(e => e.UserPhoto).HasColumnName("userPhoto");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
