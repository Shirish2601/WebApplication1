using AssetManagement.Api.Repository;
using AssetManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.Api.Controllers
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
        public ActionResult<List<Machine>> GetMachines()
        {
            try
            {
                return Ok(_machineRepository.GetMachines());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet("{assetName}/machine")]
        public ActionResult<IEnumerable<string>> GetMachinesByAssetName(string? assetName)
        {
            try
            {
                if (assetName != null)
                {
                    return _machineRepository.GetMachinesByAssetName(assetName);
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
