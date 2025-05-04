using System;

namespace Tarker.Booking.Domain.Models.SendGridEmail;

public class SendGridEmailRequestModel
{
    public ContentEmail From { get; set; } = new ContentEmail();
    public List<Personalization> Personalizations { get; set; } = new List<Personalization>();
    public List<ContentBody> Content { get; set; } = new List<ContentBody>();
}

public class ContentBody
{
    public string Type { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
public class ContentEmail
{
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
public class Personalization
{
    public string Subject { get; set; } = string.Empty;
    public List<ContentEmail> To { get; set; } = new List<ContentEmail>();
}
