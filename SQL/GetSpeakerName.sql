SELECT *
FROM SpeakerName
WHERE SpeakerId = 1
AND SpeakerNameId NOT IN (
  SELECT Predecessor
  FROM SpeakerNamePredecessor)
