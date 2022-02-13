using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Frozen.Api;
using MediatR;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Frozen.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class CharacterController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CharacterController> _logger;

        public CharacterController(IMediator mediator, ILogger<CharacterController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [SwaggerOperation(
            Summary = "Get Character by id.",
            Description = @"Get Character by id."
        )]
        [HttpGet("{characterId:int}", Name = "getCharacterById")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetCharacterByIdResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCharacterByIdResponse>> GetById([FromRoute]int characterId, CancellationToken cancellationToken)
        {
            var request = new GetCharacterByIdRequest() { CharacterId = characterId };
        
            var response = await _mediator.Send(request, cancellationToken);
        
            if (response.Character == null)
            {
                return new NotFoundObjectResult(request.CharacterId);
            }
        
            return response;
        }
        
        [SwaggerOperation(
            Summary = "Get Characters.",
            Description = @"Get Characters."
        )]
        [HttpGet(Name = "getCharacters")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetCharactersResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCharactersResponse>> Get(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetCharactersRequest(), cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Create Character.",
            Description = @"Create Character."
        )]
        [HttpPost(Name = "createCharacter")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateCharacterResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateCharacterResponse>> Create([FromBody]CreateCharacterRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName}: ({@Command})",
                nameof(CreateCharacterRequest),
                request);
        
            return await _mediator.Send(request, cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Get Character Page.",
            Description = @"Get Character Page."
        )]
        [HttpGet("page/{pageSize}/{index}", Name = "getCharactersPage")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetCharactersPageResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCharactersPageResponse>> Page([FromRoute]int pageSize, [FromRoute]int index, CancellationToken cancellationToken)
        {
            var request = new GetCharactersPageRequest { Index = index, PageSize = pageSize };
        
            return await _mediator.Send(request, cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Update Character.",
            Description = @"Update Character."
        )]
        [HttpPut(Name = "updateCharacter")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateCharacterResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateCharacterResponse>> Update([FromBody]UpdateCharacterRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                nameof(UpdateCharacterRequest),
                nameof(request.Character.CharacterId),
                request.Character.CharacterId,
                request);
        
            return await _mediator.Send(request, cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Delete Character.",
            Description = @"Delete Character."
        )]
        [HttpDelete("{characterId:int}", Name = "removeCharacter")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(RemoveCharacterResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RemoveCharacterResponse>> Remove([FromRoute]int characterId, CancellationToken cancellationToken)
        {
            var request = new RemoveCharacterRequest() { CharacterId = characterId };
        
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                nameof(RemoveCharacterRequest),
                nameof(request.CharacterId),
                request.CharacterId,
                request);
        
            return await _mediator.Send(request, cancellationToken);
        }
        
    }
}
