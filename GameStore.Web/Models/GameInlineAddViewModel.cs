using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Models;

public class GameInlineAddViewModel
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, 10000)]
    public decimal Price { get; set; }

    [Required]
    public int CategoryId { get; set; }
}