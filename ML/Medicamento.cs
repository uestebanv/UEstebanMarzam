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
        public int Cantidad { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public int TotalVendido { get; set; }
        public List<object> Medicamentos { get; set; }
        public List<object> Productos { get; set; }
    }
}