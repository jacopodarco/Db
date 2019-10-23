using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneLavoratori_Db
{
    public enum TipoLavoratore
    {
        Autonomo,
        Dipendente
    }
    public class Lavoratore
    {
        public Guid ID { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }   
        public int Eta { get; set; }
        public int Retribuzione {get;set;}
        public double RAL
        {
            get
            {
                return Retribuzione * GetMensilità();
            }
        }
        public TipoLavoratore Tipo { get; set; }
        public Lavoratore(string nome, string cognome, int età, TipoLavoratore tip, int ret)
        {
            ID = Guid.NewGuid();
            Nome = nome;
            Cognome = cognome;
            Eta = età;
            Tipo = tip;
            Retribuzione = ret;
        }
        public Lavoratore(Guid id, string nome, string cognome, int età, TipoLavoratore tip, int ret)
        {
            ID = id;
            Nome = nome;
            Cognome = cognome;
            Eta = età;
            Tipo = tip;
            Retribuzione = ret;
        }
        public int GetMensilità()
        {
            return Tipo == TipoLavoratore.Autonomo ? 12 : 13;
        }
        public double Tasse()
        {
            if (Tipo == TipoLavoratore.Autonomo)
            {
                if (Retribuzione < 50000)
                {
                    return Retribuzione * 15 / 100;
                }
                else { return Retribuzione * 30 / 100; }
            }
            else
            {
                if (Retribuzione <= 6000)
                    return 0;
                if (Retribuzione <= 15000 || Retribuzione > 6000)
                    return Retribuzione * 15 / 100;
                if (Retribuzione > 15000 || Retribuzione <= 25000)
                    return Retribuzione * 30 / 100;
                if (Retribuzione > 25000 || Retribuzione <= 35000)
                    return Retribuzione * 40 / 100;
                if (Retribuzione > 35000)
                    return Retribuzione * 50 / 100;
                else { return 0; }
            }
        }
    }
}
