﻿namespace ConcesionariaBackend.DTOs
{
    public class VehiculoDTO
    {
        public int Id { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public int Anio { get; set; } 
        public decimal? Precio { get; set; } 
        public string? Color { get; set; } 
        public int? Stock { get; set; }

        public Boolean Disponible { get; set; }
    }
}
