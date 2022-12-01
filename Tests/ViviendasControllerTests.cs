using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProyectoViviendasFelipe.Controllers;
using ProyectoViviendasFelipe.Entities;
using ProyectoViviendasFelipe.Models;
using ProyectoViviendasFelipe.Profiles;
using ProyectoViviendasFelipe.Services;

namespace ProyectoViviendasFelipe.Tests.ControllerTests
{
    public class ViviendasControllerTests : IDisposable

    {
        private ViviendasController _viviendasController;
        private HttpContext _httpContext;
        public void Dispose()
        {
        }

        public ViviendasControllerTests()
        {
            _httpContext = new DefaultHttpContext();

            //Mock
            var MockRepository = new Mock<IViviendaInfoRepository>();

            var mapperConf = new MapperConfiguration(cfg => cfg.AddProfile<ViviendaProfile>());
            var mapper = new Mapper(mapperConf);

            MockRepository.Setup(m => m.GetViviendaAsync(1, true))
                .ReturnsAsync(new Vivienda("Vivienda 1"));
            MockRepository.Setup(m => m.GetViviendaAsync(1, false))
                .ReturnsAsync(new Vivienda("Vivienda 1"));

            MockRepository.Setup(m => m.GetViviendaAsync(99, It.IsAny<bool>()))
                .ReturnsAsync(null as Vivienda);
            MockRepository.Setup(m => m.GetViviendasAsync(1, 10))
                .ReturnsAsync((new List<Vivienda>()
                {
                    new Vivienda("Vivienda 1"){Id=1},
                    new Vivienda("Vivienda 2"){Id=2}
                }, new PaginationMetadata(3, 10, 1)));

            MockRepository.Setup(m => m.GetViviendasAsync())
               .ReturnsAsync((new List<Vivienda>()
               {
                    new Vivienda("Vivienda 1"){Id=1},
                    new Vivienda("Vivienda 2"){Id=2}
               }));


            _viviendasController = new ViviendasController(MockRepository.Object, mapper);

            _viviendasController.ControllerContext = new ControllerContext()
            {
                HttpContext = _httpContext,
            };
        }

        [Fact]
        public async Task GetViviendas_OkResult()
        {
            var result = await _viviendasController.GetTodasViviendas();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<ViviendaSinReservasDto>>>(result);
            var ok = Assert.IsType<OkObjectResult>(actionResult.Result);
            var dtos = Assert.IsAssignableFrom<IEnumerable<ViviendaSinReservasDto>>(ok.Value);
            Assert.Equal(2, dtos.Count());
            Assert.Equal("Vivienda 1", dtos.First().Name);
        }

        [Fact]
        public async Task GetViviendas_OkResult_WithPagination()
        {
            var result = await _viviendasController.GetViviendas(1, 10);

            var actionResult = Assert.IsType<ActionResult<IEnumerable<ViviendaSinReservasDto>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal("{\"TotalItemCount\":3,\"TotalPageCount\":1,\"PageSize\":10,\"CurrentPage\":1}", _httpContext.Response.Headers["X-Pagination"].ToString());
        }

        [Fact]
        public async Task GetVivienda_OkResult_WithReservas()
        {
            var result = await _viviendasController.GetVivienda(1, true);
            var ok = Assert.IsType<OkObjectResult>(result);
            var dto = Assert.IsAssignableFrom<ViviendaDto>(ok.Value);
            Assert.Equal("Vivienda 1", dto.Name);

        }

        [Fact]
        public async Task GetVivienda_OkResult_WithOutReservas()
        {
            var result = await _viviendasController.GetVivienda(1, false);
            var ok = Assert.IsType<OkObjectResult>(result);
            var dto = Assert.IsAssignableFrom<ViviendaSinReservasDto>(ok.Value);
            Assert.Equal("Vivienda 1", dto.Name);

        }

        [Fact]
        public async Task GetViviend_NotFoundResult()
        {
            var result = await _viviendasController.GetVivienda(99, false);
            Assert.IsType<NotFoundResult>(result);
        }





    }
}


