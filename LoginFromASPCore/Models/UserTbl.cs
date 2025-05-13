using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LoginFromASPCore.Models;

public partial class UserTbl
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Gender { get; set; } = null!;
    public int? Age { get; set; }
    [Required]
    public string Email { get; set; } = null!;
    [DataType(DataType.Password)]
    [Required]
    public string Password { get; set; } = null!;
}
