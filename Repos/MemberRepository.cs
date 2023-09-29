using CachingwithRedis.Data;
using CachingwithRedis.Models;
using Microsoft.EntityFrameworkCore;

namespace CachingwithRedis.Repos
{
    public class MemberRepository : IMemberRepository
    {
        private readonly DatabaseContext _dbContext;

        public MemberRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Member?> GetById(int id, CancellationToken cancellationToken)
        {
            return await _dbContext
                .Set<Member>()
                .FirstAsync(member => member.Id == id);
        } 
    }
}