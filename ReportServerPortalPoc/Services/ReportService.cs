namespace ReportServerPortalPoc.Services
{
    using Microsoft.Extensions.Options;
    using RestSharp;
    using RestSharp.Authenticators;
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public class ReportService : IReportService
    {
        private readonly ILogger<ReportService> _logger;
        private readonly IRestClient _client;

        public ReportService(IOptions<ReportServerConfiguration> configuration, ILogger<ReportService> logger)
        {
            _logger = logger;
            _client = new RestClient(configuration.Value.BaseUrl)
            {
                Authenticator = new NtlmAuthenticator(configuration.Value.UserName, configuration.Value.Password)
            };

            _client.DefaultParameters.Add(new Parameter("rs:Command", "Render", ParameterType.QueryStringWithoutEncode));
            _client.DefaultParameters.Add(new Parameter("rs:Format", "HTML5", ParameterType.QueryStringWithoutEncode));
            _client.DefaultParameters.Add(new Parameter("rc:Toolbar", "false", ParameterType.QueryStringWithoutEncode));
        }

        public async Task<byte[]> Execute(IRestRequest request)
        {
            try
            {
                IRestResponse response = await _client.ExecuteGetAsync(request);

                return response.RawBytes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}