using Caso04actividadclase.interfaces;
using Caso04actividadclase.Models;
using Microsoft.AspNetCore.Mvc;

namespace Caso04actividadclase.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EvaluacionproveedorController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public EvaluacionproveedorController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var evaluaciones = await _unitOfWork
            .Repository<Evaluacionesproveedor>()
            .GetAllAsync();

        return Ok(evaluaciones);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var evaluacion = await _unitOfWork
            .Repository<Evaluacionesproveedor>()
            .GetByIdAsync(id);

        if (evaluacion == null)
            return NotFound();

        return Ok(evaluacion);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        Evaluacionesproveedor evaluacion)
    {
        await _unitOfWork
            .Repository<Evaluacionesproveedor>()
            .AddAsync(evaluacion);

        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = evaluacion.Idevaluacion },
            evaluacion
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        Evaluacionesproveedor evaluacion)
    {
        if (id != evaluacion.Idevaluacion)
            return BadRequest(
                "El ID de la URL no coincide con el ID de la evaluación.");

        var existente = await _unitOfWork
            .Repository<Evaluacionesproveedor>()
            .GetByIdAsync(id);

        if (existente == null)
            return NotFound();

        _unitOfWork
            .Repository<Evaluacionesproveedor>()
            .Update(evaluacion);

        await _unitOfWork.CompleteAsync();

        return Ok(evaluacion);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var evaluacion = await _unitOfWork
            .Repository<Evaluacionesproveedor>()
            .GetByIdAsync(id);

        if (evaluacion == null)
            return NotFound();

        _unitOfWork
            .Repository<Evaluacionesproveedor>()
            .Delete(evaluacion);

        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}