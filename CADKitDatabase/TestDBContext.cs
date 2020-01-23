using CADKitBasic.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitBasic.Database
{
    public class TestDBContext
    {
        public void Migration()
        {
            using (var context = new CADKitMigrationDbContext())
            {
                Console.WriteLine("Wersja bazy : " + GetDatabaseVersion(context).ToString());
            }
        }

        public void Test()
        {
            using (var migrationContext = new CADKitMigrationDbContext())
            {
                migrationContext.Initialize();
                Console.WriteLine("Wersja bazy : " + migrationContext.SchemaInfoes.Max(p => p.Version).ToString());

            }

            using (var context = new CADKitDbContext())
            {
                Console.WriteLine($"Ilosc typow linii : {context.Linetypes.Count().ToString()}");
                var lt = new Linetype() { Name = "Contiunous", Def1 = "aaa", Def2 = "bbb" };
                lt = AddLineType(context, lt);
                Console.WriteLine($"Ilosc typów linii : {context.Linetypes.Count().ToString()}");
                var la = new Layer() { Name = "Kontur", Linetype = lt, Colour = 2 };
                la = AddLayer(context, la);
                la = new Layer() { Name = "Cienkie", Linetype = lt, Colour = 1 };
                la = AddLayer(context, la);
                la = new Layer() { Name = "Obok", Linetype = lt, Colour = 8 };
                la = AddLayer(context, la);
                lt = new Linetype() { Name = "Hidden", Def1 = "aaa", Def2 = "bbb" };
                lt = AddLineType(context, lt);
                la = new Layer() { Name = "Ukryte", Linetype = lt, Colour = 1 };
                la = AddLayer(context, la);
                Console.WriteLine($"Ilosc warstw : {context.Layers.Count().ToString()}");
                foreach(var layer in context.Layers)
                {
                    Console.WriteLine($"Warstwa : {layer.Name} ma kolor {layer.Colour.ToString()} i typ linii {layer.Linetype.Name}");
                }
            }
        }

        private int GetDatabaseVersion(CADKitMigrationDbContext context)
        {
            var record = context.SchemaInfoes.OrderByDescending(p => p.Version).First();

            return record.Version;
        }

        private Linetype AddLineType(CADKitDbContext context, Linetype lt)
        {
            var result = context.Linetypes.FirstOrDefault(a => a.Name == lt.Name);
            if(result == null)
            {
                try
                {
                    context.Linetypes.Add(lt);
                    context.SaveChanges();
                    result = lt;
                    Console.WriteLine($"Dodano typ linii : {lt.Name}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Operacja nieudana. Błąd : {ex.Message}");
                }
            }

            return result;
        }

        private Layer AddLayer(CADKitDbContext context, Layer la)
        {
            var result = context.Layers.FirstOrDefault(a => a.Name == la.Name);
            if (result == null)
            {
                try
                {
                    context.Layers.Add(la);
                    context.SaveChanges();
                    Console.WriteLine($"Dodano warstwe : {la.Name}");
                    result = la;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Operacja nieudana. Błąd : {ex.Message}");
                }
            }

            return result;
        }
    }
}
