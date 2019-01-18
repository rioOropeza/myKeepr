using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace keepr.Models
{
  public class VaultKeep
  {
    [Required]
    public int id { get; set; }
    public int vaultId { get; set; }
    public int keepId { get; set; }
    public int userId { get; set; }

  }
}