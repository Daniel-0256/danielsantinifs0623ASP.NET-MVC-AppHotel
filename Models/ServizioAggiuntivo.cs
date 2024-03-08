using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppHotel.Models
{
    public class ServizioAggiuntivo
    {
        [Key]
        public int IdServizio { get; set; }
        [Required]
        public string Descrizione { get; set; }
        [Required]
        public decimal Prezzo { get; set; }
    }
}