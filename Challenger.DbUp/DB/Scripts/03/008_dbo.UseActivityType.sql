-- ADD ACTIVITYTYPEID COLUMN
ALTER TABLE [dbo].[FitRecords]
ADD [ActivityTypeId] [bigint] NULL
GO

-- MATCH EXISTING EXCERSIZES WITH NEW TYPES
UPDATE [dbo].[FitRecords]
SET ActivityTypeId = AT.Id
FROM [dbo].[FitRecords] FT
JOIN [dbo].[ActivityTypes] AT ON AT.Name = 
                    CASE
						WHEN [Excersize] = 'Snowboard' THEN 'Skiing 3'
                        WHEN [Excersize] = 'Jogging' THEN 'Running 1'
                        WHEN [Excersize] = 'Walking' THEN 'Walking'
                        WHEN [Excersize] = 'Swimming' THEN 'Swimming laps 2'
                        WHEN [Excersize] = 'Yoga' THEN 'Yoga Power'
                        WHEN [Excersize] = 'Cycling' THEN 'Bicycling 5'
                        WHEN [Excersize] = 'Fitness' THEN 'Fitness 2'
                        WHEN [Excersize] = 'Squash' THEN 'Squash 1'
                        WHEN [Excersize] = 'Hiking' THEN 'Hiking 2'
                        WHEN [Excersize] = 'Rollerblading' THEN 'Rollerblading 1'
                        WHEN [Excersize] = 'Badminton' THEN 'Badminton 2'
                        WHEN [Excersize] = 'Trail_Running' THEN 'Running 17'
                        WHEN [Excersize] = 'Footbal' THEN 'Soccer 1'
                     END
GO


-- UPDATE FITRECORDS 
ALTER TABLE [dbo].[FitRecords]  WITH CHECK ADD  CONSTRAINT [FK_dbo.FitRecords_dbo.ActivityTypes_Id] FOREIGN KEY([ActivityTypeId])
REFERENCES [dbo].[ActivityTypes] ([Id])
GO

-- DROP EXCERSIZE COLUMN
ALTER TABLE [dbo].[FitRecords]
DROP COLUMN [Excersize];

-- SET ACTIVITYTYPEID AS NON NULLABLE
ALTER TABLE [dbo].[FitRecords]
ALTER COLUMN [ActivityTypeId] [bigint] NOT NULL
GO
