using Microsoft.Extensions.Configuration;
using AgoraNET;
using RtcTokenBuilder = AgoraNET.RtcTokenBuilder;

namespace SchoolManagement.Application.Features.Rooms.Service;

public class AgoraTokenService
{
    private readonly string _appId;
    private readonly string _appCertificate;

    public AgoraTokenService(IConfiguration configuration)
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