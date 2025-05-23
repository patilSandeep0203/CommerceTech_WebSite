USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[Jobs_Select_ByOrganization_Paginated]    Script Date: 9/6/2022 1:17:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Jobs_Select_ByOrganization_Paginated]
	@OrganizationId int,
	@PageIndex int,
	@PageSize int

as

/*  Test Code

	Declare @OrganizationId int = 175
	, @PageIndex int = 0
	, @PageSize int = 5

	Execute dbo.Jobs_Select_ByOrganization_Paginated
		@OrganizationId
		,@PageIndex
		,@PageSize


*/

Begin 

	DECLARE @offset int = @PageIndex * @PageSize
	Select j.Id,
		   j.Title,
		   j.Description,
		   j.Requirements,
		   jt.Name as JobType,
		   l.City,
		   st.Name as State,
		   l.Zip as ZipCode,
		   j.DateCreated
		   ,TotalCount = COUNT(1) OVER()
	FROM dbo.Jobs as j inner join dbo.Locations as l
	on j.LocationId = l.Id
	inner join dbo.JobTypes as jt
	on jt.Id = j.JobTypeId
	inner join dbo.States as st
	on  l.StateId = st.Id
	Where j.OrganizationId = @OrganizationId
	ORDER BY j.Id
	OFFSET @offset Rows
	Fetch Next @PageSize Rows ONLY

End
GO
