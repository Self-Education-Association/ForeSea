CREATE VIEW [dbo].[View_All_Tables]
	AS SELECT [GUID],'CERD' AS 'TABLE' FROM [Certificate_Details]
		UNION
		SELECT [GUID],'CERR' AS 'TABLE' FROM [Certificate_Relations]
		UNION
		SELECT [GUID],'CHEC' AS 'TABLE' FROM [CheckIn_Details]
		UNION
		SELECT [GUID],'DISO' AS 'TABLE' FROM [Disobey_Details]
		UNION
		SELECT [GUID],'COUR' AS 'TABLE' FROM [Course_Details]
