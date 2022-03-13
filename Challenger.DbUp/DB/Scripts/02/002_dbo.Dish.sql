CREATE TABLE [dbo].[Dishes](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[IsPublic] [bit] DEFAULT 0,
	[PreparationTime] [int] NULL,
	[Servings] [int] NULL,
	[Energy] [int] NOT NULL,
	[Fats] [int] NOT NULL,
	[Proteins] [int] NOT NULL,
	[Carbohydrates] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Dishes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO