using System;
using System.Collections.Generic;


namespace ML
{
    public class Medicamento
    {
        public int IdMedicamento { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }
        public List<object> Medicamentos { get; set; } 
    }
}