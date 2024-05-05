namespace Ewallet.Core.Application.Services.Datetime;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}