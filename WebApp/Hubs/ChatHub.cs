using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebApp.Models;

namespace SignalRMvc.Hubs
{
    public class ChatHub : Hub
    {
        WebAppContext db = new WebAppContext();

        public void Send(string User1Id, string User2Id, string message)
        {
            int _User1Id = Int32.Parse(User1Id);
            int _User2Id = Int32.Parse(User2Id);

            int chatId;
            Chat chat = db.Chats.Where(m => m.User1Id == _User1Id && m.User2Id == _User2Id).FirstOrDefault();
            if (chat == null)
            {
                chatId = db.Chats.Where(m => m.User1Id == _User2Id && m.User2Id == _User1Id).FirstOrDefault().Id;
            }
            else
            {
                chatId = chat.Id;
            }

            int userId = db.Users.Where(m => m.ConnectionId == Context.ConnectionId).FirstOrDefault().Id;
            Message newMessage = new Message { Text = message, Date = DateTime.Now, UserId = userId };
            db.Chats.Find(chatId).Messages.Add(newMessage);
            db.SaveChanges();


            Clients.Caller.addMessage(true, message, newMessage.HoursAndMinutes);
            Clients.Others.addMessage(false, message, newMessage.HoursAndMinutes);
        }

        public void Connect(string userId)
        {
            string ConnectionId = Context.ConnectionId;
            int Id = Int32.Parse(userId);

            db.Users.Find(Id).ConnectionId = ConnectionId;
            db.SaveChanges();
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = db.Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                item.ConnectionId = "";
                db.SaveChanges();
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}