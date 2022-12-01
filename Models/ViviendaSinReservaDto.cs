namespace ProyectoViviendasFelipe.Models
{
    public class ViviendaSinReservasDto
    {
        /// <summary>
        /// El id de vivienda
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// El nombre de la vivienda
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// La descripición de la vivienda
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// La direccion de la vivienda
        /// Provincia, Municipio, calle, numero
        /// </summary>
        public string? Direccion { get; set; }

        /// <summary>
        /// Nombre del propietario de la vivienda
        /// </summary>
        public string? Propietario { get; set; }

    }
}