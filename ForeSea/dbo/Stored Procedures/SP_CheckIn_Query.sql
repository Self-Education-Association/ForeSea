-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_CheckIn_Query 
	@id INT,
	@normal INT OUTPUT,
	@late INT OUTPUT,
	@truency INT OUTPUT,
	@result SMALLINT=900 OUTPUT
AS
BEGIN
	IF (@id IN (SELECT DISTINCT ID FROM View_List_CheckIn))
	BEGIN
		SELECT @normal=NORMAL,@late=LATE,@truency=TRUENCY FROM View_List_CheckIn WHERE ID=@id
		SET @result=901
		RETURN 1
	END
	ELSE 
	BEGIN
		SET @result=902
		RETURN 1
	END
END