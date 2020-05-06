namespace ReportServerPortalPoc.Services
{
    using System.Threading.Tasks;
    using RestSharp;

    public interface IReportService
    {
        Task<byte[]> Execute(IRestRequest request);
    }
}