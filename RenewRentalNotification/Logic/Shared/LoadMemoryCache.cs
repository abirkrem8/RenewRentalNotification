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

            _memoryCache.Set("SMTPEmailAddress", _config.GetValue<string>("EmailSettings:SMTPEmailAddress"));
            _memoryCache.Set("SMTPEmailPassword", _config.GetValue<string>("EmailSettings:SMTPEmailPassword"));
            _memoryCache.Set("SMTPPort", _config.GetValue<int>("EmailSettings:SMTPPort"));
            
            _memoryCache.Set("DaysToLookAhead", _config.GetValue<int>("DaysToLookAhead"));


            _memoryCache.Set("ManagementEmailAddress", _config.GetValue<string>("ManagementEmailAddress"));
            _memoryCache.Set("CCEmailAddress", _config.GetValue<string>("CCEmailAddress"));
            _memoryCache.Set("EmailToTenantBody", _config.GetValue<string>("EmailToTenantBody"));
            _memoryCache.Set("EmailToTenantSubject", _config.GetValue<string>("EmailToTenantSubject"));

            _memoryCache.Set("EmailToManagementBody", _config.GetValue<string>("EmailToManagementBody"));
            _memoryCache.Set("EmailToManagementBodyNoMoveOuts", _config.GetValue<string>("EmailToManagementBodyNoMoveOuts"));
            _memoryCache.Set("EmailToManagementSubject", _config.GetValue<string>("EmailToManagementSubject"));

            _memoryCache.Set("AttachmentFile", _config.GetValue<string>("AttachmentFile"));
        }
    }
}
