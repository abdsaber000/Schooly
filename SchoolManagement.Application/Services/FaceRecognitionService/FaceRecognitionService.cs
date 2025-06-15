using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace SchoolManagement.Application.Services.FaceRecognitionService;

public class DataItem
{
    public string user_id { get; set; } = string.Empty;
}

public class RootObject
{
    public DataItem? data { get; set; }
    public string? message { get; set; }
}

public class FaceRecognitionService : IFaceRecognitionService
{
    private readonly string _registerUrl;
    private readonly string _verifyUrl;
    private readonly string _resetUrl;
    public FaceRecognitionService(IConfiguration configuration)
    {
        _registerUrl = configuration["AI:RegisterUrl"] ?? throw new ArgumentNullException(nameof(configuration));
        _verifyUrl = configuration["AI:VerifyUrl"] ?? throw new ArgumentNullException(nameof(configuration));
        _resetUrl = configuration["AI:ResetUrl"] ?? throw new ArgumentNullException(nameof(configuration));
    }
    public async Task<bool> RegisterFaceAsync(string studentId, IFormFile image)
    {
        using var httpClient = new HttpClient();
        using var form = new MultipartFormDataContent();

        form.Add(new StringContent(studentId), "user_id");
        using var imageContent = new StreamContent(image.OpenReadStream());
        imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(image.ContentType);
        form.Add(imageContent, "image", image.FileName);

        var response = await httpClient.PostAsync(_registerUrl, form);
        return response.IsSuccessStatusCode;
    }

    public async Task<FaceRecognitionServiceDto> VerifyFaceAsync(IFormFile image)
    {

        using var httpClient = new HttpClient();
        using var form = new MultipartFormDataContent();

        using var imageContent = new StreamContent(image.OpenReadStream());
        imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(image.ContentType);
        form.Add(imageContent, "image", image.FileName);

        var response = await httpClient.PostAsync(_verifyUrl, form);
        // response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        var userIdDict = JsonConvert.DeserializeObject<RootObject>(responseContent);

        var userId = userIdDict != null && userIdDict.data != null ? userIdDict.data.user_id : "";
        var errorMessage = userIdDict != null && userIdDict.message != null ? userIdDict.message : "";

        return new FaceRecognitionServiceDto()
        {
            StudentId = userId,
            IsSuccess = response.IsSuccessStatusCode,
            ErrorMessage = errorMessage
        };

    }

    public async Task<ResetFaceDto> ResetFaceAsync(string Id)
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.DeleteAsync($"{_resetUrl}/{Id}");
        var responseContent = await response.Content.ReadAsStringAsync();

        var userIdDict = JsonConvert.DeserializeObject<RootObject>(responseContent);

        var Message = userIdDict != null && userIdDict.message != null ? userIdDict.message : "";

        return new ResetFaceDto()
        {
            IsSuccess = response.IsSuccessStatusCode,
            Message = Message
        };
    }
}
