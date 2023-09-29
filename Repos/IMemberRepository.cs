using CachingwithRedis.Models;

namespace CachingwithRedis.Repos
{
    public interface IMemberRepository
    { 
        public  Task<Member?> GetById(int id, CancellationToken cancellationToken=default);

    }
}