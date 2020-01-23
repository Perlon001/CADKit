using CADKitBasic.Database.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitBasic.Database
{
    public class CADKitDbContext : DbContext
    {
        public CADKitDbContext() : base("name=default")
        {
            Database.Log = message => Trace.WriteLine(message);        // logowanie do okna
            Database.Log = text => Debug.WriteLine(text);              // logowanie do okna Debug
        }

        public virtual DbSet<Linetype> Linetypes { get; set; }
        public virtual DbSet<Layer> Layers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("");
            modelBuilder.Properties()
                .Where(p => p.Name == "Id")
                .Configure(p => p.IsKey().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity));

            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new LinetypeEntityMap());
            modelBuilder.Configurations.Add(new LayerEntityMap());
        }
    }
}
