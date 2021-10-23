using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonWebApp.Models
{
    public class Pokemon
    {
        public int ID { get; set; }
        public string nome { get; set; }
        public string tipo { get; set; }
        public string poderes { get; set; }
        public double peso { get; set; }
        public double altura { get; set; }
        public string elemento { get; set; }
        public string evolucao { get; set; }
        public string velocidade { get; set; }
        public string image { get; set; }
    }
}
