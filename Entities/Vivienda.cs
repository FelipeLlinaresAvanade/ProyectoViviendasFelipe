using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoViviendasFelipe.Entities
{
        public class Vivienda
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            [Required]
            [MaxLength(50)]
            public string Name { get; set; }

            [MaxLength(300)]
            public string? Description { get; set; }
            public string? Direccion { get; set; }
            public string? Propietario { get; set; }

            public ICollection<Reserva> Reservas { get; set; }
               = new List<Reserva>();

            public Vivienda(string name)
            {
                Name = name;
            }
        }

}
