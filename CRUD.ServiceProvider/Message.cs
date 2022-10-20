using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using MimeKit;

namespace CRUD.ServiceProvider
{
    public class Message
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string Attachments { get; set; }
        public Message(string to, string subject, string content, string attachments)
        {
            To = to;
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }
    }
}
