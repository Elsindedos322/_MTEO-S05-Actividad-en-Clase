using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Caso04actividadclase.Models;

public partial class Evaluacionesproveedor
{
    public int Idevaluacion { get; set; }

    public int Idproveedor { get; set; }

    public int Idcliente { get; set; }

    public int? Puntuacion { get; set; }

    public string? Comentario { get; set; }

    [JsonIgnore]
    public virtual Cliente IdclienteNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual Proveedore IdproveedorNavigation { get; set; } = null!;
}
