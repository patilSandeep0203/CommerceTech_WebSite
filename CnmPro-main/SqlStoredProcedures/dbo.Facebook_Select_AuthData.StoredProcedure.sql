USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[Facebook_Select_AuthData]    Script Date: 9/8/2022 7:40:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gonzalez, Victor
-- Create date: 9/6/22
-- Description: Inserts Facebook Token properties
-- Code Reviewer:

-- MODIFIED BY: Gonzalez, Victor
-- MODIFIED DATE:9/6/2022
-- Code Reviewer:
-- Note:
-- ==============================================

CREATE PROC [dbo].[Facebook_Select_AuthData]
	@Email nvarchar(100)

AS

/* ----- TEST CODE -----

	DECLARE @Email nvarchar(100) = 'testuser7000@gmail.com'
	
	EXECUTE dbo.Facebook_Select_AuthData @Email

*/ ----- END TEST CODE -----


Declare @status INT
		,@confirm BIT
SELECT @status = U.UserStatusId
		,@confirm = U.IsConfirmed
FROM dbo.Users as U
WHERE @Email = Email

BEGIN
	IF @confirm = 1
		IF @status < 2
			SELECT [U].[Id]
				  ,[U].[Email]
				 
				  ,R.[Name]
				  ,U.IsConfirmed
			  FROM [dbo].[Users] AS U
				   INNER JOIN
				   dbo.UserRoles AS UR
				   ON U.Id = UR.UserId
				   INNER JOIN
				   dbo.Roles AS R
				   ON UR.RoleId = R.Id

			  WHERE [U].[Email] = @Email
		ELSE
			  THROW 60001, 'User is not active.', 16
	ELSE
		THROW 60002, 'User is not confirmed.', 16
END

GO
