using ShadowStaff.Worker.Models;

namespace ShadowStaff.Worker;

public interface IJobParser
{
    List<JobOpportunity> ExtractJobs(string html);
}

