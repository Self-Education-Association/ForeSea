CREATE VIEW [dbo].[View_List_Disobey]
	AS 
		SELECT ID,COUNT(ID) AS [COUNT],SUM(B.Class) AS [SUM]
		FROM Disobey_Details A LEFT JOIN
		Disobey_Type B ON A.Type=B.Type	GROUP BY ID