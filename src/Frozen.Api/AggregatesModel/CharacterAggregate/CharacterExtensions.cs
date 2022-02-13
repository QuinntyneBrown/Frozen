using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Frozen.Api
{
    public static class CharacterExtensions
    {
        public static CharacterDto ToDto(this Character character)
        {
            return new ()
            {
                CharacterId = character.CharacterId,
                Name = character.Name,
            };
        }
        
        public static async Task<List<CharacterDto>> ToDtosAsync(this IQueryable<Character> characters, CancellationToken cancellationToken)
        {
            return await characters.Select(x => x.ToDto()).ToListAsync(cancellationToken);
        }
        
        public static List<CharacterDto> ToDtos(this IEnumerable<Character> characters)
        {
            return characters.Select(x => x.ToDto()).ToList();
        }
        
    }
}
