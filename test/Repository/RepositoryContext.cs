using System;
using System.ComponentModel;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using test.Entities.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace test.Repository
{
	public class RepositoryContext: IdentityDbContext<User>
	{
		public RepositoryContext(DbContextOptions options):base(options)
		{

		}

        /*protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<TimeOnly>()
                .HaveConversion<TimeOnlyConverter>()
                .HaveColumnType("date");


            base.ConfigureConventions(configurationBuilder);
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Workplace>()
                .HasData(
                    new Workplace { WorkplaceID = 1, Name= "Santa Fe - Architects" }
                );


            modelBuilder.Entity<Schedule>()
                .HasData(
                    new Schedule
                    {
                        ScheduleID = 1,
                        TimeStart = new DateTime(2022,1,1,9,0,0),
                        TimeEnd = new DateTime(2022, 1, 1, 18, 0, 0),
                        WorkingDays = "1-5"
                    }
                );

            /*modelBuilder.Entity<User>()
                .HasData(new User
                {
                    FirstName = "Haniel",
                    Email = "hanielaustria@gmail.com",
                    WorkplaceID = 1,
                    ScheduleID = 1
                 

                });
            */


        }


        public DbSet<Workplace> Workplaces { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Assistance> Assistances { get; set; }
    }
}

