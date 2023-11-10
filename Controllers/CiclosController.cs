﻿using Microsoft.AspNetCore.Mvc;
using UniversidadAPI.Modelos;
using UniversidadAPI.Servicios;

namespace UniversidadAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CiclosController : ControllerBase
{
    private readonly CiclosService _ciclosService;

    public CiclosController(CiclosService ciclosService) =>
        _ciclosService = ciclosService;

    [HttpGet]
    public async Task<List<Ciclo>> Get() =>
        await _ciclosService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Ciclo>> Get(string id)
    {
        var ciclo = await _ciclosService.GetAsync(id);

        if (ciclo is null)
        {
            return NotFound();
        }

        return ciclo;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Ciclo newCiclo)
    {
        await _ciclosService.CreateAsync(newCiclo);

        return CreatedAtAction(nameof(Get), new { id = newCiclo.Id }, newCiclo);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Ciclo updatedCiclo)
    {
        var ciclo = await _ciclosService.GetAsync(id);

        if (ciclo is null)
        {
            return NotFound();
        }

        updatedCiclo.Id = ciclo.Id;

        await _ciclosService.UpdateAsync(id, updatedCiclo);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var ciclo = await _ciclosService.GetAsync(id);

        if (ciclo is null)
        {
            return NotFound();
        }

        await _ciclosService.RemoveAsync(id);

        return NoContent();
    }
}
