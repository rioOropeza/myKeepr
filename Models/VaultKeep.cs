using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace keepr.Models
{
  public class VaultKeep
  {
    public int id { get; set; }
    [Required]
    public int vaultId { get; set; }
    [Required]
    public int keepId { get; set; }
    [Required]
    public string userId { get; set; }

  }
}