using AgoraNET;
using RtcTokenBuilder = AgoraNET.RtcTokenBuilder;
using Microsoft.Extensions.Configuration;

namespace SchoolManagement.Application.Services.AgoraService;

public class AgoraService : IAgoraService
{
    private readonly string _appId;
    private readonly string _appCertificate;
    
    public AgoraService(IConfiguration configuration)
    {
        _appId = configuration["AgoraSettings:AppId"];
        _appCertificate = configuration["AgoraSettings:AppCertificate"];
    }

    public string GenerateToken(string channelName, string userId, int expirationTimeInSeconds)
    {
        DateTime expirationTime = DateTime.UtcNow.AddSeconds(expirationTimeInSeconds);
        uint unixTimestamp = (uint)(expirationTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);

        string token = new RtcTokenBuilder().BuildToken(
            _appId, _appCertificate, channelName, userId, RtcUserRole.Publisher, unixTimestamp);
        
        return token;
    }
}