using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PracticaPrestamosDeJuegos.Entidades
{
    public class PrestamosDetalle
    {
        [Key]
        public int Id { get; set; }
        public int PrestamoId { get; set; }
        public int JuegoId { get; set; }
        public int Cantidad { get; set; }
    }
}
