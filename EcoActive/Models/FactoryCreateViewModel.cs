using System.ComponentModel.DataAnnotations;

namespace EcoActive.API.Models
{
    public class FactoryCreateViewModel
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Territory { get; set; } = null!;

        [Required]
        public string Type { get; set; } = null!;
    }
}
