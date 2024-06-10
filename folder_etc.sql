CREATE TABLE userInfo
(
	userId		int primary key identity(1,1) not null,
	userName	nvarchar(20) not null,
	userEmail	nvarchar(20) UNIQUE,  
	userPassword nvarchar(20) not null,
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
	followDate		DATETIME not null,
	unfollowDate	DATETIME,
	primary key (userId, followerID),
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
	recipeName NVARCHAR(255) not null,
	recipeCoverImage varbinary(MAX),
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
(1, 1, '意大利麵', NULL, '美味的意大利麵食譜', 100, 10, NULL, GETDATE(), GETDATE(), '啟用', '公開', '牛肉', '晚餐', '意大利菜', '葷', 30, 4, 400),
(2, 2, '披薩', NULL, '美味的披薩食譜', 200, 20, NULL, GETDATE(), GETDATE(), '啟用', '公開', '雞肉', '午餐', '意大利菜', '葷', 45, 8, 1200),
(3, 3, '沙拉', NULL, '健康的沙拉食譜', 150, 15, NULL, GETDATE(), GETDATE(), '啟用', '公開', '豆腐', '小吃', '美式菜', '素', 15, 2, 150),
(4, 4, '湯', NULL, '溫暖的湯食譜', 120, 12, NULL, GETDATE(), GETDATE(), '啟用', '公開', '雞肉', '晚餐', '亞洲菜', '素', 60, 6, 300),
(5, 5, '蛋糕', NULL, '美味的蛋糕食譜', 180, 18, NULL, GETDATE(), GETDATE(), '啟用', '公開', '雞蛋', '甜點', '法式菜', '素', 90, 10, 2500),
(6, 6, '三明治', NULL, '簡單的三明治食譜', 110, 11, NULL, GETDATE(), GETDATE(), '啟用', '公開', '火雞肉', '午餐', '美式菜', '葷', 10, 1, 350),
(7, 7, '煎餅', NULL, '鬆軟的煎餅食譜', 130, 13, NULL, GETDATE(), GETDATE(), '啟用', '公開', '牛奶', '早餐', '美式菜', '葷', 20, 4, 500),
(8, 8, '奶昔', NULL, '健康的奶昔食譜', 140, 14, NULL, GETDATE(), GETDATE(), '啟用', '公開', '酸奶', '小吃', '美式菜', '素', 5, 2, 200),
(9, 9, '漢堡', NULL, '多汁的漢堡食譜', 160, 16, NULL, GETDATE(), GETDATE(), '啟用', '公開', '牛肉', '晚餐', '美式菜', '葷', 25, 1, 700),
(10, 10, '陽春麵', NULL, '美味的陽春麵食譜', 170, 17, NULL, GETDATE(), GETDATE(), '啟用', '公開', '雞肉', '晚餐', '亞洲菜', '葷', 35, 4, 600);

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
(1, '技術問題', '無法登入', NULL),
(2, '帳號支援', '重置密碼', NULL),
(3, '帳單問題', '收費錯誤', NULL),
(4, '功能請求', '新增黑暗模式', NULL),
(5, '錯誤報告', '應用程式啟動時崩潰', NULL),
(6, '一般查詢', '如何使用功能X？', NULL),
(7, '回饋', '很棒的應用，但需要Y', NULL),
(8, '投訴', '客服服務緩慢', NULL),
(9, '建議', '改進UI設計', NULL),
(10, '其他', '我可以更改我的用戶名嗎？', NULL);

------------------------------------------------------------------------
INSERT INTO messageTable (recipeId, userId, messageContent, topMessageId, createTime, changeTime, violationStatus)
VALUES
(1, 1, '這道菜看起來很棒！', NULL, '2024-05-01 10:00', '2024-05-01 10:00', NULL),
(2, 2, '我試過這個食譜，很好吃！', NULL, '2024-05-02 12:00', '2024-05-02 12:00', NULL),
(3, 3, '有沒有人有更好的做法？', NULL, '2024-05-03 14:00', '2024-05-03 14:00', '無違規'),
(4, 4, '這道菜需要更多鹽。', NULL, '2024-05-04 16:00', '2024-05-04 16:00', '無違規'),
(5, 5, '這是一個垃圾食譜。', NULL, '2024-05-05 18:00', '2024-05-05 18:00', '已報告違規');

------------------------------------------------------------------------
INSERT INTO stepTable (recipeId, stepText, stepImg)
VALUES
(1, '煮沸水並烹煮意大利麵。', NULL),
(1, '準備醬汁並拌意大利麵。', NULL),
(2, '準備麵團並鋪上醬汁。', NULL),
(2, '加上配料並烘烤。', NULL),
(3, '切碎蔬菜並混合。', NULL),
(3, '加上調味料並上桌。', NULL),
(4, '炒蔬菜並加入高湯。', NULL),
(4, '烹調雞肉並調味湯。', NULL),
(5, '混合材料並烘烤。', NULL),
(5, '裝飾並上桌。', NULL),
(6, '準備材料並組裝三明治。', NULL),
(6, '切割並上桌。', NULL),
(7, '混合麵糊並煮煎餅。', NULL),
(7, '加上配料並上桌。', NULL),
(8, '混合材料並攪拌。', NULL),
(8, '倒入杯中並上桌。', NULL),
(9, '調味並煮漢堡肉。', NULL),
(9, '組裝漢堡並上桌。', NULL),
(10, '煮麵並煮蒜頭。', NULL),
(10, '用蒜味油拌麵。', NULL);

------------------------------------------------------------------------
INSERT INTO IngredientDetail (ingredientId, ingredientName, commonName, kcalg)
VALUES
('ING101', '番茄', '常見番茄', 18),
('ING102', '洋蔥', '常見洋蔥', 40),
('ING103', '麵粉', '小麥麵粉', 364),
('ING104', '乳酪', '切達乳酪', 402),
('ING105', '萵苣', '冰山萵苣', 14),
('ING106', '胡蘿蔔', '常見胡蘿蔔', 41),
('ING107', '雞肉', '雞胸肉', 165),
('ING108', '高湯', '雞肉高湯', 15),
('ING109', '糖', '白砂糖', 387),
('ING110', '雞蛋', '雞蛋', 155),
('ING111', '牛奶', '全脂牛奶', 42),
('ING112', '奶油', '無鹽奶油', 717),
('ING113', '麵包', '白麵包', 265),
('ING114', '火腿', '熟火腿', 145),
('ING115', '香蕉', '香蕉', 89),
('ING116', '草莓', '草莓', 32),
('ING117', '牛肉', '牛絞肉', 250),
('ING118', '萵苣', '長葉萵苣', 17),
('ING119', '意大利麵', '意大利麵', 158),
('ING120', '番茄醬', '罐裝番茄醬', 32);

------------------------------------------------------------------------
INSERT INTO ingredientsTable (userId, recipeId, ingredientsID, ingredientsName, ingredientsNumber, ingredientsUnit)
VALUES 
-- 插入 使用者ID 1 的 食譜ID 1 的成分數據（意大利麵）
(1, 1, 'ING119', '意大利麵', 200, '克'),
(1, 1, 'ING120', '番茄醬', 150, '克'),

-- 插入 使用者ID 2 的 食譜ID 2 的成分數據（披薩）
(2, 2, 'ING103', '麵粉', 200, '克'),
(2, 2, 'ING104', '乳酪', 100, '克'),

-- 插入 使用者ID 3 的 食譜ID 3 的成分數據（沙拉）
(3, 3, 'ING105', '萵苣', 100, '克'),
(3, 3, 'ING102', '洋蔥', 50, '克'),

-- 插入 使用者ID 4 的 食譜ID 4 的成分數據（湯）
(4, 4, 'ING107', '雞肉', 200, '克'),
(4, 4, 'ING106', '胡蘿蔔', 100, '克'),

-- 插入 使用者ID 5 的 食譜ID 5 的成分數據（蛋糕）
(5, 5, 'ING103', '麵粉', 300, '克'),
(5, 5, 'ING109', '糖', 150, '克'),

-- 插入 使用者ID 6 的 食譜ID 6 的成分數據（三明治）
(6, 6, 'ING113', '麵包', 2, '片'),
(6, 6, 'ING114', '火腿', 100, '克'),

-- 插入 使用者ID 7 的 食譜ID 7 的成分數據（煎餅）
(7, 7, 'ING103', '麵粉', 200, '克'),
(7, 7, 'ING111', '牛奶', 200, '毫升'),

-- 插入 使用者ID 8 的 食譜ID 8 的成分數據（奶昔）
(8, 8, 'ING111', '牛奶', 200, '毫升'),
(8, 8, 'ING115', '香蕉', 1, '根'),

-- 插入 使用者ID 9 的 食譜ID 9 的成分數據（漢堡）
(9, 9, 'ING117', '牛肉', 150, '克'),
(9, 9, 'ING118', '萵苣', 100, '克'),

-- 插入 使用者ID 10 的 食譜ID 10 的成分數據（陽春麵）
(10, 10, 'ING119', '意大利麵', 200, '克'),
(10, 10, 'ING107', '雞肉', 100, '克');

------------------------------------------------------------------------
Install-Package Microsoft.EntityFrameworkCore.Tools
install-package Microsoft.EntityFrameworkCore.SqlServer
Scaffold-DbContext "Data Source=DESKTOP-AVM54SB;Initial Catalog=iTastyDB;Persist Security Info=False;User ID=sa;PassWord=111111;Encrypt=False;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force
Scaffold-DbContext "Data Source=DESKTOP-PIAH2TG;Initial Catalog=iTastyDB;Persist Security Info=False;User ID=sa;PassWord=111111;Encrypt=False;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force
TRUNCATE TABLE shoppingRecipe;
