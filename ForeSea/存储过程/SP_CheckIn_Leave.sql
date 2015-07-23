CREATE PROCEDURE [dbo].[SP_CheckIn_Leave]
	@id INT,
	@result SMALLINT=500 OUTPUT
AS
	IF (SELECT ID FROM CheckIn_Details WHERE ID=@id AND State=0) IS NOT NULL
	BEGIN
		UPDATE CheckIn_Details SET State=3,Note='早退',CheckOut=GETDATE() WHERE ID=@id AND State=0
		SET @result=501
		RETURN 1
	END	
	ELSE
	BEGIN
		SET @result=502
		RETURN 1
	END
RETURN 0
