using System;
using System.Security.Cryptography;
using System.Text;

public static class RtcTokenBuilder
{
    public static string BuildTokenWithUid(string appId, string appCertificate, string channelName, string uid, RtcRole role, int privilegeExpiredTs)
    {
        if (string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(appCertificate) || string.IsNullOrEmpty(channelName))
        {
            throw new ArgumentException("appId, appCertificate, and channelName must be provided");
        }

        var ts = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var salt = new Random().Next(1, 99999999);

        // Building the raw token data
        string tokenData = $"{appId}{uid}{channelName}{salt}{ts}{privilegeExpiredTs}";

        // Creating the token signature using HMAC-SHA256
        var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(appCertificate));
        var signature = hmac.ComputeHash(Encoding.UTF8.GetBytes(tokenData));

        // Concatenate parts to create the token
        return $"{appId}:{BitConverter.ToString(signature).Replace("-", "")}:{ts}:{salt}";
    }

    public enum RtcRole
    {
        Publisher = 1,
        Subscriber = 2
    }
}