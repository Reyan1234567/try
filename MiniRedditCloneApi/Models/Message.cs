using MimeKit;

namespace MiniRedditCloneApi.Models
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public Message(IEnumerable<string> to, string subject, string content)
        {
            To = [];
            To.AddRange(to.Select(addr => new MailboxAddress("NerdHerd", addr)));
            Subject = subject;
            Content = content;
        }
    }
}
# File update Fri, Jan  9, 2026  9:16:17 PM
# Update Fri, Jan  9, 2026  9:25:58 PM
# Update Fri, Jan  9, 2026  9:34:42 PM
