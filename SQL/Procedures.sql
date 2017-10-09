USE [Festify]
GO

DROP PROCEDURE IF EXISTS NewSpeaker
GO

CREATE PROCEDURE NewSpeaker(
  @UserName NVARCHAR(255))
AS
  IF NOT EXISTS (
    SELECT SpeakerId
    FROM Speaker
	WHERE UserName = @UserName)
      INSERT INTO Speaker (UserName)
	  VALUES (@UserName)
GO

DROP PROCEDURE IF EXISTS NewSession
GO

CREATE PROCEDURE NewSession(
  @UserName NVARCHAR(255),
  @Timestamp DATETIME2)
AS
  DECLARE @SpeakerId INT

  SELECT @SpeakerId = SpeakerId
  FROM Speaker
  WHERE UserName = @UserName

  IF NOT EXISTS (
    SELECT SessionId
	FROM [Session]
	WHERE SpeakerId = @SpeakerId
	AND [Timestamp] = @Timestamp)
      INSERT INTO [Session] (SpeakerId, [Timestamp])
	  VALUES (@SpeakerId, @Timestamp)
GO

DROP PROCEDURE IF EXISTS SetSpeakerName
GO

CREATE PROCEDURE SetSpeakerName(
  @UserName NVARCHAR(255),
  @Name NVARCHAR(100))
AS
  DECLARE @SpeakerId INT

  SELECT @SpeakerId = SpeakerId
  FROM Speaker
  WHERE UserName = @UserName

  IF NOT EXISTS (
    SELECT SpeakerNameId
	FROM SpeakerName
	WHERE SpeakerId = @SpeakerId
	AND [Name] = @Name)
	  INSERT INTO SpeakerName (SpeakerId, [Name])
	  VALUES (@SpeakerId, @Name)
GO

