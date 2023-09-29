using CachingwithRedis.Data;
using CachingwithRedis.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace CachingwithRedis.Repos
{
    public class CachingMemberRepository : IMemberRepository
    {
        private readonly IMemberRepository _repository;
        private readonly IDistributedCache _cache;
        private readonly DatabaseContext _context;
        public CachingMemberRepository(
            IMemberRepository repository,
            IDistributedCache cache,
            DatabaseContext context)
        {
            _repository = repository;
            _cache = cache;
            _context = context;
        }

        public async Task<Member?> GetById(int id, CancellationToken cancellationToken = default)
        {
            string key = $"members-{id}";

            string cachedMember = await _cache.GetStringAsync(key,cancellationToken);
            Member? member;
            if (string.IsNullOrEmpty(cachedMember))
            {
                member = await _repository.GetById(id,cancellationToken);
                if (member == null)
                {
                    return member;
                }

                await _cache.SetStringAsync(key, JsonConvert.SerializeObject(member), cancellationToken);
                return member;
            }
             
            member = JsonConvert.DeserializeObject<Member>(cachedMember);
            if (member is not null)
            {
                _context.Set<Member>().Attach(member);
            }
            return member;
        }

     
    }
}