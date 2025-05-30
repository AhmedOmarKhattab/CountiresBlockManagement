namespace BlockCountriesTask.IServices
{
    public interface ITemporalBlockService
    {
        bool IsTemporarilyBlocked(string countryCode);
        void CleanupExpiredBlocks();
        public bool AddTemporaryBlock(string countryCode, int durationMinutes, out string error);
    }
}
