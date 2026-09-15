using Caso04actividadclase.interfaces;
using Caso04actividadclase.Models;
using Microsoft.AspNetCore.Mvc;

namespace Caso04actividadclase.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ServiceController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var servicios = await _unitOfWork
            .Repository<Servicio>()
            .GetAllAsync();

        return Ok(servicios);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var servicio = await _unitOfWork
            .Repository<Servicio>()
            .GetByIdAsync(id);

        if (servicio == null)
            return NotFound();

        return Ok(servicio);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Servicio servicio)
    {
        await _unitOfWork
            .Repository<Servicio>()
            .AddAsync(servicio);

        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = servicio.Idservicio },
            servicio
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Servicio servicio)
    {
        if (id != servicio.Idservicio)
            return BadRequest("El ID de la URL no coincide con el ID del servicio.");

        var existente = await _unitOfWork
            .Repository<Servicio>()
            .GetByIdAsync(id);

        if (existente == null)
            return NotFound();

        _unitOfWork
            .Repository<Servicio>()
            .Update(servicio);

        await _unitOfWork.CompleteAsync();

        return Ok(servicio);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var servicio = await _unitOfWork
            .Repository<Servicio>()
            .GetByIdAsync(id);

        if (servicio == null)
            return NotFound();

        _unitOfWork
            .Repository<Servicio>()
            .Delete(servicio);

        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}