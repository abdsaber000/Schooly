using Microsoft.Extensions.Configuration;

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
        var privilegeExpiredTs = (int)(DateTime.UtcNow.AddSeconds(expirationTimeInSeconds).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

        return RtcTokenBuilder.BuildTokenWithUid(
            _appId,
            _appCertificate,
            channelName,
            userId,
            RtcTokenBuilder.RtcRole.Publisher,
            privilegeExpiredTs);
    }
}