using System;
using System.Threading.Tasks;
using LetsCode.Kanban.Application.UserActions.AddCard;
using LetsCode.Kanban.Application.UserActions.ListAllCards;
using LetsCode.Kanban.Application.UserActions.RemoveCard;
using LetsCode.Kanban.Application.UserActions.UpdateCard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LetsCode.Kanban.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("cards")]
    public class CardsController : ControllerBase
    {
        [HttpGet]
        public async Task<ListAllCardsResult> Get([FromServices] ListAllCardsAction listAllCardsAction)
        {
            var result = await listAllCardsAction.Execute();
            return result;
        }

        [HttpPost]
        public async Task<AddCardResult> Post(
            AddCardParameters parameters,
            [FromServices] AddCardAction addCardAction)
        {
            var result = await addCardAction.Execute(parameters);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<UpdateCardResult> Put(
            Guid id, 
            UpdateCardParametersWithoutId parametersWithoutId,
            [FromServices] UpdateCardAction updateCardAction)
        {
            var parameters = new UpdateCardParameters
            {
                Id = id,
                Title = parametersWithoutId.Title,
                Content = parametersWithoutId.Content,
                ListId = parametersWithoutId.ListId,
            };

            var result = await updateCardAction.Execute(parameters);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<RemoveCardResult> Delete(
            Guid id,
            [FromServices] RemoveCardAction removeCardAction)
        {
            var parameters = new RemoveCardParameters
            {
                Id = id,
            };

            var result = await removeCardAction.Execute(parameters);
            return result;
        }
        public class UpdateCardParametersWithoutId
        {
            public string Title { get; init; }
            public string Content { get; init; }
            public string ListId { get; init; }
        }
    }
}