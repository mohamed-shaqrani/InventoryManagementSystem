
using Microsoft.Extensions.Caching.Memory;


namespace InventoryManagementSystem.App.Features.Common.OTPService
{
    public class OTPService : IOTPService
    {
        private readonly IMemoryCache _memoryCache;

        public OTPService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public string GenerateOTP()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        public void SaveOTP(UserTempData user, string otp)
        {
            _memoryCache.Set(otp, user, TimeSpan.FromMinutes(10));
        }

        public UserTempData GetTempUser(string otp)
        {
            var user = _memoryCache.Get<UserTempData>(otp);
            return user;
        }

        public string GetOTP(string email)
        {
            var otp = _memoryCache.Get<string>(email);

            return otp;
        }

        public Task<bool> IsOTPExpiredAsync(string email)
        {
            var otp = _memoryCache.Get<string>(email);
            return Task.FromResult(string.IsNullOrEmpty(otp));
        }

        public Task<bool> VerifyOTPAsync(string email, string otp)
        {

            var exist = _memoryCache.TryGetValue(otp, out UserTempData? value);
            if (exist == true)
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}




