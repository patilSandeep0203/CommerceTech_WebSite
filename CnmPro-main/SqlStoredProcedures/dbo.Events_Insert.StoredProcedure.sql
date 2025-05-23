USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[Events_Insert]    Script Date: 9/19/2022 1:40:52 PM ******/
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
CREATE PROC [dbo].[Events_Insert]
					@EventTypeId int
					,@LocationId int
					,@OrganizationId int
					,@Title nvarchar(128)
					,@Description nvarchar(500)
					,@ImageUrl nvarchar(255)
					,@IsMultiDay bit
					,@UserId int
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


	select u.id, u.email, up.firstname, up.lastname from dbo.users as u
	left outer join dbo.userprofiles as up on u.id = up.userid

	select * from dbo.eventtypes
	
*/

AS

BEGIN


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
			,@UserId
			,@UserId
			,GETUTCDATE()
			,GETUTCDATE())

		SET @Id = SCOPE_IDENTITY()
					

END
GO
