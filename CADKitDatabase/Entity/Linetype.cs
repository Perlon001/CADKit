using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.Database.Entity
{
    public class Linetype : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Def1 { get; set; }
        public string Def2 { get; set; }
        //public string Def3 { get; set; }

    }

    public class LinetypeEntityMap : EntityTypeConfiguration<Linetype>
    {
        public LinetypeEntityMap()
        {
            // Primary key
            HasKey(e => e.Id);

            //Map fields
            Property(e => e.Name).HasColumnName("Name").HasColumnType("nvarchar").HasMaxLength(30).IsRequired();
            Property(e => e.Def1).HasColumnName("Def1").HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            Property(e => e.Def2).HasColumnName("Def2").HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            //Property(e => e.Def3).HasColumnName("Def3").HasColumnType("nvarchar").HasMaxLength(150).IsRequired();

            //Map foreinkey

            // Mapping table
            ToTable("Linetypes");
        }
    }
}
