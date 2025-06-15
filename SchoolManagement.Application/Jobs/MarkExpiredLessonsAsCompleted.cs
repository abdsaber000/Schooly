using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Jobs;

public class DeleteExpiredLessonsJob : BackgroundService
{
    private readonly ILogger<DeleteExpiredLessonsJob> _logger;
    private readonly IServiceProvider _serviceProvider;
    public DeleteExpiredLessonsJob(IServiceProvider serviceProvider, ILogger<DeleteExpiredLessonsJob> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var lessonRepository = scope.ServiceProvider.GetRequiredService<ILessonRepository>();
                var result = await lessonRepository.MarkExpiredLessonsAsCompleted();
                if (result)
                {
                    _logger.LogInformation("Expired lessons marked successfully.");
                }
                else
                {
                    _logger.LogWarning("No expired lessons found to mark.");
                }
            }

            await Task.Delay(TimeSpan.FromMinutes(30) , stoppingToken); // Run every hour
        }
    }
    
}
