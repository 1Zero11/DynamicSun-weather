﻿using DynamicSun_weather.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using NPOI.HSSF.Record;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Globalization;
using System.IO;

namespace DynamicSun_weather.Data
{
    public class EntryService : IEntryService
    {
        private readonly EntryContext _entryContext;
        public EntryService(EntryContext entryContext)
        {
            _entryContext = entryContext;
        }

        async public Task<int> Upload(IEnumerable<IFormFile> files)
        {
            foreach (IFormFile file in files)
            {
                var entries = FileToEntries(file);
                await _entryContext.Entries.AddRangeAsync(entries);
                await _entryContext.SaveChangesAsync();
            }

            return 1;
        }

        public IEnumerable<EntryModel> FileToEntries(IFormFile file)
        {
            var entries = new List<EntryModel>();
            using var ms = new MemoryStream();
            file.CopyTo(ms);
            ms.Position = 0;
            var hssfwb = new XSSFWorkbook(ms);

            var sheet = hssfwb.GetSheet("Январь 2010");
            for (int rowIndex = 4; rowIndex <= sheet.LastRowNum; rowIndex++)
            {
                var row = sheet.GetRow(rowIndex);
                if (row != null) //null is when the row only contains empty cells 
                {
                    var entry = new EntryModel();

                    var date = row.GetCell(0).StringCellValue;
                    var time = row.GetCell(1).StringCellValue;
                    var pattern = "dd.mm.yyyy";

                    DateTime parsedDate;
                    DateTime.TryParseExact(date, pattern, null,
                                      DateTimeStyles.None, out parsedDate);
                    parsedDate = parsedDate.ToUniversalTime();

                    entry.Date = parsedDate;

                    entry.Temp = ParseFloat(row.GetCell(2));
                    entry.Humidity = ParseInt(row.GetCell(3));
                    entry.Dew = ParseFloat(row.GetCell(4));
                    entry.Pressure = ParseInt(row.GetCell(5));
                    entry.WindDirection = ParseString(row.GetCell(6));
                    entry.WindSpeed = ParseInt(row.GetCell(7));
                    entry.Cloudiness = ParseInt(row.GetCell(8));
                    entry.CloudinessLowerBound = ParseInt(row.GetCell(9));
                    entry.Visibility = ParseInt(row.GetCell(10));
                    entry.Conditions = ParseString(row.GetCell(11));

                    entries.Add(entry);
                }
            }

            return entries;
        }

        private int? ParseInt(ICell cell)
        {
            if (cell.CellType == CellType.Numeric)
            {
                return (int)cell.NumericCellValue;
            }
            else if (cell.CellType == CellType.String)
            {
                int ret;
                if (int.TryParse(cell.StringCellValue, out ret))
                    return ret;
            }

            return null;
        }

        private float? ParseFloat(ICell cell)
        {
            if (cell.CellType == CellType.Numeric)
            {
                return (float)cell.NumericCellValue;
            }
            else if (cell.CellType == CellType.String)
            {
                float ret;
                if (float.TryParse(cell.StringCellValue, out ret))
                    return ret;
            }

            return null;
        }

        private string? ParseString(ICell cell)
        {
            if (cell != null && cell.CellType == CellType.String)
            {
                return cell.StringCellValue;
            }
            return null;
        }

        public IEnumerable<EntryModel> GetList(int num)
        {
            return _entryContext
                .Entries
                .OrderBy(b => b.ID)
                .Skip(num)
                .Take(10)
                .ToList();
        }
    }
}
