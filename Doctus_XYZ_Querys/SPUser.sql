USE [DoctusXYZ]
GO
CREATE PROCEDURE SPUser
	@pOption INT = NULL,
	@pIdUser INT = NULL,
	@pUserName VARCHAR(30) = NULL,
	@pFullName VARCHAR(100) = NULL,
	@pPassword VARCHAR(20) = NULL,
	@pMessage varchar(256) OUTPUT
AS
BEGIN
	--CONSULTA TODOS LOS USUARIOS
	IF @pOption = 0
	BEGIN
		SELECT	IdUser, UserName, FullName
		FROM	Users
	END

	--LOGIN
	IF @pOption = 1
	BEGIN
		IF NOT EXISTS(SELECT 'x' FROM Users WHERE UserName = @pUserName AND Password = @pPassword)
		BEGIN
			SET @pMessage = '3¬Usuario o contraseña inválidos'
		END
		ELSE
		BEGIN
			SELECT	IdUser, UserName, FullName
			FROM	Users
			WHERE	UserName = @pUserName AND Password = @pPassword
		END
	END

	--INSERTAR USUARIO
	IF @pOption = 2
	BEGIN
		IF NOT EXISTS(SELECT 'x' FROM Users WHERE UserName = @pUserName)
		BEGIN
			INSERT INTO Users
				(UserName, FullName, Password)
			VALUES
				(@pUserName, @pFullName, @pPassword)
			SET @pMessage = '1¬Usuario ' + @pUserName + ' creado correctamente'
		END
		ELSE
		BEGIN
			SET @pMessage = '3¬El usuario ' + @pUserName + ' ya se encuentra registrado'
		END
	END
END