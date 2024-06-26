using Microsoft.AspNetCore.Mvc;
using PencaAPI.DTOs;
using PencaAPI.Models;
using PencaAPI.Services;

namespace PencaAPI.Controllers;

[ApiController]
[Route(template:"[controller]")]
public class PrediccionController(PrediccionService prediccionService) : ControllerBase
{
    private readonly PrediccionService _prediccionService = prediccionService;
    
    [HttpGet]
    public async Task<ActionResult<Prediccion[]>> GetAll()
    {
        try{
            var predicciones = await _prediccionService.GetAllAsync();
            if (predicciones.Length == 0) return NoContent();
            return Ok(predicciones);
        }catch (ArgumentException e) {
            return NotFound(e.Message);
        }
    }
    
    [HttpGet("{equipo_1}/{equipo_2}/{partidoFecha}/{alumno}")]
    public async Task<ActionResult<Prediccion>> GetById(DateTime partidoFecha, string equipo_1, string equipo_2, int alumno)
    {
        try
        {
            PrediccionDTO prediccionId = new PrediccionDTO(alumno ,partidoFecha, equipo_1, equipo_2);
            var prediccion = await _prediccionService.GetByIdAsync(prediccionId);
            return Ok(prediccion);
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }
    [HttpPost]
    public async Task<ActionResult<Prediccion>> Post(Prediccion prediccion)
    {
        try {
            var nuevoPrediccion = await _prediccionService.CreateAsync(prediccion);
            
            return CreatedAtAction(nameof(GetById),new {equipo_1 = nuevoPrediccion.Partido.Equipo_E1.Abreviatura , 
                                                        equipo_2 = nuevoPrediccion.Partido.Equipo_E2.Abreviatura ,
                                                        partidoFecha = nuevoPrediccion.Partido.Fecha,
                                                        alumno= nuevoPrediccion.Alumno.Cedula} , nuevoPrediccion);
        } catch (ArgumentException e) {
            return NotFound(e.Message);
        }
    }
    [HttpPut("{equipo_1}/{equipo_2}/{partidoFecha}/{alumno}")]
    public async Task<ActionResult<Prediccion>> UpdateById(DateTime partidoFecha, string equipo_1, string equipo_2,int alumno, Prediccion prediccion)
    {  
        try {
            PrediccionDTO prediccionId = new PrediccionDTO(alumno, partidoFecha, equipo_1, equipo_2);
            var prediccionEditado = await _prediccionService.UpdateAsync(prediccionId, prediccion);
        
            return CreatedAtAction(nameof(GetById),new { equipo_1 = prediccionEditado.Partido.Equipo_E1.Abreviatura , 
                                                        equipo_2 = prediccionEditado.Partido.Equipo_E2.Abreviatura ,
                                                        partidoFecha = prediccionEditado.Partido.Fecha,
                                                        alumno = prediccionEditado.Alumno.Cedula}  , prediccionEditado);
        } catch (ArgumentException e) {
            return NotFound(e.Message);
        }
    }
    [HttpDelete("{equipo_1}/{equipo_2}/{partidoFecha}")]
    public async Task<ActionResult> Delete(DateTime partidoFecha, string equipo_1, string equipo_2 ,int alumno)
    {
        try {
            PrediccionDTO prediccionId = new PrediccionDTO(alumno,partidoFecha, equipo_1, equipo_2);
            await _prediccionService.DeleteAsync(prediccionId);
            return NoContent();
        } catch (ArgumentException e) {
            return NotFound(e.Message);
        }
    }
    
}