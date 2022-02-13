using Frozen.Api;
using Frozen.Api.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Frozen.Api
{

    public class GetCharactersPageRequest: IRequest<GetCharactersPageResponse>
    {
        public int PageSize { get; set; }
        public int Index { get; set; }
    }
    public class GetCharactersPageResponse: ResponseBase
    {
        public int Length { get; set; }
        public List<CharacterDto> Entities { get; set; }
    }
    public class GetCharactersPageHandler: IRequestHandler<GetCharactersPageRequest, GetCharactersPageResponse>
    {
        private readonly IFrozenDbContext _context;
        private readonly ILogger<GetCharactersPageHandler> _logger;
    
        public GetCharactersPageHandler(IFrozenDbContext context, ILogger<GetCharactersPageHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<GetCharactersPageResponse> Handle(GetCharactersPageRequest request, CancellationToken cancellationToken)
        {
            var query = from character in _context.Characters
                select character;
            
            var length = await _context.Characters.AsNoTracking().CountAsync();
            
            var characters = await query.Page(request.Index, request.PageSize).AsNoTracking()
                .Select(x => x.ToDto()).ToListAsync();
            
            return new ()
            {
                Length = length,
                Entities = characters
            };
        }
        
    }

}
