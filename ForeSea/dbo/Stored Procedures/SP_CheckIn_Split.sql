CREATE PROCEDURE [dbo].[SP_CheckIn_Split]
	@id INT,
	@ip VARCHAR(15),
	@name VARCHAR(20),
	@lstate BIT OUTPUT,
	@sstate BIT OUTPUT,
	@wtime SMALLINT OUTPUT
AS
	SELECT @lstate=1,@sstate=0
	DECLARE @time DATETIME2(0)=GETDATE()
	DECLARE @lesson TINYINT=(SELECT Lesson FROM CheckIn_Time WHERE DATEADD(MINUTE,30,@time) BETWEEN StartIn AND EndOut)
	SELECT @wtime=DATEDIFF(MINUTE,(SELECT StartIn FROM CheckIn_Time WHERE Lesson=@lesson),@time)
	IF (@name<>(SELECT Name FROM Student WHERE ID=@id))
	BEGIN
		SET @wtime=-99
	END
	IF @lesson IS NULL
	BEGIN
		SET @lstate=0
	END
	IF (SELECT Room FROM CheckIn_Room WHERE IP=LEFT(@ip,7) AND Block IN (SELECT Block FROM CheckIn_Block WHERE CONVERT(TIME(0),@time) BETWEEN StartTime AND EndTime)) IS NULL
	BEGIN
		SET @lstate=0
	END
	IF (SELECT ID FROM Course_Details WHERE Term=dbo.F_Term() AND Lesson=@lesson AND ID=@id) IS NULL
	BEGIN
		SET @sstate=1
	END
RETURN 0