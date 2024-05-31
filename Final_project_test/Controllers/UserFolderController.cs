using Microsoft.AspNetCore.Mvc;
using Final_project_test.Models;
using System.Linq;

namespace Final_project_test.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFolderController : ControllerBase
    {
        private readonly ITastyDbContext _context;

        public UserFolderController(ITastyDbContext context)
        {
            _context = context;
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        [HttpGet("{userId}")]
        public IActionResult GetUserFolder(int userId)
        {   
            var favoriteRecipes = (from recipe in _context.RecipeTables
                                   join fav in _context.FavoritesRecipes on recipe.RecipeId equals fav.RecipeId
                                   where fav.UserId == userId
                                   select recipe.RecipeId).ToList();
            var shoppingRecipes = (from recipe in _context.RecipeTables
                                   join shop in _context.ShoppingReceipes on recipe.RecipeId equals shop.RecipeId
                                   where shop.UserId == userId
                                   select recipe.RecipeId).ToList();
            var editedRecipes = (from recipe in _context.RecipeTables
                                 join edit in _context.EditedRecipes on recipe.RecipeId equals edit.RecipeId
                                 where edit.UserId == userId
                                 select recipe.RecipeId).ToList();
            var customFolderNames = (from r in _context.CustomRecipeFolders
                                     where r.UserId == userId
                                     select r.CustomFolderName).Distinct().ToList();
            var userFolder = new array2js()
            {
                UserId = userId,
                favoriteRecipe = favoriteRecipes,
                shopRecipe = shoppingRecipes,
                editRecipe = editedRecipes,
                customRecipeName = customFolderNames,
                customRecipe = new List<List<int>>(),
                ingredientsTables = new List<IngredientsTable>(),
            };
            foreach (var folderName in customFolderNames)
            {
                var folderRecipes = (from folder in _context.CustomRecipeFolders
                                     join recipe in _context.RecipeTables on folder.RecipeId equals recipe.RecipeId
                                     where folder.CustomFolderName == folderName && folder.UserId == userId && recipe.RecipeId != null
                                     select recipe.RecipeId).ToList();
                userFolder.customRecipe.Add(folderRecipes);
            }
            foreach (var recipeId in shoppingRecipes) {
                var shopRecipeIds = (from Ingredient in _context.IngredientsTables
                                     where Ingredient.RecipeId == recipeId
                                     select Ingredient).ToList();
                userFolder.ingredientsTables.AddRange(shopRecipeIds);
            }
            return Ok(userFolder);
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        [HttpGet("recipe/{recipeId}")]
        public IActionResult GetRecipeDetails(int recipeId)
        {
            var recipe = _context.RecipeTables.FirstOrDefault(r => r.RecipeId == recipeId);

            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe);
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        [HttpGet("ingredient/{userId}/{recipeId}")]
        public IActionResult GetRecipeingrdient(int userId,int recipeId)
        {
            var shopRecipeIds = (from Ingredient in _context.IngredientsTables
                                 where Ingredient.RecipeId == recipeId && Ingredient.UserId == userId
                                 select Ingredient).ToList();

            if (shopRecipeIds == null)
            {
                return NotFound();
            }

            return Ok(shopRecipeIds);
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        public class apiCustomFolder
        {
            public int UserId { get; set; }
            public string CustomFolderName { get; set; }
            public int RecipeId { get; set; }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        [HttpPost("addrecipecustom")]
        public IActionResult AddRecipeToCustomFolder(apiCustomFolder customRecipeFolderDto)
        {
            if (customRecipeFolderDto == null || customRecipeFolderDto.UserId <= 0 || customRecipeFolderDto.RecipeId <= 0)
            {
                return BadRequest("Invalid user ID or recipe ID.");
            }
            if (customRecipeFolderDto.CustomFolderName == " 採買清單")
            {
                var shopFolder = new ShoppingReceipe
                {
                    UserId = customRecipeFolderDto.UserId,
                    RecipeId = customRecipeFolderDto.RecipeId,
                };
                _context.ShoppingReceipes.Add(shopFolder);
                _context.SaveChanges();
                return Ok(customRecipeFolderDto);
            }
            else
            {
                var customFolder = new CustomRecipeFolder
                {
                    UserId = customRecipeFolderDto.UserId,
                    RecipeId = customRecipeFolderDto.RecipeId,
                    CustomFolderName = customRecipeFolderDto.CustomFolderName,
                };
                _context.CustomRecipeFolders.Add(customFolder);
                _context.SaveChanges();
                return Ok(customRecipeFolderDto);
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        [HttpPost("addfoldercustom")]
        public IActionResult AddCustomFolder(apiCustomFolder customFolderDto)
        {
            if (customFolderDto == null || customFolderDto.UserId <= 0)
            {
                return BadRequest("Invalid user ID.");
            }

            var customFolder = new CustomRecipeFolder
            {
                UserId = customFolderDto.UserId,
                CustomFolderName = customFolderDto.CustomFolderName,
            };

            _context.CustomRecipeFolders.Add(customFolder);
            _context.SaveChanges();

            return Ok(customFolderDto);
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        [HttpPost("deletefoldercustom")]
        public IActionResult DeleteCustomFolder(apiCustomFolder customFolderDto)
        {
            if (customFolderDto == null || customFolderDto.UserId <= 0)
            {
                return BadRequest("Invalid user ID.");
            }
            var customFolders = _context.CustomRecipeFolders
                .Where(cf => cf.UserId == customFolderDto.UserId && cf.CustomFolderName == customFolderDto.CustomFolderName)
                .ToList();
            if (!customFolders.Any())
            {
                return NotFound("Custom folder(s) not found.");
            }
            _context.CustomRecipeFolders.RemoveRange(customFolders);
            _context.SaveChanges();
            return Ok(customFolderDto);
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        [HttpPost("deleterecipe")]
        public IActionResult DeleteRecipe(apiCustomFolder recipeDto)
        {
            if (recipeDto.CustomFolderName == null || recipeDto.UserId <= 0 || recipeDto.RecipeId <= 0)
            {
                return BadRequest("Invalid user ID or recipe ID.");
            }
            if(recipeDto.CustomFolderName == " 收藏的食譜")
            {
                var favoriteRecipe = _context.FavoritesRecipes.FirstOrDefault(fr => fr.UserId == recipeDto.UserId && fr.RecipeId == recipeDto.RecipeId);
                if (favoriteRecipe != null)
                {
                    _context.FavoritesRecipes.Remove(favoriteRecipe);
                    _context.SaveChanges();
                }
                return Ok(recipeDto);
            }
            else if (recipeDto.CustomFolderName == " 採買清單")
            {
                var ShopRecipe = _context.ShoppingReceipes.FirstOrDefault(fr => fr.UserId == recipeDto.UserId && fr.RecipeId == recipeDto.RecipeId);
                if (ShopRecipe != null)
                {
                    _context.ShoppingReceipes.Remove(ShopRecipe);
                    _context.SaveChanges();
                }
                return Ok(recipeDto);
            }
            else if (recipeDto.CustomFolderName == " 編輯後的食譜")
            {
                var EditRecipe = _context.EditedRecipes.FirstOrDefault(fr => fr.UserId == recipeDto.UserId && fr.RecipeId == recipeDto.RecipeId);
                if (EditRecipe != null)
                {
                    _context.EditedRecipes.Remove(EditRecipe);
                    _context.SaveChanges();
                }
                return Ok(recipeDto);
            }
            else
            {
                var customRecipe = _context.CustomRecipeFolders.FirstOrDefault(fr => fr.UserId == recipeDto.UserId && fr.RecipeId == recipeDto.RecipeId && fr.CustomFolderName == recipeDto.CustomFolderName);
                if (customRecipe != null)
                {
                    _context.CustomRecipeFolders.Remove(customRecipe);
                    _context.SaveChanges();
                }
                return Ok(recipeDto);
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        public class apiIngredientCheckbox
        {
            public int UserId { get; set; }
            public int RecipeId { get; set; }
            public string IngredientsName { get; set; }
            public string Checkbox { get; set; }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        [HttpPost("changecheckbox")]
        public IActionResult AddRecipeToCustomFolder([FromBody] apiIngredientCheckbox ingredientDto)
        {
            if (ingredientDto == null || ingredientDto.UserId <= 0 || ingredientDto.RecipeId <= 0)
            {
                return BadRequest("Invalid user ID or recipe ID.");
            }
            bool isChecked = ingredientDto.Checkbox == "true";
            var customRecipe = _context.IngredientsTables
                .FirstOrDefault(fr => fr.UserId == ingredientDto.UserId && fr.RecipeId == ingredientDto.RecipeId && fr.IngredientsName == ingredientDto.IngredientsName);
            customRecipe.Checkbox = isChecked;

            _context.IngredientsTables.Update(customRecipe);
            _context.SaveChanges();
            return Ok(isChecked);
        }

    }
}