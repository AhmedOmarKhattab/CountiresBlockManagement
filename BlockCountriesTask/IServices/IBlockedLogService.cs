using BlockCountriesTask.Models;

namespace BlockCountriesTask.IServices
{
    public interface IBlockedLogService
    {
        void Log(BlockedAttemptLog log);
        IEnumerable<BlockedAttemptLog> GetAll(int pageIndex, int pageSize);
    }
}
