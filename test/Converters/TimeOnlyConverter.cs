using System;
using System.Linq.Expressions;
using AutoMapper.Execution;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq;

namespace test.Converters
{
    public class TimeOnlyConverter : ValueConverter<TimeOnly, DateTime>
    {
        public TimeOnlyConverter() : base(timeOnly=>new DateTime(timeOnly.Ticks),dateTime=>TimeOnly.FromDateTime(dateTime))
        {
        }
    }
}

