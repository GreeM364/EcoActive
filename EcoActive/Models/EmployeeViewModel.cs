﻿using System.ComponentModel.DataAnnotations;

namespace EcoActive.API.Models
{
    public class EmployeeViewModel
    {
        public string Id { get; set; } = null!;
        public string FactoryId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public DateTime BeginningWorkingDay { get; set; }
        public DateTime EndWorkingDay { get; set; }
    }
}
