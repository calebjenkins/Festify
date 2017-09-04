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
            DefineSchedule(dbo, conference, acceptance);
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
                var fk = table.CreateForeignKey(speaker);
                var timestamp = table.CreateDateTime2Column("Timestamp");
                table.CreateUniqueIndex(fk.Column, timestamp);
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
                var fk = table.CreateForeignKey(session);
                var timestamp = table.CreateDateTime2Column("Timestamp");
                table.CreateUniqueIndex(fk.Column, timestamp);
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

        private Entity DefineSubmissionWorkflow(SchemaSpecification dbo, Entity session, Entity conference)
        {
            var submission = dbo.CreateEntity("Submission", table =>
            {
                var sessionFk = table.CreateForeignKey(session);
                var conferenceFk = table.CreateForeignKey(conference);
                var timestamp = table.CreateDateTime2Column("Timestamp");
                table.CreateUniqueIndex(sessionFk.Column, conferenceFk.Column, timestamp);
            });

            dbo.CreateEntity("SubmissionWithdrawl", table =>
            {
                var fk = table.CreateForeignKey(submission);
                table.CreateUniqueIndex(fk.Column);
            });

            var acceptance = dbo.CreateEntity("Acceptance", table =>
            {
                var fk = table.CreateForeignKey(submission);
                var timestamp = table.CreateDateTime2Column("Timestamp");
                table.CreateUniqueIndex(fk.Column, timestamp);
            });

            dbo.CreateEntity("AcceptanceWithdrawl", table =>
            {
                var fk = table.CreateForeignKey(acceptance);
                table.CreateUniqueIndex(fk.Column);
            });

            return acceptance;
        }

        private void DefineSchedule(SchemaSpecification dbo, Entity conference, Entity acceptance)
        {
            var room = dbo.CreateEntity("Room", table =>
            {
                var conferenceId = table.CreateForeignKey(conference);
                var roomNumber = table.CreateStringColumn("RoomNumber", 50);
                table.CreateUniqueIndex(conferenceId.Column, roomNumber);
            });

            var timeSlot = dbo.CreateEntity("TimeSlot", table =>
            {
                var conferenceId = table.CreateForeignKey(conference);
                var date = table.CreateDateColumn("Date");
                var time = table.CreateTimeColumn("Time", fractionalSeconds: 0);
                table.CreateUniqueIndex(conferenceId.Column, date, time);
            });

            dbo.CreateMutableProperty(acceptance, "Schedule", table =>
            {
                table.CreateForeignKey(room);
                table.CreateForeignKey(timeSlot);
            });
        }
    }
}
