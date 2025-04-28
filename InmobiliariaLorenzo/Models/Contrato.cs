using System.ComponentModel.DataAnnotations;
using System;

namespace InmobiliariaLorenzo.Models
{
    public class Contrato
    {
        public int Id { get; set; }
        public int InquilinoId { get; set; }

        [Required(ErrorMessage = "El Inquilino es obligatorio")]
        public Inquilino Inquilino { get; set; }  // Agrego inquilino entero para las tablas y ABM

        public int InmuebleId { get; set; }

        [Required(ErrorMessage = "El Inmueble es obligatorio")]
        public Inmueble Inmueble { get; set; }  // Agrego inmueble entero para las tablas y ABM > de acá sacamos propietario luego

        [Required(ErrorMessage = "La Fecha de inicio es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La Fecha de fin es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaFin { get; set; }
        
        [Required(ErrorMessage = "La Fecha de terminación es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaTerminacion { get; set; }
        
        [Required(ErrorMessage = "El Monto es obligatorio")]
        public decimal Monto { get; set; }

        public Contrato()
        {

        }

        public Contrato(int inquilinoId, int inmuebleId, DateTime fechaInicio, DateTime fechaFin, DateTime fechaTerminacion, decimal monto)
        {
            InquilinoId = inquilinoId;
            InmuebleId = inmuebleId;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            FechaTerminacion = fechaTerminacion;
            Monto = monto;
        }
    }
}