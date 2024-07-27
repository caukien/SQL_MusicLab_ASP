using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

public class UploadService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;
    private const int MaxFileSize = 5 * 1024 * 1024;
    private static readonly string[] AllowedFileTypes = { "image/jpeg", "image/png", "image/webp" };

    public UploadService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiUrl = configuration["API"];
    }

    public async Task<string> UploadFileAsync(string fileName, string fileContent, string fileType)
    {

        var data = new
        {
            name = fileName,
            data = fileContent,
            type = fileType
        };

        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(_apiUrl, content);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("File upload failed");
        }

        var responseData = await response.Content.ReadAsStringAsync();
        dynamic result = JsonConvert.DeserializeObject(responseData);
        return result.link;
    }
}