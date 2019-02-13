using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebasJSON.Data.Entities
{
    public class LookupTable
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public LookupTable(ILookupTable lookupTable)
        {
            Id = lookupTable.Id;
        }

        public LookupTable()
        {

        }
    }

    public interface ILookupTable
    {
        int Id { get; set; }
        string Code { get; set; }
        string Description { get; set; }
    }
    
}
