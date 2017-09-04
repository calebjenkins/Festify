using Schemavolution.Specification;
using System;

namespace Festify.DAL
{
    public static class HistoricalSpecificationExtensions
    {
        public static void CreateMutableProperty(this SchemaSpecification dbo, string tableName, string propertyName, PrimaryKeySpecification pk, Action<TableSpecification> definitions)
        {
            var table = dbo.CreateTable($"{tableName}{propertyName}");
            var id = table.CreateIdentityColumn($"{tableName}{propertyName}Id");
            var fkid = table.CreateIntColumn($"{tableName}Id");
            var index = table.CreateIndex(fkid);
            index.CreateForeignKey(pk);
            definitions(table);
            var propertyPk = table.CreatePrimaryKey(id);

            var predecessor = dbo.CreateTable($"{tableName}{propertyName}Predecessor");
            var predecessorId = predecessor.CreateIntColumn("Predecessor");
            var successorId = predecessor.CreateIntColumn("Successor");
            predecessor.CreatePrimaryKey(predecessorId, successorId);
            var predecessorIndex = predecessor.CreateIndex(predecessorId);
            var successorIndex = predecessor.CreateIndex(successorId);
            predecessorIndex.CreateForeignKey(propertyPk);
            successorIndex.CreateForeignKey(propertyPk);
        }
    }
}
