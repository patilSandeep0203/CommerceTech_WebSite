USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[Events_InsertV2]    Script Date: 9/28/2022 8:27:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Victor Gonzalez>
-- Create date: <09/16/2022>
-- Description: <Events_Insert>
-- Code Reviewer:

-- MODIFIED BY: N/A
-- MODIFIED DATE:N/A
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[Events_InsertV2]
					@EventTypeId int
					,@LocationTypeId int
					,@OrganizationId int
					,@Title nvarchar(128)
					,@Description nvarchar(500)
					,@ImageUrl nvarchar(255)
					,@IsMultiDay bit
					,@LineOne nvarchar(255)
			        ,@LineTwo nvarchar(255)
			        ,@City nvarchar(225)
			        ,@Zip nvarchar(50)
		 	        ,@StateId int
			        ,@Latitude float 
          			,@Longitude float
					,@CreatedBy int
					,@Id int OUTPUT

/*---------------Test Code--------------


DECLARE @EventTypeId int = 1
		,@LocationId int = 1
		,@OrganizationId int = 1
		,@Title nvarchar(128) = 'test title'
		,@Description nvarchar(500) = 'test description'
		,@ImageUrl nvarchar(255)='image url'
		,@IsMultiDay bit = 0
		,@UserId int = 4
		,@Id int = 0
			

	EXEC [dbo].[Events_Insert]
					@EventTypeId
					,@LocationId
					,@OrganizationId 
					,@Title
					,@Description
					,@ImageUrl 
					,@IsMultiDay 
					,@UserId
					,@Id OUTPUT

	Select *
	From [dbo].[Events]
	Where Id = @Id
	
*/

AS

BEGIN

Declare @LocationId int
				,@DateModified datetime2 = GETUTCDATE()

INSERT INTO [dbo].[Locations]
				   ([LocationTypeId]
				   ,[LineOne]
				   ,[LineTwo]
				   ,[City]
				   ,[Zip]
				   ,[StateId]
				   ,[Latitude]
				   ,[Longitude]
				   ,[CreatedBy])
			 VALUES
				   (@LocationTypeId
				   ,@LineOne
				   ,@LineTwo
				   ,@City
				   ,@Zip
				   ,@StateId
				   ,@Latitude
				   ,@Longitude
				   ,@CreatedBy)

		SET @LocationId = SCOPE_IDENTITY()

INSERT INTO [dbo].[Events]
           ([EventTypeId]
		   ,[LocationId]
		   ,[OrganizationId]
           ,[Title]
           ,[Description]
           ,[ImageUrl]
		   ,[IsMultiDay]
		   ,[CreatedBy]
		   ,[ModifiedBy]
		   ,[DateCreated]
           ,[DateModified])
     VALUES
           (@EventTypeId
		   ,@LocationId
		   ,@OrganizationId
			,@Title 
			,@Description 
			,@ImageUrl
			,@IsMultiDay
			,@CreatedBy
			,@CreatedBy
			,@DateModified
			,@DateModified)

		SET @Id = SCOPE_IDENTITY()
					

END
GO
