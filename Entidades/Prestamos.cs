using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PracticaPrestamosDeJuegos.Entidades
{
    public class Prestamos
    {
        [Key]
        public int PrestamoId { get; set; }
        public DateTime Fecha { get; set; }
        public int AmigoId { get; set; }
        public string Observacion { get; set; }
        public int CantidadJuegos { get; set; }
    }
}
