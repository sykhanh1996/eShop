﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace eShop.BackendServer.Services
{
    public class EmailSenderService : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            throw new NotImplementedException();
        }
    }
}
