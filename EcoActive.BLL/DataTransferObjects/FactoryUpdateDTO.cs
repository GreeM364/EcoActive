﻿namespace EcoActive.BLL.DataTransferObjects
{
    public class FactoryUpdateDTO
    {
        public string? ActivistId { get; set; }
        public string Name { get; set; } = null!;
        public string Territory { get; set; } = null!;
        public string Type { get; set; } = null!;
        public DateTime DataPaySubscription { get; set; }
    }
}
