using Schemavolution.Specification;
using System;

namespace Festify.DAL
{
    public static class HistoricalSpecificationExtensions
    {
        public static void CreateMutableProperty(this SchemaSpecification dbo, Entity entity, string propertyName, Action<TableSpecification> definitions)
        {
            var table = dbo.CreateTable($"{entity.Name}{propertyName}");
            var id = table.CreateIdentityColumn($"{entity.Name}{propertyName}Id");
            var fkid = table.CreateIntColumn($"{entity.Name}Id");
            var index = table.CreateIndex(fkid);
            index.CreateForeignKey(entity.PrimaryKey);
            definitions(table);
            var propertyPk = table.CreatePrimaryKey(id);

            var predecessor = dbo.CreateTable($"{entity.Name}{propertyName}Predecessor");
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
