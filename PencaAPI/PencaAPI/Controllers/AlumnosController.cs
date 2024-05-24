using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PencaAPI.Models;
using PencaAPI.Services;

namespace PencaAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AlumnosController(AlumnoService alumnoService) : ControllerBase
{
    private readonly AlumnoService _alumnoService = alumnoService;
    
    [HttpGet]
    public async Task<ActionResult<Alumno[]>> Get()
    {
        Alumno[] alumnos = await _alumnoService.GetAllAsync();
        return Ok(alumnos);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Alumno>> Get(int id)
    {
        try {
            var alumno = await _alumnoService.GetByIdAsync(id);
            return Ok(alumno);
        } catch (ArgumentException e) {
            return NotFound(e.Message);
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<Alumno>> Post(Alumno alumno)
    {
        await _alumnoService.CreateAsync(alumno);
        return CreatedAtAction(nameof(Get), new { id = alumno.Cedula }, alumno);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<Alumno>> Put(int id, Alumno alumno)
    {
        try {
            await _alumnoService.UpdateAsync(id, alumno);
            return Ok(alumno);
        } catch (ArgumentException e) {
            return NotFound(e.Message);
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try {
            await _alumnoService.DeleteAsync(id);
            return NoContent();
        } catch (ArgumentException e) {
            return NotFound(e.Message);
        }
    }
}