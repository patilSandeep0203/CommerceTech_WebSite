USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[Events_Delete]    Script Date: 9/19/2022 1:40:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Victor Gonzalez>
-- Create date: <09/16/2022>
-- Description: <Events_Delete>
-- Code Reviewer:

-- MODIFIED BY: N/A
-- MODIFIED DATE:N/A
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[Events_Delete]
						@Id int

/*-----------Test Code-------------

DECLARE @Id int = 1;

SELECT *
FROM [dbo].[Events]
WHERE Id = @Id

EXEC [dbo].[Events_Delete]
					@Id

SELECT *
FROM [dbo].[Events]
WHERE Id = @Id

*/

AS

BEGIN
	DELETE FROM [dbo].[Events]
		WHERE Id = @Id
END


GO
