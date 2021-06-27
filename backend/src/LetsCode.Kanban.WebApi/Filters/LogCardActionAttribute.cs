using LetsCode.Kanban.Application.Core;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace LetsCode.Kanban.WebApi.Filters
{
    public class LogCardActionAttribute : ActionFilterAttribute
    {
        private readonly IActionContext _actionContext;
        private readonly ILogger _logger;

        public LogCardActionAttribute(IActionContext actionContext, ILogger<LogCardActionAttribute> logger)
        {
            _actionContext = actionContext;
            _logger = logger;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var cardData = _actionContext.CardData;
            if (cardData == null) {
                return;
            }

            var message = GetLogMessage(cardData);
            if (message == null)
            {
                return;
            }

            _logger.LogInformation(message);

        }

        private string GetLogMessage(CardData cardData)
        {
            return cardData.Action switch
            {
                UserAction.Update => $"{cardData.Timestamp} - Card {cardData.Id} - {cardData.Title} - Updated",
                UserAction.Remove => $"{cardData.Timestamp} - Card {cardData.Id} - {cardData.Title} - Removed",
                _ => null
            };
        }
    }
}