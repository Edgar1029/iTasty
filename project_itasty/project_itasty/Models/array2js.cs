﻿using System;
using System.Collections.Generic;

namespace Final_project_test.Models;
    public class array2js
    {
    public int UserId { get; set; }
    public List<int> favoriteRecipe { get; set; }
    public List<int> shopRecipe { get; set; }
    public List<int> editRecipe { get; set; }
    public List<string> customRecipeName { get; set; }
    public List<List<int>> customRecipe { get; set; }
    public List<IngredientsTable> ingredientsTables { get; set; }

}
