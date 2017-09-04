using Schemavolution.Specification;

namespace Festify.DAL
{
    public class Entity
    {
        public string Name { get;   set; }
        public TableSpecification Table { get; set; }
        public ColumnSpecification IdColumn { get; set; }
        public PrimaryKeySpecification PrimaryKey { get; set; }
    }
}