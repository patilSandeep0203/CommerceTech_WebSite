USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[SurveyInstances_SelectBySurveyId]    Script Date: 9/20/2022 12:53:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[SurveyInstances_SelectBySurveyId]
-- =============================================
-- Author: Michael Shepard
-- Create date: 13 September 2022
-- Description: selects all response data for 
--				a given survey, returning questions,
--				answers, and answer frequency
-- Code Reviewer: Vincent Goitortua

-- MODIFIED BY: 
-- MODIFIED DATE: 
-- Code Reviewer:
-- Note: 
-- ==============================================

@Id int

as
/*

DECLARE @Id int = 2
EXECUTE dbo.SurveyInstances_SelectBySurveyId @Id

*/
BEGIN

SELECT 
	su.Id as SurveyId
	,su.[Name] as SurveyName
	,Instances = 
			(
			SELECT COUNT(*)
			FROM dbo.SurveyInstances as si
			WHERE si.SurveyId = @Id
			)
	,su.[Description] as [Description]
	,su.CompanyLogo as Logo
	,su.DateCreated as DateCreated
	,u.Id as CreatedBy
	,up.FirstName as CreatorFirstName
	,up.LastName as CreatorLastName
	,InsightData = 
				(
				SELECT
				sq.Id as 'Question.Id'
				,sq.Question as 'Question.Question'
				,qt.Id as 'Question.QuestionTypeId'
				,qt.[Name] as 'Question.QuestionTypeName'
				,sq.SortOrder as 'Question.SortOrder'
				,Answer =
					json_query(CASE
						WHEN qt.Id = 2 OR qt.Id = 3 THEN
							(
							SELECT 
								san.AnswerOptionId as OptionId
								,coalesce(sqao.[Text], sqao.[Value]) as AnswerString
								,AnswerBinary = NULL
								,Total = COUNT(*) 
							FROM dbo.SurveyAnswers AS san
							inner join dbo.SurveyQuestionAnswerOptions as sqao on san.AnswerOptionId = sqao.Id
							WHERE san.QuestionId = sq.Id
							GROUP BY san.AnswerOptionId, sqao.[Text], sqao.[Value]
							FOR JSON AUTO
							)
						WHEN qt.Id = 10 THEN
							(
							SELECT 
								san.AnswerOptionId as OptionId
								,san.Answer as AnswerString
								,san.AnswerNumber as AnswerBinary
								,Total = COUNT(*) 
							FROM dbo.SurveyAnswers AS san
							WHERE san.QuestionId = sq.Id
							GROUP BY san.AnswerNumber, san.AnswerOptionId, san.Answer
							FOR JSON AUTO
							)
						WHEN qt.Id = 1 THEN
							(
							SELECT 
								san.AnswerOptionId as OptionId
								,san.Answer as AnswerString
								,san.AnswerNumber as AnswerBinary
								,Total = NULL
							FROM dbo.SurveyAnswers AS san
							WHERE san.QuestionId = sq.Id
							FOR JSON AUTO
							)
					END)
				FROM dbo.SurveyQuestions as sq
				inner join dbo.Surveys as s on sq.SurveyId = s.Id
				inner join dbo.QuestionTypes as qt on sq.QuestionTypeId = qt.Id
				WHERE @Id = s.Id
				ORDER BY sq.SortOrder
				FOR JSON PATH
				)
FROM dbo.Surveys as su
	inner join dbo.Users as u on su.CreatedBy = u.Id
	inner join dbo.UserProfiles as up on u.Id = up.UserId
WHERE @Id = su.Id


END