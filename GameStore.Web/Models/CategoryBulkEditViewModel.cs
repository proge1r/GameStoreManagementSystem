using GameStore.Web.Data.Entities;

namespace GameStore.Web.Models;

public class CategoryBulkEditViewModel
{
    public List<Category> Categories { get; set; } = new();
}