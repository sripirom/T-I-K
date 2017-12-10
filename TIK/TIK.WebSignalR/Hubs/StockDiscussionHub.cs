using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TIK.Domain.TheSet;
using TIK.WebSignalR.Users;

namespace TIK.WebSignalR.Hubs
{
    //[Authorize]
    public class StockDiscussionHub : HubWithPresence
    {
        public StockDiscussionHub(IUserTracker<ChatHub> userTracker)
            : base(userTracker)
        {
        }


        public async Task AddStockDiscussion(int stockId, DiscussionItem discussionItem)
        {
            await Clients.Group(stockId.ToString()).InvokeAsync("addStockDiscussion", stockId, discussionItem);
        }


        public async Task JoinGroup(string groupName)
        {
            await Groups.AddAsync(Context.ConnectionId, groupName);

        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveAsync(Context.ConnectionId, groupName);

        }
    }
}
