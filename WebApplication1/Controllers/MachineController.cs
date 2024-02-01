using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MachineManagement.Models;
using MachineManagement.Api.Models;

namespace MachineManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly IMachineRepository _machineRepository;

        public MachineController(IMachineRepository machineRepository)
        {
            _machineRepository = machineRepository;
        }

        [HttpGet("{machineName}/assets")]
        public ActionResult<Asset> GetAsset(string? machineName)
        {
            try 
            {
                if (machineName != null)
                {
                    return Ok(_machineRepository.GetAsset(machineName));
                }
                else
                {
                    return BadRequest("Please Enter Machine Name");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet]
        public ActionResult<Dictionary<string, List<Asset>>> GetAssets()
        {
            try
            {
                return Ok(_machineRepository.GetAssets());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet("{assetName}/machine")]
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

        [HttpGet("latest-asset")]
        public ActionResult<IEnumerable<string>> GetMachinesThatUsesLatestAssets()
        {
            try
            {
                return Ok(_machineRepository.GetMachineThatUsesLatestAssets());
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }
}
