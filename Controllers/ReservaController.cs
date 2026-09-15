using Caso04actividadclase.interfaces;
using Caso04actividadclase.Models;
using Microsoft.AspNetCore.Mvc;

namespace Caso04actividadclase.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservaController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ReservaController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var reservas = await _unitOfWork
            .Repository<Reserva>()
            .GetAllAsync();

        return Ok(reservas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var reserva = await _unitOfWork
            .Repository<Reserva>()
            .GetByIdAsync(id);

        if (reserva == null)
            return NotFound();

        return Ok(reserva);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Reserva reserva)
    {
        await _unitOfWork
            .Repository<Reserva>()
            .AddAsync(reserva);

        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = reserva.Idreserva },
            reserva
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Reserva reserva)
    {
        if (id != reserva.Idreserva)
            return BadRequest("El ID de la URL no coincide con el ID de la reserva.");

        var existente = await _unitOfWork
            .Repository<Reserva>()
            .GetByIdAsync(id);

        if (existente == null)
            return NotFound();

        _unitOfWork
            .Repository<Reserva>()
            .Update(reserva);

        await _unitOfWork.CompleteAsync();

        return Ok(reserva);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var reserva = await _unitOfWork
            .Repository<Reserva>()
            .GetByIdAsync(id);

        if (reserva == null)
            return NotFound();

        _unitOfWork
            .Repository<Reserva>()
            .Delete(reserva);

        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}