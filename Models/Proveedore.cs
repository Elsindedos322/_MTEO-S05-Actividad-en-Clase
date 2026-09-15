using System;
using System.Collections.Generic;

namespace Caso04actividadclase.Models;

public partial class Proveedore
{
    public int Idproveedor { get; set; }

    public string Nombre { get; set; } = null!;

    public string Tiposervicio { get; set; } = null!;

    public string Contacto { get; set; } = null!;

    public decimal? Calificacionpromedio { get; set; }

    public virtual ICollection<Evaluacionesproveedor> Evaluacionesproveedors { get; set; } = new List<Evaluacionesproveedor>();

    public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
}
