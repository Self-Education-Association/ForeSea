CREATE VIEW [dbo].[View_Checkin_Details]
	AS SELECT GUID,ID,Date,State FROM Table_Checkin_Details
		WHERE VALID=1
		UNION ALL
		SELECT GUID,ID,Date,'请假' FROM Table_Checkin_Certificate
		WHERE VALID=1
