using MediatR;
using SchoolManagement.Application.Services.AgoraService;

namespace SchoolManagement.Application.Features.Rooms.Commands.CreateRoom;

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, CreateRoomResponse>
{
    private readonly IAgoraService _agoraService;
    public CreateRoomCommandHandler(IAgoraService agoraService)
    {
        _agoraService = agoraService;
    }
    public Task<CreateRoomResponse> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var token = _agoraService.GenerateToken(
            request.ChannelName,
            request.UserId,
            request.ExpirationTimeInSeconds);

        return Task.FromResult(new CreateRoomResponse
        {
            ChannelName = request.ChannelName,
            UserId = request.UserId,
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddSeconds(request.ExpirationTimeInSeconds)
        });
    }
}