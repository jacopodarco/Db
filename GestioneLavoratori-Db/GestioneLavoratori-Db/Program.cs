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
            Console.WriteLine("Scegliere l'operazione:\r\n" +
                "1 - Inserire lavoratori nel Db e mostrare le istanze presenti nella tabella \r\n" +
                "2 - Svuotare tabella del Db \r\n" +
                "3 - Mostrare RAL e quantitativo di tasse per ogni lavoratore\r\n"+
                "4 - Aggiornare informazioni lavoratore");
            int x = Int32.Parse(Console.ReadLine());

            switch(x){
                case 1:
                    InitDb();
                    DataSet ds = DbHelper.GetLav();
                    DataTable dt = ds.Tables[0];
                    print(dt);
                    break;
                case 2:
                    
                    DbHelper.Svuota("Lavoratori");

                    break;
                case 3:

                    RAL();

                    break;
                case 4:
                    Console.WriteLine("Inserire ID" );
                    Guid guid = new Guid(Console.ReadLine());
                    Lavoratore l = new Lavoratore(guid, "gg", "tt", 44, TipoLavoratore.Autonomo, 44);
                    DbHelper.Update(l);
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
        public static void InitDb()
        {
            
            Lavoratore[] array = new Lavoratore[7];
            array[0] = new Lavoratore("Luca", "Marinello", 33, TipoLavoratore.Autonomo, 3400);
            array[1] = new Lavoratore("Renato", "Derate", 54, TipoLavoratore.Dipendente, 300);
            array[2] = new Lavoratore("Treto", "Serilo", 43, TipoLavoratore.Autonomo, 1200);
            array[3] = new Lavoratore("Difonso", "TOmmaso", 23, TipoLavoratore.Autonomo, 36000);
            array[4] = new Lavoratore("Cugliemglo", "Aretre", 39, TipoLavoratore.Autonomo, 13500);
            array[5] = new Lavoratore("Ciobanu", "Leonida", 65, TipoLavoratore.Dipendente, 600);
            array[6] = new Lavoratore("Pollastro", "Galletto", 54, TipoLavoratore.Dipendente, 2200);

            foreach (var g in array)
            {
                DbHelper.Insert(g);
            }

        }
        private static void RAL()
        {
            Lavoratore[] array = new Lavoratore[7];
            array[0] = new Lavoratore("Luca", "Marinello", 33, TipoLavoratore.Autonomo, 3400);
            array[1] = new Lavoratore("Renato", "Derate", 54, TipoLavoratore.Dipendente, 300);
            array[2] = new Lavoratore("Treto", "Serilo", 43, TipoLavoratore.Autonomo, 1200);
            array[3] = new Lavoratore("Difonso", "TOmmaso", 23, TipoLavoratore.Autonomo, 36000);
            array[4] = new Lavoratore("Cugliemglo", "Aretre", 39, TipoLavoratore.Autonomo, 13500);
            array[5] = new Lavoratore("Ciobanu", "Leonida", 65, TipoLavoratore.Dipendente, 600);
            array[6] = new Lavoratore("Pollastro", "Galletto", 54, TipoLavoratore.Dipendente, 2200);

            foreach (var g in array)
            {
                Console.WriteLine("Il lavoratore" + g.Cognome + " ha un RAL di " + g.RAL + " $ e versa " +
                    g.Tasse() +" $ di tasse.");
            }
        }
    }
}
