CREATE PROCEDURE [dbo].[SP_CheckIn_Check]
	@id INT,
	@ip VARCHAR(15),
	@name VARCHAR(20)='' OUTPUT,
	@result SMALLINT=200 OUTPUT
AS
	DECLARE @time DATETIME2(0)=GETDATE()
	DECLARE @lesson TINYINT=(SELECT Lesson FROM CheckIn_Time WHERE @time BETWEEN StartIn AND EndIn)
	IF @lesson IS NULL
	BEGIN
		SET @result=202
		RETURN 1
	END
	IF (SELECT Room FROM CheckIn_Room WHERE IP=LEFT(@ip,7) AND Block IN (SELECT Block FROM CheckIn_Block WHERE CONVERT(TIME(0),@time) BETWEEN StartTime AND EndTime)) IS NULL
	BEGIN
		SET @result=203
		RETURN 1
	END
	IF (SELECT ID FROM Course_Details WHERE Term=F_TERM() AND Lesson=@lesson AND ID=@id) IS NULL
	BEGIN
		SET @result=204
		RETURN 1
	END
	SELECT @name=Name,@result=201 FROM Student WHERE ID=@id
RETURN 0
