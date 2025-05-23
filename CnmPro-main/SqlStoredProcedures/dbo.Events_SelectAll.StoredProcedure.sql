USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[Events_SelectAll]    Script Date: 9/19/2022 1:40:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	-- =============================================
-- Author: <Victor Gonzalez>
-- Create date: <09/16/2022>

-- Description: <Events_SelectAll brings in all relevant data belonging to events and includes
-- joined tables such as Locations and Organizations to bring a comprehenensive amount of
-- information >

-- Code Reviewer: N/A

-- =============================================
	
	
	CREATE PROC [dbo].[Events_SelectAll]

				 @PageIndex int
				,@PageSize int

	AS

	/*


				DECLARE @PageIndex int = 0
				,@PageSize int = 50

		Execute dbo.Events_SelectAll 
												@PageIndex
												,@PageSize

	*/

	BEGIN

		Declare @Offset int = @PageIndex * @PageSize
		
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


			  

		FROM [dbo].[Events] AS e 
		left outer join dbo.Organizations as o on o.Id = e.OrganizationId
		left outer join dbo.Locations as l on l.id = e.LocationId
		inner join dbo.EventTypes as et on et.Id = e.EventTypeId
		left outer join dbo.LocationTypes as lt on lt.Id = l.LocationTypeId
		left outer join dbo.OrganizationTypes as ot on ot.Id = o.OrganizationTypeId
		left outer join dbo.UserProfiles as up on up.UserId = e.CreatedBy
		left outer join dbo.States as s on s.Id = l.StateId





		ORDER BY e.[Id]

		OFFSET @Offset ROWS
		FETCH NEXT @PageSize ROWS ONLY


	END
GO
