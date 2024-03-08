using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HabboHotel.Models
{
    public class ServizoAggiuntivo
    {
        public int ServizioAggiuntivoId { get; set; }
        public string TipoServizio { get; set; }
        public decimal PrezzoServizio { get; set; }

        // Relazioni
        public virtual ICollection<StoricoServiziAggiuntivi> StoriciServiziAggiuntivi { get; set; }
    }
}