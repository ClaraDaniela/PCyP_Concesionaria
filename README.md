### Trabajo Practico, Programacion Concurrente y paralela
###  Dominio del Proyecto: Sistema de Gestión para Concesionarias de Autos 🚗 (SSR Motors)

El sistema permitirá administrar la venta y posventa de vehículos en concesionarias, abordando distintos aspectos clave:

🔹 ABM de Clientes (gestión de datos personales, historial de compras).
🔹 ABM de Vehículos (stock, características, precios).
🔹 ABM de Ventas (facturación, métodos de pago, seguimiento).
🔹 Gestión de Servicios de Posventa (mantenimiento, garantías, reclamos).
🔹 Reportes y Estadísticas (rendimiento de ventas, vehículos más vendidos).

##Se uso para lograr concurrecia y paralelismo: 
- Task.run: ejecucion de tareas de manera asincrona en un hilo separado.
- Async/Await: manejo de operaciones asincronas, permitiendo que el codigo espere a que se completen las tareas, y asi mejorar la esperiencia del usuario.
- Parallel.ForEach: procesamiento de colecciones en paralelo, mejorando el rendimiento al dividir el trabajo entre varios hilos.
- PLINQ: para filtrar y realizar operaciones en paralelo sobre coleciones de datos.
- Task.WhenAll: para esperar a que se completen varias tareas asincronas.
- SemaphoreSlim: para controlar el acceso a recursos compartidos, evitando condiciones de carrera y garantizando la seguridad en entornos concurrentes.

### Patrones de diseño utilizados:
- **Patron repository**: Para separar la logica de acceso a datos de la logica de negocios.
- **Patron service**: Para encapsular la logica de negocio y facilitar la reutilizacion del codigo.
- **AutoMapper**: Para mapear entre entidades y DTOs, simplificando la transferencia de datos entre capas.
- **Inyeccion de dependencias**: Para gestionar las dependencias entre clases y facilitar la prueba unitaria.
- **Entity Framework core**: Para interactuar con la base de datos de manera eficiente y segura.
- **DataFlow**: Para manejar el procesamiento concurrente en ventas.

### Como se distribuye el proyecto:
ConcesionariaBackend/
│
├── Controllers/               # Controladores que manejan las rutas HTTP
│   ├── VehiculoController.cs
│   └── VentaController.cs
│
├── DTOs/                      # Clases de transferencia de datos (lo que viaja entre cliente y servidor)
│   ├── VehiculoDTO.cs
│   └── VentaDTO.cs
│
├── Models/                    # Entidades del dominio (se mapean a las tablas)
│   ├── Vehiculo.cs
│   └── Venta.cs
│
├── Repositories/             # Interfaces y clases que acceden a la base de datos
│   ├── Interfaces/
│   │   ├── IVehiculoRepository.cs
│   │   └── IVentaRepository.cs
│   └── Implementaciones/
│       ├── VehiculoRepository.cs
│       └── VentaRepository.cs
│
├── Services/                 # Lógica de negocio
│   ├── VehiculoService.cs
│   └── VentaService.cs
│
├── Data/                     # Contexto de la base de datos
│   └── ApplicationDbContext.cs
│
├── Mappings/                 # Perfiles de AutoMapper (para mapear Model <-> DTO)
│   └── AutoMapperProfile.cs
│
├── Tests/                    # Pruebas unitarias
│   └── Services/
│       └── VentaServiceTests.cs
│
├── Program.cs                # Punto de entrada, configuración general
├── appsettings.json          # Configuraciones (como cadena de conexión)
└── ConcesionariaBackend.csproj

### Tecnologias utilizadas:
Backend (.NET 6+)
✅ Patrón Repository
✅ Patrón Service
✅ AutoMapper
✅ Entity Framework Core (MySQL)
✅ Inyección de Dependencias
✅ Programación Concurrente y Paralela