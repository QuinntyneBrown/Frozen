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

    public class GetCharacterByIdRequest: IRequest<GetCharacterByIdResponse>
    {
        public int CharacterId { get; set; }
    }
    public class GetCharacterByIdResponse: ResponseBase
    {
        public CharacterDto Character { get; set; }
    }
    public class GetCharacterByIdHandler: IRequestHandler<GetCharacterByIdRequest, GetCharacterByIdResponse>
    {
        private readonly IFrozenDbContext _context;
        private readonly ILogger<GetCharacterByIdHandler> _logger;
    
        public GetCharacterByIdHandler(IFrozenDbContext context, ILogger<GetCharacterByIdHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<GetCharacterByIdResponse> Handle(GetCharacterByIdRequest request, CancellationToken cancellationToken)
        {
            return new () {
                Character = (await _context.Characters.AsNoTracking().SingleOrDefaultAsync(x => x.CharacterId == request.CharacterId)).ToDto()
            };
        }
        
    }

}
