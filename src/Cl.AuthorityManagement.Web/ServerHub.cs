using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Cl.AuthorityManagement.Web
{
    public class ServerHub : Hub
    {
        /// <summary>
        /// 供客户端调用的服务器端代码
        /// </summary>
        /// <param name="message"></param>
        public void Send(string message)
        {
            // 调用所有客户端的sendMessage方法
            Clients.All.sendMessage(message);
        }
    }
}