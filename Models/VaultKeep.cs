using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace keepr.Models
{
  public class VaultKeep
  {
    public int Id { get; set; }
    [Required]
    public int VaultId { get; set; }
    [Required]
    public int KeepId { get; set; }
    public string UserId { get; set; }

  }
}