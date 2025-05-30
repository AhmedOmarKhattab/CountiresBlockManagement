using BlockCountriesTask.Dtos;
using BlockCountriesTask.SpecParams;

namespace BlockCountriesTask.IServices
{
    public interface ICountryBlockService
    {
        bool Add(CountryDto countryDto);
        bool Remove(string countryCode);
        bool IsBlocked(string countryCode);
        Pagination GetAll(CountrySpecParams specParams);
    }
}
