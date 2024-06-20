/****** Object:  Table [dbo].[ingredientsTable]    Script Date: 2024/6/20 上午 07:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ingredientsTable](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[recipeId] [int] NOT NULL,
	[titleName] [nvarchar](20) NULL,
	[titleId] [int] NULL,
	[ingredientsID] [char](12) NULL,
	[ingredientsName] [nvarchar](20) NULL,
	[ingredientsNumber] [real] NULL,
	[ingredientsUnit] [nvarchar](50) NULL,
	kcalg real NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ingredientsTable] ON 
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (1, 69, 1, N'醃漬用酪奶', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (2, 69, 1, NULL, 1, NULL, N'酪乳', 750, N'ml', 0.4)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (3, 69, 1, NULL, 1, N'P0300101    ', N'岩鹽', 32, N'g', 0.02)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (4, 69, 1, NULL, 1, N'P0101301    ', N'黑胡椒粉', 15, N'g', 3.72)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (5, 69, 1, NULL, 1, N'P0101801    ', N'辣椒粉', 6, N'g', 3.87)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (6, 69, 1, NULL, 1, N'I0400201    ', N'肉雞', 1.8, N'kg', 2.48)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (7, 69, 1, N'炸雞粉料', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (8, 69, 1, NULL, 7, N'A0320301    ', N'中筋麵粉', 280, N'g', 3.61)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (9, 69, 1, NULL, 7, N'P0101001    ', N'洋蔥粉', 3, N'g', 3.57)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (10, 69, 1, NULL, 7, N'P0101701    ', N'蒜粉', 3, N'g', 3.32)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (11, 69, 1, NULL, 7, N'P0300101    ', N'岩鹽', 4, N'g', 0.02)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (12, 69, 1, NULL, 7, N'P0101301    ', N'黑胡椒粉', 4, N'g', 3.72)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (13, 69, 1, NULL, 7, N'P0101801    ', N'辣椒粉', 2, N'g', 3.87)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (14, 69, 2, N'食材', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (15, 69, 2, NULL, 14, N'J2100901    ', N'白對蝦(大)', 225, N'g', 1.09)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (16, 69, 2, NULL, 14, N'G0300101    ', N'草菇', 450, N'g', 0.36)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (17, 69, 2, NULL, 14, N'E7400401    ', N'聖女小番茄', 135, N'g', 0.34)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (18, 69, 2, N'調味料', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (19, 69, 2, NULL, 18, N'E1900101    ', N'嫩薑', NULL, N'適量', 0.21)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (20, 69, 2, NULL, 18, NULL, N'香茅', NULL, N'適量', 0)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (21, 69, 2, NULL, 18, NULL, N'檸檬葉', NULL, N'適量', 0)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (22, 69, 2, NULL, 18, N'E7510101    ', N'乾長辣椒(紅皮)', NULL, N'適量', 3.82)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (23, 69, 2, NULL, 18, N'E4000101    ', N'芫荽', NULL, N'適量', 0.26)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (24, 69, 2, NULL, 18, N'ING109      ', N'糖', 15, N'g', 3.87)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (25, 69, 2, NULL, 18, N'P0800401    ', N'魚露', 15, N'ml', 0.64)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (26, 69, 2, NULL, 18, N'D3800302    ', N'檸檬汁(綠皮)', 15, N'ml', 0.31)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (27, 69, 2, NULL, 18, N'L0400101    ', N'淡煉乳', 200, N'g', 1.36)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (28, 69, 2, NULL, 18, N'P0300101    ', N'岩鹽', NULL, N'適量', 0.02)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (29, 69, 2, NULL, 18, NULL, N'水', 800, N'ml', 0)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (30, 69, 3, N'食材', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (31, 69, 3, NULL, 30, N'E6200101    ', N'冬瓜', 200, N'g', 0.13)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (32, 69, 3, NULL, 30, N'N0100301    ', N'紅砂糖', 100, N'g', 3.84)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (33, 69, 3, NULL, 30, NULL, N'水', 200, N'ml', 0)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (34, 69, 4, N'食材', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (35, 69, 4, NULL, 34, N'J2200201    ', N'明蝦仁', 100, N'g', 0.52)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (36, 69, 4, NULL, 34, N'E6200101    ', N'冬瓜', 300, N'g', 0.13)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (37, 69, 4, NULL, 34, N'K0100101    ', N'雞蛋(白殼)', 120, N'g', 1.39)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (38, 69, 4, N'調味料', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (39, 69, 4, NULL, 38, N'P0300101    ', N'岩鹽', NULL, N'適量', 0.02)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (40, 69, 4, NULL, 38, N'P0101101    ', N'白胡椒粉', 10, N'g', 3.42)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (41, 69, 4, NULL, 38, N'A0420101    ', N'玉米粉', 45, N'g', 3.69)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (42, 69, 4, NULL, 38, NULL, N'米酒', 30, N'ml', 1.23)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (43, 69, 5, N'食材', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (44, 69, 5, NULL, 43, NULL, N'水蓮', 300, N'g', 0.17)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (45, 69, 5, NULL, 43, N'G0800102    ', N'香菇(小)', 80, N'g', 0.26)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (46, 69, 5, N'調味料', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (47, 69, 5, NULL, 46, NULL, N'破布子', NULL, N'適量', 1.05)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (48, 69, 5, NULL, 46, N'E1900101    ', N'嫩薑', NULL, N'適量', 0.21)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (49, 69, 5, NULL, 46, N'P0300101    ', N'岩鹽', NULL, N'適量', 0.02)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (50, 69, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[ingredientsTable] ([id], [userId], [recipeId], [titleName], [titleId], [ingredientsID], [ingredientsName], [ingredientsNumber], [ingredientsUnit], kcalg) VALUES (51, 69, 7, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[ingredientsTable] OFF
GO
ALTER TABLE [dbo].[ingredientsTable]  WITH CHECK ADD FOREIGN KEY([ingredientsID])
REFERENCES [dbo].[IngredientDetail] ([ingredientId])
GO
ALTER TABLE [dbo].[ingredientsTable]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[userInfo] ([userId])
GO
ALTER TABLE [dbo].[ingredientsTable]  WITH CHECK ADD  CONSTRAINT [FK_ingredientsTable_recipeTable] FOREIGN KEY([recipeId])
REFERENCES [dbo].[recipeTable] ([recipeId])
GO
ALTER TABLE [dbo].[ingredientsTable] CHECK CONSTRAINT [FK_ingredientsTable_recipeTable]
GO
