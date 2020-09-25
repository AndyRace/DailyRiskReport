using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
  public partial class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
      _logger = logger;
    }

    public IActionResult Index()
    {
      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    public IActionResult Test([FromServices]IConfiguration configuration)
    {
        // get the latest email report available reference
        var httpBasicAuthenticator = new HttpBasicAuthenticator(
          configuration["Authentication:DailyRiskReport:UserName"],
          configuration["Authentication:DailyRiskReport:Password"]);

      var getLatestReportInformation =
          new RestClient(configuration["AKE:ListReports"])
          {
            Authenticator = httpBasicAuthenticator
          };

      var reportInfoResponse = getLatestReportInformation.Execute<LatestReport>(new RestRequest());

      return View(reportInfoResponse);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
