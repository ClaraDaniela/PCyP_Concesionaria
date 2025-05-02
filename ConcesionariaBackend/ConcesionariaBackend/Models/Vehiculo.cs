namespace ConcesionariaBackend.Models
{
    public class Vehiculo
    {
        public int Id { get; set; } // idVehiculo
        public string Marca { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public string? Anio { get; set; }
        public decimal? Precio { get; set; }
        public int? Stock { get; set; }
        public string? Color { get; set; }
    }
}

