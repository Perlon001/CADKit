using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.Database.Entity
{
    public class Layer : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int Colour { get; set; }

        public int LinetypeId { get; set; }
        public Linetype Linetype { get; set; }
    }

    public class LayerEntityMap : EntityTypeConfiguration<Layer>
    {
        public LayerEntityMap()
        {
            // Primary key
            HasKey(e => e.Id);

            //Map fields
            Property(e => e.Name).HasColumnName("Name").HasColumnType("nvarchar").HasMaxLength(30).IsRequired();
            Property(e => e.Colour).HasColumnName("Colour").HasColumnType("int").IsRequired();
            Property(e => e.LinetypeId).HasColumnName("LinetypeId").IsRequired();

            // Map foreinkey

            // Mapping table
            ToTable("Layers");
        }
    }

}
