CREATE PROCEDURE [dbo].[SP_CheckIn_Change]
	@id INT,
	@result SMALLINT=710 OUTPUT
AS
	SET @result=710
	IF (SELECT ID FROM CheckIn_Details WHERE ID=@id AND State=0) IS NOT NULL
	BEGIN
		UPDATE CheckIn_Details SET State=4,Note='换机中',Change=GETDATE() WHERE ID=@id AND State=0
		SET @result=711
		RETURN 1
	END	
	ELSE
	BEGIN
		SET @result=712
		RETURN 1
	END
RETURN 0
