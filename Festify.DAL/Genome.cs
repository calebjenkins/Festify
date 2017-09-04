using Schemavolution.Specification;
using System;

namespace Festify.DAL
{
    public class Genome : IGenome
    {
        public void AddGenes(DatabaseSpecification databaseSpecification)
        {
            var dbo = databaseSpecification.UseSchema("dbo");

            var speaker = DefineSpeaker(dbo);
            var session = DefineSession(dbo, speaker);
            var conference = DefineConference(dbo);
            var acceptance = DefineSubmissionWorkflow(dbo, session, conference);

            dbo.CreateEntity("Room", table =>
            {
                var conferenceId = table.CreateForeignKey(conference);
                var roomNumber = table.CreateStringColumn("RoomNumber", 50);
                table.CreateUniqueIndex(roomNumber, conferenceId.Column);
            });
        }

        private Entity DefineSpeaker(SchemaSpecification dbo)
        {
            var speaker = dbo.CreateEntity("Speaker", table =>
            {
                var userName = table.CreateStringColumn("UserName", 255);
                table.CreateUniqueIndex(userName);
            });

            dbo.CreateMutableProperty(speaker, "Name", table =>
            {
                table.CreateStringColumn("Name", 100);
            });

            return speaker;
        }

        private Entity DefineSession(SchemaSpecification dbo, Entity speaker)
        {
            var session = dbo.CreateEntity("Session", table =>
            {
                table.CreateForeignKey(speaker);
                table.CreateDateTime2Column("Timestamp");
            });

            dbo.CreateMutableProperty(session, "Title", table =>
            {
                table.CreateStringColumn("Title", 255);
            });

            dbo.CreateMutableProperty(session, "Abstract", table =>
            {
                table.CreateStringColumn("Abstract", 2000);
            });

            dbo.CreateEntity("SessionDeletion", table =>
            {
                table.CreateForeignKey(session);
                table.CreateDateTime2Column("Timestamp");
            });

            return session;
        }

        private Entity DefineConference(SchemaSpecification dbo)
        {
            var conference = dbo.CreateEntity("Conference", table =>
            {
                var guid = table.CreateGuidColumn("ConferenceGuid");
                table.CreateUniqueIndex(guid);
            });

            return conference;
        }

        private static Entity DefineSubmissionWorkflow(SchemaSpecification dbo, Entity session, Entity conference)
        {
            var submission = dbo.CreateEntity("Submission", table =>
            {
                table.CreateForeignKey(session);
                table.CreateForeignKey(conference);
                table.CreateDateTime2Column("Timestamp");
            });

            dbo.CreateEntity("SubmissionWithdrawl", table =>
            {
                table.CreateForeignKey(submission);
            });

            var acceptance = dbo.CreateEntity("Acceptance", table =>
            {
                table.CreateForeignKey(submission);
                table.CreateDateTime2Column("Timestamp");
            });

            dbo.CreateEntity("AcceptanceWithdrawl", table =>
            {
                table.CreateForeignKey(acceptance);
            });

            return acceptance;
        }
    }
}
