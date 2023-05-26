using System;
using System.Collections.Generic;

namespace ProjectFreeelancer.Models;

public partial class Recibo
{
    public int Id { get; set; }

    public string Logo { get; set; } = null!;

    public string TipoMoneda { get; set; } = null!;

    public decimal MontoCobrar { get; set; }

    public string Titulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string NombresCompletos { get; set; } = null!;

    public string TipoDocumento { get; set; } = null!;

    public string NumeroDocumento { get; set; } = null!;

    public string PdfRecibo { get; set; } = null!;
}
