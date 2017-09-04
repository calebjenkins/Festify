using Schemavolution.Specification;
using System;

namespace Festify.DAL
{
    public static class SpecificationExtensions
    {
        public static Entity CreateEntity(this SchemaSpecification schema, string tableName,
            Action<TableSpecification> definitions)
        {
            var table = schema.CreateTable(tableName);
            var id = table.CreateIdentityColumn($"{tableName}Id");
            definitions(table);
            var pk = table.CreatePrimaryKey(id);
            return new Entity
            {
                Name = tableName,
                Table = table,
                IdColumn = id,
                PrimaryKey = pk
            };
        }

        public static ForeignKey CreateForeignKey(this TableSpecification table, Entity entity)
        {
            var fkid = table.CreateIntColumn($"{entity.Name}Id");
            var index = table.CreateIndex(fkid);
            var fk = index.CreateForeignKey(entity.PrimaryKey);
            return new ForeignKey
            {
                Column = fkid,
                Index = index,
                Specification = fk
            };
        }
    }
}
