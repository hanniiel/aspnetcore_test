using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace test.Entities.Models
{
	public class User: IdentityUser
	{
		public string? FirstName { get; set; }
		public int? ScheduleID { get; set; }
		public int? WorkplaceID { get; set; }

		public virtual Schedule Schedule { get; set; }
		public virtual Workplace Workplace { get; set; }
    }
}

