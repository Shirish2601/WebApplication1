using AssetManagement.Api.MongoDB;
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
        public ActionResult<List<Asset>> GetAssetsByMachineName(string? machineName)
        {
            try 
            { 
                var result = _machineService.GetAssetsByMachineName(machineName);
                if (result == null || (result != null && result.Count == 0))
                {
                    return NotFound($"Did not find any Asset for Machine named {machineName}");
                }
                return Ok(result);
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
        public ActionResult<List<string>> GetMachinesByAssetName(string? assetName)
        {
            try
            {
                var result =  _machineService.GetMachinesByAssetName(assetName);
                if (result == null || (result != null &&  result.Count == 0))
                {
                    return NotFound($"Did not find any Machine with the Asset named {assetName}");
                }
                return Ok(result);
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
