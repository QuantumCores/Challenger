
-- ADD MISSIN MIGRATION
  INSERT INTO [ChallengerIdentity].[dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion])
  VALUES ('20221111231557_IdentityInit', '3.1.30')
GO

-- FIX EMAIL DATA
  UPDATE [ChallengerIdentity].[dbo].[AspNetUsers]
  SET Email = UserName,
	  NormalizedEmail = NormalizedUserName,
	  EmailConfirmed = 1
GO

-- FIX USERNAME DATA
  UPDATE [ChallengerIdentity].[dbo].[AspNetUsers]
  SET UserName = [U].[UserName],
	  NormalizedUserName = UPPER([U].[UserName])
  FROM [ChallengerIdentity].[dbo].[AspNetUsers] [ANU]
  JOIN [Challenger].[dbo].[Users] [U] ON [ANU].[Email] = [U].[Email]
  GO
