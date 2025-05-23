USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[Events_SelectByCreatedBy]    Script Date: 9/19/2022 1:40:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author: <Victor Gonzalez>
-- Create date: <09/16/2022>

-- Description: <Events_Select created by>

-- Code Reviewer: N/A

-- =============================================


CREATE PROC [dbo].[Events_SelectByCreatedBy]

				@CreatedBy int
				,@PageIndex int
				,@PageSize int

	AS

	/*

	Declare @CreatedBy int = 1
				,@PageIndex int = 0
				,@PageSize int = 50

		Execute dbo.Events_SelectByCreatedBy @CreatedBy
												,@PageIndex
												,@PageSize
											

	*/

BEGIN

Declare @Offset int = @PageIndex * @PageSize
	
		SELECT e.[Id]
			  ,e.[EventTypeId] AS [EventType] 
			  ,t.[Name]
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


			  

		FROM [dbo].[Events] AS e INNER JOIN dbo.Locations as l
		ON l.LocationTypeId = e.LocationId INNER JOIN dbo.Organizations as o
		ON e.OrganizationId = o.OrganizationTypeId INNER JOIN dbo.States as s
		ON l.StateId = s.Id INNER JOIN dbo.EventTypes as t 
		ON e.EventTypeId = t.Id Inner join  dbo.UserProfiles as up 
		ON up.UserId = e.CreatedBy inner join dbo.LocationTypes as lt  
		ON lt.Id = l.LocationTypeId inner join dbo.OrganizationTypes as ot 
		ON ot.Id = e.OrganizationId

	
		WHERE e.CreatedBy = @CreatedBy

		ORDER BY e.CreatedBy

		OFFSET @Offset ROWS
		FETCH NEXT @PageSize ROWS ONLY

	END
GO
