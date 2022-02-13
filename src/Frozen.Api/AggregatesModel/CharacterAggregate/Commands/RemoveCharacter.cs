using FluentValidation;
using Frozen.Api;
using Frozen.Api.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Frozen.Api
{

    public class RemoveCharacterRequest: IRequest<RemoveCharacterResponse>
    {
        public int CharacterId { get; set; }
    }
    public class RemoveCharacterResponse: ResponseBase
    {
        public CharacterDto Character { get; set; }
    }
    public class RemoveCharacterHandler: IRequestHandler<RemoveCharacterRequest, RemoveCharacterResponse>
    {
        private readonly IFrozenDbContext _context;
        private readonly ILogger<RemoveCharacterHandler> _logger;
    
        public RemoveCharacterHandler(IFrozenDbContext context, ILogger<RemoveCharacterHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<RemoveCharacterResponse> Handle(RemoveCharacterRequest request, CancellationToken cancellationToken)
        {
            var character = await _context.Characters.FindAsync(request.CharacterId);
            
            _context.Characters.Remove(character);
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return new ()
            {
                Character = character.ToDto()
            };
        }
        
    }

}
