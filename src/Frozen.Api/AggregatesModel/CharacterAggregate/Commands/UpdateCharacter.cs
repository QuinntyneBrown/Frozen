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

    public class UpdateCharacterValidator: AbstractValidator<UpdateCharacterRequest>
    {
        public UpdateCharacterValidator()
        {
            RuleFor(request => request.Character).NotNull();
            RuleFor(request => request.Character).SetValidator(new CharacterValidator());
        }
    
    }
    public class UpdateCharacterRequest: IRequest<UpdateCharacterResponse>
    {
        public CharacterDto Character { get; set; }
    }
    public class UpdateCharacterResponse: ResponseBase
    {
        public CharacterDto Character { get; set; }
    }
    public class UpdateCharacterHandler: IRequestHandler<UpdateCharacterRequest, UpdateCharacterResponse>
    {
        private readonly IFrozenDbContext _context;
        private readonly ILogger<UpdateCharacterHandler> _logger;
    
        public UpdateCharacterHandler(IFrozenDbContext context, ILogger<UpdateCharacterHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<UpdateCharacterResponse> Handle(UpdateCharacterRequest request, CancellationToken cancellationToken)
        {
            var character = await _context.Characters.SingleAsync(x => x.CharacterId == request.Character.CharacterId);
            
            character.Name = request.Character.Name;
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return new ()
            {
                Character = character.ToDto()
            };
        }
        
    }

}
