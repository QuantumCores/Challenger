
-- FIRST FROP ALL FK RELATIONS TO USERID LONG
DROP FK

-- RENAME USERID COLUMN
EXEC sp_rename '[dbo].[Users].[Id]', 'OldId', 'COLUMN';
EXEC sp_rename '[dbo].[Users].[CorrelationId]', 'Id', 'COLUMN';

-- CHANGE IDENTITY COLUMN TO NEW ID GUID

-- THIS COULD BE CHANGED TO DROP
EXEC sp_rename '[dbo].[Measurements].[UserId]', 'OldUserId', 'COLUMN';
EXEC sp_rename '[dbo].[GymRecords].[UserId]', 'OldUserId', 'COLUMN';
EXEC sp_rename '[dbo].[FitRecords].[UserId]', 'OldUserId', 'COLUMN';
EXEC sp_rename '[dbo].[Dishes].[UserId]', 'OldUserId', 'COLUMN';
EXEC sp_rename '[dbo].[DiaryRecords].[UserId]', 'OldUserId', 'COLUMN';
GO

-- ADD NEW USERID COLUMN WITH NEW TYPE
ALTER TABLE [dbo].[Users] ADD [Id] uniqueidentifier NULL
ALTER TABLE [dbo].[Measurements] ADD [UserId] uniqueidentifier NULL
ALTER TABLE [dbo].[GymRecords] ADD [UserId] uniqueidentifier NULL
ALTER TABLE [dbo].[FitRecords] ADD [UserId] uniqueidentifier NULL
ALTER TABLE [dbo].[Dishes] ADD [UserId] uniqueidentifier NULL
ALTER TABLE [dbo].[DiaryRecords] ADD [UserId] uniqueidentifier NULL
GO

-- CONVERT OLD ID TO NEW GUID
UPDATE [dbo].[Users]
SET Id = (SELECT TOP 1 [U].Id FROM [ChallengerIdentity].[dbo].[AspNetUsers] [U] WHERE [U].Id = OldId)