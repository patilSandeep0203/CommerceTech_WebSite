USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[OrgAdminDashboard_GetProsposalsByOrg]    Script Date: 9/6/2022 1:17:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[OrgAdminDashboard_GetProsposalsByOrg]
	@OrganizationId int,
	@PageIndex int,
	@PageSize int

as

/* Test Code
	
	Declare @OrganizationId int = 175,
			@PageIndex int = 0,
			@PageSize int = 100

	Execute dbo.OrgAdminDashboard_GetProsposalsByOrg
		@OrganizationId,
		@PageIndex,
		@PageSize
			


*/

Begin
	  DECLARE @offset int = @PageIndex * @PageSize

       SELECT	p.[Id]
			  ,j.[Id] AS [JobId]
			  ,jt.[Name] AS JobType
			  ,ot.[Name] As Occupation
			  ,j.[Title] as [Job Title]
			  ,j.[Description]
			  ,p.[Bid]
			  ,p.[Duration]
			  ,p.[Details] 
			  ,ps.[Name] as [ProposalStatus]
			  ,p.DateCreated
			  ,u.[Id] as [CreatedBy]
			  , u.Email
			  ,u.[Id] as [ModifiedBy]
			  , TotalCount = COUNT (1) OVER()
		

		From [dbo].[Proposals] AS p 
		INNER JOIN dbo.Jobs as j On j.Id = p.JobId 
		INNER JOIN dbo.Users as u ON u.Id = p.CreatedBy 
		INNER JOIN dbo.ProposalStatus as ps ON ps.Id = p.ProposalStatusId 
		INNER JOIN dbo.Locations as l ON l.Id = j.LocationId 
		INNER JOIN dbo.Organizations as o ON j.OrganizationId = o.Id
		INNER JOIN dbo.States as s ON l.StateId = s.Id 
		INNER JOIN dbo.JobTypes as jt ON j.JobTypeId = jt.Id 
		INNER JOIN dbo.OrganizationTypes as ot ON ot.Id = o.OrganizationTypeId
		INNER JOIN dbo.LocationTypes as lt ON lt.Id = l.LocationTypeId
		Where j.OrganizationId = @OrganizationId
	
	
		ORDER BY	Id
		OFFSET @offset ROWS
		FETCH NEXT  @PageSize ROWS ONLY
End
GO
