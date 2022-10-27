using CRUD.ServiceProvider;
using CRUD.ServiceProvider.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Net;
using System.Text;

namespace CRUD_Application.Filter
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger, IEmailSender emailSender, IConfiguration configuration)
        {
            _logger = logger;
            _emailSender = emailSender;
            _configuration = configuration;
        }
        public void OnException(ExceptionContext context)
        {
            var logFIle = _configuration.GetSection("FilePath").Value + Path.Combine("log" + Guid.NewGuid().ToString() + ".txt");
            AddToLog(context.Exception, logFIle);
            var files = _configuration.GetSection("FilePath").Value + Path.GetFileName(logFIle);
            var message = new Message("tbs.rajg@gmail.com", "Error log mail with Attachments", "This is the content from our mail with attachments.", files);
            _emailSender.SendEmail(message);
        }
        public static void AddToLog(Exception exception, string path)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(DateTime.Now.ToLocalTime().ToString("F"));
            GetExceptionInfo(exception, sb);
            sb.AppendLine("------------------------------------------------------------" + Environment.NewLine);
            File.AppendAllText(path, sb.ToString());
        }

        private static void GetExceptionInfo(Exception exception, StringBuilder sb)
        {
            sb.AppendLine(exception.GetType().ToString());
            sb.AppendLine(exception.Message);
            sb.AppendLine("Stack Trace: ");
            sb.AppendLine(exception.StackTrace);
            if (exception.InnerException != null)
            {
                sb.AppendLine("InnerException: ");
                GetExceptionInfo(exception.InnerException, sb);
            }
        }
    }
}
