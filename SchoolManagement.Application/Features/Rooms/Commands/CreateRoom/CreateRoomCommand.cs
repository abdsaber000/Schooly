using MediatR;
using SchoolManagement.Application.Features.Rooms.Service;

namespace SchoolManagement.Application.Features.Rooms.Commands.CreateRoom;

public class CreateRoomCommand : IRequest<CreateRoomResponse>
{
    public string ChannelName { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public int ExpirationTimeInSeconds { get; set; } = 3600; // Default to 1 hour
}
public class CreateRoomResponse
{
    public string ChannelName { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, CreateRoomResponse>
{
    private readonly AgoraTokenService _agoraTokenService;

    public CreateRoomCommandHandler(AgoraTokenService agoraTokenService)
    {
        _agoraTokenService = agoraTokenService;
    }

    public Task<CreateRoomResponse> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var token = _agoraTokenService.GenerateToken(
            request.ChannelName,
            request.UserId,
            request.ExpirationTimeInSeconds);

        return Task.FromResult(new CreateRoomResponse
        {
            ChannelName = request.ChannelName,
            UserId = request.UserId,
            Token = token
        });
    }
}