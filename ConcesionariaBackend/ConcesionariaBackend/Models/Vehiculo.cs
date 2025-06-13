namespace ConcesionariaBackend.Models
{
    public class Vehiculo
    {
        public int Id { get; set; } = 0!;
        public string Marca { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public int Anio { get; set; } 
        public decimal Precio { get; set; } 
        public int Stock { get; set; } 
        public string Color { get; set; } = null!;
        public int idSucursal { get; set; } = 0!;
        public Sucursal Sucursal { get; set; } = null!;
        public Boolean Disponible { get; set; } 
    }
}


