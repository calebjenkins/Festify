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
            return dbo.CreateEntity("Speaker", table =>
            {
                table.CreateStringColumn("UserName", 255);
            });
        }

        private PrimaryKeySpecification DefineSession(SchemaSpecification dbo, PrimaryKeySpecification speaker)
        {
            return dbo.CreateEntity("Session", table =>
            {
                table.CreateForeignKey("Speaker", speaker);
                table.CreateDateTime2Column("Timestamp");
            });
        }
    }
}
