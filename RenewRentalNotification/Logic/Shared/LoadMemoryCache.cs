using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewRentalNotification.Logic.Shared
{
    public interface ILoadMemoryCache
    {
        void Handle(IConfiguration _config);
    }
    public class LoadMemoryCache : ILoadMemoryCache
    {
        private IMemoryCache _memoryCache;

        public LoadMemoryCache(IMemoryCache memoryCache, IConfiguration config)
        {
            _memoryCache = memoryCache;
        }

        public void Handle(IConfiguration _config)
        {
            _memoryCache.Set("ManagementEmailAddress", _config.GetValue<string>("ManagementEmailAddress"));
            _memoryCache.Get("ManagementEmailAddress");
            _memoryCache.Set("CCEmailAddress", _config.GetValue<string>("CCEmailAddress"));
            _memoryCache.Set("EmailToTenantBody", _config.GetValue<string>("EmailToTenantBody"));
            _memoryCache.Set("EmailToTenantSubject", _config.GetValue<string>("EmailToTenantSubject"));
        }
    }
}
