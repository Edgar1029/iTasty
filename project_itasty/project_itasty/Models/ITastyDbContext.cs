using System;
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

    public virtual DbSet<ReportTable> ReportTables { get; set; }

    public virtual DbSet<SeasonalIngredient> SeasonalIngredients { get; set; }

    public virtual DbSet<ShoppingRecipe> ShoppingRecipes { get; set; }

    public virtual DbSet<StepTable> StepTables { get; set; }

    public virtual DbSet<UserFollower> UserFollowers { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.;Database=iTastyDB;Integrated Security=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomRecipeFolder>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.CustomFolderId }).HasName("PK__customRe__2001186C044A02E3");

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
            entity.HasKey(e => new { e.UserId, e.EditedRecipeId }).HasName("PK__editedRe__45BD02F7A3935877");

            entity.ToTable("editedRecipe");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.EditedRecipeId)
                .ValueGeneratedOnAdd()
                .HasColumnName("editedRecipeId");
            entity.Property(e => e.RecipeId).HasColumnName("recipeId");

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
            entity.HasKey(e => new { e.Id, e.FavoriteRecipeId }).HasName("PK__favorite__6CF7028191BB54B6");

            entity.ToTable("favoritesCheck");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FavoriteRecipeId).HasColumnName("favoriteRecipeId");
            entity.Property(e => e.Checkbox).HasColumnName("checkbox");

            entity.HasOne(d => d.FavoriteRecipe).WithMany(p => p.FavoritesChecks)
                .HasForeignKey(d => d.FavoriteRecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__favorites__favor__5DCAEF64");

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.FavoritesChecks)
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__favoritesChe__id__5CD6CB2B");
        });

        modelBuilder.Entity<FavoritesRecipe>(entity =>
        {
            entity.HasKey(e => e.FavoriteRecipeId).HasName("PK__favorite__EE4EABE481282147");

            entity.ToTable("favoritesRecipe");

            entity.Property(e => e.FavoriteRecipeId).HasColumnName("favoriteRecipeId");
            entity.Property(e => e.RecipeId).HasColumnName("recipeId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Recipe).WithMany(p => p.FavoritesRecipes)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__favorites__recip__4F7CD00D");

            entity.HasOne(d => d.User).WithMany(p => p.FavoritesRecipes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__favorites__userI__4E88ABD4");
        });

        modelBuilder.Entity<HelpForm>(entity =>
        {
            entity.HasKey(e => e.FormId).HasName("PK__helpForm__51BCB72BA0492340");

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
                .HasConstraintName("FK__helpForm__userId__60A75C0F");
        });

        modelBuilder.Entity<IngredientDetail>(entity =>
        {
            entity.HasKey(e => e.IngredientId).HasName("PK__Ingredie__2753A527B4E46072");

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
            entity.HasKey(e => e.Id).HasName("PK__ingredie__3213E83F7C968BB9");

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
                .HasConstraintName("FK__ingredien__ingre__59FA5E80");

            entity.HasOne(d => d.Recipe).WithMany(p => p.IngredientsTables)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ingredien__recip__59063A47");

            entity.HasOne(d => d.User).WithMany(p => p.IngredientsTables)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ingredien__userI__5812160E");
        });

        modelBuilder.Entity<MessageTable>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__messageT__4808B993734D0994");

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
                .HasConstraintName("FK__messageTa__recip__6477ECF3");

            entity.HasOne(d => d.User).WithMany(p => p.MessageTables)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__messageTa__viola__6383C8BA");
        });

        modelBuilder.Entity<RecipeTable>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("PK__recipeTa__C114EE833DB2EDA5");

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
            entity.HasKey(e => new { e.RecipeId, e.ViewDate }).HasName("PK__recipeVi__BD2D12EEC6F0B567");

            entity.ToTable("recipeView");

            entity.Property(e => e.RecipeId).HasColumnName("recipeId");
            entity.Property(e => e.ViewDate).HasColumnName("viewDate");
            entity.Property(e => e.ViewNum).HasColumnName("viewNum");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeViews)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__recipeVie__recip__440B1D61");
        });

        modelBuilder.Entity<ReportTable>(entity =>
        {
            entity.HasKey(e => e.ReportId);

            entity.ToTable("ReportTable");
        });

        modelBuilder.Entity<SeasonalIngredient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__seasonal__3213E83FC280863C");

            entity.ToTable("seasonalIngredients");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommonName).HasColumnName("commonName");
            entity.Property(e => e.IngredientsImg).HasColumnName("ingredientsImg");
            entity.Property(e => e.MonthId).HasColumnName("monthId");
        });

        modelBuilder.Entity<ShoppingRecipe>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ShoppingReceipeId }).HasName("PK__shopping__3C59E2388819D641");

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
                .HasConstraintName("FK__shoppingR__recip__534D60F1");

            entity.HasOne(d => d.User).WithMany(p => p.ShoppingRecipes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__shoppingR__userI__52593CB8");
        });

        modelBuilder.Entity<StepTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__stepTabl__3213E83FB8032686");

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
                .HasConstraintName("FK__stepTable__recip__6754599E");
        });

        modelBuilder.Entity<UserFollower>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.FollowerId, e.FollowDate }).HasName("PK__userFoll__4265C32DEBAEF184");

            entity.ToTable("userFollower");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.FollowerId).HasColumnName("followerId");
            entity.Property(e => e.FollowDate).HasColumnName("followDate");
            entity.Property(e => e.UnfollowDate).HasColumnName("unfollowDate");

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
            entity.HasKey(e => e.UserId).HasName("PK__userInfo__CB9A1CFF44A7B5AF");

            entity.ToTable("userInfo");

            entity.HasIndex(e => e.UserEmail, "UQ__userInfo__D54ADF55697FBE4D").IsUnique();

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
