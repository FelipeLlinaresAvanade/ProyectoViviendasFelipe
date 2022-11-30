using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Entities
{
    public class Reserva
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string NameUsuario { get; set; }
        public string? FechaInicio { get; set; }
        public string? FechaFin { get; set; }

        [ForeignKey("ViviendaId")]
        public Vivienda? Vivienda { get; set; }

        public int ViviendaId { get; set; }

        public Reserva(string nameUsuario)
        {
            NameUsuario = nameUsuario;
        }
    }
}
