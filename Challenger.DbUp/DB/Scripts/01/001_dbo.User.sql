CREATE TABLE [dbo].[Users](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	
	[UserName] [nvarchar] (100) NOT NULL,

	[Email] [nvarchar] (100) NOT NULL,
	
	[DateOfBirth] [DateTime] NOT NULL,
	
	[Height] [float] NOT NULL,

	[Sex] [varchar] (6) NOT NULL,
	
	
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

--ALTER TABLE [dbo].[FitRecords]
--ADD [BurntCalories] [float] NOT NULL
--CONSTRAINT DF_FitRecords_BurntCalories DEFAULT 0