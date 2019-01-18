using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace keepr.Models
{
  public class Vault
  {
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    public string UserId { get; set; }

  }
}
