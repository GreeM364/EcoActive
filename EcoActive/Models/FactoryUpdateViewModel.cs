using System.ComponentModel.DataAnnotations;

namespace EcoActive.API.Models
{
    public class FactoryUpdateViewModel
    {
        [Required]
        public string Name { get; set; } = null!;

        public string? ActivistId { get; set; }

        [Required]
        public string Territory { get; set; } = null!;

        [Required]
        public string Type { get; set; } = null!;
    }
}
