using System;
using System.Collections.Generic;

namespace sistemaLibros.Models;

public partial class Prestamo
{
    public int Id { get; set; }

    public int? Idlibro { get; set; }

    public int? Iduser { get; set; }

    public string? Fechainicio { get; set; }

    public string? Fechafin { get; set; }

    public virtual Libro? IdlibroNavigation { get; set; }

    public virtual Cliente? IduserNavigation { get; set; }
}
