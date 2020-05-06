namespace ReportServerPortalPoc
{
    using System.Net.Http;

    public interface IReportServerDataService
    {
    }

    public class ReportServerDataService : IReportServerDataService
    {
        private readonly HttpClient _httpClient;

        public ReportServerDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}