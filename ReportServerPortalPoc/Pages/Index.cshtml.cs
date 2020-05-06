namespace ReportServerPortalPoc.Pages
{
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using RestSharp;
    using Services;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class IndexModel : PageModel
    {
        private readonly IReportService _reportService;

        public IndexModel(IReportService reportService)
        {
            _reportService = reportService;
        }

        public string ReportValue { get; private set; }

        public async Task OnGet([FromQuery]ReportParameter parameters)
        {
            var request = new RestRequest("?/DtTest/TestReport");

            foreach (var parameterInfo in parameters.GetType().GetProperties())
            {
                var value = parameterInfo.GetValue(parameters);

                request.AddParameter(parameterInfo.Name, value, ParameterType.QueryString);
            }

            var bytes = await _reportService.Execute(request);

            ReportValue = Encoding.Default.GetString(bytes);
        }
    }
}