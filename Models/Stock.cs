using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoSupermercado.Models;
public class Stock
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int ProductId { get; set; }
    
    public Product? Product { get; set; }

    public string? ProductName { get; set; }

    public int ProductQuantity { get; set; }
}