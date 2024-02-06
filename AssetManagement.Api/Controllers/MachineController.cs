using AssetManagement.Api.Repository;
using AssetManagement.Api.Services;
using AssetManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly IMachineService _machineService;

        public MachineController(IMachineService machineService)
        {
            _machineService = machineService;
        }

        [HttpGet("{machineName}/assets")]
        public ActionResult<Asset> GetAsset(string? machineName)
        {
            try 
            {
                return Ok(_machineService.GetAsset(machineName));
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
                return Ok(_machineService.GetMachines());
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
                return _machineService.GetMachinesByAssetName(assetName);
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
                return Ok(_machineService.GetMachineThatUsesLatestAssets());
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }
}
