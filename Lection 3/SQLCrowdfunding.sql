USE Crowdfunding;

BEGIN TRANSACTION;	
BEGIN TRY
	
	--CREATING TABLE "USER"
	IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'User')
		BEGIN
			CREATE TABLE [User]
			(
				Id INT IDENTITY(1,1) PRIMARY KEY,
				Name NVARCHAR(50) NOT NULL,
				SecondName NVARCHAR(50) NOT NULL
			);

			PRINT 'Table "User" created';
		END
	ELSE
		BEGIN
			PRINT 'Table "User" already exists';
		END

	--CREATING TABLE "CATEGORY"
	IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Category')
		BEGIN
			CREATE TABLE Category
			(
				Id INT IDENTITY(1,1) PRIMARY KEY,
				Description NVARCHAR(256),
			);
			PRINT 'Table "Category" created';
		END
	ELSE
		BEGIN
			PRINT 'Table "Category" already exists';
		END

	--CREATING TABLE "PROJECT"
	IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Project')
		BEGIN
			CREATE TABLE Project
			(
				Id INT IDENTITY(1,1) PRIMARY KEY,
				Name NVARCHAR(50),
				Description NVARCHAR(200),
				CreationDate DATETIME DEFAULT GETDATE(),

				CreatorId INT,
				CategoryId INT,

				CONSTRAINT P_CreatorId FOREIGN KEY (CreatorId) REFERENCES [User](Id) ON DELETE NO ACTION, 
				CONSTRAINT P_CategoryId FOREIGN KEY (CategoryId) REFERENCES Category(Id) ON DELETE NO ACTION
			);
		PRINT 'Table "Project" created';
		END
	ELSE
		BEGIN
			PRINT 'Table "Project" already exists';
		END

	--CREATING TABLE "COMMENT"
	IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Comment')
		BEGIN
			CREATE TABLE Comment
			(
				Id INT IDENTITY(1,1) PRIMARY KEY,
				Text NVARCHAR(256),
				Date DATETIME DEFAULT GETDATE(),

				UserId INT,
				ProjectId INT,

				CONSTRAINT C_UserId FOREIGN KEY (UserId) REFERENCES [User](Id) ON DELETE NO ACTION, 
				CONSTRAINT C_ProjectId FOREIGN KEY (ProjectId) REFERENCES Project(Id) ON DELETE NO ACTION
			);
		PRINT 'Table "Comment" created';
		END
	ELSE
		BEGIN
			PRINT 'Table "Comment" already exists';
		END

	--CREATING TABLE "VOTE"
	IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Vote')
		BEGIN
			CREATE TABLE Vote
			(
				Id INT IDENTITY(1,1) PRIMARY KEY,
				DateVote DATETIME DEFAULT GETDATE(),
				UserId INT,
				ProjectId INT,

				CONSTRAINT V_UserId FOREIGN KEY (UserId) REFERENCES [User](Id) ON DELETE NO ACTION, 
				CONSTRAINT V_ProjectId FOREIGN KEY (ProjectId) REFERENCES Project(Id) ON DELETE NO ACTION
			);
		PRINT 'Table "Vote" created';
		END
	ELSE
		BEGIN
			PRINT 'Table "Vote" already exists';
		END

END TRY

BEGIN CATCH

	ROLLBACK TRANSACTION;

	DECLARE @ErrorMessage NVARCHAR(MAX) = '';
	SET @ErrorMessage = ERROR_MESSAGE();

	PRINT 'Migration Failed: ' + @ErrorMessage;

END CATCH;

--PROJECT CREATION LOGIC
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE CreateProject
	@Name NVARCHAR(50),
	@Description NVARCHAR(200) = NULL,
	@CreatorId INT NULL = NULL,
	@CategoryId INT NULL = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	BEGIN TRANSACTION;
	BEGIN TRY

		IF((SELECT COUNT(*) FROM Project WHERE Name = @Name) > 0)
		BEGIN

			COMMIT TRANSACTION;
			RETURN;

		END

		IF((SELECT COUNT(*) FROM Category WHERE Id = @CategoryId) = 0)
		BEGIN
			
			PRINT 'Failed. Such a CATEGORY does not exist.';
			COMMIT TRANSACTION;
			RETURN;

		END

		IF((SELECT COUNT(*) FROM [User] WHERE Id = @CreatorId) = 0)
		BEGIN

			PRINT 'Failed. Such a USER does not exist.';
			COMMIT TRANSACTION;
			RETURN;

		END

		INSERT INTO Project (Name, Description, CategoryId, CreatorId)
		VALUES (@Name, @Description, @CategoryId, @CreatorId)

		COMMIT TRANSACTION;

	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION;

		DECLARE @ErrorMessage NVARCHAR(MAX) = '';
		SET @ErrorMessage = ERROR_MESSAGE();

		PRINT 'Creating project failed: ' + @ErrorMessage;

	END CATCH;

END
GO

--ADDING NEW COMMENT
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO 

CREATE PROCEDURE AddingComment
	@Text NVARCHAR(256),
	@UserId INT NULL = NULL,
	@ProjectId INT NULL = NULL
AS
BEGIN
	SET NOCOUNT ON
	
	BEGIN TRANSACTION
	BEGIN TRY

		IF((SELECT COUNT(*) FROM [User] WHERE Id = @UserId) = 0)
		BEGIN

			PRINT 'Failed. Such a USER does not exist.';
			COMMIT TRANSACTION;
			RETURN;

		END
		
		
		IF((SELECT COUNT(*) FROM Project WHERE Id = @ProjectId) = 0)
		BEGIN

			PRINT 'Failed. Such a PROJECT does not exist.';
			COMMIT TRANSACTION;
			RETURN;

		END

		INSERT INTO Comment(Text, UserId, ProjectId)
		VALUES (@Text, @UserId, @ProjectId)

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH

		ROLLBACK TRANSACTION;

		DECLARE @ErrorMessage NVARCHAR(MAX) = '';
		SET @ErrorMessage = ERROR_MESSAGE();

		PRINT 'Adding comment failed: ' + @ErrorMessage;

	END CATCH

END
GO

--ADDING VOIT
CREATE PROCEDURE IncertVote
	@UserId INT NULL = NULL,
	@ProjectId INT NULL = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	BEGIN TRANSACTION;
	BEGIN TRY

		IF((SELECT COUNT(*) FROM Vote WHERE UserId = @UserId) > 0)
			BEGIN
				
				PRINT 'Failed';
				COMMIT TRANSACTION
				RETURN

			END

		IF((SELECT COUNT(*) FROM [User] WHERE Id = @UserId) = 0)
			BEGIN

				PRINT 'Failed. Such a USER does not exist.';
				COMMIT TRANSACTION;
				RETURN;

			END

		IF((SELECT COUNT(*) FROM Project WHERE Id = @ProjectId) = 0)
			BEGIN

				PRINT 'Failed. Such a PROJECT does not exist.';
				COMMIT TRANSACTION;
				RETURN;

			END
		
		INSERT INTO Vote(UserId, ProjectId)
		VALUES (@UserId, @ProjectId)

		COMMIT TRANSACTION;

	END TRY
	BEGIN CATCH

		ROLLBACK TRANSACTION;

		DECLARE @ErrorMessage NVARCHAR(MAX) = '';
		SET @ErrorMessage = ERROR_MESSAGE();

		PRINT 'Adding vote failed: ' + @ErrorMessage;

	END CATCH
END
GO

--RETRIEVING PROJECT INFORMATION.
CREATE PROCEDURE ProjectInformation
	@ProjectName NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON
	BEGIN TRY
		
		IF((SELECT COUNT(*)FROM Project WHERE Name = @ProjectName) = 0)
			BEGIN
				
				PRINT 'Failed. Such a PROJECT does not exist.';
				RETURN;

			END
		
		SELECT 
			P.Id, P.Name, 
			U.Id, U.Name, U.SecondName, 
			Ct.Description, 
			Cm.UserId,	
			ISNULL( Cm.Text, 'No comments' ) AS CommentText,
			Cm.Date,
			COUNT(V.Id) 
		FROM Project AS P
		INNER JOIN [User] AS U ON P.CreatorId = U.Id
		INNER JOIN Category AS Ct ON P.CategoryId = Ct.Id
		LEFT JOIN Comment AS Cm ON P.Id = Cm.ProjectId
		LEFT JOIN Vote AS V ON P.Id = V.ProjectId
		WHERE P.Name = @ProjectName
		GROUP BY
			P.Id, P.Name, 
			U.Id, U.Name, U.SecondName, 
			Ct.Description, 
			Cm.UserId, Cm.Text, Cm.Date
		ORDER BY Cm.Date DESC;

	END TRY
	BEGIN CATCH
		
		RETURN
		DECLARE @ErrorMessage NVARCHAR(MAX) = '';
		SET @ErrorMessage = ERROR_MESSAGE();

		PRINT 'Information failed: ' + @ErrorMessage;
		RETURN

	END CATCH
END
GO


CREATE PROCEDURE PaginetedProject
	@PageNumber INT,
	@PageSize INT = 5,
	@StartDate DATE,
	@EndDate DATE
AS
BEGIN
	SET NOCOUNT ON
	BEGIN TRY
		
		SELECT Name, CategoryId FROM Project AS P
		WHERE (@StartDate IS NULL OR P.CreationDate >= @StartDate)
			AND	(@EndDate IS NULL OR P.CreationDate <= @EndDate)
		ORDER BY CreationDate DESC
		OFFSET (@PageNumber - 1) * @PageSize ROWS
		FETCH NEXT @PageSize ROWS ONLY
	
	END TRY
	BEGIN CATCH
		
		DECLARE @ErrorMessage NVARCHAR(MAX) = '';
		SET @ErrorMessage = ERROR_MESSAGE();

		PRINT 'Paginaction failed: ' + @ErrorMessage;
		RETURN

	END CATCH
END
GO
