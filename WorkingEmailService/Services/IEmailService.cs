﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkingEmailService.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(Message message);
    }
}
