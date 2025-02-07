namespace SchoolManagement.Application.Services.EgyptTimeService;

public interface IEgyptTime
{
    (DateOnly TodayEgypt, TimeOnly CurrentTimeEgypt) GetCurrentEgyptTime();
}