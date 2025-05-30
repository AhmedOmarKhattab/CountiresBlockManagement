using BlockCountriesTask.IServices;
using System.Collections.Concurrent;

namespace BlockCountriesTask.Services
{
    public class TemporalBlockService : ITemporalBlockService
    {
        private readonly ConcurrentDictionary<string, DateTime> _tempBlocks = new();

        public bool AddTemporaryBlock(string countryCode, int durationMinutes, out string error)
        {
            error = null;
            countryCode = countryCode.ToUpper();

            if (durationMinutes < 1 || durationMinutes > 1440)
            {
                error = "Duration must be between 1 and 1440 minutes.";
                return false;
            }

            if (!IsValidCountryCode(countryCode))
            {
                error = "Invalid country code.";
                return false;
            }

            if (_tempBlocks.ContainsKey(countryCode))
            {
                error = "Country is already temporarily blocked.";
                return false;
            }

            var expiry = DateTime.UtcNow.AddMinutes(durationMinutes);
            _tempBlocks[countryCode] = expiry;
            return true;
        }

        public bool IsTemporarilyBlocked(string countryCode)
        {
            return _tempBlocks.TryGetValue(countryCode.ToUpper(), out var expiry) && expiry > DateTime.UtcNow;
        }

        public void CleanupExpiredBlocks()
        {
            var now = DateTime.UtcNow;
            foreach (var item in _tempBlocks)
            {
                if (item.Value <= now)
                {
                    _tempBlocks.TryRemove(item.Key, out _);
                }
            }
        }

        private bool IsValidCountryCode(string code)
        {
            return code.Length == 2 && code.All(char.IsLetter);
        }
    }
}
