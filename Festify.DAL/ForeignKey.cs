using Schemavolution.Specification;

namespace Festify.DAL
{
    public class ForeignKey
    {
        public ColumnSpecification Column { get; set; }
        public IndexSpecification Index { get; set; }
        public ForeignKeySpecification Specification { get; set; }
    }
}