using System.Collections.Concurrent;
using BlockCountriesTask.Dtos;
using BlockCountriesTask.IServices;
using BlockCountriesTask.SpecParams;

namespace BlockCountriesTask.Services
{
    public class CountryBlockService : ICountryBlockService
    {
        private readonly ConcurrentDictionary<string, CountryDto> _blockedCountries = new();

        public bool Add(CountryDto country)
        {
            var code = country.CountryCode.ToUpper();
            return _blockedCountries.TryAdd(code, country);
        }

        public bool Remove(string countryCode)
        {
            return _blockedCountries.TryRemove(countryCode.ToUpper(), out _);
        }

        public bool IsBlocked(string countryCode)
        {
            return _blockedCountries.ContainsKey(countryCode.ToUpper());
        }

        public Pagination GetAll(CountrySpecParams specParams)
        {
            var query = _blockedCountries.Values.AsQueryable();

            if (!string.IsNullOrWhiteSpace(specParams.Search))
            {
                specParams.Search = specParams.Search.ToUpper();
                query = query.Where(c =>
                    c.CountryCode.ToUpper().Contains(specParams.Search) ||
                    c.CountryName.ToUpper().Contains(specParams.Search));
            }
            var count = query.Count();
            var data= query
                .Skip((specParams.PageIndex - 1) * specParams.PageSize)
                .Take(specParams.PageSize)
                .ToList();
            return new Pagination(specParams.PageSize, specParams.PageIndex, count, data);
        }
    }

}
