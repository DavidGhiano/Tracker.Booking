using System;

namespace Tarker.Booking.Domain.Models.ApplicationInsights;

public class InsertApplicationInsightsModel
{
    public string Id { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Detail { get; set; } = string.Empty;

    public InsertApplicationInsightsModel(string type, string content, string detail){
        Id = Guid.NewGuid().ToString();
        Type = type;
        Content = content;
        Detail = detail;
    }
}
