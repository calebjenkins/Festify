using Schemavolution.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festify.DAL
{
    public class Genome : IGenome
    {
        public void AddGenes(DatabaseSpecification databaseSpecification)
        {
            var dbo = databaseSpecification.UseSchema("dbo");

            
        }
    }
}
