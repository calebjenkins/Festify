DROP VIEW IF EXISTS VW_Session
GO

CREATE VIEW VW_Session
AS

	SELECT [Speaker].UserName, [Session].[Timestamp], [SessionTitle].Title
	FROM [Session]
	JOIN [Speaker]
	  ON [Speaker].SpeakerId = [Session].SpeakerId
	LEFT JOIN [SessionTitle]
	  ON [Session].SessionId = [SessionTitle].SessionId
	  AND [SessionTitle].SessionTitleId NOT IN (
	    SELECT [SessionTitlePredecessor].Predecessor
	    FROM [SessionTitlePredecessor]
	  )
	WHERE [Session].SessionId NOT IN (
	  SELECT SessionId
	  FROM SessionDeletion)
GO