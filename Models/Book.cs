using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VizippAssignmentCode.Models;

[Index("Isbn", Name = "UQ__Books__447D36EA023D5A04", IsUnique = true)]
public partial class Book
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string Title { get; set; } = null!;

    [StringLength(255)]
    public string Author { get; set; } = null!;

    [Column("ISBN")]
    [StringLength(50)]
    public string Isbn { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime PublishedDate { get; set; }

    [StringLength(100)]
    public string Genre { get; set; } = null!;
}
