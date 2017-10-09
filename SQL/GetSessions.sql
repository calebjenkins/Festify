SELECT *
FROM [Session]
WHERE SessionId NOT IN (
  SELECT SessionId
  FROM SessionDeletion
  WHERE SessionDeletionId NOT IN (
    SELECT SessionDeletionId
	FROM SessionRestoration))
