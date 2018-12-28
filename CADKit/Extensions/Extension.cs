using CADKit.ServiceCAD;
using System;
using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKit.Extensions
{
    public static class Extension
    {
        public static void UsingTransaction(Action<Transaction> action)
        {
            using (var tr = CADProxy.Database.TransactionManager.StartTransaction())
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


    }
}
