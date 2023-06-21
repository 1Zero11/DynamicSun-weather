using DynamicSun_weather.Models;

namespace DynamicSun_weather.Data
{
    public interface IEntryService
    {
        IEnumerable<EntryModel> FileToEntries(IFormFile file);
        Task<int> Upload(IEnumerable<IFormFile> files);
        IEnumerable<EntryModel> GetList(int num);
    }
}