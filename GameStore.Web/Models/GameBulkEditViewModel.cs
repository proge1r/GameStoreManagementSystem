using GameStore.Web.Data.Entities;

namespace GameStore.Web.Models;

public class GameBulkEditViewModel
{
    public List<Game> Games { get; set; } = new();
}