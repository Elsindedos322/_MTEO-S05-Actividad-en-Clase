using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Caso04actividadclase.Models;

public partial class Reserva
{
    public int Idreserva { get; set; }

    public int Idcliente { get; set; }

    public int Idpaquete { get; set; }

    public DateTime? Fechareserva { get; set; }

    public string? Estadoreserva { get; set; }

    public decimal Montototal { get; set; }

    [JsonIgnore]
    public virtual Cliente IdclienteNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual Paquetesturistico IdpaqueteNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
