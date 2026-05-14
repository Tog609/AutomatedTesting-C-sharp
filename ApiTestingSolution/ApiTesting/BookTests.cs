using Allure.Net.Commons;
using Allure.Net.Commons.Attributes;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Serilog;
using System.Net;
using System.Net.Http.Json;

namespace ApiTesting;

[TestFixture]
[AllureSuite("Books API")]
public class BookTests : ApiTestBase
{
    private object GenerateBookPayload() => new
    {
        title = $"Book {Guid.NewGuid()}",
        author = $"Author {Guid.NewGuid()}",
        isbn = $"{Random.Shared.Next(100, 999)}-{Random.Shared.Next(1000000, 9999999)}",
        publishedDate = DateTime.UtcNow.ToString("o")
    };
    [Test]
    public async Task CreateBook_ShouldReturn201()
    {
        Log.Information("=== Test: CreateBook_ShouldReturn201 ===");

        var payload = GenerateBookPayload();
        Log.Information("Payload: {@Payload}", payload);

        var response = await Client.PostAsJsonAsync($"{BaseUrl}/Books", payload);
        Log.Information("StatusCode: {StatusCode}", response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Log.Information("Response body: {Content}", content);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

        var data = JObject.Parse(content);
        var createdBookId = data["id"]?.ToString();
        Log.Information("Created BookId: {BookId}", createdBookId);

        if (!string.IsNullOrEmpty(createdBookId))
        {
            await Client.DeleteAsync($"{BaseUrl}/Books/{createdBookId}");
            Log.Information("Cleanup: deleted BookId {BookId}", createdBookId);
        }
    }

    [Test]
    public async Task CreateDuplicateBook_ShouldFail()
    {
        Log.Information("=== Test: CreateDuplicateBook_ShouldFail ===");

        var payload = GenerateBookPayload();
        Log.Information("Payload: {@Payload}", payload);

        var firstResponse = await Client.PostAsJsonAsync($"{BaseUrl}/Books", payload);
        var firstContent = await firstResponse.Content.ReadAsStringAsync();
        Log.Information("First response: {Content}", firstContent);

        var createdBookId = JObject.Parse(firstContent)["id"]?.ToString();
        Log.Information("Created BookId: {BookId}", createdBookId);

        var duplicateResponse = await Client.PostAsJsonAsync($"{BaseUrl}/Books", payload);
        Log.Information("Duplicate StatusCode: {StatusCode}", duplicateResponse.StatusCode);

        Assert.That(
            duplicateResponse.StatusCode,
            Is.EqualTo(HttpStatusCode.BadRequest).Or.EqualTo(HttpStatusCode.Conflict));

        if (!string.IsNullOrEmpty(createdBookId))
        {
            await Client.DeleteAsync($"{BaseUrl}/Books/{createdBookId}");
            Log.Information("Cleanup: deleted BookId {BookId}", createdBookId);
        }
    }

    [Test]
    public async Task GetAllBooks_ShouldReturnList()
    {
        Log.Information("=== Test: GetAllBooks_ShouldReturnList ===");

        var response = await Client.GetAsync($"{BaseUrl}/Books");
        Log.Information("StatusCode: {StatusCode}", response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Log.Information("Response body: {Content}", content);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task GetBookById_ShouldReturnCorrectBook()
    {
        Log.Information("=== Test: GetBookById_ShouldReturnCorrectBook ===");
        Log.Information("BookId from Setup: {BookId}", BookId);

        var response = await Client.GetAsync($"{BaseUrl}/Books/{BookId}");
        Log.Information("StatusCode: {StatusCode}", response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Log.Information("Response body: {Content}", content);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task GetBook_InvalidId_ShouldReturn400()
    {
        Log.Information("=== Test: GetBook_InvalidId_ShouldReturn400 ===");

        var response = await Client.GetAsync($"{BaseUrl}/Books/invalid-id-format");
        Log.Information("StatusCode: {StatusCode}", response.StatusCode);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task GetBook_NonExistentId_ShouldReturn404()
    {
        Log.Information("=== Test: GetBook_NonExistentId_ShouldReturn404 ===");

        var fakeId = Guid.NewGuid().ToString();
        Log.Information("FakeId: {FakeId}", fakeId);

        var response = await Client.GetAsync($"{BaseUrl}/Books/{fakeId}");
        Log.Information("StatusCode: {StatusCode}", response.StatusCode);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task UpdateBook_ShouldReturn204()
    {
        Log.Information("=== Test: UpdateBook_ShouldReturn204 ===");

        var updatedBook = GenerateBookPayload();
        Log.Information("Updated payload: {@Payload}", updatedBook);

        var response = await Client.PutAsJsonAsync($"{BaseUrl}/Books/{BookId}", updatedBook);
        Log.Information("StatusCode: {StatusCode}", response.StatusCode);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }

    [Test]
    public async Task GetUpdatedBook_ShouldReflectChanges()
    {
        Log.Information("=== Test: GetUpdatedBook_ShouldReflectChanges ===");

        var updatedBook = GenerateBookPayload();
        Log.Information("Updated payload: {@Payload}", updatedBook);

        await Client.PutAsJsonAsync($"{BaseUrl}/Books/{BookId}", updatedBook);

        var response = await Client.GetAsync($"{BaseUrl}/Books/{BookId}");
        Log.Information("StatusCode: {StatusCode}", response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Log.Information("Response body: {Content}", content);

        Assert.That(content, Does.Contain(updatedBook.GetType().GetProperty("title")?.GetValue(updatedBook)?.ToString()));
    }

    [Test]
    public async Task UpdateBook_NonExistentId_ShouldReturn404()
    {
        Log.Information("=== Test: UpdateBook_NonExistentId_ShouldReturn404 ===");

        var updatedBook = GenerateBookPayload();
        Log.Information("Updated payload: {@Payload}", updatedBook);

        var fakeId = Guid.NewGuid().ToString();
        Log.Information("FakeId: {FakeId}", fakeId);

        var response = await Client.PutAsJsonAsync($"{BaseUrl}/Books/{fakeId}", updatedBook);
        Log.Information("StatusCode: {StatusCode}", response.StatusCode);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task UpdateBook_InvalidId_ShouldReturn400()
    {
        Log.Information("=== Test: UpdateBook_InvalidId_ShouldReturn400 ===");

        var updatedBook = GenerateBookPayload();
        Log.Information("Updated payload: {@Payload}", updatedBook);

        var response = await Client.PutAsJsonAsync($"{BaseUrl}/Books/invalid-id", updatedBook);
        Log.Information("StatusCode: {StatusCode}", response.StatusCode);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task DeleteBook_ShouldReturn204()
    {
        Log.Information("=== Test: DeleteBook_ShouldReturn204 ===");
        Log.Information("BookId: {BookId}", BookId);

        var response = await Client.DeleteAsync($"{BaseUrl}/Books/{BookId}");
        Log.Information("StatusCode: {StatusCode}", response.StatusCode);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

        BookId = null;
    }

    [Test]
    public async Task GetDeletedBook_ShouldReturn404()
    {
        Log.Information("=== Test: GetDeletedBook_ShouldReturn404 ===");
        Log.Information("BookId: {BookId}", BookId);

        await Client.DeleteAsync($"{BaseUrl}/Books/{BookId}");

        var response = await Client.GetAsync($"{BaseUrl}/Books/{BookId}");
        Log.Information("StatusCode: {StatusCode}", response.StatusCode);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

        BookId = null;
    }

    [Test]
    public async Task DeleteBook_NonExistentId_ShouldReturn404()
    {
        Log.Information("=== Test: DeleteBook_NonExistentId_ShouldReturn404 ===");

        var fakeId = Guid.NewGuid().ToString();
        Log.Information("FakeId: {FakeId}", fakeId);

        var response = await Client.DeleteAsync($"{BaseUrl}/Books/{fakeId}");
        Log.Information("StatusCode: {StatusCode}", response.StatusCode);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task DeleteBook_InvalidId_ShouldReturn400()
    {
        Log.Information("=== Test: DeleteBook_InvalidId_ShouldReturn400 ===");

        var response = await Client.DeleteAsync($"{BaseUrl}/Books/invalid-id-format");
        Log.Information("StatusCode: {StatusCode}", response.StatusCode);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }
}
