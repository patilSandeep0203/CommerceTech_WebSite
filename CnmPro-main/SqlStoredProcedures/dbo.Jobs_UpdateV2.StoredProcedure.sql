USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[Jobs_UpdateV2]    Script Date: 9/14/2022 9:52:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Nicholas Rodriguez>
-- Create date: <07/01/2022>
-- Description: <Jobs_Update>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[Jobs_UpdateV2]
					@JobTypeId int
					,@Title nvarchar(200)
					,@Description nvarchar(4000)
					,@Requirements nvarchar(3000)
					,@IsActive bit
					,@OrganizationId int
					,@LocationTypeId int
					,@LocationId int
					,@LineOne nvarchar(255)
					,@LineTwo nvarchar(255)
			        ,@City nvarchar(225)
			        ,@Zip nvarchar(50)
		 	        ,@StateId int
			        ,@Latitude float 
          			,@Longitude float
					,@ContactName nvarchar(100)
					,@ContactPhone nvarchar(20)
					,@ContactEmail nvarchar(200)
					,@UserId int
					,@Id int 

/*---------------Test Code--------------
DECLARE @JobTypeId int = 111
		,@LocationId int = 222
		,@OrganizationId int = 333
		,@Title nvarchar(200) = 'updated title'
		,@Description nvarchar(4000) = 'updated description'
		,@Requirements nvarchar(3000) = 'updated requirements'
		,@IsActive bit = 0
		,@ContactName nvarchar(100) = 'John Doe'
		,@ContactPhone nvarchar(20) = '(555) 123-4567'
		,@ContactEmail nvarchar(200) = 'test@email.com'
		,@UserId int = 555
		,@Id int = 1
			
SELECT *
FROM [dbo].[Jobs]
WHERE Id = @Id

	EXEC [dbo].[Jobs_Update]
						@JobTypeId
						,@LocationId
						,@OrganizationId
						,@Title 
						,@Description 
						,@Requirements 
						,@IsActive 
						,@ContactName
						,@ContactPhone
						,@ContactEmail
						,@UserId
						,@Id 

SELECT *
FROM [dbo].[Jobs]
WHERE Id = @Id

*/

AS

BEGIN

UPDATE [dbo].[Locations]
   SET [LocationTypeId] = @LocationTypeId
      ,[LineOne] = @LineOne
      ,[LineTwo] = @LineTwo
      ,[City] = @City
      ,[Zip] = @Zip
      ,[StateId] = @StateId
      ,[Latitude] = @Latitude
      ,[Longitude] = @Longitude
      ,[DateModified] = GETUTCDATE()
      ,[ModifiedBy] = @UserId
 WHERE Id = @LocationId

UPDATE [dbo].[Jobs]
   SET [JobTypeId] = @JobTypeId
      ,[LocationId] = @LocationId
      ,[OrganizationId] = @OrganizationId
      ,[Title] = @Title
      ,[Description] = @Description
      ,[Requirements] = @Requirements
      ,[IsActive] = @IsActive
      ,[ContactName] = @ContactName
      ,[ContactPhone] = @ContactPhone
      ,[ContactEmail] = @ContactEmail
      ,[ModifiedBy] = @UserId
      ,[DateModified] = GETUTCDATE()
 WHERE Id = @Id
					
END
GO
