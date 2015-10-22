CREATE PROCEDURE [dbo].[SP_CheckIn_Run]
	@ip VARCHAR(15),
	@id INT=0 OUTPUT,
	@name NVARCHAR(20)='' OUTPUT,
	@state TINYINT=-1 OUTPUT,
	@room NVARCHAR(10)='' OUTPUT,
	@result SMALLINT=100 OUTPUT
AS
	DECLARE @time DATETIME2(0)=GETDATE()
	IF (SELECT IP FROM CheckIn_Room WHERE IP=LEFT(@ip,7) AND Enable=1) IS NULL
	BEGIN
		SET @result=102
		RETURN 1
	END
	ELSE 
	IF (SELECT IP FROM CheckIn_Room WHERE IP=LEFT(@ip,7) AND Block IN (SELECT Name FROM CheckIn_Block WHERE CONVERT(TIME,@time) BETWEEN StartTime AND EndTime)) IS NULL
	BEGIN
		SET @result=104
		RETURN 1
	END
	IF (SELECT IP FROM CheckIn_Details WHERE IP=@ip AND [State]=0 AND DATEDIFF(MINUTE,Keep,CONVERT(TIME(0),@time))<=dbo.F_KeepOvertime()) IS NOT NULL
	BEGIN
		SELECT @id=A.ID,@name=B.Name,@state=A.[State],@room=C.Room,@result=103
		FROM CheckIn_Details A
		JOIN Student B ON A.ID=B.ID
		JOIN CheckIn_Room C ON LEFT(A.IP,7)=C.IP 
		WHERE A.IP=@IP AND A.State =0
		RETURN 1
	END
	UPDATE CheckIn_Details SET State=3,Note='换机超时' WHERE IP=@ip AND State=0
	SET @result=101
RETURN 0
