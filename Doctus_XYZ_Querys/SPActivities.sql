USE [DoctusXYZ]
GO
CREATE PROCEDURE SPActivities
	@pOption INT = NULL,
	@pIdActivity INT = NULL,
	@pDescription VARCHAR(100) = NULL,
	@pIdUser INT = NULL,
	@pMessage varchar(256) OUTPUT
AS
BEGIN
	--CONSULTAR TODAS LAS ACTIVIDADES
	IF @pOption = 0
	BEGIN
		SELECT	A.IdActivity, A.Description, A.IdUser, B.UserName
		FROM	Activities A
		JOIN	Users B ON A.IdUser = B.IdUser
	END

	--CONSULTAR LAS ACTIVIDADES POR USUARIO
	IF @pOption = 1
	BEGIN
		SELECT	A.IdActivity, A.Description, A.IdUser, B.UserName
		FROM	Activities A
		JOIN	Users B ON A.IdUser = B.IdUser
		WHERE	A.IdUser = @pIdUser
	END

	--INSERTAR ACTIVIDAD POR USUARIO
	IF @pOption = 2
	BEGIN
		INSERT INTO Activities
			(Description, IdUser)
		VALUES
			(@pDescription, @pIdUser)
	END
END
GO