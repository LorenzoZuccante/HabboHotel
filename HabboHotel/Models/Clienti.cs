using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HabboHotel.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; } 
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string Citta { get; set; }
        public string Provincia { get; set; }
        public string Email { get; set; }
        public int Telefono { get; set; }
        public int Cellulare { get; set; }
        public string CodiceFiscale { get; set; }

        // Relazioni
        public virtual ICollection<Prenotazione> Prenotazioni { get; set; }
    }
}