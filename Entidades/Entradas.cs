using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PracticaPrestamosDeJuegos.Entidades
{
    public class Entradas
    {
        [Key]
        public int EntradaId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int JuegoId { get; set; }
        public int Existencia{ get; set; }
    }
}
