using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using 自定义权限控制.Models;
using Microsoft.AspNet.SignalR.Hubs;

namespace SingnalRAndIldentity
{
    [HubName("systemHub")]
    public class ChatHub : Hub
    {
        List<UserGroup> _Users = new List<UserGroup>();
        public static List<User> ConnectedUsers = new List<User>();
        public void Send(string name, string message)
        {
            Clients.All.hello();
            Clients.All.addNewMessageToPage(name, message);
        }

        public void Connect(int userId, string userName)
        {
            var connectId = Context.ConnectionId;
        }

        public void AddUser(UserGroup user )
        {
            user.ID = int.Parse(Context.ConnectionId);
            if (!_Users.Any(b => b.ID == user.ID))
               {
                    _Users.Add(user);
               }
            
                //Clients.All.newUser(user);
                Clients.All.userList(_Users);
        }

        public List<UserGroup> GetUser()
        {
            return _Users;
        }
    }
}