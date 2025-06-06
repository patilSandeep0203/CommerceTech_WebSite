USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[Events_Update]    Script Date: 9/19/2022 1:40:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author: <Victor Gonzalez>
-- Create date: <09/16/2022>

-- Description: <Events_Update>

-- Code Reviewer: N/A

-- =============================================

CREATE PROC [dbo].[Events_Update]
					@EventTypeId int
					,@LocationId int
					,@OrganizationId int
					,@Title nvarchar(128)
					,@Description nvarchar(500)
					,@ImageUrl nvarchar(255)
					,@IsMultiDay bit
					,@UserId int
					,@Id int 

/*---------------Test Code--------------
DECLARE @EventTypeId int = 111
		,@LocationId int = 222
		,@OrganizationId int = 333
		,@Title nvarchar(128) = 'test title'
		,@Description nvarchar(500) = 'test description'
		,@ImageUrl nvarchar(255)='image url'
		,@IsMultiDay bit = 0
		,@UserId int = 999
		,@Id int = 1
			
SELECT *
FROM [dbo].[Events]
WHERE Id = @Id

	EXEC [dbo].[Events_Update]
					@EventTypeId
					,@LocationId
					,@OrganizationId 
					,@Title
					,@Description
					,@ImageUrl 
					,@IsMultiDay 
					,@UserId 
					,@Id 

	Select *
	From [dbo].[Events]
	Where Id = @Id

*/

AS

BEGIN

UPDATE [dbo].[Events]
   SET		[EventTypeId]= @EventTypeId
		   ,[LocationId]= @LocationId
		   ,[OrganizationId]= @OrganizationId
           ,[Title]=@Title
           ,[Description]= @Description
           ,[ImageUrl]= @ImageUrl
		   ,[IsMultiDay]= @IsMultiDay
		   ,[ModifiedBy] = @UserId
           ,[DateModified] = GETUTCDATE()
 WHERE Id = @Id
					
END
GO
