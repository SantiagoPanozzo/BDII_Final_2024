using Microsoft.AspNetCore.Mvc;
using PencaAPI.DTOs;
using PencaAPI.Models;
using PencaAPI.Services;

namespace PencaAPI.Controllers;

[ApiController]
[Route(template:"[controller]")]
public class PartidoController(PartidoService partidoService) : ControllerBase
{
    private readonly PartidoService _partidoService = partidoService;
    
    [HttpGet]
    public async Task<ActionResult<Partido[]>> GetAll()
    {
        var Partidos = await _partidoService.GetAllAsync();
        if (Partidos.Length == 0) return NoContent();
        return Ok(Partidos);
    }
    
    [HttpGet("{equipo_1}/{equipo_2}/{partidoFecha}")]
    public async Task<ActionResult<Partido>> GetById(DateTime partidoFecha, string equipo_1, string equipo_2)
    {
        try
        {
            PartidoDTO partidoId = new PartidoDTO(partidoFecha, equipo_1, equipo_2);
            var Partido = await _partidoService.GetByIdAsync(partidoId);
            return Ok(Partido);
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }
    [HttpPost]
    public async Task<ActionResult<Partido>> Post(Partido partido)
    {
        Console.WriteLine("acaaa");
        var nuevoPartido = await _partidoService.CreateAsync(partido);
        
        return CreatedAtAction(nameof(GetById),new { equipo_1 = nuevoPartido.Equipo_E1.Abreviatura , 
                                                     equipo_2 = nuevoPartido.Equipo_E2.Abreviatura ,
                                                     partidoFecha = nuevoPartido.Fecha} , nuevoPartido);
    }
    [HttpPut("{equipo_1}/{equipo_2}/{partidoFecha}")]
    public async Task<ActionResult<Partido>> UpdateById(DateTime partidoFecha, string equipo_1, string equipo_2, Partido partido)
    {   
        PartidoDTO partidoId = new PartidoDTO(partidoFecha, equipo_1, equipo_2);
        var partidoEditado = await _partidoService.UpdateAsync(partidoId, partido);
       // PartidoDTO nuevoPartidoDTO= new PartidoDTO(partidoEditado.Fecha, partidoEditado.Equipo_E1.Abreviatura,partidoEditado.Equipo_E2.Abreviatura);
        return CreatedAtAction(nameof(GetById),new { equipo_1 = partidoEditado.Equipo_E1.Abreviatura , 
                                                     equipo_2 = partidoEditado.Equipo_E2.Abreviatura ,
                                                     partidoFecha = partidoEditado.Fecha}  , partidoEditado);
    }
    
}