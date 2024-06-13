using Microsoft.AspNetCore.Mvc;
using PencaAPI.Services;
namespace PencaAPI.Controllers;

[ApiController]
[Route(template:"[controller]")]
public class CarreraController(CarreraService carreraService) : ControllerBase
{
    private readonly CarreraService _carreraService = carreraService;
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var carreras = await _carreraService.GetAllAsync();
        if (carreras.Length == 0) return NoContent();
        return Ok(carreras);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var carrera = await _carreraService.GetByIdAsync(id);
            return Ok(carrera);
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }
    
}