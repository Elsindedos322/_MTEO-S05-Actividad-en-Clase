using System;
using System.Collections.Generic;

namespace Caso04actividadclase.Models;

public partial class Pago
{
    public int Idpago { get; set; }

    public int Idreserva { get; set; }

    public decimal Monto { get; set; }

    public DateTime? Fechapago { get; set; }

    public string Metodopago { get; set; } = null!;

    public string Estadopago { get; set; } = null!;

    public virtual Reserva IdreservaNavigation { get; set; } = null!;
}
