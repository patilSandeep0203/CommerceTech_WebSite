USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[Events_Search_Pagination_With_Location_Radius]    Script Date: 9/19/2022 1:40:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author: <Victor Gonzalez>
-- Create date: <09/16/2022>
-- Description: <Events_SearchPagination with location radius>
-- MODIFIED BY: <>
-- MODIFIED DATE:<>
-- Code Reviewer:
-- Note:  

-- =============================================
CREATE PROC [dbo].[Events_Search_Pagination_With_Location_Radius]
@PageIndex int
				,@PageSize int
				,@Query nvarchar(50)
				,@Lat decimal(10,7)
				,@Long decimal(10,7)
				,@Distance int
AS
/*-----TEST CODE-----

Declare @PageIndex int = 0
				,@PageSize int = 10
				,@Query nvarchar(50) = 'engineer'
				,@Lat decimal(10,7) = 32.7269669
				,@Long decimal(10,7) = -118.1647094
				,@Distance int = 100



EXECUTE [dbo].[Events_Search_Pagination_With_Location_Radius]
		@PageIndex
				,@PageSize
				,@Query
				,@Lat
				,@Long
				,@Distance

	

		

-----END TEST CODE-----
*/


BEGIN

Declare @Offset int = @PageIndex * @PageSize
		 ;WITH AllRecords AS (
		SELECT e.[Id] AS [EventId]
			  ,e.[EventTypeId] AS [EventType] 
			  ,et.[Name] AS [EventTypeName]
			  ,e.[Title] AS [EventTitle]
			  ,e.[Description]AS [EventDescription]
			  ,e.[LocationId] AS [LocationId]
			  ,l.[LocationTypeId] AS [LocationTypeId]
			  ,e.[ImageUrl] AS [ImageUrl]
			  ,lt.[Name]AS [LocationTypeName]
			  ,l.[LineOne]
			  ,l.[LineTwo]
			  ,l.[City]
			  ,l.[Zip]
			  ,l.[StateId] As [StateId]
			  ,s.[Name]AS [StateName]
			  ,s.[Code]AS [StateCode]
			  ,l.[Latitude]
			  ,l.[Longitude]
			  ,e.[OrganizationId] AS [Organization]
			  ,o.[OrganizationTypeId] AS [OrganizationType] 
			  ,ot.[Name]AS [OrgTypeName]
			  ,o.[Name]AS [OrgName]
			  ,o.[Headline]AS [OrgHeadline]
			  ,o.[Description]AS [OrgDescription]
			  ,o.[Logo]
			  ,o.[Phone]
			  ,o.[SiteUrl]
			  ,up.[Id] AS [UserId]
			  ,e.[CreatedBy]
			  ,up.FirstName
			  ,up.LastName
			  ,up.Mi
			  ,up.AvatarUrl
			  ,e.[DateCreated]
			  ,e.[DateModified]
			  ,TotalCount = COUNT(1) OVER()
			  ,( 3959 * acos( cos( radians(@lat) ) * cos( radians( l.Latitude ) ) * cos( radians(l.Longitude ) - radians(@Long) ) + sin( radians(@Lat) ) * sin( radians(l.Latitude) ) ) ) AS Distance
		

			  

		FROM [dbo].[Events] AS e 
		left outer join dbo.Organizations as o on o.Id = e.OrganizationId
inner join dbo.Locations as l on l.id = e.LocationId
inner join dbo.EventTypes as et on et.Id = e.EventTypeId
left outer join dbo.LocationTypes as lt on lt.Id = l.LocationTypeId
left outer join dbo.OrganizationTypes as ot on ot.Id = o.OrganizationTypeId
left outer join dbo.UserProfiles as up on up.UserId = e.CreatedBy
left outer join dbo.States as s on s.Id = l.StateId

ORDER BY e.Id

		OFFSET @Offset ROWS
		FETCH NEXT @PageSize ROWS ONLY
		)
	, filteredRecords as (
			SELECT  *
			FROM    AllRecords
			WHERE   distance <= @distance  
			AND	(AllRecords.EventTitle LIKE  '%' + @Query + '%'
			OR AllRecords.EventDescription LIKE '%' + @Query + '%')
			
			)
	SELECT
			filteredRecords.[EventId]
			,filteredRecords.[EventType]
			,filteredRecords.[EventTypeName]
			,filteredRecords.[EventTitle]
			,filteredRecords.[EventDescription]
			,filteredRecords.[ImageUrl]
			,filteredRecords.[LocationId]
			,filteredRecords.[LocationTypeId]
			,filteredRecords.[LocationTypeName]
			,filteredRecords.[LineOne]
			,filteredRecords.[LineTwo]
			,filteredRecords.[City]
			,filteredRecords.[Zip]
			,filteredRecords.[StateId]
			,filteredRecords.[StateName]
			,filteredRecords.[StateCode]
			,filteredRecords.[Latitude]
			,filteredRecords.[Longitude]
			,filteredRecords.[Organization]
			,filteredRecords.[OrganizationType]
			,filteredRecords.[OrgTypeName]
			,filteredRecords.[OrgName]
			,filteredRecords.[OrgHeadline]
			,filteredRecords.[OrgDescription]
			,filteredRecords.[Logo]
			,filteredRecords.[Phone]
			,filteredRecords.[SiteUrl]
			,filteredRecords.[UserId]
			,filteredRecords.[CreatedBy]
			,filteredRecords.[FirstName]
			,filteredRecords.[LastName]
			,filteredRecords.[Mi]
			,filteredRecords.[AvatarUrl]
			,filteredRecords.[DateCreated]
			,filteredRecords.[DateModified]
			,filteredRecords.[Distance]
			,TotalCount = COUNT(1) OVER()
    FROM    filteredRecords
    ORDER BY distance ASC;		
	END
GO
