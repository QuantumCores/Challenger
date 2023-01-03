USE [Challenger]
GO
/****** Object:  Table [dbo].[SchemaVersions]    Script Date: 11/15/2022 5:38:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SchemaVersions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ScriptName] [nvarchar](255) NOT NULL,
	[Applied] [datetime] NOT NULL,
 CONSTRAINT [PK_SchemaVersions_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[SchemaVersions] ON 
GO
INSERT [dbo].[SchemaVersions] ([Id], [ScriptName], [Applied]) VALUES (1, N'Challenger.DbUp.DB.Scripts._01.001_dbo.User.sql', CAST(N'2022-11-12T16:27:19.657' AS DateTime))
GO
INSERT [dbo].[SchemaVersions] ([Id], [ScriptName], [Applied]) VALUES (2, N'Challenger.DbUp.DB.Scripts._01.002_dbo.Measurement.sql', CAST(N'2022-11-12T16:27:19.700' AS DateTime))
GO
INSERT [dbo].[SchemaVersions] ([Id], [ScriptName], [Applied]) VALUES (3, N'Challenger.DbUp.DB.Scripts._01.003_dbo.GymRecord.sql', CAST(N'2022-11-12T16:27:19.703' AS DateTime))
GO
INSERT [dbo].[SchemaVersions] ([Id], [ScriptName], [Applied]) VALUES (4, N'Challenger.DbUp.DB.Scripts._01.004_dbo.FitRecord.sql', CAST(N'2022-11-12T16:27:19.707' AS DateTime))
GO
INSERT [dbo].[SchemaVersions] ([Id], [ScriptName], [Applied]) VALUES (5, N'Challenger.DbUp.DB.Scripts._03.001_dbo.AddUserIdentityGuid.sql', CAST(N'2022-11-15T16:36:51.347' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[SchemaVersions] OFF
GO
