USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[Blogs_SelectAll_V3]    Script Date: 10/5/2022 5:55:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Morgan Frost>
-- Create date: <07/01/2022>
-- Description: <Blogs_SelectAll>
-- Code Reviewer:Jordan Poole 07/06/2022

-- MODIFIED BY: author
-- MODIFIED DATE:07/01/2020
-- Code Reviewer:
-- Note:
-- =============================================

ALTER proc [dbo].[Blogs_SelectAll_V3]
	@PageIndex int
	,@PageSize int

/*------TEST CODE------

		Declare @PageIndex int = 0
				,@PageSize int = 15

		Execute dbo.Blogs_SelectAll_V3
			@PageIndex
			,@PageSize

*/

AS

BEGIN
	Declare @offset int = @PageIndex * @PageSize

SELECT	b.[Id]
		,b.[Title]
		,b.[Subject]
		,b.[Content]
		,b.[IsPublished]
		,b.[ImageUrl]
		,b.[DateCreated]
		,b.[DateModified]
		,b.[DatePublish]
		,bt.[Id]
		,bt.[Name] as BlogType
		,b.[AuthorId]
		,up.[UserId] as Author
		,up.[FirstName]
		,up.[LastName]
		,up.[Mi]
		,up.[AvatarUrl]
		,TotalCount = COUNT(1) OVER()

	
	FROM dbo.Blogs as b 
						inner join dbo.BlogTypes as bt

						on b.BlogTypeId = bt.Id

						left outer join dbo.UserProfiles as up
						
						on b.AuthorId = up.UserId
	

	ORDER BY b.DateCreated DESC

	OFFSET @offSet Rows
   FETCH Next @PageSize Rows ONLY

END

