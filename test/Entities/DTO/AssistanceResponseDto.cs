using System;
using test.Entities.Models;

namespace test.Entities.DTO
{
    public class AssistanceResponseDto
    {
        public bool Successful { get; set; }
        public AssistanceType? AssistanceType { get; set; }
        public string? ErrorMessage { get; set; }
    }
}

