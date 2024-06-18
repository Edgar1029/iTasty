using Microsoft.AspNetCore.Mvc;
using project_itasty.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace project_itasty.Controllers.Api
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
                                   orderby recipe.RecipeId
                                   select recipe.RecipeId).ToList();
            var shoppingRecipes = (from recipe in _context.RecipeTables
                                   join shop in _context.ShoppingRecipes on recipe.RecipeId equals shop.RecipeId
                                   where shop.UserId == userId
                                   orderby recipe.RecipeId
                                   select recipe.RecipeId).ToList();
            var editedRecipes = (from recipe in _context.RecipeTables
                                 join edit in _context.EditedRecipes on recipe.RecipeId equals edit.RecipeId
                                 where edit.UserId == userId
                                 orderby recipe.RecipeId
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
            foreach (var recipeId in shoppingRecipes)
            {
                var shopRecipeIds = (from Ingredient in _context.IngredientsTables
                                     where Ingredient.RecipeId == recipeId
                                     select Ingredient).ToList();
                userFolder.ingredientsTables.AddRange(shopRecipeIds);
            }
            return Ok(userFolder);
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        public class IngredientWithCheckboxDto
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public int RecipeId { get; set; }
            public string TitleName { get; set; }
            public int? TitleId { get; set; }
            public string IngredientsID { get; set; }
            public string IngredientsName { get; set; }
            public float? IngredientsNumber { get; set; }
            public string IngredientsUnit { get; set; }
            public bool? Checkbox { get; set; }
        }

        [HttpGet("recipe/{userId}/{recipeId}")]
        public IActionResult GetRecipeDetails(int userId, int recipeId)
        {
            var favoriteRecipeId = (from a in _context.FavoritesRecipes
                                    where a.RecipeId == recipeId && a.UserId == userId
                                    select a.FavoriteRecipeId).FirstOrDefault();
            if (favoriteRecipeId == null)
            {
                var fav1 = new FavoritesRecipe
                {
                    UserId = userId,
                    RecipeId = recipeId,
                };
                _context.FavoritesRecipes.Add(fav1);
                _context.SaveChanges();
            }
            var id = (from a in _context.IngredientsTables
                      where a.RecipeId == recipeId && a.TitleName == null
                      select a.Id).ToList();
            FavoritesCheck favoritecheck = new FavoritesCheck();
            foreach (var item in id)
            {
                favoritecheck = (from check in _context.FavoritesChecks
                                 where check.FavoriteRecipeId == favoriteRecipeId && check.Id == item
                                 select check).FirstOrDefault();
                if (favoritecheck == null)
                {
                    var fav = new FavoritesCheck
                    {
                        FavoriteRecipeId = favoriteRecipeId,
                        Id = item,
                        Checkbox = false,
                    };
                    _context.FavoritesChecks.Add(fav);
                    _context.SaveChanges();
                }
            }
            var recipe = _context.RecipeTables.FirstOrDefault(r => r.RecipeId == recipeId);

            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe);
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet("ingredient/{userId}/{recipeId}")]
        public IActionResult GetRecipeingrdient(int userId, int recipeId)
        {
            var favoriteRecipeId = (from a in _context.FavoritesRecipes
                                    where a.RecipeId == recipeId && a.UserId == userId
                                    select a.FavoriteRecipeId).FirstOrDefault();
            var id = (from a in _context.IngredientsTables
                      where a.RecipeId == recipeId && a.TitleName == null
                      select a.Id).ToList();
            //FavoritesCheck favoritecheck = new FavoritesCheck();
            //foreach (var item in id)
            //{
            //    favoritecheck = (from check in _context.FavoritesChecks
            //                     where check.FavoriteRecipeId == favoriteRecipeId && check.Id == item
            //                     select check).FirstOrDefault();

            //    if (favoritecheck == null)
            //    {
            //        var fav = new FavoritesCheck
            //        {
            //            FavoriteRecipeId = favoriteRecipeId,
            //            Id = item,
            //            Checkbox = false,
            //        };
            //        _context.FavoritesChecks.Add(fav);
            //        _context.SaveChanges();
            //    }
            //}
            var allIngredients = (from ingredient in _context.IngredientsTables
                                  join check in _context.FavoritesChecks
                                  on ingredient.Id equals check.Id
                                  where check.FavoriteRecipeId == favoriteRecipeId
                                  select new IngredientWithCheckboxDto
                                  {
                                      Id = ingredient.Id,
                                      UserId = ingredient.UserId,
                                      RecipeId = ingredient.RecipeId,
                                      TitleName = ingredient.TitleName,
                                      TitleId = ingredient.TitleId,
                                      IngredientsID = ingredient.IngredientsId,
                                      IngredientsName = ingredient.IngredientsName,
                                      IngredientsNumber = ingredient.IngredientsNumber,
                                      IngredientsUnit = ingredient.IngredientsUnit,
                                      Checkbox = check.Checkbox
                                  }).ToList();

            return Ok(allIngredients);
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        [HttpGet("recipeView/${recipeId}")]
        public IActionResult GetRecipeView(int recipeId)
        {
            var recipeView = (from Ingredient in _context.IngredientsTables
                              where Ingredient.RecipeId == recipeId
                              select Ingredient).ToList();

            if (recipeView == null)
            {
                return NotFound();
            }

            return Ok(recipeView);
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
            if (customRecipeFolderDto.CustomFolderName != " 採買清單")
            {
                var customFolder = new CustomRecipeFolder
                {
                    UserId = customRecipeFolderDto.UserId,
                    RecipeId = customRecipeFolderDto.RecipeId,
                    CustomFolderName = customRecipeFolderDto.CustomFolderName,
                };
                _context.CustomRecipeFolders.Add(customFolder);
                _context.SaveChanges();
            }
            else
            {
                var checktotfalse = (from a in _context.ShoppingRecipes
                                     where a.UserId == customRecipeFolderDto.UserId && a.RecipeId == customRecipeFolderDto.RecipeId
                                     select a).ToList();
                foreach (var item in checktotfalse)
                {
                    item.Checkbox = false;
                    _context.ShoppingRecipes.Update(item);
                    _context.SaveChanges();
                }
            }
            return Ok(customRecipeFolderDto);
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
            if (recipeDto.CustomFolderName == " 收藏的食譜")
            {
                var favoriteRecipeId = (from a in _context.FavoritesRecipes
                                        where a.RecipeId == recipeDto.RecipeId && a.UserId == recipeDto.UserId
                                        select a.FavoriteRecipeId).FirstOrDefault();
                var id = (from a in _context.IngredientsTables
                          where a.RecipeId == recipeDto.RecipeId
                          select a.Id).ToList();
                FavoritesCheck favoritecheck = new FavoritesCheck();
                foreach (var item in id)
                {
                    favoritecheck = (from check in _context.FavoritesChecks
                                     where check.FavoriteRecipeId == favoriteRecipeId && check.Id == item
                                     select check).FirstOrDefault();
                    _context.FavoritesChecks.Remove(favoritecheck);
                    _context.SaveChanges();
                }

                //var favoriteRecipe = _context.FavoritesRecipes.FirstOrDefault(fr => fr.UserId == recipeDto.UserId && fr.RecipeId == recipeDto.RecipeId);
                //if (favoriteRecipe != null)
                //{
                //    _context.FavoritesRecipes.Remove(favoriteRecipe);
                //    _context.SaveChanges();
                //}
                return Ok(recipeDto);
            }
            else if (recipeDto.CustomFolderName == " 採買清單")
            {
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
            var Id = (from a in _context.IngredientsTables
                      where a.IngredientsName == ingredientDto.IngredientsName && a.RecipeId == ingredientDto.RecipeId
                      select a.Id).FirstOrDefault();
            var favoriteRecipeId = (from a in _context.FavoritesRecipes
                                    where a.UserId == ingredientDto.UserId && a.RecipeId == ingredientDto.RecipeId
                                    select a.FavoriteRecipeId).FirstOrDefault();
            var customRecipe = _context.FavoritesChecks
                .FirstOrDefault(fr => fr.Id == Id && fr.FavoriteRecipeId == favoriteRecipeId);
            customRecipe.Checkbox = isChecked;

            _context.FavoritesChecks.Update(customRecipe);
            _context.SaveChanges();
            return Ok(customRecipe);
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        public class apiShopIngredientCheckbox
        {
            public int UserId { get; set; }
            public int RecipeId { get; set; }
            public string FolderName { get; set; }
        }
        [HttpPost("addToShoppingList")]
        public IActionResult AddRecipeToShoppingList([FromBody] apiShopIngredientCheckbox ingredientDto)
        {
            if (ingredientDto == null || ingredientDto.UserId <= 0 || ingredientDto.RecipeId <= 0)
            {
                return BadRequest("Invalid user ID or recipe ID.");
            }
            var ingredients = (from a in _context.IngredientsTables
                               where a.RecipeId == ingredientDto.RecipeId && a.TitleName == null
                               select a).ToList();
            var recipe = (from a in _context.RecipeTables
                          where a.RecipeId == ingredientDto.RecipeId
                          select a.RecipeName).First();
            var recipeimg = (from a in _context.RecipeTables
                             where a.RecipeId == ingredientDto.RecipeId
                             select a.RecipeCoverImage).First();
            var nnaammee = ingredientDto.FolderName.TrimStart();
            var checkifsame = (from a in _context.ShoppingRecipes
                               where a.UserId == ingredientDto.UserId && a.RecipeId == ingredientDto.RecipeId && a.FolderName == nnaammee
                               select a).FirstOrDefault();
            if (checkifsame == null)
            {
                foreach (var ingredient in ingredients)
                {
                    var favoriteRecipeId = (from a in _context.FavoritesRecipes
                                            where a.UserId == ingredientDto.UserId && a.RecipeId == ingredientDto.RecipeId
                                            select a.FavoriteRecipeId).FirstOrDefault();
                    var ingredientcheck = (from a in _context.FavoritesChecks
                                           where a.FavoriteRecipeId == favoriteRecipeId && a.Id == ingredient.Id
                                           select a.Checkbox).FirstOrDefault();
                    var shoppingIngredient = new ShoppingRecipe
                    {
                        UserId = ingredientDto.UserId,
                        RecipeId = ingredientDto.RecipeId,
                        RecipeName = recipe,
                        RecipeCoverImage = recipeimg,
                        FolderName = nnaammee,
                        ShoppingIngredientsName = ingredient.IngredientsName,
                        ShoppingIngredientsNumber = ingredient.IngredientsNumber,
                        ShoppingIngredientsUnit = ingredient.IngredientsUnit,
                        Checkbox = ingredientcheck,
                        IngredientTime = DateTime.Now
                    };
                    _context.ShoppingRecipes.Add(shoppingIngredient);
                    _context.SaveChanges();
                }
            }
            else
            {
                foreach (var ingredient in ingredients)
                {
                    var needtoadd = (from a in _context.ShoppingRecipes
                                     where a.UserId == ingredientDto.UserId && a.RecipeId == ingredientDto.RecipeId && a.FolderName == nnaammee && a.ShoppingIngredientsName == ingredient.IngredientsName
                                     select a).ToList();
                    foreach (var ingredientmore in needtoadd)
                    {
                        ingredientmore.ShoppingIngredientsNumber = ingredientmore.ShoppingIngredientsNumber + ingredient.IngredientsNumber;
                        _context.ShoppingRecipes.Update(ingredientmore);
                        _context.SaveChanges();
                    }
                }
            }
            var sameingredientdifrecipe = (from a in _context.IngredientsTables
                                           where a.RecipeId == ingredientDto.RecipeId && a.TitleName == null
                                           select a.IngredientsName).ToList();
            foreach (var item in sameingredientdifrecipe)
            {
                var inshopplistdifrecipe = (from a in _context.ShoppingRecipes
                                            where a.ShoppingIngredientsName == item && a.FolderName == nnaammee
                                            select a).ToList();
                foreach(var item2 in inshopplistdifrecipe)
                {
                    item2.Checkbox = false;
                    _context.ShoppingRecipes.Update(item2);
                    _context.SaveChanges();
                }
            }
            



            //if (checkifsame == null)
            //{
            //    foreach (var ingredient in ingredients)
            //    {
            //        var sameingredientdifrecipe = (from a in _context.ShoppingRecipes
            //                                       where a.UserId == ingredientDto.UserId && a.ShoppingIngredientsName == ingredient.IngredientsName && a.FolderName == nnaammee
            //                                       select a).FirstOrDefault();
            //        if (sameingredientdifrecipe != null)
            //        {
            //            var sameingredientdifrecipelist = (from a in _context.ShoppingRecipes
            //                                               where a.UserId == ingredientDto.UserId && a.ShoppingIngredientsName == ingredient.IngredientsName && a.FolderName == nnaammee
            //                                               select a).ToList();
            //            foreach (var aaa in sameingredientdifrecipelist)
            //            {
            //                aaa.Checkbox = false;
            //                _context.ShoppingRecipes.Update(aaa);
            //                _context.SaveChanges();
            //            }
            //        }
            //        else
            //        {
            //            var favoriteRecipeId = (from a in _context.FavoritesRecipes
            //                                    where a.UserId == ingredientDto.UserId && a.RecipeId == ingredientDto.RecipeId
            //                                    select a.FavoriteRecipeId).FirstOrDefault();
            //            var ingredientcheck = (from a in _context.FavoritesChecks
            //                                   where a.FavoriteRecipeId == favoriteRecipeId && a.Id == ingredient.Id
            //                                   select a.Checkbox).FirstOrDefault();
            //            var shoppingIngredient = new ShoppingRecipe
            //            {
            //                UserId = ingredientDto.UserId,
            //                RecipeId = ingredientDto.RecipeId,
            //                RecipeName = recipe,
            //                RecipeCoverImage = recipeimg,
            //                FolderName = nnaammee,
            //                ShoppingIngredientsName = ingredient.IngredientsName,
            //                ShoppingIngredientsNumber = ingredient.IngredientsNumber,
            //                ShoppingIngredientsUnit = ingredient.IngredientsUnit,
            //                Checkbox = ingredientcheck,
            //                IngredientTime = DateTime.Now
            //            };
            //            _context.ShoppingRecipes.Add(shoppingIngredient);
            //            _context.SaveChanges();
            //        }
            //    }
            //}
            //else
            //{
            //    foreach (var ingredient in ingredients)
            //    {
            //        var needtoadd = (from a in _context.ShoppingRecipes
            //                         where a.UserId == ingredientDto.UserId && a.RecipeId == ingredientDto.RecipeId && a.FolderName == nnaammee && a.ShoppingIngredientsName == ingredient.IngredientsName
            //                         select a).ToList();
            //        foreach (var ingredientmore in needtoadd)
            //        {
            //            ingredientmore.ShoppingIngredientsNumber = ingredientmore.ShoppingIngredientsNumber + ingredient.IngredientsNumber;
            //            _context.ShoppingRecipes.Update(ingredientmore);
            //            _context.SaveChanges();
            //        }
            //    }
            //}
            return Ok(checkifsame);
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        [HttpGet("ShowShoppingList/{userId}")]
        public IActionResult ShowRecipeToShoppingList(int userId)
        {
            var shopRecipeIds = (from Ingredient in _context.ShoppingRecipes
                                 where Ingredient.UserId == userId
                                 select Ingredient).ToList();

            if (shopRecipeIds == null)
            {
                return NotFound();
            }

            return Ok(shopRecipeIds);
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        [HttpPost("changeshopcheckbox/{userId}/{categoryId}/{ingredient}/{check}")]
        public IActionResult ChangeShopCheckbox(int userId, string categoryId, string ingredient, bool check)
        {
            ingredient = ingredient.TrimStart();
            var shoplist = (from a in _context.ShoppingRecipes
                            where a.UserId == userId && a.FolderName == categoryId && a.ShoppingIngredientsName == ingredient
                            select a).ToList();
            foreach (var shop in shoplist)
            {
                shop.IngredientTime = DateTime.Now;
                shop.Checkbox = check;
                _context.ShoppingRecipes.Update(shop);
                _context.SaveChanges();
            }
            return Ok(shoplist);
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        [HttpGet("getShopFolderRecipe/{userId}/{folderName}")]
        public IActionResult GetShopFolderRecipe(int userId, string folderName)
        {
            var shopRecipeIds = (from Ingredient in _context.ShoppingRecipes
                                 where Ingredient.UserId == userId && Ingredient.FolderName == folderName
                                 select Ingredient.RecipeId).Distinct().ToList();

            if (shopRecipeIds == null)
            {
                return NotFound();
            }

            return Ok(shopRecipeIds);
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        [HttpPost("deleteshopfolder/{userId}/{folderName}")]
        public IActionResult Deleteshopfolder(int userId, string folderName)
        {
            var shoplist = (from a in _context.ShoppingRecipes
                            where a.UserId == userId && a.FolderName == folderName
                            select a).ToList();
            foreach (var shop in shoplist)
            {
                _context.ShoppingRecipes.Remove(shop);
                _context.SaveChanges();
            }
            return Ok(shoplist);
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        [HttpPost("deleteshoprecipefolder/{userId}/{folderName}/{recipeId}")]
        public IActionResult DeleteShopRecipeFolder(int userId, string folderName, int recipeId)
        {
            var shoplist = (from a in _context.ShoppingRecipes
                            where a.UserId == userId && a.FolderName == folderName && a.RecipeId == recipeId
                            select a).ToList();
            foreach (var shop in shoplist)
            {
                _context.ShoppingRecipes.Remove(shop);
                _context.SaveChanges();
            }
            return Ok(shoplist);
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        [HttpGet("shopingredient/{userId}/{folderName}/{recipeId}")]
        public IActionResult ShopIngredient(int userId, string folderName, int recipeId)
        {
            var shopRecipeIds = (from Ingredient in _context.ShoppingRecipes
                                 where Ingredient.UserId == userId && Ingredient.FolderName == folderName && Ingredient.RecipeId == recipeId
                                 select Ingredient).ToList();

            if (shopRecipeIds == null)
            {
                return NotFound();
            }

            return Ok(shopRecipeIds);
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------


        [HttpGet("filtered-recipes/{userId}/{category}")]
        public IActionResult GetFilteredRecipes(int userId, string category)
        {
            var data = (from a in _context.ShoppingRecipes
                        where a.UserId == userId && a.FolderName == category
                        select a).ToList();
            var result = data
            .GroupBy(r => r.RecipeId)
            .Where(g => g.All(r => r.Checkbox == true))
            .Select(g => g.Key);

            return Ok(result);
        }
    }
}