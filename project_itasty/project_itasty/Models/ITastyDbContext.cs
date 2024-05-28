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

    public virtual DbSet<FavoritesRecipe> FavoritesRecipes { get; set; }

    public virtual DbSet<HelpForm> HelpForms { get; set; }

    public virtual DbSet<IngredientsTable> IngredientsTables { get; set; }

    public virtual DbSet<MessageTable> MessageTables { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeTable> RecipeTables { get; set; }

    public virtual DbSet<SeasonalIngredient> SeasonalIngredients { get; set; }

    public virtual DbSet<ShoppingReceipe> ShoppingReceipes { get; set; }

    public virtual DbSet<StepTable> StepTables { get; set; }

    public virtual DbSet<UserFollower> UserFollowers { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.;Database=iTastyDB;Integrated Security=True;Encrypt=False;MultipleActiveResultSets=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomRecipeFolder>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.CustomFolderId }).HasName("PK__customRe__2001186C524F22CC");

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
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__customRec__recip__5CD6CB2B");

            entity.HasOne(d => d.User).WithMany(p => p.CustomRecipeFolders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__customRec__userI__5BE2A6F2");
        });

        modelBuilder.Entity<EditedRecipe>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.EditedRecipeId }).HasName("PK__editedRe__45BD02F78AA5EE02");

            entity.ToTable("editedRecipe");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.EditedRecipeId)
                .ValueGeneratedOnAdd()
                .HasColumnName("editedRecipeId");
            entity.Property(e => e.RecipeId).HasColumnName("recipeId");

            entity.HasOne(d => d.Recipe).WithMany(p => p.EditedRecipes)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__editedRec__recip__60A75C0F");

            entity.HasOne(d => d.User).WithMany(p => p.EditedRecipes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__editedRec__userI__5FB337D6");
        });

        modelBuilder.Entity<FavoritesRecipe>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.FavoriteRecipeId }).HasName("PK__favorite__957EF641DDCDF556");

            entity.ToTable("favoritesRecipe");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.FavoriteRecipeId)
                .ValueGeneratedOnAdd()
                .HasColumnName("favoriteRecipeId");
            entity.Property(e => e.RecipeId).HasColumnName("recipeId");

            entity.HasOne(d => d.Recipe).WithMany(p => p.FavoritesRecipes)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__favorites__recip__6477ECF3");

            entity.HasOne(d => d.User).WithMany(p => p.FavoritesRecipes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__favorites__userI__6383C8BA");
        });

        modelBuilder.Entity<HelpForm>(entity =>
        {
            entity.HasKey(e => e.FormId).HasName("PK__helpForm__51BCB72BADE52E04");

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
                .HasConstraintName("FK_Questionnaire_User");
        });

        modelBuilder.Entity<IngredientsTable>(entity =>
        {
            entity.ToTable("ingredientsTable");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Checkbox).HasColumnName("checkbox");
            entity.Property(e => e.IngredientsId)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ingredientsID");
            entity.Property(e => e.IngredientsName)
                .HasMaxLength(20)
                .HasColumnName("ingredientsName");
            entity.Property(e => e.IngredientsNumber).HasColumnName("ingredientsNumber");
            entity.Property(e => e.IngredientsPrice).HasColumnName("ingredientsPrice");
            entity.Property(e => e.IngredientsUnit)
                .HasMaxLength(5)
                .HasColumnName("ingredientsUnit");
            entity.Property(e => e.RecipeId).HasColumnName("recipeId");
            entity.Property(e => e.TitleId).HasColumnName("titleId");
            entity.Property(e => e.TitleName)
                .HasMaxLength(20)
                .HasColumnName("titleName");

            entity.HasOne(d => d.Recipe).WithMany(p => p.IngredientsTables)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ingredientsTable_recipeTable");

            entity.HasOne(d => d.Title).WithMany(p => p.InverseTitle)
                .HasForeignKey(d => d.TitleId)
                .HasConstraintName("FK_ingredientsTable_ingredientsTable");
        });

        modelBuilder.Entity<MessageTable>(entity =>
        {
            entity.ToTable("messageTable");

            entity.Property(e => e.Id).HasColumnName("id");
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
                .HasConstraintName("FK_messageTable_recipeTable");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("PK__recipe__C114EE834861EFFF");

            entity.ToTable("recipe");

            entity.Property(e => e.RecipeId)
                .ValueGeneratedNever()
                .HasColumnName("recipeId");
            entity.Property(e => e.CommentsBoardId).HasColumnName("commentsBoardID");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("createdDate");
            entity.Property(e => e.Favorites).HasColumnName("favorites");
            entity.Property(e => e.IngredientsTable1Id).HasColumnName("ingredientsTable1ID");
            entity.Property(e => e.IngredientsTableId).HasColumnName("ingredientsTableID");
            entity.Property(e => e.LastModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("lastModifiedDate");
            entity.Property(e => e.ParentRecipeId).HasColumnName("parentRecipeID");
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
            entity.Property(e => e.StepNimage).HasColumnName("stepNImage");
            entity.Property(e => e.StepNtext).HasColumnName("stepNText");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Views).HasColumnName("views");

            entity.HasOne(d => d.ParentRecipe).WithMany(p => p.InverseParentRecipe)
                .HasForeignKey(d => d.ParentRecipeId)
                .HasConstraintName("FK__recipe__parentRe__59063A47");

            entity.HasOne(d => d.User).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__recipe__userId__5812160E");
        });

        modelBuilder.Entity<RecipeTable>(entity =>
        {
            entity.ToTable("recipeTable");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChangeTime)
                .HasColumnType("smalldatetime")
                .HasColumnName("changeTime");
            entity.Property(e => e.Collections).HasColumnName("collections");
            entity.Property(e => e.CreateTime)
                .HasColumnType("smalldatetime")
                .HasColumnName("createTime");
            entity.Property(e => e.CuisineStyle)
                .HasMaxLength(5)
                .HasColumnName("cuisineStyle");
            entity.Property(e => e.HealthyOptions)
                .HasMaxLength(5)
                .HasColumnName("healthyOptions");
            entity.Property(e => e.MealType)
                .HasMaxLength(5)
                .HasColumnName("mealType");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.OriginalRecipeid).HasColumnName("originalRecipeid");
            entity.Property(e => e.ProteinUsed)
                .HasMaxLength(4)
                .HasColumnName("proteinUsed");
            entity.Property(e => e.PublicOrnot)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("publicORnot");
            entity.Property(e => e.RecipeCover).HasColumnName("recipeCover");
            entity.Property(e => e.RecipeIntroduction)
                .HasMaxLength(200)
                .HasColumnName("recipeIntroduction");
            entity.Property(e => e.RecipeState)
                .HasMaxLength(10)
                .HasColumnName("recipeState");
            entity.Property(e => e.TimesWatched).HasColumnName("timesWatched");
            entity.Property(e => e.UserId).HasColumnName("userId");
        });

        modelBuilder.Entity<SeasonalIngredient>(entity =>
        {
            entity.HasKey(e => e.IngredientsId).HasName("PK__seasonal__A9B070FBB0F78204");

            entity.ToTable("seasonalIngredients");

            entity.Property(e => e.IngredientsId).HasColumnName("ingredientsId");
            entity.Property(e => e.IngredientsImg).HasColumnName("ingredientsImg");
            entity.Property(e => e.IngredientsName)
                .HasMaxLength(50)
                .HasColumnName("ingredientsName");
            entity.Property(e => e.MonthId).HasColumnName("monthId");
        });

        modelBuilder.Entity<ShoppingReceipe>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ShoppingReceipeId }).HasName("PK__shopping__3C59E2384744113C");

            entity.ToTable("shoppingReceipe");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ShoppingReceipeId)
                .ValueGeneratedOnAdd()
                .HasColumnName("shoppingReceipeId");
            entity.Property(e => e.RecipeId).HasColumnName("recipeId");

            entity.HasOne(d => d.Recipe).WithMany(p => p.ShoppingReceipes)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__shoppingR__recip__68487DD7");

            entity.HasOne(d => d.User).WithMany(p => p.ShoppingReceipes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__shoppingR__userI__6754599E");
        });

        modelBuilder.Entity<StepTable>(entity =>
        {
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
                .HasConstraintName("FK_stepTable_recipeTable");
        });

        modelBuilder.Entity<UserFollower>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.FollowerId }).HasName("PK__userFoll__CE0DEA7496F15FE0");

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
                .HasConstraintName("FK__userFollo__follo__5535A963");

            entity.HasOne(d => d.User).WithMany(p => p.UserFollowerUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__userFollo__userI__5441852A");
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__userInfo__CB9A1CFF334E7AEF");

            entity.ToTable("userInfo");

            entity.HasIndex(e => e.Email, "UQ__userInfo__AB6E6164C582F710").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("createTime");
            entity.Property(e => e.Email)
                .HasMaxLength(20)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .HasColumnName("password");
            entity.Property(e => e.UserBanner).HasColumnName("userBanner");
            entity.Property(e => e.UserIntro)
                .HasMaxLength(50)
                .HasColumnName("userIntro");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .HasColumnName("userName");
            entity.Property(e => e.UserPermissions).HasColumnName("userPermissions");
            entity.Property(e => e.UserPhoto).HasColumnName("userPhoto");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
