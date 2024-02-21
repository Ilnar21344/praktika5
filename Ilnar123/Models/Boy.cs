using System;
using System.Collections.Generic;

namespace Ilnar123.Models;

public partial class Boy
{
    public int Id { get; set; }

    public string? Surname { get; set; }

    public string? Name { get; set; }

    public string? Patronymic { get; set; }

    public string? Age { get; set; }
    public DateTime Birthdate { get; internal set; }
    public string? FullName { get; internal set; }
}
