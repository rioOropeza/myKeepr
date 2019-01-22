using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace keepr.Models
{
  public class Keep
  {
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    public string UserId { get; set; }
    [Required]
    public string Img { get; set; }
    public bool IsPrivate { get; set; }
    public int Views { get; set; }
    public int Keeps { get; set; }

  }
}