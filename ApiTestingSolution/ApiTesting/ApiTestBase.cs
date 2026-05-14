using NUnit.Framework;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Serilog;

namespace ApiTesting;

public class ApiTestBase
{
    protected static HttpClient Client;
    protected static string BaseUrl;

    private static IConfiguration _config;

    protected string? BookId;

    [OneTimeSetUp]
    public async Task OneTimeSetup()
    {
        LoggerConfig.ConfigureLogger();

        _config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        BaseUrl = _config["ApiBaseUrl"];

        Client = new HttpClient();

        var token = await GetAuthToken();

        Client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        Log.Information("Authorization completed successfully");
    }

    [SetUp]
    public async Task Setup()
    {
        var payload = new
        {
            title = $"Test Book {Guid.NewGuid()}",
            author = "IvanLemesh",
            isbn = $"{Random.Shared.Next(100, 999)}-{Random.Shared.Next(1000000, 9999999)}",
            publishedDate = DateTime.UtcNow.ToString("o")
        };
        Log.Information("Creating test book...");
        var response = await Client.PostAsJsonAsync(
            $"{BaseUrl}/Books",
            payload);
        Log.Information("Create Book Response: {Response}", response);
        var content = await response.Content.ReadAsStringAsync();

        var data = JObject.Parse(content);

        BookId = data["id"]?.ToString();
        Log.Information("Book created with ID: {BookId}", BookId);
    }

    [TearDown]
    public async Task TearDown()
    {
        if (!string.IsNullOrEmpty(BookId))
        {
            Log.Information("Deleting book with ID: {BookId}", BookId);

            await Client.DeleteAsync($"{BaseUrl}/Books/{BookId}");

        }
    }

    private async Task<string> GetAuthToken()
    {
        using var authClient = new HttpClient();

        var tokenUrl = _config["Auth:TokenUrl"];

        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>(
                "client_Id",
                _config["Auth:ClientId"]),

            new KeyValuePair<string, string>(
                "client_Secret",
                _config["Auth:ClientSecret"]),

            new KeyValuePair<string, string>(
                "scope",
                _config["Auth:Scope"]),

            new KeyValuePair<string, string>(
                "grant_type",
                "client_credentials"),
        });

        Log.Information("Requesting auth token...");

        var response = await authClient.PostAsync(tokenUrl, content);

        var json = JObject.Parse(
            await response.Content.ReadAsStringAsync());

        Log.Information("Token received successfully");

        return json["access_token"]?.ToString();
    }
}