using CADKit.Database.Entity;
using SQLite.CodeFirst;
using System.Data.Entity;

namespace CADKit.Database
{
    public class CADKitDbInitializer : SqliteCreateDatabaseIfNotExists<CADKitMigrationDbContext>
    {
        public CADKitDbInitializer(DbModelBuilder modelBuilder) : base(modelBuilder)
        {

        }

        protected override void Seed(CADKitMigrationDbContext context)
        {
            // Here you can seed your core data if you have any.

            // Add record to table DBVersion
            var version = new SchemaInfo() { Version = 0 };
            context.SchemaInfoes.Add(version);
            context.SaveChanges();
        }
    }
}