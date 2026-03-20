using Moq;
using Xunit;
using ShadowStaff.Worker;
using ShadowStaff.Worker.Models;
using MimeKit;

public class ArchitectTests
{
    [Theory]
    [InlineData("IT Support Specialist", "Support")] // Should trigger Support Lens
    [InlineData("Senior .NET Developer", "Engineering")] // Should trigger Engineering Lens
    public async Task Architect_SelectsCorrectLens_BasedOnTitle(string jobTitle, string expectedLens)
    {
        // 1. Arrange
        var mockGemini = new Mock<IGeminiClient>();
        var job = new JobOpportunity(Title: jobTitle, 
                                    Company: "TestCorp", 
                                    Location: "Somewhere over the rainbow", 
                                    Description: "Description here...");
        
        // Mocking the response from Gemini
        mockGemini.Setup(x => x.GenerateDraftAsync(It.IsAny<JobOpportunity>(), It.IsAny<string>()))
                  .ReturnsAsync("Mocked Markdown Content");

        var triage = new TriageService();
        
        // 2. Act
        var detectedLens = triage.DetermineLens(job);

        // 3. Assert
        Assert.Equal(expectedLens, detectedLens);
    }
    [Fact]
    public void ExtractJobs_FromActualEmailSource_ReturnsFullList()
    {
        // Arrange
        var parser = new HiringCafeParser();
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        var path = Path.Combine(baseDir, "Samples", "Job-Postings-HiringCafe.eml");

        if (!File.Exists(path)) throw new FileNotFoundException($"Sample missing: {path}");

        // Mimic n8n by extracting ONLY the decoded HTML body
        var message = MimeMessage.Load(path);
        string? actualHtml = message.HtmlBody;

        // Act
        var result = parser.ExtractJobs(actualHtml!);

        // Assert
        Assert.NotEmpty(result);
        Assert.True(result.Count >= 10, "Should detect at least 10 jobs in a standard bulletin");
    }
}