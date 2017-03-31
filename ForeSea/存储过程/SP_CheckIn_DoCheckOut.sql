CREATE PROCEDURE [dbo].[SP_CheckIn_DoCheckOut]
	@id INT,
	@state NVARCHAR(50)='' OUTPUT,
	@result SMALLINT=800 OUTPUT
AS
	SET @result=800
	DECLARE @datetime DATETIME2(0)=GETDATE()
	DECLARE @time TIME=@datetime
	DECLARE @date DATE=@datetime
	IF (SELECT ID FROM CheckIn_Details WHERE ID=@id AND Date=@date AND State=0) IS NULL
	BEGIN
		SET @result=803
		RETURN 1
	END
	IF (SELECT Lesson FROM CheckIn_Time WHERE @datetime BETWEEN StartOut AND EndOut) IS NULL
	BEGIN
		SET @result=802
		SET @state=(SELECT ID FROM CheckIn_Details WHERE ID=@id AND Date=@date AND State =0)
		RETURN 1
	END
	UPDATE CheckIn_Details SET State=CASE LateState WHEN 1 THEN 2 ELSE 1 END,CheckOut=@time,CheckOutState=1
		WHERE ID=@id AND State=0
	SET @result=801
RETURN 0
