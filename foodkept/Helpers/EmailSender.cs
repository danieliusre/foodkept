﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FoodKept.Helpers
{
    public class EmailSender
    {
        private SmtpClient smtpClient;

        public EmailSender()
        {
            smtpClient = createClient();
        }

        public async Task<bool> sendEmailAsync(string email, string body)
        {
            MailMessage mail = new MailMessage();
            mail.IsBodyHtml = true;
            mail.From = new MailAddress("foodkepterino@gmail.com", "FoodKept");
            mail.To.Add(new MailAddress(email));
            mail.Subject = "Confirmation link";
            mail.Body = body;
            try
            {
                await Task.Run(() => smtpClient.Send(message: mail));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        SmtpClient createClient()
        {
            SmtpClient newClient = new SmtpClient();
            newClient.Credentials = new System.Net.NetworkCredential("foodkepterino@gmail.com", "foodkept4");
            newClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            newClient.EnableSsl = true;
            newClient.Port = 587;
            newClient.Host = "smtp.gmail.com";
            return newClient;
        }
    }
}
