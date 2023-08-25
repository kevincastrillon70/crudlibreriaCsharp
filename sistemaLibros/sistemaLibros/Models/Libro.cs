using System;
using System.Collections.Generic;

namespace sistemaLibros.Models;

public partial class Libro
{
    public int Id { get; set; }

    public string? Titulo { get; set; }

    public string? Autor { get; set; }

    public string? Genero { get; set; }

    public string? Fechapublicacion { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
