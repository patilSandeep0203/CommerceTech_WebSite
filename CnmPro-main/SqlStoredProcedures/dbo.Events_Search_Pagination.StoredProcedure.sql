USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[Events_Search_Pagination]    Script Date: 9/19/2022 1:40:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author: <Victor Gonzalez>
-- Create date: <09/16/2022>

-- Description: <Events_search pagination>

-- Code Reviewer: N/A

-- =============================================

CREATE PROC [dbo].[Events_Search_Pagination]   @PageIndex int ,@PageSize int ,@Query nvarchar(50)
	
AS
/*-----TEST CODE-----

Declare @PageIndex int = 0
		,@PageSize int = 10
		,@Query nvarchar(50) = 'civil'



EXECUTE [dbo].[Events_Search_Pagination ]
		@PageIndex
		,@PageSize
		,@Query

	

		EXECUTE dbo.Events_SelectAll @PageIndex 
		,@PageSize

-----END TEST CODE-----
*/


BEGIN

Declare @Offset int = @PageIndex * @PageSize
	
		SELECT e.[Id]
			  ,e.[EventTypeId] AS [EventType] 
			  ,et.[Name]
			  ,e.[Title]
			  ,e.[Description]
			  ,e.[LocationId] AS [Location]
			  ,l.[LocationTypeId] AS [LocationType]
			  ,e.[ImageUrl]
			  ,lt.[Name]
			  ,l.[LineOne]
			  ,l.[LineTwo]
			  ,l.[City]
			  ,l.[Zip]
			  ,l.[StateId] As [StateId]
			  ,s.[Name]
			  ,s.[Code]
			  ,l.[Latitude]
			  ,l.[Longitude]
			  ,e.[OrganizationId] AS [Organization]
			  ,o.[OrganizationTypeId] AS [OrganizationType] 
			  ,ot.[Name]
			  ,o.[Name]
			  ,o.[Headline]
			  ,o.[Description]
			  ,o.[Logo]
			  ,o.[Phone]
			  ,o.[SiteUrl]
			  ,up.[Id]
			  ,e.[CreatedBy]
			  ,up.FirstName
			  ,up.LastName
			  ,up.Mi
			  ,up.AvatarUrl
			  ,e.[DateCreated]
			  ,e.[DateModified]
			  ,TotalCount = COUNT(1) OVER()


			  

		FROM [dbo].[Events] AS e 
		left outer join dbo.Organizations as o on o.Id = e.OrganizationId
left outer join dbo.Locations as l on l.id = e.LocationId
inner join dbo.EventTypes as et on et.Id = e.EventTypeId
left outer join dbo.LocationTypes as lt on lt.Id = l.LocationTypeId
left outer join dbo.OrganizationTypes as ot on ot.Id = o.OrganizationTypeId
left outer join dbo.UserProfiles as up on up.UserId = e.CreatedBy
left outer join dbo.States as s on s.Id = l.StateId

	
		WHERE	e.Title LIKE  '%' + @Query + '%'
		OR  e.Description LIKE '%' + @Query + '%'
		OR  l.Zip LIKE '%' + @Query + '%'
		OR  l.City LIKE '%' + @Query + '%'

		ORDER BY e.Id

		OFFSET @Offset ROWS
		FETCH NEXT @PageSize ROWS ONLY

	END
GO
