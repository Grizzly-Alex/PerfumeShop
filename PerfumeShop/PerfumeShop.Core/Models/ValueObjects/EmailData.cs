namespace PerfumeShop.Core.Models.ValueObjects;

public class EmailData
{
    #region Sender
    public string? From { get; }
    public string? DisplayName { get; }
    public string? ReplyTo { get; }
    public string? ReplyToName { get; }
    #endregion

    #region Receiver
    public List<string> To { get; }
    public List<string> Bcc { get; }
    public List<string> Cc { get; }
    #endregion

    #region Content
    public string? Subject { get; }
    public string? Body { get; }
    public IFormFileCollection Attachments { get; }
    #endregion

    public EmailData(string subject, 
        List<string> to, string? body = null, List<string>? bcc = null, List<string>? cc = null,
        string? from = null, string? displayName = null, string? replyTo = null, string? replyToName = null,
        IFormFileCollection? attachments = null)
    {
        Subject = subject;
        To = to;
        Body = body;
        Bcc = bcc ?? new List<string>();
        Cc = cc ?? new List<string>();
        From = from;
        DisplayName = displayName;
        ReplyTo = replyTo;
        ReplyToName = replyToName;
        Attachments = attachments ?? new FormFileCollection();
    }
}
