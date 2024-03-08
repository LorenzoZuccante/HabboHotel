using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HabboHotel.Models
{
    public class StoricoServiziAggiuntivi
    {
        public int StoricoServiziAggiuntiviId { get; set; }
        public int PrenotazioneId { get; set; }
        public int ServizioAggiuntivoId { get; set; }
        public string DataServizio { get; set; }
        public decimal PrezzoTotale { get; set; }

        // Relazioni
        public virtual Prenotazione Prenotazione { get; set; }
       
    }
}