using CADProxy;
using System;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
#endif


namespace CADKit.Extensions
{
    public static class TransactionExtensions
    {
        public static void UsingTransaction(this Database database, Action<Transaction> action)
        {
            using (var tr = database.TransactionManager.StartTransaction())
            {
                try
                {
                    action(tr);
                    tr.Commit();
                }
                catch (Exception)
                {
                    tr.Abort();
                    throw;
                }
            }
        }

        public static void UsingTransaction(Action<Transaction> action)
        {
            using (var tr = ProxyCAD.Database.TransactionManager.StartTransaction())
            {
                try
                {
                    action(tr);
                    tr.Commit();
                }
                catch (Exception ex)
                {
                    tr.Abort();
                    throw ex;
                }
            }
        }
    }
}
