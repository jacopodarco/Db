using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestioneLavoratori_Db.Helpers;

namespace GestioneLavoratori_Db
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Operazione: ");
            int x = Int32.Parse(Console.ReadLine());

            switch(x){
                case 1:
                    InitDb();
                    DataSet ds = DbHelper.GetLav();
                    DataTable dt = ds.Tables[0];
                    print(dt);
                    Console.ReadLine();
                    break;
                case 2:
                    
                    DbHelper.Svuota("Lavoratori");
                    ds = DbHelper.GetLav();
                    DataTable dc = ds.Tables[0];
                    print(dc);
                    break;
                case 3:
                
                    Console.WriteLine("ff");
                    break;
            }
            Console.ReadLine();

        }
        private static void print(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}", row[0], row[1], row[2], row[3], row[4], row[5]);
            }
        }
        private static void InitDb()
        {
            Lavoratore[] array = new Lavoratore[3];
            array[0] = new Lavoratore("Luca", "Marinello", 33, TipoLavoratore.Autonomo, 3400);
            array[1] = new Lavoratore("Renato", "Marinello", 54, TipoLavoratore.Dipendente, 300);
            array[2] = new Lavoratore("Treto", "Serilo", 43, TipoLavoratore.Autonomo, 1200);


            foreach (var g in array)
            {
                DbHelper.Insert(g);
            }
        }
    }
}
