CREATE TABLE userInfo
(
	userId		int primary key identity(1,1) not null,
	userName	nvarchar(20) not null,
	userEmail	nvarchar(30) UNIQUE,  
	userPassword nvarchar(MAX) not null,
	userPhoto	varbinary(MAX),
	userBanner	varbinary(MAX),	
	userIntro	nvarchar(50),
	userPermissions int not null check (userPermissions BETWEEN 1 AND 3) ,--1管理員 2一般會員 3停權  狀態
	userCreateTime smalldatetime not null default GETDATE() 
)
--------------------------------------------------------------------------
CREATE TABLE userFollower
(
	userId			int not null,
	followerId		int not null,
	followDate		DATE not null,
	unfollowDate	DATE,
	primary key (userId, followerId, followDate),
	FOREIGN KEY (userId) REFERENCES userInfo(userId),
	FOREIGN KEY (followerId) REFERENCES userInfo(userId) 
)
--------------------------------------------------------------------------
CREATE TABLE recipeTable(
    recipeId INT primary key not null,
    userId INT not null,
    recipeName NVARCHAR(255) not null,
	recipeCoverImage varbinary(MAX),
	recipeIntroduction NVARCHAR(MAX),
	views INT not null,
    favorites INT not null,
	parentRecipeId INT,
	createdDate DATETIME not null,
    lastModifiedDate DATETIME,
	recipeStatus NVARCHAR(50),--這個食譜有沒有被檢舉
	publicPrivate NVARCHAR(10), --自己選擇此食譜有沒有公開
	proteinUsed NVARCHAR(50), --食材(雞、豬、牛、羊)
	mealType NVARCHAR(50), --早、午、晚餐
	cuisineStyle NVARCHAR(50), --菜式(中式、西式)
	healthyOptions NVARCHAR(50), --葷素
	cookingTime INT, --烹飪時間 (以分鐘計)
    servings INT, --份數
    calories INT, --卡路里
	FOREIGN KEY (userId) REFERENCES userInfo(userId),
	FOREIGN KEY (parentRecipeId) REFERENCES recipeTable(recipeId)
);
--------------------------------------------------------------------------
create table recipeView
(
	recipeId	int,
	viewDate	Date,
	viewNum		int,
	primary key (recipeId, viewDate),
	FOREIGN KEY (recipeId) REFERENCES recipeTable([recipeId]),
);
--------------------------------------------------------------------------
CREATE TABLE customRecipeFolder (
    userId INT not null,
    customFolderId INT IDENTITY,
    customFolderName NVARCHAR(50) NOT NULL,
    recipeId INT,
	primary key(userId,customFolderId),
	FOREIGN KEY (userId) REFERENCES userInfo(userId),
	FOREIGN KEY (recipeId) REFERENCES recipeTable(recipeId)
);
--------------------------------------------------------------------------
CREATE TABLE editedRecipe(
    userId INT not null,
    editedRecipeId INT IDENTITY,
    recipeId INT NOT NULL,
	primary key(userId,editedRecipeId),
	FOREIGN KEY (userId) REFERENCES userInfo(userId),
	FOREIGN KEY (recipeId) REFERENCES recipeTable(recipeId)
);
--------------------------------------------------------------------------
CREATE TABLE favoritesRecipe (
    userId INT not null,
    favoriteRecipeId INT IDENTITY,
    recipeId INT NOT NULL,
	primary key(userId,favoriteRecipeId),
	FOREIGN KEY (userId) REFERENCES userInfo(userId),
	FOREIGN KEY (recipeId) REFERENCES recipeTable(recipeId)
);
--------------------------------------------------------------------------
CREATE TABLE shoppingReceipe (
    userId INT not null,
    shoppingReceipeId INT IDENTITY,
    recipeId INT NOT NULL,
	primary key(userId,shoppingReceipeId),
	FOREIGN KEY (userId) REFERENCES userInfo(userId),
	FOREIGN KEY (recipeId) REFERENCES recipeTable(recipeId)
);
----------------------------------------------------------------------------
CREATE TABLE [dbo].[IngredientDetail](
	[ingredientId] [char](12) primary key NOT NULL,
	[ingredientName] [nvarchar](20) NULL,
	[commonName] [nvarchar](max) NULL,
	[kcalg] [real] NULL,
)
--------------------------------------------------------------------------
CREATE TABLE [dbo].[ingredientsTable](
	[id] [int] IDENTITY(1,1) primary key NOT NULL,
	userId int NOT NULL,
	recipeId int NOT NULL,
	[titleName] [nvarchar](20) NULL,
	[titleId] [int] NULL,
	[ingredientsID] [char](12) NULL,
	[ingredientsName] [nvarchar](20) NULL,
	[ingredientsNumber] [real] NULL,
	[ingredientsUnit] [nvarchar](50) NULL,
	[checkbox] [bit] NULL,
	FOREIGN KEY (userId) REFERENCES userInfo(userId),
	FOREIGN KEY (recipeId) REFERENCES recipeTable(recipeId),
	FOREIGN KEY ([ingredientsID]) REFERENCES [IngredientDetail]([ingredientId])
)
----------------------------------------------------------------------------
CREATE TABLE [dbo].[helpForm](
	[formId] [int] IDENTITY(1,1) primary key NOT NULL,
	[userId] [int] NOT NULL,
	[questionType] [nvarchar](200) NOT NULL,
	[questionContent] [nvarchar](50) NOT NULL,
	[questionImage] [varbinary](max) NULL,
	FOREIGN KEY (userId) REFERENCES userInfo(userId)
)
----------------------------------------------------------------------------
CREATE TABLE [dbo].[messageTable](
	[messageId] [int] IDENTITY(1,1) primary key NOT NULL,
	[recipeId] [int] NOT NULL,
	[userID] [int] NOT NULL,
	[messageContent] [nvarchar](150) NOT NULL,
	[topMessageid] [int] NULL,
	[createTime] [smalldatetime] NOT NULL,
	[changeTime] [smalldatetime] NOT NULL,
	[violationStatus] [nvarchar](20) NULL
	FOREIGN KEY (userId) REFERENCES userInfo(userId),
	FOREIGN KEY (recipeId) REFERENCES recipeTable(recipeId)
)

----------------------------------------------------------------------------
CREATE TABLE [dbo].[stepTable](
	[id] [int] IDENTITY(1,1) primary key NOT NULL,
	[recipeId] [int] NOT NULL,
	[stepText] [nvarchar](150) NOT NULL,
	[stepImg] [varbinary](max) NULL,
	FOREIGN KEY (recipeId) REFERENCES recipeTable(recipeId)
)

----------------------------------------------------------------------------
CREATE TABLE [dbo].[seasonalIngredients](
	[id] [int] IDENTITY(1,1) primary key NOT NULL,
	[monthId] [int] NOT NULL,
	[seasonalIngredientId] [char](12) NULL,
	[commonName] [nvarchar](max) NULL,
	FOREIGN KEY ([seasonalIngredientId]) REFERENCES [IngredientDetail]([ingredientId])
)

----------------------------------------------------------------------------
INSERT INTO userInfo (userName, userEmail, userPassword, userPhoto, userBanner, userIntro, userPermissions, userCreateTime)
VALUES
('Alice', 'alice@example.com', 'password1', NULL, NULL, 'Hello, I am Alice!', 2, GETDATE()),
('Bob', 'bob@example.com', 'password2', NULL, NULL, 'Hello, I am Bob!', 2, GETDATE()),
('Carol', 'carol@example.com', 'password3', NULL, NULL, 'Hello, I am Carol!', 2, GETDATE()),
('Dave', 'dave@example.com', 'password4', NULL, NULL, 'Hello, I am Dave!', 1, GETDATE()),
('Eve', 'eve@example.com', 'password5', NULL, NULL, 'Hello, I am Eve!', 2, GETDATE()),
('Frank', 'frank@example.com', 'password6', NULL, NULL, 'Hello, I am Frank!', 2, GETDATE()),
('Grace', 'grace@example.com', 'password7', NULL, NULL, 'Hello, I am Grace!', 2, GETDATE()),
('Heidi', 'heidi@example.com', 'password8', NULL, NULL, 'Hello, I am Heidi!', 3, GETDATE()),
('Ivan', 'ivan@example.com', 'password9', NULL, NULL, 'Hello, I am Ivan!', 2, GETDATE()),
('Judy', 'judy@example.com', 'password10', NULL, NULL, 'Hello, I am Judy!', 2, GETDATE());
------------------------------------------------------------------------
INSERT INTO userFollower (userId, followerId, followDate, unfollowDate)
VALUES
(1, 2, GETDATE(), NULL),
(1, 3, GETDATE(), NULL),
(2, 3, GETDATE(), NULL),
(2, 4, GETDATE(), NULL),
(3, 4, GETDATE(), NULL),
(3, 5, GETDATE(), NULL),
(4, 5, GETDATE(), NULL),
(4, 6, GETDATE(), NULL),
(5, 6, GETDATE(), NULL),
(5, 7, GETDATE(), NULL);
------------------------------------------------------------------------
INSERT INTO recipeTable (recipeId, userId, recipeName, recipeCoverImage, recipeIntroduction, views, favorites, parentRecipeId, createdDate, lastModifiedDate, recipeStatus, publicPrivate, proteinUsed, mealType, cuisineStyle, healthyOptions, cookingTime, servings, calories)
VALUES
(1, 1, N'意大利麵', NULL, N'美味的意大利麵食譜', 100, 10, NULL, GETDATE(), GETDATE(), 'active', 'public', N'牛肉', N'晚餐', N'西式菜', N'葷', 30, 4, 400),
(2, 2, N'披薩', NULL, N'美味的披薩食譜', 200, 20, NULL, GETDATE(), GETDATE(), 'active', 'public', N'雞肉', N'午餐', N'西式菜', N'葷', 45, 8, 1200),
(3, 3, N'沙拉', NULL, N'健康的沙拉食譜', 150, 15, NULL, GETDATE(), GETDATE(), 'active', 'public', N'豆腐', N'小吃', N'西式菜', N'素', 15, 2, 150),
(4, 4, N'湯', NULL, N'温暖的湯食譜', 120, 12, NULL, GETDATE(), GETDATE(), 'active', 'public', N'雞肉', N'晚餐', N'中式菜', N'素', 60, 6, 300),
(5, 5, N'蛋糕', NULL, N'美味的蛋糕食譜', 180, 18, NULL, GETDATE(), GETDATE(), 'active', 'public', N'雞蛋', N'甜點', N'西式菜', N'素', 90, 10, 2500),
(6, 6, N'三明治', NULL, N'簡單的三明治食譜', 110, 11, NULL, GETDATE(), GETDATE(), 'active', 'public', N'火雞肉', N'午餐', N'西式菜', N'葷', 10, 1, 350),
(7, 7, N'煎餅', NULL, N'鬆軟的煎餅食譜', 130, 13, NULL, GETDATE(), GETDATE(), 'active', 'public', N'牛奶', N'早餐', N'西式菜', N'葷', 20, 4, 500),
(8, 8, N'奶昔', NULL, N'健康的奶昔食譜', 140, 14, NULL, GETDATE(), GETDATE(), 'active', 'public', N'酸奶', N'小吃', N'西式菜', N'素', 5, 2, 200),
(9, 9, N'漢堡', NULL, N'多汁的漢堡食譜', 160, 16, NULL, GETDATE(), GETDATE(), 'active', 'public', N'牛肉', N'晚餐', N'西式菜', N'葷', 25, 1, 700),
(10, 10, N'陽春麵', NULL, N'美味的陽春麵食譜', 170, 17, NULL, GETDATE(), GETDATE(), 'active', 'public', N'雞肉', N'晚餐', N'西式菜', N'葷', 35, 4, 600);
------------------------------------------------------------------------
INSERT INTO customRecipeFolder (userId, customFolderName, recipeId)
VALUES
(1, 'My Favorites', 1),
(1, 'Quick Meals', 2),
(2, 'Healthy Recipes', 3),
(2, 'Family Dinners', 4),
(3, 'Desserts', 5),
(3, 'Lunch Ideas', 6),
(4, 'Breakfast Recipes', 7),
(4, 'Smoothies', 8),
(5, 'Dinner Parties', 9),
(5, 'Pasta Dishes', 10);
------------------------------------------------------------------------
INSERT INTO editedRecipe (userId, recipeId)
VALUES
(1, 1),
(1, 2),
(2, 3),
(2, 4),
(3, 5),
(3, 6),
(4, 7),
(4, 8),
(5, 9),
(5, 10);
------------------------------------------------------------------------
INSERT INTO favoritesRecipe (userId, recipeId)
VALUES
(1, 1),
(1, 2),
(2, 3),
(2, 4),
(3, 5),
(3, 6),
(4, 7),
(4, 8),
(5, 9),
(5, 10);
------------------------------------------------------------------------
INSERT INTO shoppingReceipe (userId, recipeId)
VALUES
(1, 1),
(1, 2),
(2, 3),
(2, 4),
(3, 5),
(3, 6),
(4, 7),
(4, 8),
(5, 9),
(5, 10);

------------------------------------------------------------------------
INSERT INTO helpForm (userId, questionType, questionContent, questionImage)
VALUES
(1, 'Technical Issue', 'Unable to login', NULL),
(2, 'Account Support', 'Password reset', NULL),
(3, 'Billing Issue', 'Incorrect charge', NULL),
(4, 'Feature Request', 'Add dark mode', NULL),
(5, 'Bug Report', 'App crashes on startup', NULL),
(6, 'General Inquiry', 'How to use feature X?', NULL),
(7, 'Feedback', 'Great app but needs Y', NULL),
(8, 'Complaint', 'Slow customer service', NULL),
(9, 'Suggestion', 'Improve UI design', NULL),
(10, 'Other', 'Can I change my username?', NULL);

------------------------------------------------------------------------
INSERT INTO messageTable (recipeId, userId, messageContent, topMessageId, createTime, changeTime, violationStatus)
VALUES
(1, 1, N'這道菜看起來很棒！', NULL, '2024-05-01 10:00', '2024-05-01 10:00', NULL),
(2, 2, N'我試過這個食譜，很好吃！', NULL, '2024-05-02 12:00', '2024-05-02 12:00', NULL),
(3, 3, N'有沒有人有更好的做法？', NULL, '2024-05-03 14:00', '2024-05-03 14:00', 'No Violation'),
(4, 4, N'這道菜需要更多鹽。', NULL, '2024-05-04 16:00', '2024-05-04 16:00', 'No Violation'),
(5, 5, N'這是一個垃圾食譜。', NULL, '2024-05-05 18:00', '2024-05-05 18:00', 'Violation Reported');

------------------------------------------------------------------------
INSERT INTO stepTable (recipeId, stepText, stepImg)
VALUES
(1, 'Boil water and cook spaghetti.', NULL),
(1, 'Prepare sauce and toss spaghetti.', NULL),
(2, 'Prepare dough and spread sauce.', NULL),
(2, 'Add toppings and bake.', NULL),
(3, 'Chop vegetables and mix.', NULL),
(3, 'Add dressing and serve.', NULL),
(4, 'Sauté vegetables and add broth.', NULL),
(4, 'Cook chicken and season soup.', NULL),
(5, 'Mix ingredients and bake.', NULL),
(5, 'Decorate and serve.', NULL),
(6, 'Prepare ingredients and assemble sandwich.', NULL),
(6, 'Cut and serve.', NULL),
(7, 'Mix batter and cook pancakes.', NULL),
(7, 'Serve with toppings.', NULL),
(8, 'Combine ingredients and blend.', NULL),
(8, 'Pour into glasses and serve.', NULL),
(9, 'Season and cook patties.', NULL),
(9, 'Assemble burgers and serve.', NULL),
(10, 'Boil pasta and cook garlic.', NULL),
(10, 'Toss pasta with garlic-infused oil.', NULL);

------------------------------------------------------------------------
Install-Package Microsoft.EntityFrameworkCore.Tools
install-package Microsoft.EntityFrameworkCore.SqlServer
Scaffold-DbContext "Data Source=DESKTOP-AVM54SB;Initial Catalog=iTastyDB;Persist Security Info=False;User ID=sa;PassWord=111111;Encrypt=False;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force
Scaffold-DbContext "Data Source=DESKTOP-PIAH2TG;Initial Catalog=iTastyDB;Persist Security Info=False;User ID=sa;PassWord=111111;Encrypt=False;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force
