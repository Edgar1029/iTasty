CREATE TABLE userInfo
(
	userId		int primary key identity(1,1),
	userName	nvarchar(20) not null,
	userPhoto	varbinary(MAX)
)

CREATE TABLE userFollower
(
	userId			int not null,
	followerId		int not null,
	followDate		DATETIME not null,
	unfollowDate	DATETIME,
	primary key (userID, followerID),
	FOREIGN KEY (userID) REFERENCES userInfo(userId),
	FOREIGN KEY (followerID) REFERENCES userInfo(userId) 
)

CREATE TABLE recipe(
    recipeId INT primary key,
    userId INT,
    recipeName NVARCHAR(255),
    ingredientsTableID INT,
    stepNImage varbinary(MAX),
    stepNText NVARCHAR(MAX),
    views INT,
    favorites INT,
    parentRecipeID INT,
    ingredientsTable1ID INT,
    commentsBoardID INT,
    recipeCoverImage varbinary(MAX),
    createdDate DATETIME,
    lastModifiedDate DATETIME,
    recipeIntroduction NVARCHAR(MAX),
    recipeStatus NVARCHAR(50),
    publicPrivate NVARCHAR(10),
	FOREIGN KEY (userId) REFERENCES userInfo(userId),
	FOREIGN KEY (parentRecipeID) REFERENCES recipe(recipeId)
);

CREATE TABLE customRecipeFolder (
    userId INT,
    customFolderId INT IDENTITY,
    customFolderName NVARCHAR(50) NOT NULL,
    recipeId INT NOT NULL,
	primary key(userId,customFolderId),
	FOREIGN KEY (userId) REFERENCES userInfo(userId),
	FOREIGN KEY (recipeId) REFERENCES recipe(recipeId)
);

CREATE TABLE editedRecipe(
    userId INT,
    editedRecipeId INT IDENTITY,
    recipeId INT NOT NULL,
	primary key(userId,editedRecipeId),
	FOREIGN KEY (userId) REFERENCES userInfo(userId),
	FOREIGN KEY (recipeId) REFERENCES recipe(recipeId)
);

CREATE TABLE favoritesRecipe (
    userId INT,
    favoriteRecipeId INT IDENTITY,
    recipeId INT NOT NULL,
	primary key(userId,favoriteRecipeId),
	FOREIGN KEY (userId) REFERENCES userInfo(userId),
	FOREIGN KEY (recipeId) REFERENCES recipe(recipeId)
);

CREATE TABLE shoppingReceipe (
    userId INT,
    shoppingReceipeId INT IDENTITY,
    recipeId INT NOT NULL,
	primary key(userId,shoppingReceipeId),
	FOREIGN KEY (userId) REFERENCES userInfo(userId),
	FOREIGN KEY (recipeId) REFERENCES recipe(recipeId)
);