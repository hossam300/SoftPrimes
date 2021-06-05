using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HelperServices.Hubs
{
   
    public class ChatHub : Hub
    {
      
        public override async Task OnConnectedAsync()
        {
            ChatHubConnectionHandler.AddConnection(Context.ConnectionId, Context.User.Identity.Name);
           await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            ChatHubConnectionHandler.RemoveConnection(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
        //public async Task SendMessage(int roomId, int userId, string message)
        //{
        //    var userRoom = _chatService.GetuserRoom(roomId);
        //    ChatMessage m = new ChatMessage()
        //    {
        //        ChatRoomId = roomId,
        //        Content = message,
        //        CreatedBy = userId
        //    };

        //    await _chatService.AddMessageToRoomAsync(roomId, m);
        //    await Clients.Groups(roomId.ToString()).SendAsync("ReceiveMessage", userId, message, roomId, m.ChatMessageId, m.CreatedOn);
        //}

        //public async Task JoinChatRoom(int? TransactionId, int userId, int Type, int? ToUserId)
        //{
        //    int roomId = 0;
        //    // Privte Chat
        //    if (Type == 1)
        //    {
        //        roomId = (int)TransactionId;
        //    }
        //    // Transaction Chat
        //    else if (Type == 2)
        //    {
        //        roomId = (int)ToUserId;
        //    }

        //    var room = _chatService.GetChatRoomsNyIdAsync(roomId);
        //    if (room == null)
        //    {
        //        ChatRoom chatRoom = new ChatRoom()
        //        {
        //            ChatRoomName = roomId.ToString(),
        //            Active = true,

        //        };
        //        await _chatService.AddChatRoomAsync(chatRoom, userId);
        //    }
        //    else
        //    {
        //        await _chatService.AddChatUseToRoomAsync(roomId, userId);
        //    }
        //    await Groups.AddToGroupAsync(userId.ToString(), roomId.ToString());
        //    await Clients.Groups(roomId.ToString()).SendAsync("JoinChatRoom", roomId);
        //}
        //public async Task GetOnlineUsers()
        //{
        //    var users = await _chatService.GetOnlineUsers(UsersOnline);
        //    await Clients.All.SendAsync("GetOnlineUsers", users);
        //}

    }
    //// To Use ConnectionId for Communication  
    public static class ChatHubConnectionHandler
    {
        //Use AddUser and RemoveUser instead of adding items directly
        public static Dictionary<string, string> Connections = new Dictionary<string, string>();

        public static void AddConnection(string connectionId, string userName)
        {
            Connections.Add(connectionId, userName);
            UserConnectedTask?.Invoke(userName);//Invoke it if not null
        }

        public static void RemoveConnection(string connectionId)
        {
            string userName;
            if (Connections.TryGetValue(connectionId, out userName))
            {
                Connections.Remove(connectionId);
                if (!Connections.ContainsValue(userName))
                {
                    UserDisconnectedTask?.Invoke(userName);//Invoke it if not null
                }
            }
        }

        public static Action<string> UserConnectedTask;
        public static Action<string> UserDisconnectedTask;
    }
}

