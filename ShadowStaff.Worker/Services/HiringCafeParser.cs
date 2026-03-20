using HtmlAgilityPack;
using ShadowStaff.Worker.Models;
using System.Net;

namespace ShadowStaff.Worker;

public class HiringCafeParser : IJobParser
{
    public List<JobOpportunity> ExtractJobs(string html)
    {
        var jobs = new List<JobOpportunity>();
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        // TARGET: Look directly for the content anchors (h3 tags) instead of layout tables
        var headingNodes = doc.DocumentNode.SelectNodes("//h3");

        if (headingNodes == null) return jobs;

        foreach (var heading in headingNodes)
        {
            var title = heading.InnerText?.Trim();
            
            // Skip empty headings or navigational headers
            if (string.IsNullOrWhiteSpace(title) || title.Length < 3 || title.Contains("HiringCafe"))
                continue;

            // The Company and Location are typically in the very next <div> or <p>
            var metaDataNode = heading.SelectSingleNode("following-sibling::div") ?? 
                               heading.SelectSingleNode("following-sibling::p");
                               
            var metaData = metaDataNode?.InnerText;
            
            string company = "Unknown";
            string location = "Remote";

            if (!string.IsNullOrWhiteSpace(metaData))
            {
                // Decode entities like &nbsp; before splitting
                metaData = WebUtility.HtmlDecode(metaData).Trim();
                
                // Handle different types of dashes or separators used in emails
                var separators = new[] { "—", "-", "•", "|" };
                var parts = metaData.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                
                if (parts.Length > 0) company = parts[0].Trim();
                if (parts.Length > 1) location = parts[1].Trim();
            }

            jobs.Add(new JobOpportunity(
                Title: WebUtility.HtmlDecode(title),
                Company: company,
                Location: location,
                // Grab the text of the parent container as a fallback for the description
                Description: WebUtility.HtmlDecode(heading.ParentNode?.InnerText?.Trim() ?? "")
            ));
        }

        return jobs;
    }
}