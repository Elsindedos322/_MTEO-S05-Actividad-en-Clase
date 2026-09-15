using System;
using System.Collections.Generic;

namespace Caso04actividadclase.Models;

public partial class Paquetesturistico
{
    public int Idpaquete { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Preciobase { get; set; }

    public DateOnly Fechainicio { get; set; }

    public DateOnly Fechafin { get; set; }

    public int Cupomaximo { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    public virtual ICollection<Servicio> Idservicios { get; set; } = new List<Servicio>();
}
