namespace SchoolManagement.Application.Services.EgyptTimeService;

public class EgyptTime:IEgyptTime
{
    public (DateOnly TodayEgypt, TimeOnly CurrentTimeEgypt) GetCurrentEgyptTime()
    {
        var egyptTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Africa/Cairo");
        var nowEgypt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, egyptTimeZone);

        var todayEgypt = DateOnly.FromDateTime(nowEgypt);
        var currentTimeEgypt = TimeOnly.FromDateTime(nowEgypt);

        return (todayEgypt, currentTimeEgypt);
    }
}