﻿using System.ComponentModel.DataAnnotations;

namespace EcoActive.API.Models
{
    public class FactoryAdministratorCreateViewModel
    {
        [Required]
        public string FactoryId { get; set; } = null!;

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public string Patronymic { get; set; } = null!;

        [Required]
        public string Phone { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Password { get; set; } = null!;
    }
}
