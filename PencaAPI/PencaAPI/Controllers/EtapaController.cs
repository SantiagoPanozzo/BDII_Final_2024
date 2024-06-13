using Microsoft.AspNetCore.Mvc;
using PencaAPI.Models;
using PencaAPI.Services;

namespace PencaAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class EtapaController(EtapaService etapaService) : ControllerBase
{
    private readonly EtapaService _etapaService = etapaService;
    
    [HttpGet]
    public async Task<ActionResult<Etapa[]>> Get()
    {
        Etapa[] etapas = await _etapaService.GetAllAsync();
        if (etapas.Length == 0) return NoContent();
        return Ok(etapas);
    }
    
    [HttpGet(template:"{id}")]
    public async Task<ActionResult<Etapa>> Get(int id)
    {
        try {
            var etapa = await _etapaService.GetByIdAsync(id);
            return Ok(etapa);
        } catch (ArgumentException e) {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Etapa>> Post(Etapa etapa)
    {
        var nuevaEtapa = await _etapaService.CreateAsync(etapa);
        return CreatedAtAction(nameof(Get), new { id = etapa.Id }, nuevaEtapa);
    }

    [HttpPut]
    public async Task<ActionResult<Etapa>> Put(int id, Etapa etapa)
    {
        try
        {
            await _etapaService.UpdateAsync(id, etapa);
            return Ok(etapa);
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete(template: "{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _etapaService.DeleteAsync(id);
            return NoContent();
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }
    
}