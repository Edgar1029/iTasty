/****** Object:  Table [dbo].[messageTable]    Script Date: 2024/6/18 上午 04:08:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[messageTable](
	[messageId] [int] IDENTITY(1,1) NOT NULL,
	[recipeId] [int] NOT NULL,
	[userID] [int] NOT NULL,
	[messageContent] [nvarchar](150) NOT NULL,
	[topMessageid] [int] NULL,
	[createTime] [smalldatetime] NOT NULL,
	[changeTime] [smalldatetime] NOT NULL,
	[violationStatus] [nvarchar](20) NULL,
	existDelete nvarchar(10) NULL,
PRIMARY KEY CLUSTERED 
(
	[messageId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[messageTable] ON 
GO
INSERT [dbo].[messageTable] ([messageId], [recipeId], [userID], [messageContent], [topMessageid], [createTime], [changeTime], [violationStatus], existDelete) VALUES (1, 1, 69, N'這是我吃過最好吃的炸雞', NULL, CAST(N'2024-05-01T10:00:00' AS SmallDateTime), CAST(N'2024-05-01T10:00:00' AS SmallDateTime), N'violation', NULL)
GO
INSERT [dbo].[messageTable] ([messageId], [recipeId], [userID], [messageContent], [topMessageid], [createTime], [changeTime], [violationStatus], existDelete) VALUES (2, 2, 2, N'菇菇好吃，讚讚!!', NULL, CAST(N'2024-05-02T12:00:00' AS SmallDateTime), CAST(N'2024-05-02T12:00:00' AS SmallDateTime), N'No violation', NULL)
GO
INSERT [dbo].[messageTable] ([messageId], [recipeId], [userID], [messageContent], [topMessageid], [createTime], [changeTime], [violationStatus], existDelete) VALUES (3, 3, 3, N'這也太天然了吧....', NULL, CAST(N'2024-05-03T14:00:00' AS SmallDateTime), CAST(N'2024-05-03T14:00:00' AS SmallDateTime), N'No violation', NULL)
GO
INSERT [dbo].[messageTable] ([messageId], [recipeId], [userID], [messageContent], [topMessageid], [createTime], [changeTime], [violationStatus], existDelete) VALUES (4, 1, 1, N'謝謝你的讚美', 1, CAST(N'2024-05-05T20:32:00' AS SmallDateTime), CAST(N'2024-05-05T20:32:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[messageTable] ([messageId], [recipeId], [userID], [messageContent], [topMessageid], [createTime], [changeTime], [violationStatus], existDelete) VALUES (5, 1, 52, N'好食譜，救了我的家族聚會!!', NULL, CAST(N'2024-06-16T15:00:00' AS SmallDateTime), CAST(N'2024-06-16T15:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[messageTable] ([messageId], [recipeId], [userID], [messageContent], [topMessageid], [createTime], [changeTime], [violationStatus], existDelete) VALUES (6, 1, 69, N'不會，應該的', 1, CAST(N'2024-05-16T21:11:00' AS SmallDateTime), CAST(N'2024-05-16T21:11:00' AS SmallDateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[messageTable] OFF
GO
ALTER TABLE [dbo].[messageTable]  WITH CHECK ADD FOREIGN KEY([userID])
REFERENCES [dbo].[userInfo] ([userId])
GO
ALTER TABLE [dbo].[messageTable]  WITH CHECK ADD  CONSTRAINT [FK_messageTable_recipeTable] FOREIGN KEY([recipeId])
REFERENCES [dbo].[recipeTable] ([recipeId])
GO
ALTER TABLE [dbo].[messageTable] CHECK CONSTRAINT [FK_messageTable_recipeTable]
GO
