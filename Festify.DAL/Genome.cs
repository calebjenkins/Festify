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
        }

        private PrimaryKeySpecification DefineSpeaker(SchemaSpecification dbo)
        {
            var speaker = dbo.CreateEntity("Speaker", table =>
            {
                table.CreateStringColumn("UserName", 255);
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
    }
}
