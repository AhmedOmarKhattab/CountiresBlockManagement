using BlockCountriesTask.Dtos;

namespace BlockCountriesTask.IServices
{
    public interface IIpLookupService
    {
        Task<IpLookupResultDto> LookupIpAsync(string ipAddress);
    }
}
