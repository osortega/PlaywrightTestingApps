
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Echo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EchoController : ControllerBase
    {
        private readonly ILogger<EchoController> _logger;

        public EchoController(ILogger<EchoController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public ContentResult Get()
        {
            var res = new Dictionary<string, string>();
            var str = "";
            foreach (var (headerName, values) in Request.Headers)
            {
                res.Add($"[Header] {headerName}", values.ToString());
                str = str + "<div item=";
                str += values.ToString();
                str += ">";
                str += headerName;
                str += "</div>";
            }

            res.Add("[Request] Host", Request.Host.ToString());
            res.Add("[Request] Scheme", Request.Scheme);

            return new ContentResult
            {
                ContentType = "text/html",
                Content = str
            };
        }

        [HttpGet("~/index")]
        public ContentResult Get1()
        {
            var str = "<div item=Calling multiple requests to this endpoint> Calling multiple requests to this endpoint </div>";

            return new ContentResult
            {
                ContentType = "text/html",
                Content = str
            };
        }

        [HttpGet("/echo2")]
        public IDictionary<string, string> Post(IDictionary<string, string> body)
        {
            var res = new Dictionary<string, string>();

            foreach (var (headerName, values) in Request.Headers)
            {
                res.Add($"{headerName}", values.ToString());
            }

            return res;
        }

        [HttpPost]
        public IDictionary<string, string> GetEcho2(IDictionary<string, string> body)
        {
            var res = new Dictionary<string, string>(body);

            foreach (var (headerName, values) in Request.Headers)
            {
                res.Add($"[Header] {headerName}", values.ToString());
            }

            return res;
        }

        [HttpPost("~/index")]
        public ContentResult Post2()
        {
            return new ContentResult
            {
                ContentType = "text/html",
                Content = "Calling multiple requests to this endpoint"
            };

        }
    }
}
