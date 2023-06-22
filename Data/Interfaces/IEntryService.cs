using DynamicSun_weather.Models;

namespace DynamicSun_weather.Data.Interfaces
{
    public interface IEntryService
    {
        IEnumerable<EntryModel> FileToEntries(IFormFile file);
        Task<int> Upload(IEnumerable<IFormFile> files);
        IQueryable<EntryModel> GetListPage(int perPage, int? page, int? year, int? month);
    }
}