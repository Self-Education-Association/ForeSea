CREATE VIEW [dbo].[View_List_Course]
	AS SELECT ID,COUNT(ID) AS [COUNT] FROM Course_Details GROUP BY ID
