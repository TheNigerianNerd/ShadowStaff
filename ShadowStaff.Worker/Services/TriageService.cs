using ShadowStaff.Worker.Models;

public class TriageService
{
    private readonly string[] _supportKeywords = { "Support", "Systems", "Identity", "Admin", "Infrastructure" };

    public string DetermineLens(JobOpportunity job)
    {
        // Check Title and Description for Support keywords
        bool isSupport = _supportKeywords.Any(k => 
            job.Title.Contains(k, StringComparison.OrdinalIgnoreCase) || 
            job.Description.Contains(k, StringComparison.OrdinalIgnoreCase));

        return isSupport ? "Support" : "Engineering";
    }
}