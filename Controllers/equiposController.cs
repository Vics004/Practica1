using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practica1.Models;
using Microsoft.EntityFrameworkCore;


namespace Practica1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly equiposContext _equiposContext;

        public equiposController(equiposContext equiposContext)
        {
            _equiposContext = equiposContext;
        }

        // Para poder ver todos los registros:

        /*
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<equipos> listadoEquipo = (from e in _equiposContext.equipos select e).ToList();

            if (listadoEquipo.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoEquipo);
        }
        */


        // Para buscar los registros por ID

        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            equipos? equipo = (from e in _equiposContext.equipos where e.id_equipos == id select e).FirstOrDefault();

            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);
        }

        // Para buscar por texto (Campo descipcion)

        [HttpGet]
        [Route("Find/{filtro}")]

        public IActionResult FindByDescription(string filtro)
        {
            equipos? equipo = (from e in _equiposContext.equipos where e.descripcion.Contains(filtro) select e).FirstOrDefault();

            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);
        }

        // Para agregar un nuevo registro:

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarEquipo([FromBody] equipos equipo)
        {
            try
            {
                _equiposContext.equipos.Add(equipo);
                _equiposContext.SaveChanges();
                return Ok(equipo);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        // Para actualizar un registro

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarEquipo(int id, [FromBody] equipos equipoModificar)
        {
            equipos? equipoActual = (from e in _equiposContext.equipos where e.id_equipos == id select e).FirstOrDefault();

            if (equipoActual == null) 
            {return NotFound();}

            equipoActual.nombre = equipoModificar.nombre;
            equipoActual.descripcion = equipoModificar.descripcion;
            equipoActual.marca_id = equipoModificar.marca_id;
            equipoActual.tipo_equipo_id = equipoModificar.tipo_equipo_id;
            equipoActual.anio_compra = equipoModificar.anio_compra;
            equipoActual.costo = equipoModificar.costo;

            _equiposContext.Entry(equipoActual).State = EntityState.Modified;
            _equiposContext.SaveChanges();

            return Ok(equipoModificar);
        }

        // Para Eliminar un registro

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarEquipo(int id)
        {
            equipos? equipo = (from e in _equiposContext.equipos where e.id_equipos == id select e).FirstOrDefault();

            if (equipo == null)
            { return NotFound(); }

            _equiposContext.equipos.Attach(equipo);
            _equiposContext.equipos.Remove(equipo);
            _equiposContext.SaveChanges();

            return Ok(equipo);
        }

        //Guia 3

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            var listadoEquipo = (from e in _equiposContext.equipos 
                                 join t in _equiposContext.tipo_equipo
                                 on e.tipo_equipo_id equals t.id_tipo_equipo
                                 join m in _equiposContext.marcas
                                 on e.marca_id equals m.id_marcas
                                 join es in _equiposContext.estados_equipo
                                 on e.estado_equipo_id equals es.id_estados_equipo
                                 select new 
                                 {
                                     e.id_equipos,
                                     e.nombre,
                                     e.descripcion,
                                     e.tipo_equipo_id,
                                     tipo_equipo=t.descripcion,
                                     e.marca_id,
                                     marca=m.nombre_marca,
                                     e.estado_equipo_id,
                                     estado_equipo=es.descripcion,
                                     detalle = $"Tipo : { t.descripcion }, Marca { m.nombre_marca } Estado Equipos { es.descripcion }",
                                     e.estado
                                 }).OrderBy(resultado => resultado.estado_equipo_id)
                                 .ThenBy(resultado => resultado.marca_id)
                                 .ThenByDescending(resultado => resultado.tipo_equipo_id).ToList();

            if (listadoEquipo.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoEquipo);
        }
    }
}
