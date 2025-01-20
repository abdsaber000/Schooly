using System.ComponentModel.DataAnnotations;
using MediatR;

namespace SchoolManagement.Application.Features.Rooms.Commands.CreateRoom;

public class CreateRoomCommand : IRequest<CreateRoomResponse>
{
    public string ChannelName { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    
    [Range(300, int.MaxValue, ErrorMessage = "Expiration time must be greater than or equal 5 minutes.")] // from 5 min 
    public int ExpirationTimeInSeconds { get; set; } = 3600; // Default to 1 hour
}
public class CreateRoomResponse
{
    public string ChannelName { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}

