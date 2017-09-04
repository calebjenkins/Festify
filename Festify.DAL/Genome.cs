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
            DefineSubmissionWorkflow(dbo, session, conference);
        }

        private PrimaryKeySpecification DefineSpeaker(SchemaSpecification dbo)
        {
            var speaker = dbo.CreateEntity("Speaker", table =>
            {
                var userName = table.CreateStringColumn("UserName", 255);
                table.CreateUniqueIndex(userName);
            });

            dbo.CreateMutableProperty("Speaker", "Name", speaker, table =>
            {
                table.CreateStringColumn("Name", 100);
            });

            return speaker;
        }

        private PrimaryKeySpecification DefineSession(SchemaSpecification dbo, PrimaryKeySpecification speaker)
        {
            var session = dbo.CreateEntity("Session", table =>
            {
                table.CreateForeignKey("Speaker", speaker);
                table.CreateDateTime2Column("Timestamp");
            });

            dbo.CreateMutableProperty("Session", "Title", session, table =>
            {
                table.CreateStringColumn("Title", 255);
            });

            dbo.CreateMutableProperty("Session", "Abstract", session, table =>
            {
                table.CreateStringColumn("Abstract", 2000);
            });

            dbo.CreateEntity("SessionDeletion", table =>
            {
                table.CreateForeignKey("Session", session);
                table.CreateDateTime2Column("Timestamp");
            });

            return session;
        }

        private PrimaryKeySpecification DefineConference(SchemaSpecification dbo)
        {
            var conference = dbo.CreateEntity("Conference", table =>
            {
                var guid = table.CreateGuidColumn("ConferenceGuid");
                table.CreateUniqueIndex(guid);
            });

            return conference;
        }

        private static void DefineSubmissionWorkflow(SchemaSpecification dbo, PrimaryKeySpecification session, PrimaryKeySpecification conference)
        {
            var submission = dbo.CreateEntity("Submission", table =>
            {
                table.CreateForeignKey("Session", session);
                table.CreateForeignKey("Conference", conference);
                table.CreateDateTime2Column("Timestamp");
            });

            dbo.CreateEntity("SubmissionWithdrawl", table =>
            {
                table.CreateForeignKey("Submission", submission);
            });

            var acceptance = dbo.CreateEntity("Acceptance", table =>
            {
                table.CreateForeignKey("Submission", submission);
                table.CreateDateTime2Column("Timestamp");
            });

            dbo.CreateEntity("AcceptanceWithdrawl", table =>
            {
                table.CreateForeignKey("Acceptance", acceptance);
            });
        }
    }
}
