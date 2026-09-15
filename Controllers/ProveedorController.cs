using Caso04actividadclase.interfaces;
using Caso04actividadclase.Models;
using Microsoft.AspNetCore.Mvc;

namespace Caso04actividadclase.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProveedorController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ProveedorController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var proveedores = await _unitOfWork
            .Repository<Proveedore>()
            .GetAllAsync();

        return Ok(proveedores);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var proveedor = await _unitOfWork
            .Repository<Proveedore>()
            .GetByIdAsync(id);

        if (proveedor == null)
            return NotFound();

        return Ok(proveedor);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Proveedore proveedor)
    {
        await _unitOfWork
            .Repository<Proveedore>()
            .AddAsync(proveedor);

        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = proveedor.Idproveedor },
            proveedor
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Proveedore proveedor)
    {
        if (id != proveedor.Idproveedor)
            return BadRequest("El ID de la URL no coincide con el ID del proveedor.");

        var existente = await _unitOfWork
            .Repository<Proveedore>()
            .GetByIdAsync(id);

        if (existente == null)
            return NotFound();

        _unitOfWork
            .Repository<Proveedore>()
            .Update(proveedor);

        await _unitOfWork.CompleteAsync();

        return Ok(proveedor);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var proveedor = await _unitOfWork
            .Repository<Proveedore>()
            .GetByIdAsync(id);

        if (proveedor == null)
            return NotFound();

        _unitOfWork
            .Repository<Proveedore>()
            .Delete(proveedor);

        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}