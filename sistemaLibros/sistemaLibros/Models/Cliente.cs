using System;
using System.Collections.Generic;

namespace sistemaLibros.Models;

public partial class Cliente
{
    public int IdCli { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Cedula { get; set; } = null!;

    public string FechaNac { get; set; } = null!;

    public int? Estado { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
