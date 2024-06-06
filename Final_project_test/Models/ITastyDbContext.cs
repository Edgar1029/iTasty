using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Final_project_test.Models;

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-AVM54SB;Initial Catalog=iTastyDB;Persist Security Info=False;User ID=sa;PassWord=111111;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomRecipeFolder>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.CustomFolderId }).HasName("PK__customRe__2001186C7D65B061");

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
                .HasConstraintName("FK__customRec__recip__47DBAE45");

            entity.HasOne(d => d.User).WithMany(p => p.CustomRecipeFolders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__customRec__userI__46E78A0C");
        });

        modelBuilder.Entity<EditedRecipe>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.EditedRecipeId }).HasName("PK__editedRe__45BD02F7D5EF7F35");

            entity.ToTable("editedRecipe");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.EditedRecipeId)
                .ValueGeneratedOnAdd()
                .HasColumnName("editedRecipeId");
            entity.Property(e => e.RecipeCoverImage).HasColumnName("recipeCoverImage");
            entity.Property(e => e.RecipeId).HasColumnName("recipeId");
            entity.Property(e => e.RecipeName)
                .HasMaxLength(255)
                .HasColumnName("recipeName");

            entity.HasOne(d => d.Recipe).WithMany(p => p.EditedRecipes)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__editedRec__recip__4BAC3F29");

            entity.HasOne(d => d.User).WithMany(p => p.EditedRecipes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__editedRec__userI__4AB81AF0");
        });

        modelBuilder.Entity<FavoritesCheck>(entity =>
        {
            entity.HasKey(e => new { e.FavoriteRecipeId, e.Id }).HasName("PK__favorite__0D6F9567B8D2B92A");

            entity.ToTable("favoritesCheck");

            entity.Property(e => e.FavoriteRecipeId).HasColumnName("favoriteRecipeId");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Checkbox).HasColumnName("checkbox");

            entity.HasOne(d => d.FavoriteRecipe).WithMany(p => p.FavoritesChecks)
                .HasForeignKey(d => d.FavoriteRecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__favorites__favor__7F2BE32F");

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.FavoritesChecks)
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__favoritesChe__id__00200768");
        });

        modelBuilder.Entity<FavoritesRecipe>(entity =>
        {
            entity.HasKey(e => e.FavoriteRecipeId).HasName("PK__favorite__EE4EABE472389043");

            entity.ToTable("favoritesRecipe");

            entity.Property(e => e.FavoriteRecipeId).HasColumnName("favoriteRecipeId");
            entity.Property(e => e.RecipeId).HasColumnName("recipeId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Recipe).WithMany(p => p.FavoritesRecipes)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__favorites__recip__7C4F7684");

            entity.HasOne(d => d.User).WithMany(p => p.FavoritesRecipes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__favorites__userI__7B5B524B");
        });

        modelBuilder.Entity<HelpForm>(entity =>
        {
            entity.HasKey(e => e.FormId).HasName("PK__helpForm__51BCB72BE2062C9E");

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
                .HasConstraintName("FK__helpForm__userId__693CA210");
        });

        modelBuilder.Entity<IngredientDetail>(entity =>
        {
            entity.HasKey(e => e.IngredientId).HasName("PK__Ingredie__2753A5272EE897BB");

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
            entity.HasKey(e => e.Id).HasName("PK__ingredie__3213E83F26AD5741");

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
                .HasConstraintName("FK__ingredien__ingre__66603565");

            entity.HasOne(d => d.Recipe).WithMany(p => p.IngredientsTables)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ingredien__recip__656C112C");

            entity.HasOne(d => d.User).WithMany(p => p.IngredientsTables)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ingredien__userI__6477ECF3");
        });

        modelBuilder.Entity<MessageTable>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__messageT__4808B993DD717CE6");

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
                .HasConstraintName("FK__messageTa__recip__6D0D32F4");

            entity.HasOne(d => d.User).WithMany(p => p.MessageTables)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__messageTa__viola__6C190EBB");
        });

        modelBuilder.Entity<RecipeTable>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("PK__recipeTa__C114EE837148ACCF");

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
                .HasConstraintName("FK__recipeTab__paren__412EB0B6");

            entity.HasOne(d => d.User).WithMany(p => p.RecipeTables)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__recipeTab__userI__403A8C7D");
        });

        modelBuilder.Entity<RecipeView>(entity =>
        {
            entity.HasKey(e => new { e.RecipeId, e.ViewDate }).HasName("PK__recipeVi__BD2D12EE6944B892");

            entity.ToTable("recipeView");

            entity.Property(e => e.RecipeId).HasColumnName("recipeId");
            entity.Property(e => e.ViewDate).HasColumnName("viewDate");
            entity.Property(e => e.ViewNum).HasColumnName("viewNum");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeViews)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__recipeVie__recip__440B1D61");
        });

        modelBuilder.Entity<SeasonalIngredient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__seasonal__3213E83FB43596F1");

            entity.ToTable("seasonalIngredients");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommonName).HasColumnName("commonName");
            entity.Property(e => e.MonthId).HasColumnName("monthId");
            entity.Property(e => e.SeasonalIngredientId)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("seasonalIngredientId");

            entity.HasOne(d => d.SeasonalIngredientNavigation).WithMany(p => p.SeasonalIngredients)
                .HasForeignKey(d => d.SeasonalIngredientId)
                .HasConstraintName("FK__seasonalI__seaso__72C60C4A");
        });

        modelBuilder.Entity<ShoppingRecipe>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ShoppingReceipeId }).HasName("PK__shopping__3C59E238ADDA912D");

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
                .HasConstraintName("FK__shoppingR__recip__5FB337D6");

            entity.HasOne(d => d.User).WithMany(p => p.ShoppingRecipes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__shoppingR__userI__5EBF139D");
        });

        modelBuilder.Entity<StepTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__stepTabl__3213E83FE3B790C4");

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
                .HasConstraintName("FK__stepTable__recip__6FE99F9F");
        });

        modelBuilder.Entity<UserFollower>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.FollowerId }).HasName("PK__userFoll__CE0DEA74C415582F");

            entity.ToTable("userFollower");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.FollowerId).HasColumnName("followerId");
            entity.Property(e => e.FollowDate)
                .HasColumnType("datetime")
                .HasColumnName("followDate");
            entity.Property(e => e.UnfollowDate)
                .HasColumnType("datetime")
                .HasColumnName("unfollowDate");

            entity.HasOne(d => d.Follower).WithMany(p => p.UserFollowerFollowers)
                .HasForeignKey(d => d.FollowerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__userFollo__follo__3D5E1FD2");

            entity.HasOne(d => d.User).WithMany(p => p.UserFollowerUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__userFollo__userI__3C69FB99");
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__userInfo__CB9A1CFF4B6A850A");

            entity.ToTable("userInfo");

            entity.HasIndex(e => e.UserEmail, "UQ__userInfo__D54ADF559702715A").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.UserBanner).HasColumnName("userBanner");
            entity.Property(e => e.UserCreateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("userCreateTime");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(20)
                .HasColumnName("userEmail");
            entity.Property(e => e.UserIntro)
                .HasMaxLength(50)
                .HasColumnName("userIntro");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .HasColumnName("userName");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(20)
                .HasColumnName("userPassword");
            entity.Property(e => e.UserPermissions).HasColumnName("userPermissions");
            entity.Property(e => e.UserPhoto).HasColumnName("userPhoto");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
