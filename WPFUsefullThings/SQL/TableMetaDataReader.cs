using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUsefullThings
{
    public static class TableMetaDataReader
    {
        public static void Read()
        {

            Server server = new Server("MAXASUS");
            Database db = server.Databases["MSSQLDB"];

            foreach (Table table in db.Tables)
            {
                Console.WriteLine($"Table: {table.Name}");
                foreach (Column column in table.Columns)
                {
                    Console.WriteLine($" - Column: {column.Name}, Type: {column.DataType.Name}, Nullable: {column.Nullable}, MaxLength: {column.DataType.MaximumLength}");
                }
            }
        }
    }
}
