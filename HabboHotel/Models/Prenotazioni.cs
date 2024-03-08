using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HabboHotel.Models
{
    public class Prenotazione
    {
        public int PrenotazioneId { get; set; }
        public string DataPrenotazione { get; set; }
        public string SoggiornoDa { get; set; }
        public string SoggiornoA { get; set; }
        public decimal Tariffa { get; set; }
        public int IdPensione { get; set; }
        public int ClienteId { get; set; }
        public int CameraId { get; set; }

        // Relazioni
        public virtual Cliente Cliente { get; set; }
        public virtual Camera Camera { get; set; }
        public virtual Pensione Pensione { get; set; }
        public virtual ICollection<StoricoServiziAggiuntivi> StoriciServiziAggiuntivi { get; set; }
    }
}