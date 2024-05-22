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
}