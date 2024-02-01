using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IMachineRepository _machineRepository;

        public AssetController(IMachineRepository machineRepository)
        {
            _machineRepository = machineRepository;
        }

        [HttpGet("{assetName}")]
        public ActionResult<IEnumerable<string>> GetMachines(string? assetName)
        {
            try
            {
                if (assetName != null)
                {
                    return _machineRepository.GetMachines(assetName);
                }
                return BadRequest($"Please Enter Valid Machine Name");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }
}
