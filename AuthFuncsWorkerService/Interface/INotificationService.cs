﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsWorkerService.Interface
{
    public interface INotificationService
    {
        void SendNotification(string recipient, string message);
    }
}
