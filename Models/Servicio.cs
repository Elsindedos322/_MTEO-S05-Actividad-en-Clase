using System;
using System.Collections.Generic;

namespace Caso04actividadclase.Models;

public partial class Servicio
{
    public int Idservicio { get; set; }

    public int Idproveedor { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Costo { get; set; }

    public int Capacidaddisponible { get; set; }

    public virtual Proveedore IdproveedorNavigation { get; set; } = null!;

    public virtual ICollection<Paquetesturistico> Idpaquetes { get; set; } = new List<Paquetesturistico>();
}
