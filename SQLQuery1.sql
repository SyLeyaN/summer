
CREATE DATABASE Library_DataBase;
GO

USE Library_DataBase;
GO
CREATE ROLE User_role AUTHORIZATION simple_user_for_library;  
GO
ALTER ROLE User_role ADD MEMBER simple_user_for_library;  
GO
ALTER ROLE User_role ADD MEMBER librarian_for_library;  
GO
ALTER ROLE User_role ADD MEMBER admin_for_library;  
GO
CREATE ROLE Librarian_role AUTHORIZATION librarian_for_library;  
GO
ALTER ROLE Librarian_role ADD MEMBER admin_for_library;  
GO
ALTER ROLE Librarian_role ADD MEMBER librarian_for_library;  
GO
CREATE ROLE Admin_role AUTHORIZATION admin_for_library;  
GO
ALTER ROLE Admin_role ADD MEMBER admin_for_library;  
GO

CREATE TABLE LibraryObjectsTypes
(Id TINYINT NOT NULL,
NameOfType NVARCHAR(10) NOT NULL,
CONSTRAINT PK_LibraryObjectsTypes PRIMARY KEY (Id)
)
GO

INSERT INTO dbo.LibraryObjectsTypes (Id, NameOfType)
VALUES (1, 'Book'), (2, 'Newspaper'), (3, 'Patent');
GO

CREATE TABLE LibraryObjects
(LibraryObjectId INT NOT NULL IDENTITY(1, 1),
Title NVARCHAR(300) NOT NULL,
PublishingYear INT NOT NULL,
NumberOfPages INT NOT NULL,
Note NVARCHAR(2000) NULL,
ObjectType TINYINT NOT NULL,
CONSTRAINT PK_LibraryObject PRIMARY KEY (LibraryObjectId),
CONSTRAINT FK_LibraryObject_LibraryObjectsTypes FOREIGN KEY (ObjectType) REFERENCES LibraryObjectsTypes (Id)
)
GO

CREATE TABLE Books
(BookId INT NOT NULL,
PublishingCity NVARCHAR (200) NOT NULL,
PublishingHouse NVARCHAR (300) NOT NULL,
ISBN VARCHAR (18) NULL,
CONSTRAINT PK_Book PRIMARY KEY (BookId),
CONSTRAINT FK_Book_LibraryObject FOREIGN KEY (BookId) REFERENCES LibraryObjects (LibraryObjectId)
)
GO

CREATE TABLE Patents
(PatentId INT NOT NULL,
Country NVARCHAR (200) NOT NULL,
ApplicationDate DATE NULL,
PublishingDate DATE NOT NULL,
RegistrationNumber VARCHAR (9) NOT NULL,
CONSTRAINT PK_Patent PRIMARY KEY (PatentId),
CONSTRAINT FK_Patent_LibraryObject FOREIGN KEY (PatentId) REFERENCES LibraryObjects (LibraryObjectId)
)
GO

ALTER TABLE Patents ALTER COLUMN ApplicationDate DATETIME2 ;  
GO 

ALTER TABLE Patents ALTER COLUMN PublishingDate DATETIME2 ;  
GO

CREATE TABLE Newspapers
(NewspaperId INT NOT NULL,
PublishingCity NVARCHAR (200) NOT NULL,
PublishingHouse NVARCHAR (300) NOT NULL,
ISSN VARCHAR (13) NULL,
CONSTRAINT PK_Newspaper PRIMARY KEY (NewspaperId),
CONSTRAINT FK_Newspaper_LibraryObject FOREIGN KEY (NewspaperId) REFERENCES LibraryObjects (LibraryObjectId)
)
GO

CREATE TABLE NewspaperIssues
(NewspaperIssueId INT NOT NULL IDENTITY (1, 1),
PublishingDate DATE NOT NULL,
Number INT NULL,
NewspaperId INT NOT NULL,
CONSTRAINT PK_NewspaperIssue PRIMARY KEY (NewspaperIssueId),
CONSTRAINT FK_NewspaperIssue_Newspaper FOREIGN KEY (NewspaperId) REFERENCES Newspapers (NewspaperId)
)
GO

CREATE TABLE Persons
(PersonId INT NOT NULL IDENTITY(1, 1),
PersonName NVARCHAR (50) NOT NULL,
PersonSurname NVARCHAR (200) NOT NULL,
CONSTRAINT PK_Person PRIMARY KEY (PersonId)
)
GO


CREATE TABLE BooksPersons
(BookId INT NOT NULL,
PersonId INT NOT NULL,
CONSTRAINT PK_BooksPersons PRIMARY KEY (BookId, PersonId),
CONSTRAINT FK_BooksPersons_Books FOREIGN KEY (BookId) REFERENCES Books (BookId),
CONSTRAINT FK_BooksPersons_Persons FOREIGN KEY (PersonId) REFERENCES Persons (PersonId)
)
GO

CREATE TABLE PatentsPersons
(PatentId INT NOT NULL,
PersonId INT NOT NULL,
CONSTRAINT PK_PatentsPersons PRIMARY KEY (PatentId, PersonId),
CONSTRAINT FK_PatentsPersons_Books FOREIGN KEY (PatentId) REFERENCES Patents (PatentId),
CONSTRAINT FK_PatentsPersons_Persons FOREIGN KEY (PersonId) REFERENCES Persons (PersonId)
)
GO

ALTER TABLE LibraryObjects
ADD CheckDelete BIT NULL
GO

ALTER TABLE NewspaperIssues
ADD CheckDelete BIT NULL
GO

CREATE OR ALTER PROCEDURE SelectAllBooks
AS
BEGIN
SELECT 
	LO.LibraryObjectId AS Id,
	LO.Title AS Title,
	LO.NumberOfPages AS NumberOfPages,
	B.PublishingCity AS PublishingCity,
	B.PublishingHouse AS PublishingHouse,
	LO.PublishingYear AS PublishingYear,
	B.ISBN AS ISBN,
	LO.Note AS Note
FROM dbo.LibraryObjects AS LO
INNER JOIN dbo.Books AS B
ON LO.LibraryObjectId = B.BookId
WHERE LO.CheckDelete IS NULL;

SELECT 
	P.PersonId AS Id,
	P.PersonName AS [Name],
	P.PersonSurname AS Surname,
	BP.BookId AS BookId
FROM dbo.LibraryObjects AS LO
INNER JOIN dbo.BooksPersons AS BP
ON LO.LibraryObjectId = BP.BookId
INNER JOIN dbo.Persons AS P
ON P.PersonId = BP.PersonId
WHERE LO.CheckDelete IS NULL;

END
GO
GRANT EXECUTE ON OBJECT::dbo.SelectAllBooks 
    TO User_role;  

CREATE OR ALTER PROCEDURE SelectAllPatents
AS
BEGIN
SELECT 
	LO.LibraryObjectId AS Id, 
	LO.Title AS Title,
	LO.NumberOfPages AS NumberOfPages,
	P.Country AS Country,
	LO.PublishingYear AS PublishingYear,
	P.ApplicationDate AS ApplicationDate,
	P.PublishingDate AS PublishingDate,
	P.RegistrationNumber AS RegistrationNumber,
	LO.Note AS Note
FROM dbo.LibraryObjects AS LO
INNER JOIN dbo.Patents AS P
ON LO.LibraryObjectId = P.PatentId
WHERE LO.CheckDelete IS NULL;

SELECT 
	P.PersonId AS Id,
	P.PersonName AS [Name],
	P.PersonSurname AS Surname,
	PP.PatentId AS PatentId
FROM dbo.LibraryObjects AS LO
INNER JOIN  dbo.PatentsPersons AS PP
ON LO.LibraryObjectId = PP.PatentId
INNER JOIN dbo.Persons AS P
ON P.PersonId = PP.PersonId
WHERE LO.CheckDelete IS NULL;
END
GO

GRANT EXECUTE ON OBJECT::dbo.SelectAllPatents 
    TO User_role;

CREATE OR ALTER PROCEDURE SelectAllNewspapers
AS
BEGIN
SELECT 
	LO.LibraryObjectId AS Id,
	LO.Title AS Title,
	LO.NumberOfPages AS NumberOfPages,
	LO.PublishingYear AS PublishingYear,
	N.PublishingCity AS PublishingCity,
	N.PublishingHouse AS PublishingHouse,
	N.ISSN AS ISSN,
	LO.Note AS Note
FROM dbo.LibraryObjects AS LO
INNER JOIN dbo.Newspapers AS N
ON LO.LibraryObjectId = N.NewspaperId
WHERE LO.CheckDelete IS NULL;
END

SELECT 
	NI.NewspaperIssueId AS Id,
	NI.NewspaperId AS NewspaperId, 
	NI.Number AS Number,
	NI.PublishingDate AS PublishingDate
FROM dbo.LibraryObjects AS LO
INNER JOIN dbo.NewspaperIssues AS NI
ON LO.LibraryObjectId = NI.NewspaperId
LEFT JOIN dbo.Newspapers as N
ON N.NewspaperId = NI.NewspaperId
WHERE LO.CheckDelete IS NULL AND NI.CheckDelete IS NULL;
GO
GRANT EXECUTE ON OBJECT::dbo.SelectAllNewspapers 
    TO User_role;

CREATE OR ALTER PROCEDURE SelectAllPersons
AS
BEGIN
SELECT 
	Persons.PersonId AS Id,
	Persons.PersonName AS [Name],
	Persons.PersonSurname AS Surname
FROM dbo.Persons;
END
GO
GRANT EXECUTE ON OBJECT::dbo.SelectAllPersons
    TO User_role;

CREATE OR ALTER PROCEDURE SelectByIdBooks
@BookId INT
AS
BEGIN
SELECT 
	LO.LibraryObjectId AS Id, 
	LO.Title AS Title,
	LO.NumberOfPages AS NumberOfPages,
	LO.PublishingYear AS PublishingYear,
	B.PublishingCity AS PublishingCity,
	B.PublishingHouse AS PublishingHouse,
	B.ISBN AS ISBN,
	LO.Note AS Note
FROM dbo.LibraryObjects AS LO
INNER JOIN dbo.Books AS B
ON LO.LibraryObjectId = @BookId AND B.BookId = @BookId
WHERE LO.CheckDelete IS NULL;

SELECT 
	P.PersonId AS Id,
	P.PersonName AS [Name],
	P.PersonSurname AS Surname,
	BP.BookId AS BookId
FROM dbo.LibraryObjects AS LO
INNER JOIN dbo.BooksPersons AS BP
ON LO.LibraryObjectId = BP.BookId
INNER JOIN dbo.Persons AS P
ON BP.PersonId = P.PersonId
WHERE LO.CheckDelete IS NULL;
END
GO

GRANT EXECUTE ON OBJECT::dbo.SelectByIdBooks
    TO User_role;

CREATE OR ALTER PROCEDURE SelectByIdPatents
@PatentId INT
AS
BEGIN
SELECT 
	LO.LibraryObjectId AS Id,
	LO.Title AS Title,
	LO.NumberOfPages AS NumberOfPages,
	LO.PublishingYear AS PublishingYear,
	P.Country AS Country,
	P.ApplicationDate AS ApplicationDate,
	P.PublishingDate AS PublishingDate,
	P.RegistrationNumber AS RegistrationNumber,
	LO.Note AS Note
FROM dbo.LibraryObjects AS LO
INNER JOIN dbo.Patents AS P
ON LO.LibraryObjectId = @PatentId AND P.PatentId = @PatentId
WHERE LO.CheckDelete IS NULL;

SELECT 
	P.PersonId AS Id,
	P.PersonName AS [Name],
	P.PersonSurname AS Surname,
	PP.PatentId AS PatentId
FROM dbo.LibraryObjects AS LO
INNER JOIN dbo.PatentsPersons AS PP
ON LO.LibraryObjectId = PP.PatentId
INNER JOIN dbo.Persons AS P
ON P.PersonId = PP.PersonId AND PP.PatentId = @PatentId
WHERE LO.CheckDelete IS NULL;
END
GO

GRANT EXECUTE ON OBJECT::dbo.SelectByIdPatents
    TO User_role;

CREATE OR ALTER PROCEDURE SelectByIdNewspapers
@NewspaperId INT
AS
BEGIN
SELECT 
	LO.LibraryObjectId AS Id, 
	LO.Title AS Title,
	LO.NumberOfPages AS NumberOfPages,
	LO.PublishingYear AS PublishingYear,
	N.PublishingCity AS PublishingCity,
	N.PublishingHouse AS PublishingHouse,
	N.ISSN AS ISSN,
	LO.Note AS Note
FROM dbo.LibraryObjects AS LO
INNER JOIN dbo.Newspapers AS N
ON LO.LibraryObjectId = @NewspaperId AND N.NewspaperId = @NewspaperId
WHERE LO.CheckDelete IS NULL;	

SELECT 
	NI.NewspaperIssueId AS Id,
	NI.Number AS Number,
	NI.PublishingDate AS PublishingDate
FROM dbo.NewspaperIssues AS NI
WHERE NI.NewspaperId = @NewspaperId AND NI.CheckDelete IS NULL;
END
GO

GRANT EXECUTE ON OBJECT::dbo.SelectByIdNewspapers
    TO User_role;

CREATE OR ALTER PROCEDURE SelectByIdPersons
@PersonId INT
AS
BEGIN
SELECT 
	Persons.PersonId AS Id, 
	Persons.PersonName AS [Name], 
	Persons.PersonSurname AS Surname
FROM dbo.Persons
WHERE Persons.PersonId = @PersonId;
END
GO

GRANT EXECUTE ON OBJECT::dbo.SelectByIdPersons
    TO User_role;

CREATE OR ALTER PROCEDURE DeletePerson
@PersonId INT
AS
BEGIN
	DELETE FROM dbo.Persons 
	WHERE PersonId = @PersonId;
	RETURN @@ROWCOUNT;
END
GO

GRANT EXECUTE ON OBJECT::dbo.DeletePerson
    TO Admin_role;

CREATE OR ALTER PROCEDURE SelectByTitleLibraryObjects
@Title NVARCHAR(300)
AS
BEGIN

CREATE TABLE #SelectedLibraryObjects
(Id INT NOT NULL,
Title NVARCHAR(300) NOT NULL,
PublishingYear INT NOT NULL,
NumberOfPages INT NOT NULL,
Note NVARCHAR(2000) NULL,
ObjectType TINYINT NOT NULL
);

INSERT INTO #SelectedLibraryObjects (Id, Title, PublishingYear, NumberOfPages, Note, ObjectType) 
SELECT 
	LibraryObjectId,
	Title,
	NumberOfPages,
	PublishingYear,
	Note,
	ObjectType
FROM dbo.LibraryObjects
WHERE Title = @Title AND CheckDelete IS NULL;

SELECT 
	SLO.Id AS Id,
	SLO.Title AS Title,
	SLO.NumberOfPages AS NumberOfPages,
	SLO.PublishingYear AS PublishingYear,
	SLO.Note AS Note,
	B.ISBN AS ISBN,
	B.PublishingCity AS PublishingCity,
	B.PublishingHouse AS PublishingHouse
FROM #SelectedLibraryObjects AS SLO
INNER JOIN dbo.Books AS B
ON B.BookId = SLO.Id;

SELECT 
	BP.BookId AS BookId,
	P.PersonId AS PersonId,
	P.PersonName AS [Name],
	P.PersonSurname AS Surname
FROM #SelectedLibraryObjects AS SLO
INNER JOIN dbo.BooksPersons AS BP
ON BP.BookId = SLO.Id
INNER JOIN dbo.Persons AS P
ON P.PersonId = BP.PersonId;

SELECT
	SLO.Id AS Id,
	SLO.Title AS Title,
	SLO.NumberOfPages AS NumberOfPages,
	SLO.PublishingYear AS PublishingYear,
	SLO.Note AS Note,
	P.Country AS Country,
	P.ApplicationDate AS ApplicationDate,
	P.PublishingDate AS PublishingDate,
	P.RegistrationNumber AS RegistrationNumber
FROM #SelectedLibraryObjects AS SLO
INNER JOIN dbo.Patents AS P
ON SLO.Id = P.PatentId;

SELECT 
	PP.PatentId AS PatentId,
	P.PersonId AS PersonId,
	P.PersonName AS [Name],
	P.PersonSurname AS Surname
FROM #SelectedLibraryObjects AS SLO
INNER JOIN dbo.PatentsPersons AS PP
ON PP.PatentId = SLO.Id
INNER JOIN dbo.Persons AS P
ON P.PersonId = PP.PersonId;

SELECT
	SLO.Id AS Id,
	SLO.Title AS Title,
	SLO.NumberOfPages AS NumberOfPages,
	SLO.PublishingYear AS PublishingYear,
	SLO.Note AS Note,
	N.PublishingCity AS PublishingCity,
	N.PublishingHouse AS PublishingHouse,
	N.ISSN AS ISSN
FROM #SelectedLibraryObjects AS SLO
INNER JOIN dbo.Newspapers AS N
ON SLO.Id = N.NewspaperId;

SELECT
	NI.NewspaperIssueId AS Id,
	NI.NewspaperId AS NewspaperId,
	NI.Number AS Number,
	NI.PublishingDate AS PublishingDate
FROM #SelectedLibraryObjects AS SLO
INNER JOIN dbo.NewspaperIssues AS NI
ON SLO.Id = NI.NewspaperId
WHERE NI.CheckDelete IS NULL;

DROP TABLE #SelectedLibraryObjects;

END
GO
GRANT EXECUTE ON OBJECT::dbo.SelectByTitleLibraryObjects
    TO User_role;

CREATE OR ALTER PROCEDURE SelectAllLibraryObjects
AS
BEGIN

SELECT 
	LO.LibraryObjectId AS Id,
	LO.Title AS Title,
	LO.NumberOfPages AS NumberOfPages,
	LO.PublishingYear AS PublishingYear,
	LO.Note AS Note,
	B.ISBN AS ISBN,
	B.PublishingCity AS PublishingCity,
	B.PublishingHouse AS PublishingHouse
FROM LibraryObjects AS LO
INNER JOIN dbo.Books AS B
ON B.BookId = LO.LibraryObjectId
WHERE LO.CheckDelete IS NULL;

SELECT 
	BP.BookId AS BookId,
	P.PersonId AS PersonId,
	P.PersonName AS [Name],
	P.PersonSurname AS Surname
FROM dbo.LibraryObjects AS LO
INNER JOIN dbo.BooksPersons AS BP
ON LO.LibraryObjectId = BP.BookId
INNER JOIN dbo.Persons AS P
ON P.PersonId = BP.PersonId
WHERE LO.CheckDelete IS NULL;

SELECT
	LO.LibraryObjectId AS Id,
	LO.Title AS Title,
	LO.NumberOfPages AS NumberOfPages,
	LO.PublishingYear AS PublishingYear,
	LO.Note AS Note,
	P.Country AS Country,
	P.ApplicationDate AS ApplicationDate,
	P.PublishingDate AS PublishingDate,
	P.RegistrationNumber AS RegistrationNumber
FROM LibraryObjects AS LO
INNER JOIN dbo.Patents AS P
ON LO.LibraryObjectId = P.PatentId
WHERE LO.CheckDelete IS NULL;

SELECT 
	P.PersonId AS Id,
	P.PersonName AS [Name],
	P.PersonSurname AS Surname,
	PP.PatentId AS PatentId
FROM dbo.LibraryObjects AS LO
INNER JOIN dbo.PatentsPersons AS PP
ON LO.LibraryObjectId = PP.PatentId
INNER JOIN dbo.Persons AS P
ON P.PersonId = PP.PersonId
WHERE LO.CheckDelete IS NULL;

SELECT
	LO.LibraryObjectId AS Id,
	LO.Title AS Title,
	LO.NumberOfPages AS NumberOfPages,
	LO.PublishingYear AS PublishingYear,
	LO.Note AS Note,
	N.PublishingCity AS PublishingCity,
	N.PublishingHouse AS PublishingHouse,
	N.ISSN AS ISSN
FROM LibraryObjects AS LO
INNER JOIN dbo.Newspapers AS N
ON LO.LibraryObjectId = N.NewspaperId
WHERE LO.CheckDelete IS NULL;

SELECT
	NI.NewspaperIssueId AS Id,
	NI.NewspaperId AS NewspaperId,
	NI.Number AS Number,
	NI.PublishingDate AS PublishingDate
FROM dbo.NewspaperIssues AS NI
WHERE NI.CheckDelete IS NULL;

END
GO

GRANT EXECUTE ON OBJECT::dbo.SelectAllLibraryObjects
    TO User_role;

CREATE OR ALTER PROCEDURE DeleteByIdLibraryObject
@IdForDelete INT 
AS
BEGIN
DECLARE @LibraryObjectType INT;
SELECT @LibraryObjectType = ObjectType
FROM dbo.LibraryObjects
WHERE LibraryObjectId = @IdForDelete;

BEGIN TRY
	BEGIN TRANSACTION
	IF @LibraryObjectType = 1 --book
	BEGIN
		IF (SELECT COUNT(*) FROM BooksPersons WHERE BookId = @IdForDelete) <> 0
		BEGIN
		DELETE FROM dbo.BooksPersons
		WHERE BookId = @IdForDelete;
		END
	
		DELETE FROM dbo.Books 
		WHERE BookId = @IdForDelete;
	END
	
	ELSE IF @LibraryObjectType = 2 --newspaper
	BEGIN
		IF (SELECT COUNT(*) FROM NewspaperIssues WHERE  NewspaperId = @IdForDelete) <> 0
		BEGIN
		DELETE FROM dbo.NewspaperIssues
		WHERE NewspaperId = @IdForDelete;
		END
	
		DELETE FROM dbo.Newspapers 
		WHERE NewspaperId = @IdForDelete;
	END
	
	ELSE IF @LibraryObjectType = 3 --patent
	BEGIN
	
		IF (SELECT COUNT(*) FROM PatentsPersons WHERE  PatentId = @IdForDelete) <> 0
		BEGIN
		DELETE FROM dbo.PatentsPersons
		WHERE PatentId = @IdForDelete;
		END

		DELETE FROM dbo.Patents 
		WHERE PatentId = @IdForDelete;
	END
	
	DELETE FROM dbo.LibraryObjects 
	WHERE LibraryObjectId = @IdForDelete;
	COMMIT;

	RETURN @@ROWCOUNT;
	
END TRY
BEGIN CATCH
	ROLLBACK;
	RETURN -1;
END CATCH
END
GO

GRANT EXECUTE ON OBJECT::dbo.DeleteByIdLibraryObject
    TO Admin_role;

CREATE OR ALTER PROCEDURE SelectByAuthorBooks
@AuthorId INT
AS
BEGIN
SELECT
	LO.LibraryObjectId AS Id,
	LO.NumberOfPages AS NumberOfPages,
	LO.Note AS Note,
	LO.PublishingYear AS PublishingYear,
	LO.Title AS Title,
	B.ISBN AS ISBN,
	B.PublishingCity AS PublishingCity,
	B.PublishingHouse AS PublishingHouse
FROM dbo.LibraryObjects AS LO
INNER JOIN dbo.Books AS B
ON B.BookId = LO.LibraryObjectId
INNER JOIN dbo.BooksPersons AS BP
ON BP.BookId = B.BookId AND BP.PersonId = @AuthorId
WHERE LO.CheckDelete IS NULL;

SELECT 
	BP2.BookId AS BookId,
	P.PersonId AS PersonId,
	P.PersonName AS [Name],
	P.PersonSurname AS Surname
FROM dbo.BooksPersons AS BP
INNER JOIN dbo.BooksPersons AS BP2
ON BP2.BookId = BP.BookId
INNER JOIN dbo.Persons AS P
ON P.PersonId = BP2.PersonId
WHERE BP.PersonId = @AuthorId;
END
GO

GRANT EXECUTE ON OBJECT::dbo.SelectByAuthorBooks
    TO User_role;

CREATE OR ALTER PROCEDURE SelectByInventorPatents
@InventorId INT
AS
BEGIN
SELECT
	LO.LibraryObjectId AS Id,
	LO.NumberOfPages AS NumberOfPages,
	LO.Note AS Note,
	LO.PublishingYear AS PublishingYear,
	LO.Title AS Title,
	P.ApplicationDate AS ApplicationDate,
	P.Country AS Country,
	P.PublishingDate AS PublishingDate,
	P.RegistrationNumber AS RegistrationNumber
FROM dbo.LibraryObjects AS LO
INNER JOIN dbo.Patents AS P
ON P.PatentId = LO.LibraryObjectId
INNER JOIN dbo.PatentsPersons AS PP
ON PP.PatentId = P.PatentId AND PP.PersonId = @InventorId
WHERE LO.CheckDelete IS NULL;

SELECT 
	PP.PatentId AS PatentId,
	P.PersonId AS PersonId,
	P.PersonName AS [Name],
	P.PersonSurname AS Surname
FROM dbo.PatentsPersons AS PP
INNER JOIN dbo.PatentsPersons AS PP2
ON PP2.PatentId = PP.PatentId
INNER JOIN dbo.Persons AS P
ON P.PersonId = PP2.PersonId
WHERE PP.PersonId = @InventorId;

END
GO

GRANT EXECUTE ON OBJECT::dbo.SelectByInventorPatents
    TO User_role;

CREATE OR ALTER PROCEDURE SelectBooksPatentsByPerson
@PersonId INT
AS
BEGIN
SELECT
	LO.LibraryObjectId AS Id,
	LO.NumberOfPages AS NumberOfPages,
	LO.Note AS Note,
	LO.PublishingYear AS PublishingYear,
	LO.Title AS Title,
	P.ApplicationDate AS ApplicationDate,
	P.Country AS Country,
	P.PublishingDate AS PublishingDate,
	P.RegistrationNumber AS RegistrationNumber
FROM dbo.LibraryObjects AS LO
INNER JOIN dbo.Patents AS P
ON P.PatentId = LO.LibraryObjectId
INNER JOIN dbo.PatentsPersons AS PP
ON PP.PatentId = P.PatentId AND PP.PersonId = @PersonId
WHERE LO.CheckDelete IS NULL;

SELECT 
	PP.PatentId AS PatentId,
	P.PersonId AS PersonId,
	P.PersonName AS [Name],
	P.PersonSurname AS Surname
FROM dbo.PatentsPersons AS PP
INNER JOIN dbo.PatentsPersons AS PP2
ON PP2.PatentId = PP.PatentId
INNER JOIN dbo.Persons AS P
ON P.PersonId = PP2.PersonId
WHERE PP.PersonId = @PersonId;


SELECT
	LO.LibraryObjectId AS Id,
	LO.NumberOfPages AS NumberOfPages,
	LO.Note AS Note,
	LO.PublishingYear AS PublishingYear,
	LO.Title AS Title,
	B.ISBN AS ISBN,
	B.PublishingCity AS PublishingCity,
	B.PublishingHouse AS PublishingHouse
FROM dbo.LibraryObjects AS LO
INNER JOIN dbo.Books AS B
ON B.BookId = LO.LibraryObjectId
INNER JOIN dbo.BooksPersons AS BP
ON BP.BookId = B.BookId AND BP.PersonId = @PersonId
WHERE LO.CheckDelete IS NULL;

SELECT 
	BP2.BookId AS BookId,
	P.PersonId AS PersonId,
	P.PersonName AS [Name],
	P.PersonSurname AS Surname
FROM dbo.BooksPersons AS BP
INNER JOIN dbo.BooksPersons AS BP2
ON BP2.BookId = BP.BookId
INNER JOIN dbo.Persons AS P
ON P.PersonId = BP2.PersonId
WHERE BP.PersonId = @PersonId;
END
GO

GRANT EXECUTE ON OBJECT::dbo.SelectBooksPatentsByPerson
    TO User_role;

CREATE OR ALTER PROCEDURE SelectByPublishingHouseBooks
@PublishingHouseFilter NVARCHAR (300)
AS
BEGIN
DECLARE @PublishingHousePattern NVARCHAR (300) = @PublishingHouseFilter + '%';

SELECT 
	LO.LibraryObjectId AS Id,
	LO.Title AS Title,
	LO.NumberOfPages AS NumberOfPages,
	LO.PublishingYear AS PublishingYear,
	LO.Note AS Note,
	B.ISBN AS ISBN,
	B.PublishingCity AS PublishingCity,
	B.PublishingHouse AS PublishingHouse
FROM dbo.LibraryObjects AS LO
INNER JOIN dbo.Books AS B
ON B.BookId = LO.LibraryObjectId 
WHERE B.PublishingHouse LIKE @PublishingHousePattern AND LO.CheckDelete IS NULL;

SELECT
	BP.BookId AS BookId,
	P.PersonId AS Id,
	P.PersonName AS [Name],
	P.PersonSurname AS Surname
FROM dbo.Books AS B
INNER JOIN dbo.BooksPersons AS BP
ON B.BookId = BP.BookId
INNER JOIN dbo.Persons AS P
ON P.PersonId = BP.PersonId
WHERE B.PublishingHouse LIKE @PublishingHousePattern;

END
GO

GRANT EXECUTE ON OBJECT::dbo.SelectByPublishingHouseBooks
    TO User_role;

CREATE OR ALTER PROCEDURE InsertPerson
@Name NVARCHAR (50),
@Surname NVARCHAR (200),
@Id INT OUTPUT
AS
BEGIN
	INSERT INTO Persons (PersonName, PersonSurname)
	VALUES (@Name, @Surname);	
	SET @Id = SCOPE_IDENTITY();
END
GO

GRANT EXECUTE ON OBJECT::dbo.InsertPerson
    TO Librarian_role;

CREATE NONCLUSTERED INDEX IX_NewspaperIssue_PublishingDate  
    ON dbo.NewspaperIssues (PublishingDate);
GO

CREATE OR ALTER PROCEDURE InsertNewspaperIssue
@Number INT,
@PublishingDate DATE,
@NewspaperId INT,
@Id INT OUTPUT
AS
BEGIN

BEGIN TRY
	BEGIN TRANSACTION
	
	DECLARE @NewspaperPublishingYear INT;
	SET @NewspaperPublishingYear = (SELECT PublishingYear
	FROM dbo.LibraryObjects
	WHERE LibraryObjectId = @NewspaperId);
	
	IF NOT EXISTS (SELECT PublishingDate
	FROM dbo.NewspaperIssues
	WHERE NewspaperId = @NewspaperId AND PublishingDate = @PublishingDate)
	AND @NewspaperPublishingYear = YEAR(@PublishingDate)
	BEGIN
		INSERT INTO NewspaperIssues (NewspaperId, Number, PublishingDate, CheckDelete)
		VALUES (@NewspaperId, @Number, @PublishingDate, NULL);
	COMMIT;
		SET @Id = SCOPE_IDENTITY();
	END
	ELSE
	BEGIN
		ROLLBACK;
		RETURN -1;
	END
END TRY
BEGIN CATCH
	THROW;
	RETURN -1;
END CATCH
END

GO

GRANT EXECUTE ON OBJECT::dbo.InsertNewspaperIssue
    TO Librarian_role;

CREATE NONCLUSTERED INDEX IX_Newspaper_ISSN 
    ON dbo.Newspapers (ISSN);
GO

CREATE NONCLUSTERED INDEX IX_LibraryObject_Title  
    ON dbo.LibraryObjects (Title);
GO

CREATE OR ALTER PROCEDURE InsertNewspaper
@Title NVARCHAR (300),
@NumberOfPages INT,
@PublishingYear INT,
@Note NVARCHAR (2000),
@PublishingCity NVARCHAR (200),
@PublishingHouse NVARCHAR (300),
@ISSN VARCHAR (13),
@Id INT OUTPUT
AS
BEGIN
BEGIN TRY
	BEGIN TRANSACTION

	DECLARE @NewspaperId INT;
	
	IF @ISSN IS NULL
	BEGIN
		IF EXISTS (SELECT *
		FROM dbo.LibraryObjects AS LO
		INNER JOIN dbo.Newspapers AS N
		ON LO.LibraryObjectId = N.NewspaperId
		WHERE ObjectType = 2 AND Title = @Title AND PublishingHouse = @PublishingHouse)
		BEGIN
			ROLLBACK;
			RETURN -1;
		END

	END
	ELSE 
	BEGIN
	IF EXISTS (SELECT *
		FROM dbo.Newspapers AS N
		INNER JOIN dbo.LibraryObjects AS LO
		ON LO.LibraryObjectId = N.NewspaperId 
		WHERE N.ISSN = @ISSN)
	IF NOT EXISTS (SELECT *
		FROM dbo.Newspapers AS N
		INNER JOIN dbo.LibraryObjects AS LO
		ON LO.LibraryObjectId = N.NewspaperId 
		WHERE N.ISSN = @ISSN AND LO.Title = @Title)
		BEGIN
			ROLLBACK;
			RETURN -1;
		END
	END

		INSERT INTO LibraryObjects (Title, NumberOfPages, PublishingYear, Note, ObjectType, CheckDelete)
		VALUES (@Title, @NumberOfPages, @PublishingYear, @Note, 2, NULL);
	
		SET @NewspaperId = SCOPE_IDENTITY();
	
		INSERT INTO Newspapers (NewspaperId, PublishingCity, PublishingHouse, ISSN)
		VALUES (@NewspaperId, @PublishingCity, @PublishingHouse, @ISSN);
		SET @Id = @NewspaperId;
		
		COMMIT;
END TRY

BEGIN CATCH
	THROW;
	RETURN -1;
END CATCH

END
GO

GRANT EXECUTE ON OBJECT::dbo.InsertNewspaper
    TO Librarian_role;

CREATE TYPE TableOfPersons AS TABLE(
PersonId INT NOT NULL,
PRIMARY KEY (PersonId)
);

GRANT EXECUTE ON TYPE::dbo.TableOfPersons
    TO Librarian_role;

CREATE NONCLUSTERED INDEX IX_Book_ISBN 
    ON dbo.Books (ISBN);
GO

CREATE NONCLUSTERED INDEX IX_LibraryObject_TitlePublishingYear 
    ON dbo.LibraryObjects (Title, PublishingYear);
GO

CREATE OR ALTER PROCEDURE InsertBook
@Title NVARCHAR (300),
@NumberOfPages INT,
@PublishingYear INT,
@Note NVARCHAR (2000),
@PublishingCity NVARCHAR (200),
@PublishingHouse NVARCHAR (300),
@ISBN VARCHAR (18),
@TableOfPersons TableOfPersons READONLY,
@Id INT OUTPUT
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION	
	DECLARE @InputAuthorsRows INT = (SELECT COUNT(*) FROM @TableOfPersons);
	DECLARE @InputRows INT =  (SELECT COUNT(*)
		FROM dbo.LibraryObjects AS LO
		LEFT JOIN dbo.BooksPersons AS BP
		ON BP.BookId = LO.LibraryObjectId
		INNER JOIN @TableOfPersons AS TP
		ON TP.PersonId = BP.PersonId
		WHERE Title = @Title AND PublishingYear = @PublishingYear);
	IF @ISBN IS NOT NULL
	BEGIN
		IF EXISTS (SELECT * 
		FROM dbo.Books
		WHERE ISBN = @ISBN)
		BEGIN
			ROLLBACK;
			RETURN -1;
		END
	END
	ELSE IF (@InputAuthorsRows = @InputRows)
		BEGIN
			ROLLBACK;
			RETURN -1;
		END

		INSERT INTO LibraryObjects (Title, NumberOfPages, PublishingYear, Note, ObjectType, CheckDelete)
		VALUES (@Title, @NumberOfPages, @PublishingYear, @Note, 1, NULL);
	
		SET @Id = SCOPE_IDENTITY();
	
		INSERT INTO Books (BookId, PublishingCity, PublishingHouse, ISBN)
		VALUES (@Id, @PublishingCity, @PublishingHouse, @ISBN);
		
		CREATE TABLE #Persons ( Id INT);
		INSERT INTO #Persons (Id) 
		SELECT PersonId FROM @TableOfPersons;

		IF (SELECT COUNT(*) FROM @TableOfPersons) <> 0
		BEGIN
		WHILE (SELECT COUNT (*) FROM #Persons) <> 0
		BEGIN
		DECLARE @PersonId INT = (SELECT TOP 1 Id FROM #Persons)

		INSERT INTO BooksPersons (BookId, PersonId)
		SELECT @Id, @PersonId;

		DELETE FROM #Persons WHERE Id = @PersonId
		END
		END

		COMMIT;
END TRY

BEGIN CATCH
		ROLLBACK;
		RETURN -1;
END CATCH

END
GO 

GRANT EXECUTE ON OBJECT::dbo.InsertBook
    TO Librarian_role;

CREATE NONCLUSTERED INDEX IX_Patent_CountryRegistrationNumber
    ON dbo.Patents (Country, RegistrationNumber);
GO

CREATE OR ALTER PROCEDURE InsertPatent
@Title NVARCHAR (300),
@NumberOfPages INT,
@PublishingYear INT,
@Note NVARCHAR (2000),
@Country NVARCHAR (200),
@ApplicationDate DATETIME2,
@PublishingDate DATETIME2,
@RegistrationNumber VARCHAR (9),
@TableOfPersons TableOfPersons READONLY,
@Id INT OUTPUT
AS
BEGIN
BEGIN TRY
	BEGIN TRANSACTION
	IF EXISTS (SELECT *
		FROM dbo.LibraryObjects AS LO
		INNER JOIN dbo.Patents AS P
		ON P.PatentId = LO.LibraryObjectId
		WHERE P.Country = @Country AND P.RegistrationNumber = @RegistrationNumber)
		BEGIN
			ROLLBACK;
			RETURN -1;
		END
	
	ELSE
	BEGIN
		INSERT INTO LibraryObjects (Title, NumberOfPages, PublishingYear, Note, ObjectType, CheckDelete)
		VALUES (@Title, @NumberOfPages, @PublishingYear, @Note, 3, NULL);
	
		DECLARE @PatentId INT = SCOPE_IDENTITY();
	
		INSERT INTO Patents(PatentId, RegistrationNumber, ApplicationDate, PublishingDate, Country)
		VALUES (@PatentId, @RegistrationNumber, @ApplicationDate, @PublishingDate, @Country);
		SET @Id = @PatentId;

		CREATE TABLE #Persons ( Id INT);
		INSERT INTO #Persons (Id) 
		SELECT PersonId FROM @TableOfPersons;

		IF (SELECT COUNT(*) FROM @TableOfPersons) <> 0
		BEGIN
		WHILE (SELECT COUNT (*) FROM #Persons) <> 0
		BEGIN
		DECLARE @PersonId INT = (SELECT TOP 1 Id FROM #Persons)

		INSERT INTO PatentsPersons(PatentId, PersonId)
		SELECT @Id, @PersonId;

		DELETE FROM #Persons WHERE Id = @PersonId
		END
		END

		COMMIT;
		END
END TRY
BEGIN CATCH
		ROLLBACK;
		THROW;
END CATCH

END
GO

GRANT EXECUTE ON OBJECT::dbo.InsertPatent
    TO Librarian_role;


CREATE OR ALTER PROCEDURE CheckObjectLikeDeletedById
@ObjectId INT
AS
BEGIN
BEGIN TRY
	BEGIN TRANSACTION
	DECLARE @ObjectType INT = (SELECT ObjectType FROM LibraryObjects WHERE LibraryObjectId = @ObjectId);
	UPDATE LibraryObjects SET CheckDelete = 1
	WHERE CheckDelete IS NULL AND LibraryObjectId = @ObjectId;

	IF @ObjectType = 3
	UPDATE NewspaperIssues SET CheckDelete = 1
	WHERE NewspaperId = @ObjectId;
	COMMIT
END TRY

BEGIN CATCH
	ROLLBACK;
	RETURN -1;
END CATCH
END
GO

GRANT EXECUTE ON OBJECT::dbo.CheckObjectLikeDeletedById
    TO Librarian_role;

CREATE OR ALTER PROCEDURE SelectAllDeletedObjects
AS
BEGIN
SELECT 
	LO.LibraryObjectId AS Id,
	LO.Title AS Title,
	LO.NumberOfPages AS NumberOfPages,
	LO.PublishingYear AS PublishingYear,
	LO.Note AS Note,
	B.ISBN AS ISBN,
	B.PublishingCity AS PublishingCity,
	B.PublishingHouse AS PublishingHouse
FROM LibraryObjects AS LO
INNER JOIN dbo.Books AS B
ON B.BookId = LO.LibraryObjectId
WHERE LO.CheckDelete = 1;

SELECT 
	BP.BookId AS BookId,
	P.PersonId AS PersonId,
	P.PersonName AS [Name],
	P.PersonSurname AS Surname
FROM dbo.LibraryObjects AS LO
INNER JOIN dbo.BooksPersons AS BP
ON LO.LibraryObjectId = BP.BookId
INNER JOIN dbo.Persons AS P
ON P.PersonId = BP.PersonId
WHERE LO.CheckDelete = 1;

SELECT
	LO.LibraryObjectId AS Id,
	LO.Title AS Title,
	LO.NumberOfPages AS NumberOfPages,
	LO.PublishingYear AS PublishingYear,
	LO.Note AS Note,
	P.Country AS Country,
	P.ApplicationDate AS ApplicationDate,
	P.PublishingDate AS PublishingDate,
	P.RegistrationNumber AS RegistrationNumber
FROM LibraryObjects AS LO
INNER JOIN dbo.Patents AS P
ON LO.LibraryObjectId = P.PatentId
WHERE LO.CheckDelete = 1;

SELECT 
	P.PersonId AS Id,
	P.PersonName AS [Name],
	P.PersonSurname AS Surname,
	PP.PatentId AS PatentId
FROM dbo.LibraryObjects AS LO
INNER JOIN dbo.PatentsPersons AS PP
ON LO.LibraryObjectId = PP.PatentId
INNER JOIN dbo.Persons AS P
ON P.PersonId = PP.PersonId
WHERE LO.CheckDelete = 1;

SELECT
	LO.LibraryObjectId AS Id,
	LO.Title AS Title,
	LO.NumberOfPages AS NumberOfPages,
	LO.PublishingYear AS PublishingYear,
	LO.Note AS Note,
	N.PublishingCity AS PublishingCity,
	N.PublishingHouse AS PublishingHouse,
	N.ISSN AS ISSN
FROM LibraryObjects AS LO
INNER JOIN dbo.Newspapers AS N
ON LO.LibraryObjectId = N.NewspaperId
WHERE LO.CheckDelete = 1;

SELECT
	NI.NewspaperIssueId AS Id,
	NI.NewspaperId AS NewspaperId,
	NI.Number AS Number,
	NI.PublishingDate AS PublishingDate
FROM dbo.NewspaperIssues AS NI
WHERE NI.CheckDelete = 1;

END
GO

GRANT EXECUTE ON OBJECT::dbo.SelectAllDeletedObjects
    TO Admin_role;

CREATE OR ALTER PROCEDURE RestoreObjectById
@ObjectId INT
AS
BEGIN
BEGIN TRY
	BEGIN TRANSACTION
	DECLARE @ObjectType INT = (SELECT ObjectType FROM LibraryObjects WHERE LibraryObjectId = @ObjectId);
	UPDATE LibraryObjects SET CheckDelete = NULL
	WHERE CheckDelete = 1 AND LibraryObjectId = @ObjectId;
	IF @ObjectType = 3

	UPDATE NewspaperIssues SET CheckDelete = NULL
	WHERE NewspaperId = @ObjectId;
	COMMIT
END TRY

BEGIN CATCH
		ROLLBACK;
		RETURN -1;
END CATCH
END
GO

GRANT EXECUTE ON OBJECT::dbo.RestoreObjectById
    TO Admin_role;

CREATE TABLE dbo.LOG(
Id INT NOT NULL IDENTITY (1, 1),
TypeOfObject NVARCHAR(20) NOT NULL,
ObjectId INT NOT NULL,
TextDescription NVARCHAR(MAX) NOT NULL,
UserName NVARCHAR(MAX) NOT NULL,
CreateAt DATETIME  NOT NULL DEFAULT GETDATE(),
CONSTRAINT PK_Id PRIMARY KEY (Id)
);
GO

CREATE OR ALTER TRIGGER InsertNewLibraryObject
ON dbo.LibraryObjects
AFTER INSERT
AS
BEGIN
	INSERT INTO dbo.LOG (TypeOfObject, ObjectId, TextDescription, UserName)
	VALUES ('Library Object', 
			(SELECT CAST(LibraryObjectId AS NVARCHAR(10)) FROM INSERTED), 
			(SELECT 'Insert New Library Object with values: Title: ' + Title +
			', Year: ' + CAST(PublishingYear AS NVARCHAR(10)) + ', Number of pages: ' + CAST(NumberOfPages AS NVARCHAR(10)) FROM INSERTED),
			CURRENT_USER);
END
GO

CREATE OR ALTER TRIGGER InsertNewBook
ON dbo.Books
AFTER INSERT
AS
BEGIN
	INSERT INTO dbo.LOG (TypeOfObject, ObjectId, TextDescription, UserName)
	VALUES ('Book', 
			(SELECT CAST(BookId AS NVARCHAR(10)) FROM INSERTED), 
			(SELECT 'Insert New Book with values: City: ' + PublishingCity + 
			', Publishing House: ' + PublishingHouse + ', ISBN: ' + ISNULL(ISBN, 'NULL') FROM INSERTED),
			CURRENT_USER);
END
GO

CREATE OR ALTER TRIGGER InsertNewNewspaper
ON dbo.Newspapers
AFTER INSERT
AS
BEGIN
	INSERT INTO dbo.LOG (TypeOfObject, ObjectId, TextDescription, UserName)
	VALUES ('Newspaper', 
			(SELECT CAST(NewspaperId AS NVARCHAR(10)) FROM INSERTED), 
			(SELECT 'Insert New Newspaper with values: City: ' + PublishingCity + 
			', Publishing House: ' + PublishingHouse + ', ISSN: ' + ISNULL(ISSN, 'NULL') FROM INSERTED),
			CURRENT_USER);
END
GO


CREATE OR ALTER TRIGGER InsertNewPatent
ON dbo.Patents
AFTER INSERT
AS
BEGIN
	INSERT INTO dbo.LOG (TypeOfObject, ObjectId, TextDescription, UserName)
	VALUES ('Patent', 
			(SELECT CAST(PatentId AS NVARCHAR(10)) FROM INSERTED), 
			(SELECT 'Insert New Patent with values: Country: ' + Country + 
			', Publishing Date: ' + CAST(PublishingDate AS NVARCHAR(10)) + ', Application Date: ' + CAST(ApplicationDate AS NVARCHAR(10)) +
			', Registration number: ' + RegistrationNumber FROM INSERTED),
			CURRENT_USER);
END
GO

CREATE OR ALTER TRIGGER InsertNewNewspaperIssue
ON dbo.NewspaperIssues
AFTER INSERT
AS
BEGIN
	INSERT INTO dbo.LOG (TypeOfObject, ObjectId, TextDescription, UserName)
	VALUES ('NewspaperIssue', 
			(SELECT CAST(NewspaperIssueId AS NVARCHAR(10)) FROM INSERTED), 
			(SELECT 'Insert New Newspaper Issue with values: Number: ' + CAST(Number AS NVARCHAR(10)) + 
			', NewspaperId: ' + CAST(NewspaperId AS NVARCHAR(10)) FROM INSERTED) ,
			CURRENT_USER);
END
GO

SELECT 'My string: ' + ISNULL(CAST('ass' AS NVARCHAR(15)), 'NULL');

CREATE OR ALTER TRIGGER InsertNewPerson
ON dbo.Persons
AFTER INSERT
AS
BEGIN
	INSERT INTO dbo.LOG (TypeOfObject, ObjectId, TextDescription, UserName)
	VALUES ('Person', 
			(SELECT CAST(PersonId AS NVARCHAR(10)) FROM INSERTED), 
			(SELECT 'Insert New Person with values: Name: ' + PersonName + 
			', Surname: ' + PersonSurname FROM INSERTED),
			CURRENT_USER);
END
GO

ALTER TABLE dbo.BooksPersons
ADD Id INT NOT NULL IDENTITY (1, 1)
GO

CREATE OR ALTER TRIGGER InsertNewPersonBook
ON dbo.BooksPersons
AFTER INSERT
AS
BEGIN
	INSERT INTO dbo.LOG (TypeOfObject, ObjectId, TextDescription, UserName)
	VALUES ('PersonBook', 
			(SELECT CAST(Id AS NVARCHAR(10)) FROM INSERTED), 
			(SELECT 'Insert Book Person with values: BookId: ' + CAST(BookId AS NVARCHAR(10)) + 
			', PersonId: ' + CAST(PersonId AS NVARCHAR(10)) FROM INSERTED),
			CURRENT_USER);
END
GO

ALTER TABLE dbo.PatentsPersons
ADD Id INT NOT NULL IDENTITY (1, 1)
GO

CREATE OR ALTER TRIGGER InsertNewPersonPatent
ON dbo.PatentsPersons
AFTER INSERT
AS
BEGIN
	INSERT INTO dbo.LOG (TypeOfObject, ObjectId, TextDescription, UserName)
	VALUES ('PersonPatent', 
			(SELECT CAST(Id AS NVARCHAR(10)) FROM INSERTED),
			(SELECT 'Insert Patent Person with values: PatentId: ' + CAST(PatentId AS NVARCHAR(10)) + 
			', PersonId: ' + CAST(PersonId AS NVARCHAR(10)) FROM INSERTED), CURRENT_USER);
END
GO


CREATE OR ALTER TRIGGER CheckObjectByDeleted
ON dbo.LibraryObjects
AFTER UPDATE
AS
BEGIN
	INSERT INTO dbo.LOG (TypeOfObject, ObjectId, TextDescription, UserName)
	VALUES ('Library Object',
			(SELECT CAST(LibraryObjectId AS NVARCHAR(10)) FROM INSERTED),
			(SELECT 'Update Library Object: Past Values: Title: ' + Title +
			', Year: ' + CAST(PublishingYear AS NVARCHAR(5)) + ', Number of pages: ' + CAST(NumberOfPages AS NVARCHAR(10)) FROM DELETED) + 
			(SELECT ' New Values: Title: ' + Title +
			', Year: ' + CAST(PublishingYear AS NVARCHAR(5)) + ', Number of pages: ' + CAST(NumberOfPages AS NVARCHAR(10)) FROM INSERTED), CURRENT_USER);
END
GO

CREATE OR ALTER TRIGGER CheckNewspaperIssueByDeleted
ON dbo.NewspaperIssues
AFTER UPDATE
AS
BEGIN
	INSERT INTO dbo.LOG (TypeOfObject, ObjectId, TextDescription, UserName)
	VALUES ('NewspaperIssue', 
			(SELECT CAST(NewspaperIssueId AS NVARCHAR(10)) FROM DELETED), 
			(SELECT 'Delete Newspaper Issue: Past values: Number: ' + CAST(Number AS NVARCHAR(10)) + 
			', NewspaperId: ' + CAST(NewspaperId AS NVARCHAR(10)) FROM DELETED) +
			(SELECT ' New values: Number: ' + CAST(Number AS NVARCHAR(10)) + 
			', NewspaperId: ' + CAST(NewspaperId AS NVARCHAR(10)) FROM INSERTED),
			CURRENT_USER);
END
GO

CREATE OR ALTER TRIGGER DeleteLibraryObject
ON dbo.LibraryObjects
AFTER DELETE
AS
BEGIN
	INSERT INTO dbo.LOG (TypeOfObject, ObjectId, TextDescription, UserName)
	VALUES ('Library Object',
			(SELECT CAST(LibraryObjectId AS NVARCHAR(10)) FROM DELETED),
			(SELECT 'Delete Library Object with Values: Title: ' + Title +
			', Year: ' + CAST(PublishingYear AS NVARCHAR(5)) + ', Number of pages: ' + CAST(NumberOfPages AS NVARCHAR(10)) FROM DELETED),
			CURRENT_USER);
END
GO
	
CREATE OR ALTER TRIGGER DeleteBook
ON dbo.Books
AFTER DELETE
AS
BEGIN
	INSERT INTO dbo.LOG (TypeOfObject, ObjectId, TextDescription, UserName)
	VALUES ('Book', 
			(SELECT CAST(BookId AS NVARCHAR(10)) FROM DELETED), 
			(SELECT 'Delete Book with values: City: ' + PublishingCity + 
			', Publishing House: ' + PublishingHouse + ', ISBN: ' + ISNULL(ISBN, 'NULL') FROM DELETED),
			CURRENT_USER);
END
GO

CREATE OR ALTER TRIGGER DeletePatent
ON dbo.Patents
AFTER DELETE
AS
BEGIN
	INSERT INTO dbo.LOG (TypeOfObject, ObjectId, TextDescription, UserName)
	VALUES ('Patent', 
			(SELECT CAST(PatentId AS NVARCHAR(10)) FROM DELETED), 
			(SELECT 'Delete Patent with values: Country: ' + Country + 
			', Publishing Date: ' + CAST(PublishingDate AS NVARCHAR(10)) + ', Application Date: ' + ISNULL(CAST(ApplicationDate AS NVARCHAR(10)), 'NULL') +
			', Registration number: ' + RegistrationNumber FROM DELETED),
			CURRENT_USER);
END
GO

CREATE OR ALTER TRIGGER DeleteNewspaper
ON dbo.Newspapers
AFTER DELETE
AS
BEGIN
	INSERT INTO dbo.LOG (TypeOfObject, ObjectId, TextDescription, UserName)
	VALUES ('Newspaper', 
			(SELECT CAST(NewspaperId AS NVARCHAR(10)) FROM DELETED), 
			(SELECT 'Insert New Newspaper with values: City: ' + PublishingCity + 
			', Publishing House: ' + PublishingHouse + ', ISSN: ' + ISNULL(ISSN, 'NULL') FROM DELETED),
			CURRENT_USER);
END
GO

CREATE OR ALTER TRIGGER DeleteNewspaperIssue
ON dbo.NewspaperIssues
AFTER DELETE
AS
BEGIN
	INSERT INTO dbo.LOG (TypeOfObject, ObjectId, TextDescription, UserName)
	VALUES ('NewspaperIssue', 
			(SELECT CAST(NewspaperIssueId AS NVARCHAR(10)) FROM DELETED), 
			(SELECT 'Delete Newspaper Issue with values: Number: ' + CAST(Number AS NVARCHAR(10)) + 
			', NewspaperId: ' + CAST(NewspaperId AS NVARCHAR(10)) FROM DELETED) ,
			CURRENT_USER);
END
GO

CREATE OR ALTER TRIGGER DeleteNewPerson
ON dbo.Persons
AFTER DELETE
AS
BEGIN
	INSERT INTO dbo.LOG (TypeOfObject, ObjectId, TextDescription, UserName)
	VALUES ('Person', 
			(SELECT CAST(PersonId AS NVARCHAR(10)) FROM DELETED), 
			(SELECT 'Delete Person with values: Name: ' + PersonName + 
			', Surname: ' + PersonSurname FROM DELETED),
			CURRENT_USER);
END
GO

CREATE OR ALTER TRIGGER DeletePersonBook
ON dbo.BooksPersons
AFTER DELETE
AS
BEGIN
	INSERT INTO dbo.LOG (TypeOfObject, ObjectId, TextDescription, UserName)
	VALUES ('PersonBook', 
			(SELECT CAST(Id AS NVARCHAR(10)) FROM DELETED), 
			(SELECT 'Delete BookPerson with values: BookId: ' + CAST(BookId AS NVARCHAR(10)) + 
			', PersonId: ' + CAST(PersonId AS NVARCHAR(10)) FROM DELETED),
			CURRENT_USER);
END
GO

CREATE OR ALTER TRIGGER DeletePersonPatent
ON dbo.PatentsPersons
AFTER DELETE
AS
BEGIN
	INSERT INTO dbo.LOG (TypeOfObject, ObjectId, TextDescription, UserName)
	VALUES ('PersonPatent', 
			(SELECT CAST(Id AS NVARCHAR(10)) FROM DELETED),
			(SELECT 'Delete PatentPerson with values: PatentId: ' + CAST(PatentId AS NVARCHAR(10))+ 
			', PersonId: ' + CAST(PersonId AS NVARCHAR(10)) FROM DELETED), CURRENT_USER);
END
GO

CREATE NONCLUSTERED INDEX IX_NewspaperIssue_NewspaperIdNewspaperIssueId
ON dbo.NewspaperIssues (NewspaperId, NewspaperIssueId);
GO

CREATE OR ALTER PROCEDURE DeleteNewspaperIssueById
@NewspaperId INT,
@NewspaperIssueId INT
AS
BEGIN
	DELETE FROM dbo.NewspaperIssues 
	WHERE NewspaperId = @NewspaperId AND NewspaperIssueId = @NewspaperIssueId;

	RETURN @@ROWCOUNT;
END

GO

CREATE OR ALTER PROCEDURE CheckNewspaperIssueLikeDeletedById
@NewspaperId INT,
@NewspaperIssueId INT
AS
BEGIN
	UPDATE dbo.NewspaperIssues SET CheckDelete = 1
	WHERE NewspaperId = @NewspaperId AND NewspaperIssueId = @NewspaperIssueId;

	RETURN @@ROWCOUNT;
END

GO

CREATE OR ALTER PROCEDURE RestoreNewspaperIssue
@NewspaperId INT,
@NewspaperIssueId INT
AS
BEGIN
	UPDATE dbo.NewspaperIssues SET CheckDelete = NULL
	WHERE NewspaperId = @NewspaperId AND NewspaperIssueId = @NewspaperIssueId;

	RETURN @@ROWCOUNT;
END

GO



	DELETE FROM PatentsPersons
	DELETE FROM Books
	DELETE FROM Patents
	DELETE FROM NewspaperIssues
	DELETE FROM Newspapers
	DELETE FROM LibraryObjects
	SELECT * FROM LibraryObjects


CREATE OR ALTER PROCEDURE SelectLibraryObjectsForPage
@PageNum INT,
@PageCount INT
AS
BEGIN

SELECT 
	LO.LibraryObjectId AS Id,
	LO.Title AS Title,
	LO.NumberOfPages AS NumberOfPages,
	LO.PublishingYear AS PublishingYear,
	LO.Note AS Note,
	B.ISBN AS ISBN,
	B.PublishingCity AS PublishingCity,
	B.PublishingHouse AS PublishingHouse
FROM LibraryObjects AS LO
INNER JOIN dbo.Books AS B
ON B.BookId = LO.LibraryObjectId
WHERE LO.CheckDelete IS NULL
ORDER BY LO.LibraryObjectId
OFFSET (@PageNum - 1)*@PageCount ROWS 
FETCH NEXT @PageCount ROWS ONLY;

SELECT 
	BP.BookId AS BookId,
	P.PersonId AS PersonId,
	P.PersonName AS [Name],
	P.PersonSurname AS Surname
FROM dbo.LibraryObjects AS LO
INNER JOIN dbo.BooksPersons AS BP
ON LO.LibraryObjectId = BP.BookId
INNER JOIN dbo.Persons AS P
ON P.PersonId = BP.PersonId
WHERE LO.CheckDelete IS NULL;

SELECT
	LO.LibraryObjectId AS Id,
	LO.Title AS Title,
	LO.NumberOfPages AS NumberOfPages,
	LO.PublishingYear AS PublishingYear,
	LO.Note AS Note,
	P.Country AS Country,
	P.ApplicationDate AS ApplicationDate,
	P.PublishingDate AS PublishingDate,
	P.RegistrationNumber AS RegistrationNumber
FROM LibraryObjects AS LO
INNER JOIN dbo.Patents AS P
ON LO.LibraryObjectId = P.PatentId
WHERE LO.CheckDelete IS NULL
ORDER BY LO.LibraryObjectId
OFFSET (@PageNum - 1)*@PageCount ROWS 
FETCH NEXT @PageCount ROWS ONLY;;

SELECT 
	P.PersonId AS Id,
	P.PersonName AS [Name],
	P.PersonSurname AS Surname,
	PP.PatentId AS PatentId
FROM dbo.LibraryObjects AS LO
INNER JOIN dbo.PatentsPersons AS PP
ON LO.LibraryObjectId = PP.PatentId
INNER JOIN dbo.Persons AS P
ON P.PersonId = PP.PersonId
WHERE LO.CheckDelete IS NULL;

SELECT
	LO.LibraryObjectId AS Id,
	LO.Title AS Title,
	LO.NumberOfPages AS NumberOfPages,
	LO.PublishingYear AS PublishingYear,
	LO.Note AS Note,
	N.PublishingCity AS PublishingCity,
	N.PublishingHouse AS PublishingHouse,
	N.ISSN AS ISSN
FROM LibraryObjects AS LO
INNER JOIN dbo.Newspapers AS N
ON LO.LibraryObjectId = N.NewspaperId
WHERE LO.CheckDelete IS NULL
ORDER BY LO.LibraryObjectId
OFFSET (@PageNum - 1)*@PageCount ROWS 
FETCH NEXT @PageCount ROWS ONLY;;

SELECT
	NI.NewspaperIssueId AS Id,
	NI.NewspaperId AS NewspaperId,
	NI.Number AS Number,
	NI.PublishingDate AS PublishingDate
FROM dbo.NewspaperIssues AS NI
WHERE NI.CheckDelete IS NULL;

END
GO

GRANT EXECUTE ON OBJECT::dbo.SelectLibraryObjectsForPage
    TO User_role;



CREATE OR ALTER PROCEDURE SelectLibraryObjectsForPageSortByYearDirect
@PageNum INT,
@PageCount INT
AS
BEGIN

SELECT 
	LO.LibraryObjectId AS Id,
	LO.Title AS Title,
	LO.NumberOfPages AS NumberOfPages,
	LO.PublishingYear AS PublishingYear,
	LO.Note AS Note,
	B.ISBN AS ISBN,
	B.PublishingCity AS PublishingCity,
	B.PublishingHouse AS PublishingHouse
FROM LibraryObjects AS LO
INNER JOIN dbo.Books AS B
ON B.BookId = LO.LibraryObjectId
WHERE LO.CheckDelete IS NULL
ORDER BY LO.PublishingYear
OFFSET (@PageNum - 1)*@PageCount ROWS 
FETCH NEXT @PageCount ROWS ONLY;

SELECT 
	BP.BookId AS BookId,
	P.PersonId AS PersonId,
	P.PersonName AS [Name],
	P.PersonSurname AS Surname
FROM dbo.LibraryObjects AS LO
INNER JOIN dbo.BooksPersons AS BP
ON LO.LibraryObjectId = BP.BookId
INNER JOIN dbo.Persons AS P
ON P.PersonId = BP.PersonId
WHERE LO.CheckDelete IS NULL;

SELECT
	LO.LibraryObjectId AS Id,
	LO.Title AS Title,
	LO.NumberOfPages AS NumberOfPages,
	LO.PublishingYear AS PublishingYear,
	LO.Note AS Note,
	P.Country AS Country,
	P.ApplicationDate AS ApplicationDate,
	P.PublishingDate AS PublishingDate,
	P.RegistrationNumber AS RegistrationNumber
FROM LibraryObjects AS LO
INNER JOIN dbo.Patents AS P
ON LO.LibraryObjectId = P.PatentId
WHERE LO.CheckDelete IS NULL
ORDER BY LO.PublishingYear
OFFSET (@PageNum - 1)*@PageCount ROWS 
FETCH NEXT @PageCount ROWS ONLY;;

SELECT 
	P.PersonId AS Id,
	P.PersonName AS [Name],
	P.PersonSurname AS Surname,
	PP.PatentId AS PatentId
FROM dbo.LibraryObjects AS LO
INNER JOIN dbo.PatentsPersons AS PP
ON LO.LibraryObjectId = PP.PatentId
INNER JOIN dbo.Persons AS P
ON P.PersonId = PP.PersonId
WHERE LO.CheckDelete IS NULL;

SELECT
	LO.LibraryObjectId AS Id,
	LO.Title AS Title,
	LO.NumberOfPages AS NumberOfPages,
	LO.PublishingYear AS PublishingYear,
	LO.Note AS Note,
	N.PublishingCity AS PublishingCity,
	N.PublishingHouse AS PublishingHouse,
	N.ISSN AS ISSN
FROM LibraryObjects AS LO
INNER JOIN dbo.Newspapers AS N
ON LO.LibraryObjectId = N.NewspaperId
WHERE LO.CheckDelete IS NULL
ORDER BY LO.PublishingYear
OFFSET (@PageNum - 1)*@PageCount ROWS 
FETCH NEXT @PageCount ROWS ONLY;;

SELECT
	NI.NewspaperIssueId AS Id,
	NI.NewspaperId AS NewspaperId,
	NI.Number AS Number,
	NI.PublishingDate AS PublishingDate
FROM dbo.NewspaperIssues AS NI
WHERE NI.CheckDelete IS NULL;

END
GO

GRANT EXECUTE ON OBJECT::dbo.SelectLibraryObjectsForPageSortByYearDirect
    TO User_role;


CREATE OR ALTER PROCEDURE SelectLibraryObjectsForPageSortByYearReverse
@PageNum INT,
@PageCount INT
AS
BEGIN

SELECT 
	LO.LibraryObjectId AS Id,
	LO.Title AS Title,
	LO.NumberOfPages AS NumberOfPages,
	LO.PublishingYear AS PublishingYear,
	LO.Note AS Note,
	B.ISBN AS ISBN,
	B.PublishingCity AS PublishingCity,
	B.PublishingHouse AS PublishingHouse
FROM LibraryObjects AS LO
INNER JOIN dbo.Books AS B
ON B.BookId = LO.LibraryObjectId
WHERE LO.CheckDelete IS NULL
ORDER BY LO.PublishingYear DESC
OFFSET (@PageNum - 1)*@PageCount ROWS 
FETCH NEXT @PageCount ROWS ONLY;

SELECT 
	BP.BookId AS BookId,
	P.PersonId AS PersonId,
	P.PersonName AS [Name],
	P.PersonSurname AS Surname
FROM dbo.LibraryObjects AS LO
INNER JOIN dbo.BooksPersons AS BP
ON LO.LibraryObjectId = BP.BookId
INNER JOIN dbo.Persons AS P
ON P.PersonId = BP.PersonId
WHERE LO.CheckDelete IS NULL;

SELECT
	LO.LibraryObjectId AS Id,
	LO.Title AS Title,
	LO.NumberOfPages AS NumberOfPages,
	LO.PublishingYear AS PublishingYear,
	LO.Note AS Note,
	P.Country AS Country,
	P.ApplicationDate AS ApplicationDate,
	P.PublishingDate AS PublishingDate,
	P.RegistrationNumber AS RegistrationNumber
FROM LibraryObjects AS LO
INNER JOIN dbo.Patents AS P
ON LO.LibraryObjectId = P.PatentId
WHERE LO.CheckDelete IS NULL
ORDER BY LO.PublishingYear DESC
OFFSET (@PageNum - 1)*@PageCount ROWS 
FETCH NEXT @PageCount ROWS ONLY;;

SELECT 
	P.PersonId AS Id,
	P.PersonName AS [Name],
	P.PersonSurname AS Surname,
	PP.PatentId AS PatentId
FROM dbo.LibraryObjects AS LO
INNER JOIN dbo.PatentsPersons AS PP
ON LO.LibraryObjectId = PP.PatentId
INNER JOIN dbo.Persons AS P
ON P.PersonId = PP.PersonId
WHERE LO.CheckDelete IS NULL;

SELECT
	LO.LibraryObjectId AS Id,
	LO.Title AS Title,
	LO.NumberOfPages AS NumberOfPages,
	LO.PublishingYear AS PublishingYear,
	LO.Note AS Note,
	N.PublishingCity AS PublishingCity,
	N.PublishingHouse AS PublishingHouse,
	N.ISSN AS ISSN
FROM LibraryObjects AS LO
INNER JOIN dbo.Newspapers AS N
ON LO.LibraryObjectId = N.NewspaperId
WHERE LO.CheckDelete IS NULL
ORDER BY LO.PublishingYear DESC
OFFSET (@PageNum - 1)*@PageCount ROWS 
FETCH NEXT @PageCount ROWS ONLY;;

SELECT
	NI.NewspaperIssueId AS Id,
	NI.NewspaperId AS NewspaperId,
	NI.Number AS Number,
	NI.PublishingDate AS PublishingDate
FROM dbo.NewspaperIssues AS NI
WHERE NI.CheckDelete IS NULL;

END
GO

GRANT EXECUTE ON OBJECT::dbo.SelectLibraryObjectsForPageSortByYearReverse
    TO User_role;