USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[Jobs_DeleteV2]    Script Date: 9/20/2022 1:11:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Nicholas Rodriguez>
-- Create date: <07/01/2022>
-- Description: <Jobs_Delete>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[Jobs_DeleteV2]
						@Id int

/*-----------Test Code-------------

DECLARE @Id int = 148;

SELECT *
FROM [dbo].[Jobs]
WHERE Id = @Id

EXEC [dbo].[Jobs_Delete]
					@Id

SELECT *
FROM [dbo].[Jobs]
WHERE Id = @Id

*/

AS

BEGIN
	DELETE FROM dbo.Proposals
		WHERE JobId = @Id 

	DELETE FROM [dbo].[Jobs]
		WHERE Id = @Id
END


GO
