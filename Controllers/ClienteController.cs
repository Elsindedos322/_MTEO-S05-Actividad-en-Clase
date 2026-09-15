using Caso04actividadclase.interfaces;
using Caso04actividadclase.Models;
using Microsoft.AspNetCore.Mvc;

namespace Caso04actividadclase.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ClienteController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var clientes = await _unitOfWork
            .Repository<Cliente>()
            .GetAllAsync();

        return Ok(clientes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var cliente = await _unitOfWork
            .Repository<Cliente>()
            .GetByIdAsync(id);

        if (cliente == null)
            return NotFound();

        return Ok(cliente);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Cliente cliente)
    {
        await _unitOfWork
            .Repository<Cliente>()
            .AddAsync(cliente);

        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = cliente.Idcliente },
            cliente
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Cliente cliente)
    {
        if (id != cliente.Idcliente)
            return BadRequest("El ID de la URL no coincide con el ID del cliente.");

        var clienteExistente = await _unitOfWork
            .Repository<Cliente>()
            .GetByIdAsync(id);

        if (clienteExistente == null)
            return NotFound();

        _unitOfWork
            .Repository<Cliente>()
            .Update(cliente);

        await _unitOfWork.CompleteAsync();

        return Ok(cliente);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var cliente = await _unitOfWork
            .Repository<Cliente>()
            .GetByIdAsync(id);

        if (cliente == null)
            return NotFound();

        _unitOfWork
            .Repository<Cliente>()
            .Delete(cliente);

        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}