using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HabboHotel.Models
{
    public class Pensione
    {
        public int PensioneId { get; set; }
        public string TipoPensione { get; set; }
        public decimal Prezzo { get; set; }

        // Relazioni
        public virtual ICollection<Prenotazione> Prenotazioni { get; set; }
    }
}