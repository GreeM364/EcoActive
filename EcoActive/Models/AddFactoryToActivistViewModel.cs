using System.ComponentModel.DataAnnotations;

namespace EcoActive.API.Models
{
    public class AddFactoryToActivistViewModel
    {
        [Required]
        public string FactoryId { get; set; } = null!;
    }
}
