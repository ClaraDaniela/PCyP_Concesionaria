namespace ConcesionariaBackend.DTOs
{
    public class VehiculoDTO
    {
        public int Id { get; set; }
        public required string Marca { get; set; }
        public required string Modelo { get; set; }
        public int Anio { get; set; }
        public decimal Precio { get; set; }
        public required string Color { get; set; }
        public int Stock { get; set; }

    }
}
