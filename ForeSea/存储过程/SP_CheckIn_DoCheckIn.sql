﻿
CREATE PROCEDURE [dbo].[SP_CheckIn_DoCheckIn]
	@id INT,
	@ip VARCHAR(15),
	@name NVARCHAR(20)='' OUTPUT,
	@state TINYINT=-1 OUTPUT,
	@room NVARCHAR(10)='' OUTPUT,
	@result SMALLINT OUTPUT
AS
	SET @result=300
	DECLARE @datetime DATETIME2(0)=GETDATE()
	DECLARE @time TIME(0)=CONVERT(TIME,@datetime)
	DECLARE @date DATE=CONVERT(DATE,@datetime)
	DECLARE @lesson TINYINT=(SELECT Lesson FROM CheckIn_Time WHERE @datetime BETWEEN StartIn AND EndOut)
	IF @lesson IS NULL
	BEGIN
		SET @result=302
		RETURN 1
	END
	IF (SELECT ID FROM DBO.Course_Details WHERE ID=@id AND Term=DBO.F_Term() AND Lesson=@lesson) IS NULL
	BEGIN
		SET @result=305
		RETURN 1
	END
	IF (SELECT ID FROM CheckIn_Details WHERE ID=@id AND State=4) IS NOT NULL
	BEGIN
		IF DATEDIFF(MINUTE,(SELECT Change FROM CheckIn_Details WHERE ID=@id AND State=4),@time)>dbo.F_ChangeOvertime()
		BEGIN
			UPDATE CheckIn_Details SET State=3,Note='换机超时' WHERE ID=@id AND State=4
			SET @result=312
			RETURN 1
		END
		IF (SELECT IP FROM CheckIn_Details WHERE ID=@id AND State=4)=@ip
		BEGIN
			SET @result=313
			RETURN 1
		END
		IF (SELECT ID FROM CheckIn_Details WHERE ID=@id AND State=0) IS NULL
		BEGIN
			DECLARE @LateState BIT=(SELECT LateState FROM CheckIn_Details WHERE ID=@id AND State=4)
			UPDATE CheckIn_Details SET State=5,Change=@time,Note='换机完成' WHERE ID=@id AND State=4
			INSERT INTO CheckIn_Details(ID,Lesson,Date,CheckIn,Keep,IP,LateState,State)
				VALUES(@id,@lesson,@date,@time,@time,@ip,@LateState,0)
			SET @result=311
			SET @state=0
			SELECT @name=Name FROM dbo.Student WHERE ID=@id
			SELECT @room=Room FROM dbo.CheckIn_Room WHERE IP=LEFT(@ip,7)
			RETURN 1
		END
		ELSE
		BEGIN
			SET @result=314
		END
	END
	IF (SELECT ID FROM CheckIn_Details WHERE ID=@id AND Date=@date) IS NOT NULL
	BEGIN
		SET @result=303
		RETURN 1
	END
	IF (SELECT Lesson FROM CheckIn_Time WHERE @datetime BETWEEN StartIn AND EndIn) IS NULL
	BEGIN
		SET @result=304
		RETURN 1
	END
	IF (SELECT Lesson FROM CheckIn_Time WHERE Lesson=@lesson AND @datetime BETWEEN StartIn AND LateIn) IS NULL
		SET @LateState=1
	ELSE
		SET @LateState=0
	IF (SELECT ID FROM CheckIn_Details WHERE ID=@id AND State=0) IS NULL
	BEGIN
		INSERT INTO CheckIn_Details(ID,IP,Lesson,Date,CheckIn,LateState,State)
			VALUES(@id,@ip,@lesson,@date,@time,@LateState,0)
		SET @result=301
		SET @state=0
		SELECT @name=Name FROM dbo.Student WHERE ID=@id
		SELECT @room=Room FROM dbo.CheckIn_Room WHERE IP=LEFT(@ip,7)
		RETURN 1
	END
	ELSE 
	BEGIN 
		SET @result=306
		RETURN 1
	END
RETURN 0
