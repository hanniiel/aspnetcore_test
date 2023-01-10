using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace test.Entities.Models;

public class Schedule
{
	public int ScheduleID { get; set; }
	[Column(TypeName ="varchar(20)")]
	public string WorkingDays { get; set; } = "0-6"; //format 0-6 0,1,2,3,4,5,6 D=0
	public DateTime TimeStart { get; set; }
    public DateTime TimeEnd { get; set; }

}
    

