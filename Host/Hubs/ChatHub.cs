﻿using Microsoft.AspNet.SignalR;
using System;
using System.Threading.Tasks;

namespace Host.Hubs
{
    public class ChatHub : Hub
    {
        /// <summary> 
        /// add connection to group 
        /// </summary> 
        /// <param name="groupName"></param> 
        /// <returns></returns> 
        public Task JoinGroup(string groupName)
        {
            return Groups.Add(Context.ConnectionId, groupName);
        }

        /// <summary> 
        /// remove connection from group 
        /// </summary> 
        /// <param name="groupName"></param> 
        /// <returns></returns> 
        public Task LeaveGroup(string groupName)
        {
            return Groups.Remove(Context.ConnectionId, groupName);
        }

        /// <summary> 
        /// send message to the connections in the group. 
        /// </summary> 
        /// <param name="groupName"></param> 
        /// <param name="userName"></param> 
        /// <param name="message"></param> 
        /// <param name="sendTime"></param> 
        public void SendToGroup(string groupName, string userName, string message, DateTime sendTime)
        {
            Clients.Group(groupName).ReceiveMessage(userName, message, sendTime);
        }
    }
}
