using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Entities;
using ProyectoFinal.Models;
using ProyectoFinal.Services;

namespace ProyectoFinal.Controllers
{
        [ApiController]
        [Route("api/viviendas/{viviendaId}/reservas")]
        public class ReservasController : ControllerBase
        {
            private readonly IViviendaInfoRepository _viviendaInfoRepository;
            private readonly IMapper _mapper;

            public ReservasController(IViviendaInfoRepository viviendaInfoRepository, IMapper mapper)
            {
                _viviendaInfoRepository = viviendaInfoRepository ??
                    throw new ArgumentNullException(nameof(viviendaInfoRepository));
                _mapper = mapper ??
                    throw new ArgumentNullException(nameof(mapper));
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<ReservaDto>>> GetReservas(
                int viviendaId)
            {
                if (!await _viviendaInfoRepository.ViviendaExistsAsync(viviendaId))
                {
                    return NotFound();
                }

                var reservasPorVivienda = await _viviendaInfoRepository
                    .GetReservasParaViviendaAsync(viviendaId);

                return Ok(_mapper.Map<IEnumerable<ReservaDto>>(reservasPorVivienda));
            }

            [HttpGet("{reservaId}", Name = "GetReserva")]
            public async Task<ActionResult<ReservaDto>> GetReserva(
                int viviendaId, int reservaId)
            {
                if (!await _viviendaInfoRepository.ViviendaExistsAsync(viviendaId))
                {
                    return NotFound();
                }

                var reserva = await _viviendaInfoRepository
                    .GetReservaParaViviendaAsync(viviendaId, reservaId);

                if (reserva == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<ReservaDto>(reserva));
            }

            [HttpPost]
            public async Task<ActionResult<Reserva?>> CreateReserva(
               int viviendaId,
               ReservaCreateUpdateDto reserva)
            {
                if (!await _viviendaInfoRepository.ViviendaExistsAsync(viviendaId))
                {
                    return NotFound();
                }

                var finalReserva = _mapper.Map<Entities.Reserva>(reserva);

                await _viviendaInfoRepository.AddReservaParaViviendaAsync(
                    viviendaId, finalReserva);

                await _viviendaInfoRepository.SaveChangesAsync();

                var createdReservaToReturn =
                    _mapper.Map<Models.ReservaDto>(finalReserva);

                return CreatedAtRoute("GetReserva",
                     new
                     {
                         viviendaId = viviendaId,
                         reservaId = createdReservaToReturn.Id
                     },
                     createdReservaToReturn);
            }

            [HttpPatch("{reservaId}")]
            public async Task<ActionResult> PartiallyUpdateReserva(
                int viviendaId, int reservaId,
                JsonPatchDocument<ReservaCreateUpdateDto> patchDocument)
            {
                if (!await _viviendaInfoRepository.ViviendaExistsAsync(viviendaId))
                {
                    return NotFound();
                }

                var reservaEntity = await _viviendaInfoRepository
                    .GetReservaParaViviendaAsync(viviendaId, reservaId);
                if (reservaEntity == null)
                {
                    return NotFound();
                }

                var reservaToPatch = _mapper.Map<ReservaCreateUpdateDto>(
                    reservaEntity);

                patchDocument.ApplyTo(reservaToPatch, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!TryValidateModel(reservaToPatch))
                {
                    return BadRequest(ModelState);
                }

                _mapper.Map(reservaToPatch, reservaEntity);
                await _viviendaInfoRepository.SaveChangesAsync();

                return NoContent();
            }

            [HttpPut("{reservaId}")]
            public async Task<ActionResult> UpdateReserva(int viviendaId, int reservaId,
               ReservaCreateUpdateDto reserva)
            {
                if (!await _viviendaInfoRepository.ViviendaExistsAsync(viviendaId))
                {
                    return NotFound();
                }

                var reservaEntity = await _viviendaInfoRepository
                    .GetReservaParaViviendaAsync(viviendaId, reservaId);
                if (reservaEntity == null)
                {
                    return NotFound();
                }

                _mapper.Map(reserva, reservaEntity);

                await _viviendaInfoRepository.SaveChangesAsync();

                return NoContent();
            }


            [HttpDelete("{reservaId}")]
            public async Task<ActionResult> DeleteReserva(
                int viviendaId, int reservaId)
            {
                if (!await _viviendaInfoRepository.ViviendaExistsAsync(viviendaId))
                {
                    return NotFound();
                }

                var reservaEntity = await _viviendaInfoRepository
                    .GetReservaParaViviendaAsync(viviendaId, reservaId);
                if (reservaEntity == null)
                {
                    return NotFound();
                }

                _viviendaInfoRepository.DeleteReserva(reservaEntity);
                await _viviendaInfoRepository.SaveChangesAsync();

                return NoContent();
            }

        }
}
