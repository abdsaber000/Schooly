namespace SchoolManagement.Application.Services.AgoraService;

public interface IAgoraService
{
   string GenerateToken(string channelName, string userId, int expirationTimeInSeconds);
}