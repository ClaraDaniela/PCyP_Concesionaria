namespace ConcesionariaBackend.Models
{
    public class InformeHistorico
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = null!; // "Venta" o "Servicio"
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;

        public decimal MontoTotal { get; set; }
        public DateTime FechaGeneracion { get; set; }
    }
}

