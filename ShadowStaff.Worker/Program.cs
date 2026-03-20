using Microsoft.AspNetCore.Mvc;
using ShadowStaff.Worker;

var builder = WebApplication.CreateBuilder(args);

// 1. Dependency Injection
builder.Services.AddSingleton<IJobParser, HiringCafeParser>();
builder.Services.AddSingleton<TriageService>();
builder.Services.AddHttpClient<IGeminiClient, GeminiClient>();

var app = builder.Build();

// 2. The Ingest Endpoint for n8n
app.MapPost("/ingest", async ([FromBody] IngestRequest request, 
    IJobParser parser, 
    TriageService triage, 
    IGeminiClient gemini) =>
{
    // Parse the HTML from n8n
    var jobs = parser.ExtractJobs(request.Html);
    
    var results = new List<ProcessedDraft>();

    foreach (var job in jobs)
    {
        // Step 1: Determine Lens
        string lens = triage.DetermineLens(job);
        
        // Step 2: Select Master CV (This is where your logic lives!)
        string masterCv = (lens == "Support") ? "SUPPORT_CV_TEXT" : "ENGINEERING_CV_TEXT";

        // Step 3: Call Gemini
        var draft = await gemini.GenerateDraftAsync(job, masterCv);

        results.Add(new ProcessedDraft(job.Title, job.Company, lens, draft));
    }

    return Results.Ok(new { JobCount = results.Count, Drafts = results });
});

app.Run();

// DTOs for the JSON Contract
public record IngestRequest(string Html);
public record ProcessedDraft(string Title, string Company, string Lens, string Draft);