using Microsoft.AspNetCore.Mvc;
using System;
using XM.Security.Classes;

namespace XM.Security.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        
        private readonly ILogger<RolesController> _logger;

        public RolesController(ILogger<RolesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string bearerToken)
        {
            var url = "https://api.xm.com.co/bct/seguridad/v1/aplicaciones/233/roles";
            var httpClient = new HttpClient();
            var apiClient = new ApiClient(httpClient);
            var result = await apiClient.ExecuteApiRequest(url, HttpMethod.Get, bearerToken);
            if (result == null) 
            { 
                return Ok(new { }); 
            }

            return Ok(result);
        }
    }
}
