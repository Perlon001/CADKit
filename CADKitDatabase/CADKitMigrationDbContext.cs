using CADKit.Database.Entity;
using System.Data.Common;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace CADKit.Database
{
    public class CADKitMigrationDbContext : DbContext
    {
        public static int RequiredDatabaseVersion = 1;

        public CADKitMigrationDbContext() : base("name=default")
        {
            Configure();
        }

        public CADKitMigrationDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Configure();
        }

        public CADKitMigrationDbContext(DbConnection connection, bool contextOwnsConnection) : base(connection, contextOwnsConnection)
        {
            Configure();
        }

        public virtual DbSet<SchemaInfo> SchemaInfoes { get; set; }
        
        private void Configure()
        {
            Database.Log = message => Trace.WriteLine(message);        // logowanie do okna
            Database.Log = text => Debug.WriteLine(text);              // logowanie do okna Debug
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("");
            modelBuilder.Entity<SchemaInfo>();

            var initializer = new CADKitDbInitializer(modelBuilder);
            System.Data.Entity.Database.SetInitializer(initializer);
        }

        public void Initialize()
        {
            using( var context = new CADKitMigrationDbContext())
            {
                int currentVersion = 0;
                if(context.SchemaInfoes.Count() > 0)
                {
                    currentVersion = context.SchemaInfoes.Max(p => p.Version);
                }
                CADKitMigrationHelper helper = new CADKitMigrationHelper();
                while(currentVersion < RequiredDatabaseVersion)
                {
                    currentVersion++;
                    foreach(var migration in helper.Migrations)
                    {
                        foreach(var command in migration.Value)
                        {
                            context.Database.ExecuteSqlCommand(command);
                        }
                    }
                    context.SchemaInfoes.Add(new SchemaInfo() { Version = currentVersion });
                    context.SaveChanges();
                }
            }
        }
    }
}