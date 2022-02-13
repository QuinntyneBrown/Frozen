using FluentValidation;
using Frozen.Api;
using Frozen.Api.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Frozen.Api
{

    public class CreateCharacterValidator: AbstractValidator<CreateCharacterRequest>
    {
        public CreateCharacterValidator()
        {
            RuleFor(request => request.Character).NotNull();
            RuleFor(request => request.Character).SetValidator(new CharacterValidator());
        }
    
    }
    public class CreateCharacterRequest: IRequest<CreateCharacterResponse>
    {
        public CharacterDto Character { get; set; }
    }
    public class CreateCharacterResponse: ResponseBase
    {
        public CharacterDto Character { get; set; }
    }
    public class CreateCharacterHandler: IRequestHandler<CreateCharacterRequest, CreateCharacterResponse>
    {
        private readonly IFrozenDbContext _context;
        private readonly ILogger<CreateCharacterHandler> _logger;
    
        public CreateCharacterHandler(IFrozenDbContext context, ILogger<CreateCharacterHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<CreateCharacterResponse> Handle(CreateCharacterRequest request, CancellationToken cancellationToken)
        {
            var character = new Character();
            
            _context.Characters.Add(character);
            
            character.Name = request.Character.Name;
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return new ()
            {
                Character = character.ToDto()
            };
        }
        
    }

}
