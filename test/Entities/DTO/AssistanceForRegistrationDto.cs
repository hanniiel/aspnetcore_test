using System;
using System.ComponentModel.DataAnnotations;
using test.Entities.Models;

namespace test.Entities.DTO
{
    public class AssistanceForRegistrationDto
    {
        public string? UserId { get; set; } // get this on the jwt not really necessary I think
        public DateTime FechaInicialTurno { get; set; } //set this on the server side
        public DateTime FechaFinalTurno { get; set; } //same as before
        public int? IdSchedule { get; set; } //if this belongs to a Employee then is attached to the obj itself
        public int IdWorkplace { get; set; } //same as before

    }
}

