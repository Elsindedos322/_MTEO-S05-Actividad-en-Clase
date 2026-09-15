using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Caso04actividadclase.Models;

public partial class Cliente
{
    public int Idcliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telefono { get; set; }

    public string Documentoidentidad { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Evaluacionesproveedor> Evaluacionesproveedors { get; set; } = new List<Evaluacionesproveedor>();

    [JsonIgnore]
    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
