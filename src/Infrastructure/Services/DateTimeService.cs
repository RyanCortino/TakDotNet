using TakDotNet.Application.Common.Interfaces;

namespace TakDotNet.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
