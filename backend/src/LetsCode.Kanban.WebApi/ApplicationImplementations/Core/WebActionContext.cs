using System.Threading;
using LetsCode.Kanban.Application.Core;
using LetsCode.Kanban.Application.Models;
using Microsoft.AspNetCore.Http;

namespace LetsCode.Kanban.WebApi.ApplicationImplementations.Core
{
    public class WebActionContext : IActionContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDateTime _dateTime;
        public CancellationToken Cancel => _httpContextAccessor.HttpContext.RequestAborted;
        public CardData CardData { get; private set; }
        public WebActionContext(IHttpContextAccessor httpContextAccessor, IDateTime dateTime)
        {
            _httpContextAccessor = httpContextAccessor;
            _dateTime = dateTime;
        }

        public void RegisterUserAction(Card card, UserAction userAction)
        {
            CardData = new CardData
            {
                Id = card.Id,
                Timestamp = _dateTime.Now,
                Action = userAction,
                Content = card.Content,
                ListId = card.ListId,
                Title = card.Title,
            };
        }
    }
}