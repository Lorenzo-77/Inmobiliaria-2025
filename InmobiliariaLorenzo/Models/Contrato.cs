using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InmobiliariaLorenzo.Models;
public class Contrato
{
    [Key]
    [Display(Name = "N°")]
    public int Id_Contrato { get; set; }


    [Required, Display(Name = "Fecha de Inicio")]
    public DateTime Fecha_Inicio { get; set; }


    [Required,Display(Name = "Fecha de Finalización")]
    public DateTime Fecha_Fin { get; set; }


    [Required, Display(Name ="Monto Mensual")]
    public decimal Monto { get; set; }


    [Required,Display(Name ="Inmueble")]
    public int Id_Inmueble{ get; set; }
    [ForeignKey(nameof(Id_Inmueble))]
    public Inmueble? Inmueble { get; set; }


    [Display(Name = "Inquilino")]
    public int Id_Inquilino{ get; set; }
    [ForeignKey(nameof(Id_Inquilino))]
    public Inquilino? Inquilino { get; set; }
}