CREATE TABLE [dbo].[DiaryRecords](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[DiaryDate] [DateTime] NOT NULL,
	[Energy] [float] NOT NULL,
	[Fats] [float] NOT NULL,
	[Proteins] [float] NOT NULL,
	[Carbohydrates] [float] NOT NULL,
 CONSTRAINT [PK_dbo.DiaryRecords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO