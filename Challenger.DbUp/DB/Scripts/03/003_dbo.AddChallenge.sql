
-- ADD CHALLENGE TABLE
CREATE TABLE [dbo].[Challenges](

			[Id] [bigint] IDENTITY(1,1) NOT NULL,
	
			[CreatorId] [uniqueidentifier] NOT NULL,

			[UserId] [bigint] NOT NULL,

			[Name] [nvarchar] (100) NOT NULL,
	
			[StartDate] [DateTime] NOT NULL,
			
			[EndDate] [DateTime] NOT NULL,
	
			[Formula] [nvarchar] (500) NOT NULL,	
	
		 CONSTRAINT [PK_dbo.Challenges] PRIMARY KEY CLUSTERED 
		(
			[ID] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]

-- ADD USER CHALLENGE TABLE		
CREATE TABLE [dbo].[UserChallenges](

			[Id] [bigint] IDENTITY(1,1) NOT NULL,
	
			[UserCorrelationId] [uniqueidentifier] NOT NULL,
			
			[UserId] [bigint] NOT NULL,

			[ChallengeId] [bigint] NOT NULL,
	
	
		 CONSTRAINT [PK_dbo.UserChallenges] PRIMARY KEY CLUSTERED 
		(
			[ID] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]
		
		
ALTER TABLE [dbo].[UserChallenges]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserChallenges_dbo.Challenges_Id] FOREIGN KEY([ChallengeId])
REFERENCES [dbo].[Challenges] ([Id])
GO

ALTER TABLE [dbo].[UserChallenges]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserChallenges_dbo.Users_Id] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO