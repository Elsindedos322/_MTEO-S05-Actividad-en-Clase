using Caso04actividadclase.interfaces;
using Caso04actividadclase.Models;
using Microsoft.AspNetCore.Mvc;

namespace Caso04actividadclase.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PagoController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public PagoController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var pagos = await _unitOfWork
            .Repository<Pago>()
            .GetAllAsync();

        return Ok(pagos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var pago = await _unitOfWork
            .Repository<Pago>()
            .GetByIdAsync(id);

        if (pago == null)
            return NotFound();

        return Ok(pago);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Pago pago)
    {
        await _unitOfWork
            .Repository<Pago>()
            .AddAsync(pago);

        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = pago.Idpago },
            pago
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Pago pago)
    {
        if (id != pago.Idpago)
            return BadRequest("El ID de la URL no coincide con el ID del pago.");

        var existente = await _unitOfWork
            .Repository<Pago>()
            .GetByIdAsync(id);

        if (existente == null)
            return NotFound();

        _unitOfWork
            .Repository<Pago>()
            .Update(pago);

        await _unitOfWork.CompleteAsync();

        return Ok(pago);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var pago = await _unitOfWork
            .Repository<Pago>()
            .GetByIdAsync(id);

        if (pago == null)
            return NotFound();

        _unitOfWork
            .Repository<Pago>()
            .Delete(pago);

        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}