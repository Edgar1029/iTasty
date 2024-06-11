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
    favoriteRecipeId INT IDENTITY primary key,
    recipeId INT NOT NULL,
	FOREIGN KEY (userId) REFERENCES userInfo(userId),
	FOREIGN KEY (recipeId) REFERENCES recipeTable(recipeId)
);
--------------------------------------------------------------------------
CREATE TABLE shoppingRecipe (
    userId INT not null,
    shoppingReceipeId INT IDENTITY,
    recipeId INT NOT NULL,
	recipeName NVARCHAR(255) not null,
	recipeCoverImage varbinary(MAX),
	folderName NVARCHAR(50) NOT NULL,
	[shoppingIngredientsName] [nvarchar](20) NULL,
	[shoppingIngredientsNumber] [real] NULL,
	[shoppingIngredientsUnit] [nvarchar](50) NULL,
	[checkbox] [bit] NULL,
	[ingredientTime] [smalldatetime] NOT NULL,
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
	FOREIGN KEY (userId) REFERENCES userInfo(userId),
	FOREIGN KEY (recipeId) REFERENCES recipeTable(recipeId),
	FOREIGN KEY ([ingredientsID]) REFERENCES [IngredientDetail]([ingredientId])
)	
----------------------------------------------------------------------------
CREATE TABLE favoritesCheck(
	favoriteRecipeId INT ,
	[id] int NOT NULL,
	[checkbox] [bit] NULL,
	primary key(id,favoriteRecipeId),
	FOREIGN KEY ([id]) REFERENCES [ingredientsTable](id),
	FOREIGN KEY (favoriteRecipeId) REFERENCES favoritesRecipe(favoriteRecipeId)
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
	[commonName] [nvarchar](max) NULL,
	ingredientsImg varbinary(MAX),	
	IsActive bit
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

------------------------------------------------------------------------
INSERT INTO favoritesRecipe (userId, recipeId)
VALUES
(3, 3),
(3, 4),
(3, 5),
(3, 6),
(3, 9),
(3, 10),
(3, 7),
(3, 8);
------------------------------------------------------------------------

------------------------------------------------------------------------
INSERT INTO helpForm (userId, questionType, questionContent, questionImage)
VALUES
(1, N'技術問題', N'無法登入', NULL),
(2, N'帳號支援', N'重置密碼', NULL),
(3, N'帳單問題', N'收費錯誤', NULL),
(4, N'功能請求', N'新增黑暗模式', NULL),
(5, N'錯誤報告', N'應用程式啟動時崩潰', NULL),
(6, N'一般查詢', N'如何使用功能X？', NULL),
(7, N'回饋', N'很棒的應用，但需要Y', NULL),
(8, N'投訴', N'客服服務緩慢', NULL),
(9, N'建議', N'改進UI設計', NULL),
(10, N'其他', N'我可以更改我的用戶名嗎？', NULL);

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
INSERT INTO IngredientDetail (ingredientId, ingredientName, commonName, kcalg)
VALUES
('ING101', N'番茄', N'常見番茄', 18),
('ING102', N'洋蔥', N'常見洋蔥', 40),
('ING103', N'麵粉', N'小麥麵粉', 364),
('ING104', N'乳酪', N'切達乳酪', 402),
('ING105', N'萵苣', N'冰山萵苣', 14),
('ING106', N'胡蘿蔔', N'常見胡蘿蔔', 41),
('ING107', N'雞肉', N'雞胸肉', 165),
('ING108', N'高湯', N'雞肉高湯', 15),
('ING109', N'糖', N'白砂糖', 387),
('ING110', N'雞蛋', N'雞蛋', 155),
('ING111', N'牛奶', N'全脂牛奶', 42),
('ING112', N'奶油', N'無鹽奶油', 717),
('ING113', N'麵包', N'白麵包', 265),
('ING114', N'火腿', N'熟火腿', 145),
('ING115', N'香蕉', N'香蕉', 89),
('ING116', N'草莓', N'草莓', 32),
('ING117', N'牛肉', N'牛絞肉', 250),
('ING118', N'萵苣', N'長葉萵苣', 17),
('ING119', N'意大利麵', N'意大利麵', 158),
('ING120', N'番茄醬', N'罐裝番茄醬', 32);

------------------------------------------------------------------------
INSERT INTO ingredientsTable (userId, recipeId, ingredientsID, ingredientsName, ingredientsNumber, ingredientsUnit)
VALUES 
-- 插入 使用者ID 1 的 食譜ID 1 的成分數據（意大利麵）
(1, 1, 'ING119', N'意大利麵', 200, N'克'),
(1, 1, 'ING120', N'番茄醬', 150, N'克'),

-- 插入 使用者ID 2 的 食譜ID 2 的成分數據（披薩）
(2, 2, 'ING103', N'麵粉', 200, N'克'),
(2, 2, 'ING104', N'乳酪', 100, N'克'),

-- 插入 使用者ID 3 的 食譜ID 3 的成分數據（沙拉）
(3, 3, 'ING105', N'萵苣', 100, N'克'),
(3, 3, 'ING102', N'洋蔥', 50, N'克'),

-- 插入 使用者ID 4 的 食譜ID 4 的成分數據（湯）
(4, 4, 'ING107', N'雞肉', 200, N'克'),
(4, 4, 'ING106', N'胡蘿蔔', 100, N'克'),

-- 插入 使用者ID 5 的 食譜ID 5 的成分數據（蛋糕）
(5, 5, 'ING103', N'麵粉', 300, N'克'),
(5, 5, 'ING109', N'糖', 150, N'克'),

-- 插入 使用者ID 6 的 食譜ID 6 的成分數據（三明治）
(6, 6, 'ING113', N'麵包', 2, N'片'),
(6, 6, 'ING114', N'火腿', 100, N'克'),

-- 插入 使用者ID 7 的 食譜ID 7 的成分數據（煎餅）
(7, 7, 'ING103', N'麵粉', 200, N'克'),
(7, 7, 'ING111', N'牛奶', 200, N'毫升'),

-- 插入 使用者ID 8 的 食譜ID 8 的成分數據（奶昔）
(8, 8, 'ING111', N'牛奶', 200, N'毫升'),
(8, 8, 'ING115', N'香蕉', 1, N'根'),

-- 插入 使用者ID 9 的 食譜ID 9 的成分數據（漢堡）
(9, 9, 'ING117', N'牛肉', 150, N'克'),
(9, 9, 'ING118', N'萵苣', 100, N'克'),

-- 插入 使用者ID 10 的 食譜ID 10 的成分數據（陽春麵）
(10, 10, 'ING119', N'意大利麵', 200, N'克'),
(10, 10, 'ING107', N'雞肉', 100, N'克');

------------------------------------------------------------------------
Install-Package Microsoft.EntityFrameworkCore.Tools
install-package Microsoft.EntityFrameworkCore.SqlServer
Scaffold-DbContext "Data Source=DESKTOP-AVM54SB;Initial Catalog=iTastyDB;Persist Security Info=False;User ID=sa;PassWord=111111;Encrypt=False;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force
Scaffold-DbContext "Data Source=DESKTOP-PIAH2TG;Initial Catalog=iTastyDB;Persist Security Info=False;User ID=sa;PassWord=111111;Encrypt=False;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force
TRUNCATE TABLE shoppingRecipe;
