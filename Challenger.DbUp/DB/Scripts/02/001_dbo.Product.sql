CREATE TABLE [dbo].[Products](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,	
	[Brand] [nvarchar](32) NULL,
	[Barcode] [bigint] NULL,
	[Size] [int] NOT NULL,
	[SizeUnit] [char](1) NOT NULL,
	[Energy] [int] NOT NULL,
	[Fats] [int] NOT NULL,
	[Proteins] [int] NOT NULL,
	[Carbohydrates] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO