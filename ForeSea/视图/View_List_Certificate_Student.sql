CREATE VIEW [dbo].[View_List_Certificate_Student]
	AS SELECT ID,COUNT(A.GUID) AS [COUNT]
		FROM Certificate_Relations A
		LEFT JOIN Certificate_Details B ON A.PID=B.GUID
		GROUP BY ID
