using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkingEmailService.Services
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }


        public Message(IEnumerable<string> to, string title, string body)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x)));
            Title = title;
            Body = body;
        }
    }
}
