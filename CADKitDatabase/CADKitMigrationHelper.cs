using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitBasic.Database
{
    public class CADKitMigrationHelper
    {
        public Dictionary<int, IList<string>> Migrations { get; set; }

        public CADKitMigrationHelper()
        {
            Migrations = new Dictionary<int, IList<string>>();
            Migration_001();
        }

        private void Migration_001()
        {
            const int version = 1;

            IList<string> steps = new List<string>();
            // Create tables and indexes
            steps.Add("CREATE TABLE [Linetypes] ([Id] INTEGER PRIMARY KEY, [Name] nvarchar, [Def1] nvarchar, [Def2] nvarchar)");
            steps.Add("CREATE UNIQUE INDEX [IX_Linetypes_Name] ON [Linetypes] ([Name])");
            steps.Add("CREATE TABLE [Layers] ([Id] INTEGER PRIMARY KEY, [Name] nvarchar, [Colour] INTEGER, [LinetypeId] INTEGER NOT NULL, FOREIGN KEY([LinetypeId]) REFERENCES [Linetypes]([Id]))");
            steps.Add("CREATE UNIQUE INDEX [IX_Layers_Name] ON [Layers] ([Name])");

            // ... add another steps

            Migrations.Add(version, steps);
        }
    }
}
