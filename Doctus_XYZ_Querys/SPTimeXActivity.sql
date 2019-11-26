USE [DoctusXYZ]
GO
CREATE PROCEDURE SPTimeXActivity
	@pOption INT = NULL,
	@pIdTimeXActivity INT = NULL,
	@pIdActivity INT = NULL,
	@pTimeWorked INT = NULL,
	@pIdUser INT = NULL,
	@pDateActivity VARCHAR(20) = NULL,
	@pMessage varchar(256) OUTPUT
AS
BEGIN
	DECLARE @TotalActividad INT = 0;
	--CONSULTAR TODOS LOS TIEMPOS DE LAS ACTIVIDADES
	IF @pOption = 0
	BEGIN
		SELECT	A.IdTimeXActivity, A.IdActivity, C.Description, TimeWorked, A.IdUser, B.UserName, DateActivity
		FROM	TimeXActivities A
		JOIN	Users B ON A.IdUser = B.IdUser
		JOIN	Activities C ON A.IdActivity = C.IdActivity
	END

	--CONSULTAR LOS TIEMPOS DE ACTIVIDADES POR USUARIO
	IF @pOption = 1
	BEGIN
		SELECT	A.IdTimeXActivity, A.IdActivity, C.Description, TimeWorked, A.IdUser, B.UserName, DateActivity
		FROM	TimeXActivities A
		JOIN	Users B ON A.IdUser = B.IdUser
		JOIN	Activities C ON A.IdActivity = C.IdActivity
		WHERE	A.IdActivity = @pIdActivity AND A.IdUser = @pIdUser
	END

	--INSERTAR TIEMPO DE ACTIVIDAD POR USUARIO
	IF @pOption = 2
	BEGIN
		SET @TotalActividad = (SELECT SUM(TimeWorked) AS Total FROM TimeXActivities WHERE IdActivity = 4 AND DateActivity = '25/11/2019' GROUP BY IdActivity)
		IF ((@TotalActividad + @pTimeWorked) > 8)
		BEGIN
			SET @pMessage = '3¬No se puede registrar el tiempo, supera las 8 horas para la fecha: ' + @pDateActivity
		END
		ELSE
		BEGIN
			INSERT INTO TimeXActivities
				(IdActivity, TimeWorked, IdUser, DateActivity)
			VALUES
				(@pIdActivity, @pTimeWorked, @pIdUser, @pDateActivity)
		END
	END
END