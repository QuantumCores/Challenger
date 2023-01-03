
-- ADD CORRELATIONID COLUMN WITH NEW TYPE
ALTER TABLE [dbo].[Users] ADD [CorrelationId] uniqueidentifier NULL
GO

-- POPULATE NEW COLUMN
UPDATE [dbo].[Users]
SET [CorrelationId] = [ANU].[Id]
FROM [ChallengerIdentity].[dbo].[AspNetUsers] [ANU]
JOIN [dbo].[Users] [U] ON [ANU].[Email] = [U].[Email]
GO

-- CHANGE DATA TYPE
ALTER TABLE [dbo].[Users]
ALTER COLUMN [CorrelationId] uniqueidentifier NOT NULL

ALTER TABLE [dbo].[Users]
ALTER COLUMN [DateOfBirth] [DateTime] NULL

ALTER TABLE [dbo].[Users]
ALTER COLUMN [Height] [float] NULL
GO

ALTER TABLE [dbo].[Users]
ALTER COLUMN [Sex] [varchar] (6) NULL
GO

-- DROP COLUMNS THAT ARE IN IDENTITY PROJECT
ALTER TABLE [dbo].[Users]
DROP COLUMN	[UserName] 

ALTER TABLE [dbo].[Users]
DROP COLUMN	[Email] 
GO	
