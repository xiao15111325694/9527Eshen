using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SingnalRAndIldentity.Models;

namespace SingnalRAndIldentity
{
    public class ChatHub : Hub
    {
        List<ApplicationUser> _Users = new List<ApplicationUser>();
        public void Send(string name, string message)
        {
            Clients.All.hello();
            Clients.All.addNewMessageToPage(name, message);
        }

        public void AddUser(ApplicationUser user )
        {
            user.Id = Context.ConnectionId;
            if (!_Users.Any(b => b.Id == user.Id))
               {
                    _Users.Add(user);
                }
            
                //Clients.All.newUser(user);
                Clients.All.userList(_Users);
        }

        public List<ApplicationUser> GetUser()
        {
            return _Users;
        }
    }
}