USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[EventTypes_SelectAll]    Script Date: 9/19/2022 1:40:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Victor Gonzalez>
-- Create date: <09/16/2022>
-- Description:	<Select All EventTypes>
-- Code Reviewer:

-- MODIFIED BY: n/a
-- MODIFIED DATE: n/a
-- Code Reviewer: 
-- Note: 
-- =============================================

CREATE PROC [dbo].[EventTypes_SelectAll] 

	AS

	/*------Test Code------
		
	Execute [dbo].[EventTypes_SelectAll]


	*/---------------------

	BEGIN


	SELECT [Id]
		  ,[Name]

	  FROM [dbo].[EventTypes]

	END
GO
