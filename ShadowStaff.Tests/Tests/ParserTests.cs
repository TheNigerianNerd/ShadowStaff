using Xunit;
using ShadowStaff.Worker;

public class ParserTests
{
    [Fact]
public void ExtractJobs_WithMultipleJobs_ReturnsAllItems()
{
    var parser = new HiringCafeParser();
    string fakeHtml = @"
        <table style='vertical-align:top'>
            <h3><span>IT Support Specialist</span></h3>
            <div>Oportun — Remote</div>
        </table>";

    var result = parser.ExtractJobs(fakeHtml);

    Assert.Single(result);
    Assert.Equal("IT Support Specialist", result[0].Title);
    Assert.Equal("Oportun", result[0].Company);
    Assert.Equal("Remote", result[0].Location); // New Assertion
}
}