using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppHotel.Models
{
    public class CheckOutViewModel
    {
        public int IdPrenotazione { get; set; }

        [Display(Name = "Data di inizio")]
        [DataType(DataType.Date)]
        public DateTime DataInizio { get; set; }

        [Display(Name = "Data di fine")]
        [DataType(DataType.Date)]
        public DateTime DataFine { get; set; }

        [Display(Name = "Tariffa giornaliera")]
        public decimal Tariffa { get; set; }

        [Display(Name = "Caparra pagata")]
        public decimal Caparra { get; set; }

        [Display(Name = "Totale soggiorno")]
        public decimal TotaleSoggiorno { get; set; }

        [Display(Name = "Totale servizi aggiuntivi")]
        public decimal TotaleServizi { get; set; }

        [Display(Name = "Totale da pagare")]
        public decimal TotaleDaPagare { get; set; }
    }
}