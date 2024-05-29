/****** Object:  Table [dbo].[ingredientsTable]    Script Date: 2024/5/29 下午 03:48:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ingredientsTable](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[recipeId] [int] NOT NULL,
	[titleName] [nvarchar](20) NULL,
	[titleId] [int] NULL,
	[ingredientsID] [char](12) NULL,
	[ingredientsName] [nvarchar](20) NULL,
	[ingredientsNumber] [real] NULL,
	[ingredientsUnit] [nvarchar](5) NULL,
	[ingredientsPrice] [int] NULL,
	[checkbox] [bit] NOT NULL,
	shoppingListuserId int NULL,
 CONSTRAINT [PK_ingredientsTable] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ingredientsTable] ON 
GO
INSERT [dbo].[ingredientsTable] ([id], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], [ingredientsPrice], [checkbox], shoppingListuserId) VALUES (1, 1, N'食材', NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL)
GO
INSERT [dbo].[ingredientsTable] ([id], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], [ingredientsPrice], [checkbox], shoppingListuserId) VALUES (2, 1, NULL, 1, N'E7700701    ', N'綠豆芽', 300, N'g', NULL, 0, NULL)
GO
INSERT [dbo].[ingredientsTable] ([id], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], [ingredientsPrice], [checkbox], shoppingListuserId) VALUES (3, 1, NULL, 1, N'R4701201    ', N'嫩豆腐', 300, N'g', NULL, 0, NULL)
GO
INSERT [dbo].[ingredientsTable] ([id], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], [ingredientsPrice], [checkbox], shoppingListuserId) VALUES (4, 1, NULL, 1, N'I0300301    ', N'豬上肩肉', 250, N'g', NULL, 0, NULL)
GO
INSERT [dbo].[ingredientsTable] ([id], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], [ingredientsPrice], [checkbox], shoppingListuserId) VALUES (5, 1, NULL, 1, N'E2600201    ', N'韮菜', NULL, N'適量', NULL, 0, NULL)
GO
INSERT [dbo].[ingredientsTable] ([id], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], [ingredientsPrice], [checkbox], shoppingListuserId) VALUES (6, 1, NULL, 1, N'E2100101    ', N'大蒜', 15, N'g', NULL, 0, NULL)
GO
INSERT [dbo].[ingredientsTable] ([id], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], [ingredientsPrice], [checkbox], shoppingListuserId) VALUES (7, 1, N'醬料', NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL)
GO
INSERT [dbo].[ingredientsTable] ([id], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], [ingredientsPrice], [checkbox], shoppingListuserId) VALUES (8, 1, NULL, 7, N'P0700101    ', N'醬油', 22.5, N'g', NULL, 0, NULL)
GO
INSERT [dbo].[ingredientsTable] ([id], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], [ingredientsPrice], [checkbox], shoppingListuserId) VALUES (9, 1, NULL, 7, N'P0900201    ', N'香油', 15, N'g', NULL, 0, NULL)
GO
INSERT [dbo].[ingredientsTable] ([id], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], [ingredientsPrice], [checkbox], shoppingListuserId) VALUES (10, 1, NULL, 7, N'P0600101    ', N'米醋', 15, N'g', NULL, 0, NULL)
GO
INSERT [dbo].[ingredientsTable] ([id], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], [ingredientsPrice], [checkbox], shoppingListuserId) VALUES (11, 1, NULL, 7, N'P1003201    ', N'辣椒醬', 15, N'g', NULL, 0, NULL)
GO
INSERT [dbo].[ingredientsTable] ([id], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], [ingredientsPrice], [checkbox], shoppingListuserId) VALUES (12, 1, NULL, 7, NULL, N'水', 30, N'g', NULL, 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[ingredientsTable] OFF
GO
ALTER TABLE [dbo].[ingredientsTable]  WITH CHECK ADD  CONSTRAINT [FK_ingredientsTable_Ingredients] FOREIGN KEY([ingredientsID])
REFERENCES [dbo].[Ingredients] ([id])
GO
ALTER TABLE [dbo].[ingredientsTable] CHECK CONSTRAINT [FK_ingredientsTable_Ingredients]
GO
ALTER TABLE [dbo].[ingredientsTable]  WITH CHECK ADD  CONSTRAINT [FK_ingredientsTable_ingredientsTable] FOREIGN KEY([titleId])
REFERENCES [dbo].[ingredientsTable] ([id])
GO
ALTER TABLE [dbo].[ingredientsTable] CHECK CONSTRAINT [FK_ingredientsTable_ingredientsTable]
GO
ALTER TABLE [dbo].[ingredientsTable]  WITH CHECK ADD  CONSTRAINT [FK_ingredientsTable_recipeTable] FOREIGN KEY([recipeId])
REFERENCES [dbo].[recipeTable] ([id])
GO
ALTER TABLE [dbo].[ingredientsTable] CHECK CONSTRAINT [FK_ingredientsTable_recipeTable]
GO
