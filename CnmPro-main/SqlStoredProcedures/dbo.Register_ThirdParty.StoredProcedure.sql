USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[Register_ThirdParty]    Script Date: 9/8/2022 7:40:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gonzalez, Victor
-- Create date: 9/3/22
-- Description: Registers user to capture Facebook api key
-- Code Reviewer:

-- MODIFIED BY: Gonzalez, Victor
-- MODIFIED DATE:9/6/2022
-- Code Reviewer:
-- Note:
-- ==============================================

CREATE proc [dbo].[Register_ThirdParty]
			@Email nvarchar(255)
			,@Token varchar(200)
			,@TokenType int
			,@Id int OUTPUT
as
/*
			DECLARE @Id int = 0
			DECLARE @Email nvarchar(255) = 'FbUser@test.net'
					,@Token varchar(200) = 'abc-123456'
					,@TokenType int = 3
			EXECUTE dbo.Register_ThirdParty
					@Email
					,@Token
					,@TokenType
					,@Id OUTPUT
			SELECT *
				From dbo.Users where [Id] = @Id
			SELECT *
				From dbo.UserTokens where [Token] = @Token
*/
BEGIN
			INSERT INTO [dbo].[Users]
				([Email]
				,[Password]
				,[IsConfirmed]
				,[UserStatusId])
			VALUES
				(@Email
				,'Pass12345!'
				,'True'
				,'1')
			SET @Id = SCOPE_IDENTITY()
			INSERT INTO [dbo].[UserTokens]
				([Token]
				,[UserId]
				,[TokenType])
			VALUES
				(@Token
				,@Id
				,@TokenType)
			SET @Token = SCOPE_IDENTITY()
END
GO
