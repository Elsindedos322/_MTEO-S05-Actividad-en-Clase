using Caso04actividadclase.interfaces;
using Caso04actividadclase.Models;
using Microsoft.AspNetCore.Mvc;

namespace Caso04actividadclase.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaqueteturisticoController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public PaqueteturisticoController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var paquetes = await _unitOfWork
            .Repository<Paquetesturistico>()
            .GetAllAsync();

        return Ok(paquetes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var paquete = await _unitOfWork
            .Repository<Paquetesturistico>()
            .GetByIdAsync(id);

        if (paquete == null)
            return NotFound();

        return Ok(paquete);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Paquetesturistico paquete)
    {
        await _unitOfWork
            .Repository<Paquetesturistico>()
            .AddAsync(paquete);

        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = paquete.Idpaquete },
            paquete
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        Paquetesturistico paquete)
    {
        if (id != paquete.Idpaquete)
            return BadRequest("El ID de la URL no coincide con el ID del paquete.");

        var existente = await _unitOfWork
            .Repository<Paquetesturistico>()
            .GetByIdAsync(id);

        if (existente == null)
            return NotFound();

        _unitOfWork
            .Repository<Paquetesturistico>()
            .Update(paquete);

        await _unitOfWork.CompleteAsync();

        return Ok(paquete);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var paquete = await _unitOfWork
            .Repository<Paquetesturistico>()
            .GetByIdAsync(id);

        if (paquete == null)
            return NotFound();

        _unitOfWork
            .Repository<Paquetesturistico>()
            .Delete(paquete);

        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}