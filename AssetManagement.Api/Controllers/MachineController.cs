using AssetManagement.Api.MongoDB;
using AssetManagement.Api.MongoDBModels;
//using AssetManagement.Api.Services;
//using AssetManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly MongoDbService _mongoDbService;
        public MachineController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        [HttpGet("{machineName}/assets")]
        public ActionResult<List<Asset>> GetAsset(string? machineName)
        {
            try 
            { 
                var result = _mongoDbService.GetAsset(machineName);
                if (result == null || (result != null && result.Count == 0))
                {
                    return NotFound($"Did not find Asset for Machine name {machineName}");
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
                return Ok(_mongoDbService.GetMachines());
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
                var result =  _mongoDbService.GetMachinesByAssetName(assetName);
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
                return Ok(_mongoDbService.GetMachineThatUsesLatestAssets());
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }
}
