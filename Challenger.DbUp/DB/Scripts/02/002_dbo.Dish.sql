CREATE TABLE [dbo].[Dishes](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[IsPublic] [bit] DEFAULT 0,
	[PreparationTime] [int] NULL,
	[Servings] [int] NULL	
 CONSTRAINT [PK_dbo.Dishes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Dishes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Dishes_dbo.Users_Id] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[Dishes] CHECK CONSTRAINT [FK_dbo.Dishes_dbo.Users_Id]
GO