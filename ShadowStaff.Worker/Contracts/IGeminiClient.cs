using ShadowStaff.Worker.Models;

public interface IGeminiClient
{
    Task<string> GenerateDraftAsync(JobOpportunity job, string masterCV);
}