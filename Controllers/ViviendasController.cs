using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Entities;
using ProyectoFinal.Models;
using ProyectoFinal.Services;
using System.Text.Json;

namespace ProyectoFinal.Controllers
{
    [Route("api/viviendas")]
    [ApiController]
    public class ViviendasController : ControllerBase
    {
        private readonly IViviendaInfoRepository _viviendaInfoRepository;
        private readonly IMapper _mapper;
        const int maxViviendasPageSize = 10;

        public ViviendasController(IViviendaInfoRepository viviendaInfoRepository,
            IMapper mapper)
        {
            _viviendaInfoRepository = viviendaInfoRepository ??
                throw new ArgumentNullException(nameof(viviendaInfoRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViviendaSinReservasDto>>> GetTodasViviendas()
        {
            var collection = await _viviendaInfoRepository
                .GetViviendasAsync();

            return Ok(_mapper.Map<IEnumerable<ViviendaSinReservasDto>>(collection));
        }

        [HttpGet("paginadas")]
        public async Task<ActionResult<IEnumerable<ViviendaSinReservasDto>>> GetViviendas(
            int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxViviendasPageSize)
            {
                pageSize = maxViviendasPageSize;
            }

            var (viviendaEntities, paginationMetadata) = await _viviendaInfoRepository
                .GetViviendasAsync(pageNumber, pageSize);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<ViviendaSinReservasDto>>(viviendaEntities));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVivienda(
            int id, bool includeReservas = true)
        {
            var vivienda = await _viviendaInfoRepository.GetViviendaAsync(id, includeReservas);
            if (vivienda == null)
            {
                return NotFound();
            }

            if (includeReservas)
            {
                return Ok(_mapper.Map<ViviendaDto>(vivienda));
            }

            return Ok(_mapper.Map<ViviendaSinReservasDto>(vivienda));
        }

        [HttpGet("nombre")]
        public async Task<ActionResult<IEnumerable<ViviendaSinReservasDto>>> GetViviendas(
            string name)
        {
            var (viviendaEntities, numViviendas) = await _viviendaInfoRepository.GetViviendasAsync(name);
            if (numViviendas == 0)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<ViviendaSinReservasDto>>(viviendaEntities));
        }

        [HttpPost]
        public async Task<ActionResult<Vivienda?>> CreateVivienda(
               ViviendaSinReservasDto vivienda)
        {

            var finalVivienda = _mapper.Map<Entities.Vivienda>(vivienda);

            _viviendaInfoRepository.AddVivienda(finalVivienda);

            await _viviendaInfoRepository.SaveChangesAsync();

           return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Vivienda?>> DeleteVivienda(int id
               )
        {
            _viviendaInfoRepository.DeleteVivienda(id);

            await _viviendaInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<Vivienda?>> UpdateVivienda(int id,
               ViviendaSinReservasDto vivienda)
        {
            if (!await _viviendaInfoRepository.ViviendaExistsAsync(id))
            {
                return NotFound();
            }

            var v = await _viviendaInfoRepository.GetViviendaAsync(id, true);
            v.Description = vivienda.Description;
            v.Direccion = vivienda.Direccion;
            v.Name = vivienda.Name;
            v.Propietario = vivienda.Propietario;
            _viviendaInfoRepository.UpdateVivienda(v);

            await _viviendaInfoRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}

