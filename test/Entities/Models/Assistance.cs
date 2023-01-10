using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace test.Entities.Models
{
    public class Assistance
    {
        public int AssistanceID { get; set; }
        public Guid UserID { get; set; }
        public DateTime? FechaInicialTurno { get; set; }
        public DateTime? FechaFinalTurno { get; set; }
        public int WorkplaceID { get; set; }
        public int ScheduleID { get; set; }
        public AssistanceType Type { get; set; }

        public virtual Workplace Workplace { get; set; }
        public virtual Schedule Schedule { get; set; }

    }

    public enum AssistanceType
    {
        Asistencia,
        Retardo,
        Falta,
    }
}

