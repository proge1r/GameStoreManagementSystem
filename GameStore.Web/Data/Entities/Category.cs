using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Data.Entities;

public class Category
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public List<Game>? Games { get; set; } = new();
}