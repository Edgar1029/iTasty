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

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<SeasonalIngredient> SeasonalIngredients { get; set; }

    public virtual DbSet<ShoppingReceipe> ShoppingReceipes { get; set; }

    public virtual DbSet<UserFollower> UserFollowers { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.;Database=iTastyDB;Integrated Security=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomRecipeFolder>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.CustomFolderId }).HasName("PK__customRe__2001186C6A6C12EC");

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
            entity.HasKey(e => new { e.UserId, e.EditedRecipeId }).HasName("PK__editedRe__45BD02F77A68B3E3");

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
            entity.HasKey(e => new { e.UserId, e.FavoriteRecipeId }).HasName("PK__favorite__957EF6416F72CCA3");

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
            entity.HasKey(e => e.FormId).HasName("PK__helpForm__51BCB72B290CD113");

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

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("PK__recipe__C114EE8365EADB17");

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

        modelBuilder.Entity<SeasonalIngredient>(entity =>
        {
            entity.HasKey(e => e.IngredientsId).HasName("PK__seasonal__A9B070FB258841EF");

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
            entity.HasKey(e => new { e.UserId, e.ShoppingReceipeId }).HasName("PK__shopping__3C59E2382B45DFCD");

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

        modelBuilder.Entity<UserFollower>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.FollowerId }).HasName("PK__userFoll__CE0DEA7494712316");

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
            entity.HasKey(e => e.UserId).HasName("PK__userInfo__CB9A1CFF1D71FAEF");

            entity.ToTable("userInfo");

            entity.HasIndex(e => e.Email, "UQ__userInfo__AB6E6164DBD31543").IsUnique();

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
