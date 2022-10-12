USE [SOT#25Db]
GO

INSERT INTO [dbo].[ProgrammeItems]
           ([Title]
           ,[ProgrammeId]
           ,[MediaTypeId]
           ,[DateTimeItemReleased]
           ,[Description])
     VALUES
           (<Title, nvarchar(200),>
           ,<ProgrammeId, int,>
           ,<MediaTypeId, int,>
           ,<DateTimeItemReleased, datetime2(7),>
           ,<Description, nvarchar(max),>)
GO

