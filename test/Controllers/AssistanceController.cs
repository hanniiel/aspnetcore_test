using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using test.Repository;
using test.Entities.DTO;
using test.JwtFeatures;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using test.Entities.Models;
using static System.Convert;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace test.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class AssistanceController : ControllerBase
{
    private readonly RepositoryContext _context;
    private readonly IMapper _mapper;
    private readonly JwtHandler _jwtHandler;
    private readonly UserManager<User> _userManager;

    public AssistanceController(RepositoryContext context, IMapper mapper, JwtHandler jwtHandler, UserManager<User> userManager)
    {
        _context = context;
        _mapper = mapper;
        _jwtHandler = jwtHandler;
        _userManager = userManager;
    }


    public static bool ValidateWorkingDay(String workingDays)
    {
        List<int> wDays = new List<int>();

        if (workingDays.Contains("-"))
        {
            var range = workingDays.Split("-");
            for (int i = ToInt32(range[0]); i <= ToInt32(range[1]); i++)
            {
                wDays.Add(i);
            }
        }
        else if (workingDays.Contains(","))
        {
            var days = workingDays.Split(",");
            foreach (var day in days)
            {
                wDays.Add(ToInt32(day));
            }
        }

        return wDays.Contains(((int)DateOnly.FromDateTime(DateTime.Now).DayOfWeek));
    }
    // POST api/assistance
    public async Task<ActionResult> Post([FromBody] AssistanceForRegistrationDto assistanceForRegistrationDto)
    {
        try
        {
            if (assistanceForRegistrationDto == null || !ModelState.IsValid)
                return BadRequest();
            //options: get assistance dates and workplaces
            //we can specify worplaces from user or by checkin device location and getting nearest worplaceID
            //the Schedule might be attached to the user/relationship by default so it never really changes (set this from users instance)
            var assistance = _mapper.Map<Assistance>(assistanceForRegistrationDto);

            //await _context.Assistances.AddAsync(assistance);
            var user = await _userManager.FindByNameAsync(User.Identity?.Name!);
            assistance.UserID = Guid.Parse(user!.Id);
            assistance.ScheduleID = 1; //get this from the user linked schedule
            //get schedule
            await _context.Entry(user!).Reference(u => u.Schedule).LoadAsync();
            var schedule = user!.Schedule;
            //
            if (!ValidateWorkingDay(schedule.WorkingDays))
                return BadRequest();

            ///schedule start
            var scheduleStart = TimeOnly.FromDateTime(schedule.TimeStart);
            var scheduleEnd = TimeOnly.FromDateTime(schedule.TimeEnd);
            //assistance time
            var assStart = TimeOnly.FromDateTime(assistanceForRegistrationDto.FechaInicialTurno);
            var assEnd = TimeOnly.FromDateTime(assistanceForRegistrationDto.FechaFinalTurno);
            //
            var query = _context.Assistances.Where(x => x.FechaInicialTurno == assistance.FechaInicialTurno).Count();
            if (query > 0)
            {
                //duplicated assistance
                return BadRequest(new AssistanceResponseDto { Successful = false, ErrorMessage="Ya se encuentra una asistencia del día de hoy" });
            }

            //
            if (assStart.IsBetween(scheduleStart, scheduleStart.AddMinutes(5))
                && assEnd.IsBetween(scheduleEnd.AddMinutes(-5), scheduleEnd.AddMinutes(5)))
            {
                assistance.Type = AssistanceType.Asistencia;
            }
            else if (assStart.IsBetween(scheduleStart.AddMinutes(-20), scheduleStart.AddMinutes(20))
                && assEnd.IsBetween(scheduleEnd.AddMinutes(-20),scheduleEnd.AddMinutes(20)))
            {
                assistance.Type = AssistanceType.Retardo;
            }
            else
            {
                assistance.Type = AssistanceType.Falta;
            }
            await _context.Assistances.AddAsync(assistance);
            await _context.SaveChangesAsync();

            return Ok(new AssistanceResponseDto { AssistanceType = assistance.Type, Successful = true });

        }
        catch (Exception ex)
        {
            return BadRequest(new AssistanceResponseDto {Successful=false,ErrorMessage=ex.Message });
        }

    }


}

