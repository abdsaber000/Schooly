using MediatR;
using SchoolManagement.Application.Services.AgoraService;

namespace SchoolManagement.Application.Features.Rooms.Commands.CreateRoom;

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
            Token = token,
            expiresAt = DateTime.UtcNow.AddSeconds(request.ExpirationTimeInSeconds)
        });
    }
}