using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HabboHotel.Models
{
    public class Camera
    {
        public int CameraId { get; set; }
        public bool Tipologia { get; set; } 
        public string Descrizione { get; set; }
        public decimal Caparra { get; set; }

        // Relazioni
        public virtual ICollection<Prenotazione> Prenotazioni { get; set; }
    }
}