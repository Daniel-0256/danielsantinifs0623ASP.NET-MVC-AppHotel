using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppHotel.Models
{
    public class Prenotazione
    {
        [Key]
        public int IdPrenotazione { get; set; }
        [Required]
        public DateTime DataPrenotazione { get; set; }
        [Required]
        public DateTime DataInizio { get; set; }
        [Required]
        public DateTime DataFine { get; set; }
        public decimal Caparra { get; set; }
        [Required]
        public decimal Tariffa { get; set; }
        public string Dettagli { get; set; }
        [Required]
        public int IdCliente { get; set; }
        [Required]
        public int IdCamera { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Camera Camera { get; set; }
    }
}