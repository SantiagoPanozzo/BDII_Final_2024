using Microsoft.AspNetCore.Mvc;
using PencaAPI.Models;
using PencaAPI.Services;

namespace PencaAPI.Controllers;

[ApiController]
[Route(template:"[controller]")]
public class EquipoController(EquipoService equipoService) : ControllerBase
{
    private readonly EquipoService _equipoService = equipoService;
    
    [HttpGet]
    public async Task<ActionResult<Equipo[]>> GetAll()
    {
        try{
            var equipos = await _equipoService.GetAllAsync();
            if (equipos.Length == 0) return NoContent();
            return Ok(equipos);
        } catch (ArgumentException e) {
            return NotFound(e.Message);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Equipo>> GetById(string id)
    {
        try
        {
            var equipo = await _equipoService.GetByIdAsync(id);
            return Ok(equipo);
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }
    
}