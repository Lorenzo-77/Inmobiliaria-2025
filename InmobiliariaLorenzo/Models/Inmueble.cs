using System.ComponentModel.DataAnnotations;

namespace InmobiliariaLorenzo.Models;

public class Inmueble
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El propietario es obligatorio")]
    public int PropietarioId { get; set; }

    [Required(ErrorMessage = "La dirección es obligatoria")]
    public string Direccion { get; set; }

    [Required(ErrorMessage = "El uso es obligatorio")]
    public UsoInmueble Uso { get; set; }

    [Required(ErrorMessage = "El tipo es obligatorio")]
    public TipoInmueble Tipo { get; set; }

    [Required(ErrorMessage = "La cantidad de ambientes es obligatoria")]
    [Range(1, 100, ErrorMessage = "Debe tener al menos un ambiente")]
    public int? Ambientes { get; set; }

    public double? Latitud { get; set; }
    public double? Longitud { get; set; }

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(1, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal Precio { get; set; }

    public bool Activo { get; set; }
    public bool Disponible { get; set; }

    public Propietario? Propietario { get; set; }

    public Inmueble() { }

    public Inmueble(int id, int propietarioId, Propietario propietario, string direccion, UsoInmueble uso, TipoInmueble tipo, int? ambientes, double? latitud, double? longitud, decimal precio, bool activo, bool disponible)
    {
        Id = id;
        PropietarioId = propietarioId;
        Propietario = propietario;
        Direccion = direccion;
        Uso = uso;
        Tipo = tipo;
        Ambientes = ambientes;
        Latitud = latitud;
        Longitud = longitud;
        Precio = precio;
        Activo = activo;
        Disponible = disponible;
    }
}


// Enumerador para Uso de Inmueble
public enum UsoInmueble
{
    Comercial = 1,
    Personal = 2
}

// Enumerador para Tipo de Inmueble
public enum TipoInmueble
{
    Casa = 1,
    Oficina = 2,
    Departamento = 3,
    Almacen = 4
}