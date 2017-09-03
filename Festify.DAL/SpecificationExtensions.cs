using Schemavolution.Specification;
using System;

namespace Festify.DAL
{
    public static class SpecificationExtensions
    {
        public static PrimaryKeySpecification CreateEntity(this SchemaSpecification dbo, string tableName,
            Action<TableSpecification> definitions)
        {
            var table = dbo.CreateTable(tableName);
            var id = table.CreateIdentityColumn($"{tableName}Id");
            definitions(table);
            return table.CreatePrimaryKey(id);
        }

        public static ForeignKeySpecification CreateForeignKey(this TableSpecification table, string tableName, PrimaryKeySpecification pk)
        {
            var fkid = table.CreateIntColumn($"{tableName}Id");
            var index = table.CreateIndex(fkid);
            return index.CreateForeignKey(pk);
        }
    }
}
