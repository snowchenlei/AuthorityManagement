﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Cl.AuthorityManagement.Web
{
    public class PushHub : Hub
    {
        public void Send(string message)
        {
            Clients.All.showMessage(message);
        }
    }
}