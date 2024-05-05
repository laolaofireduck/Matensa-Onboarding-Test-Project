namespace Ewallet.Core.Application.Services.Datetime;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}