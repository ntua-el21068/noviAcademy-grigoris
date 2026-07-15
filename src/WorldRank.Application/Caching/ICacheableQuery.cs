using System;
using System.Collections.Generic;
using System.Text;

namespace WorldRank.Application.Caching

{
    public interface ICacheableQuery
    {
        string CacheKey { get; }
    }
}
