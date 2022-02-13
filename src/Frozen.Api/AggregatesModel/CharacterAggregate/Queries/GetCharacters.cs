using Frozen.Api;
using Frozen.Api.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Frozen.Api
{

    public class GetCharactersRequest: IRequest<GetCharactersResponse> { }
    public class GetCharactersResponse: ResponseBase
    {
        public List<CharacterDto> Characters { get; set; }
    }
    public class GetCharactersHandler: IRequestHandler<GetCharactersRequest, GetCharactersResponse>
    {
        private readonly IFrozenDbContext _context;
        private readonly ILogger<GetCharactersHandler> _logger;
    
        public GetCharactersHandler(IFrozenDbContext context, ILogger<GetCharactersHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<GetCharactersResponse> Handle(GetCharactersRequest request, CancellationToken cancellationToken)
        {
            return new () {
                Characters = await _context.Characters.AsNoTracking().ToDtosAsync(cancellationToken)
            };
        }
        
    }

}
