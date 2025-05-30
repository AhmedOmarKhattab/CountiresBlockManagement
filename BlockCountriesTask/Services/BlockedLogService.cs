using BlockCountriesTask.IServices;
using BlockCountriesTask.Models;

namespace BlockCountriesTask.Services
{
    public class BlockedLogService : IBlockedLogService
    {
        private readonly List<BlockedAttemptLog> _logs = new();

        public void Log(BlockedAttemptLog log)
        {
            lock (_logs)
            {
                _logs.Add(log);
            }
        }

        public IEnumerable<BlockedAttemptLog> GetAll(int pageIndex, int pageSize)
        {
            lock (_logs)
            {
                return _logs
                    .OrderByDescending(l => l.Timestamp)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
        }
    }

}
