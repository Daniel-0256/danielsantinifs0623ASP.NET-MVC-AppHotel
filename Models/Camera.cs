using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppHotel.Models
{
    public class Camera
    {
        [Key]
        public int IdCamera { get; set; }
        [Required]
        public string NumeroCamera { get; set; }
        [Required]
        public string Descrizione { get; set; }
        [Required]
        public string Tipologia { get; set; }
    }
}