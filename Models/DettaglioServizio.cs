using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppHotel.Models
{
    public class DettaglioServizio
    {
        [Key]
        public int IdDettaglio { get; set; }
        [Required]
        public int IdPrenotazione { get; set; }
        [Required]
        public int IdServizio { get; set; }
        [Required]
        public DateTime Data { get; set; }
        [Required]
        public int Quantita { get; set; }

        public virtual Prenotazione Prenotazione { get; set; }
        public virtual ServizioAggiuntivo ServizioAggiuntivo { get; set; }
    }
}