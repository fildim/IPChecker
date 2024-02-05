using IPChecker.DTOS.ReportDTOS;
using IPChecker.Models;
using Microsoft.Data.Sqlite;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace IPChecker.Services
{
    public interface IReportService
    {
        Task<List<ReportDTO>> Get(string[] codes);
    }

    public class ReportService : IReportService
    {
        private readonly ILogger<ReportService> _logger;

        public ReportService(ILogger<ReportService> logger)
        {
            _logger = logger;
        }


        public async Task<List<ReportDTO>> Get(string[] codes)
        {
            using (var connection = new SqliteConnection("DataSource=localhost\\IPCHECKER_DB;ReadWrite"))
            {
                var resultDtos = new List<ReportDTO>();

                try
                {
                    await connection.OpenAsync();

                    var command = connection.CreateCommand();

                    string query =
                            @"
                            select name as CountryName, count(NAME) as AddressesCount, max(updated_at) as LastAddressUpdated
	                        from COUNTRIES as c join IP_ADDRESSES as i on c.ID = i.COUNTRY_ID
	                        {0} 
	                        group by name
	                        order by AddressesCount desc
                        ";

                    if (codes != null)
                    {
                        string[] placeholders = codes.Select((code, index) => $"@code{index}").ToArray();
                        string inClause = string.Join(", ", placeholders);

                        command.CommandText = string.Format(query, $"WHERE TWO_LETTER_CODE IN ({inClause})");

                        for (int i = 0; i < codes.Length; i++)
                        {
                            command.Parameters.AddWithValue($"@code{i}", $"'{codes[i]}'");
                        }
                    }
                    else
                    {
                        command.CommandText = string.Format(query, string.Empty);
                    }

                    using (var reader =await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var info = new ReportDTO
                            {
                                CountryName = reader["CountryName"].ToString()!,
                                AddressesCount = Convert.ToInt32(reader["AddressesCount"]),
                                LastAddressUpdated = Convert.ToDateTime(reader["LastAddressUpdated"])
                            };

                            resultDtos.Add(info);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
                finally
                {
                    connection.Close();
                }

                return resultDtos;
            }

        }


    }

}
