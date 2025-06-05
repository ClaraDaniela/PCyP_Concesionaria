namespace ConcesionariaBackend.DTOs
{
    public class InformeHistoricoDTO
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = null!;
        public decimal MontoTotal { get; set; }
        public DateTime FechaGeneracion { get; set; }
    }

}
