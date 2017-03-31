CREATE PROCEDURE [dbo].[SP_CheckIn_NotHere]
	@id INT,
	@result SMALLINT=600 OUTPUT
AS
	SET @result=600
	IF (SELECT ID FROM CheckIn_Details WHERE ID=@id AND State=0) IS NOT NULL
	BEGIN
		UPDATE CheckIn_Details SET State=3,Note='无操作',CheckOut=GETDATE() WHERE ID=@id AND State=0
		SET @result=601
		RETURN 1
	END	
	ELSE
	BEGIN
		SET @result=602
		RETURN 1
	END
RETURN 0