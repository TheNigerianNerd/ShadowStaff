using System.Text;
using System.Text.Json;
using System.Net.Http.Json;
using ShadowStaff.Worker.Models;

public class GeminiClient : IGeminiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private const string ModelUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent";

    public GeminiClient(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _apiKey = config["Gemini:ApiKey"] ?? throw new ArgumentNullException("Gemini API Key missing");
    }

    public async Task<string> GenerateDraftAsync(JobOpportunity job, string masterCv)
    {
        var requestBody = new
        {
            contents = new[]
            {
                new { parts = new[] { new { text = BuildPrompt(job, masterCv) } } }
            }
        };

        var response = await _httpClient.PostAsJsonAsync($"{ModelUrl}?key={_apiKey}", requestBody);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadFromJsonAsync<JsonElement>();
        // Navigating Gemini's specific JSON response structure
        return json.GetProperty("candidates")[0]
                   .GetProperty("content")
                   .GetProperty("parts")[0]
                   .GetProperty("text")
                   .GetString() ?? "";
    }

    private string BuildPrompt(JobOpportunity job, string masterCv)
    {
        return $@"
            You are an expert career coach. 
            CONTEXT: Here is my Master CV: {masterCv}
            TASK: Create a tailored Markdown CV and a short Cover Letter for this job:
            TITLE: {job.Title}
            COMPANY: {job.Company}
            DESCRIPTION: {job.Description}
            
            INSTRUCTIONS: Focus on high-impact achievements. Return only the Markdown.";
    }
}