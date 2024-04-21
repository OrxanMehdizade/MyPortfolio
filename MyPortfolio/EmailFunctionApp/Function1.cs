using System.Net;
using Grpc.Core;
using System.Net.Mail;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;

namespace EmailFunctionApp
{
    public static class Function1
    {
        [FunctionName("SendEmail")]
        public static async Task<IActionResult> Run(
              [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
              ILogger log)
        {
            log.LogInformation("Received a request to send an email.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);

                string email = data?.email;
                string message = data?.message;

                // Configure SMTP client
                using (SmtpClient smtpClient = new SmtpClient("smtp.example.com"))
                {
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new NetworkCredential("your-email@example.com", "your-password");
                    smtpClient.EnableSsl = true;

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("your-email@example.com");
                    mailMessage.To.Add("recipient@example.com");
                    mailMessage.Subject = "Portfolio Contact Form Submission";
                    mailMessage.Body = message;

                    // Send the email
                    smtpClient.Send(mailMessage);
                }

                return new OkResult();
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Failed to send email.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
