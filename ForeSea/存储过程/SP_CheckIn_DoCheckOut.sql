CREATE PROCEDURE [dbo].[SP_CheckIn_DoCheckOut]
	@id INT,
	@state NVARCHAR(50)='' OUTPUT,
	@result SMALLINT=600 OUTPUT
AS
	DECLARE @time TIME=GETDATE()
	DECLARE @date DATE=GETDATE()
	IF (SELECT ID FROM CheckIn_Details WHERE ID=@id AND Date=@date AND State=0) IS NULL
	BEGIN
		SET @result=603
		RETURN 1
	END
	IF (SELECT Lesson FROM CheckIn_Time WHERE @time BETWEEN StartOut AND EndOut) IS NULL
	BEGIN
		SET @result=602
		SET @state=(SELECT ID FROM CheckIn_Details WHERE ID=@id AND Date=@date AND State =0)
		RETURN 1
	END
	UPDATE CheckIn_Details SET State=CASE LateState WHEN 1 THEN 2 ELSE 3 END,CheckOut=@time,CheckOutState=1
		WHERE ID=@id AND State=0
	SET @result=601
RETURN 0
