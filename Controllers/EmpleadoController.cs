using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practica1.Models;
using Microsoft.EntityFrameworkCore;


namespace Practica1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly ElOlivoContext _ElOlivoContext;

        public EmpleadoController(ElOlivoContext elOlivoContext)
        {
            _ElOlivoContext = elOlivoContext;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<Empleado> listadoEmpleado = (from e in _ElOlivoContext.empleado select e).ToList();

            if (listadoEmpleado.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoEmpleado);
        }
    }
}
