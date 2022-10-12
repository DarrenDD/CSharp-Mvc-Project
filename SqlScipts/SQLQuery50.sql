/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [MediaTypeId]
      ,[Title]
      ,[ThumbnailImagePath]
  FROM [SOT#25Db].[dbo].[MediaType]