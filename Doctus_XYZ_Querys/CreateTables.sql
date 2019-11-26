USE DoctusXYZ
GO
CREATE TABLE Users
(
	IdUser INT IDENTITY(1,1) NOT NULL,
	UserName VARCHAR(30) NOT NULL,
	FullName VARCHAR(100) NULL,
	Password VARCHAR(20) NOT NULL,
	PRIMARY KEY (IdUser)
)
GO
CREATE TABLE Activities
(
	IdActivity INT IDENTITY(1,1) NOT NULL,
	Description VARCHAR(100) NOT NULL,
	IdUser INT NOT NULL,
	PRIMARY KEY (IdActivity),
    CONSTRAINT FK_IdUser FOREIGN KEY (IdUser)
    REFERENCES Users(IdUser)

)
GO
CREATE TABLE TimeXActivities
(
	IdTimeXActivity INT IDENTITY(1,1) NOT NULL,
	IdActivity INT NOT NULL,
	TimeWorked INT NOT NULL,
	IdUser INT NOT NULL,
	DateActivity VARCHAR(20) NOT NULL,
	PRIMARY KEY (IdTimeXActivity),
    CONSTRAINT FK_IdUserXActivities FOREIGN KEY (IdUser)
    REFERENCES Users(IdUser),
    CONSTRAINT FK_IdActivity FOREIGN KEY (IdActivity)
    REFERENCES Activities(IdActivity)
)

--ALTER TABLE Activities ADD PRIMARY KEY (IdActivity);
--ALTER TABLE TimeXActivities ADD PRIMARY KEY (IdTimeXActivity);
--ALTER TABLE Users ADD PRIMARY KEY (IdUser);

--ALTER TABLE Activities
--    ADD FOREIGN KEY (IdUser)
--        REFERENCES Users (IdUser);

--ALTER TABLE TimeXActivities
--    ADD FOREIGN KEY (IdActivity)
--        REFERENCES Activities (IdActivity);

--ALTER TABLE TimeXActivities
--    ADD FOREIGN KEY (IdUser)
--        REFERENCES Users (IdUser);

--DROP TABLE TimeXActivities DROP TABLE Activities DROP TABLE Users