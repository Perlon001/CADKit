using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.Database.Entity
{
    public class SchemaInfo : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int Version { get; set; }
    }
}
