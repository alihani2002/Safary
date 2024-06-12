using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Service.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmailBodyBuilder: IEmailBodyBuilder
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public EmailBodyBuilder(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public string GetEmailBody(string templete, Dictionary<string, string> Placeholders)
        {
            var filePath = $"{webHostEnvironment.WebRootPath}/templates/{templete}.html";
            StreamReader streamReader = new(filePath);
            var templeteContent = streamReader.ReadToEnd();
            streamReader.Close();
            foreach (var placeholder in Placeholders)
                templeteContent = templeteContent.Replace($"[{placeholder.Key}]", placeholder.Value);

            return templeteContent;
        }
    }
}
