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

        /// <summary>
        /// Gets the list of Assets by Machine name
        /// </summary>
        /// <param name="machineName">Name of the machine that you want to look assets for</param>
        /// <response code="200">Information retrieved</response>
        /// <response code="404">Machine name not found</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>List of Assets</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{machineName}/assets")]
        public ActionResult<List<Asset>> GetAssetsByMachineName(string? machineName)
        {
            try 
            { 
                if (!string.IsNullOrEmpty(machineName))
                {
                    machineName = machineName.ToLower().Trim();
                }
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
       
        /// <summary>
        /// Gets the List of all Machines
        /// </summary>
        /// <response code="200">Information retrieved</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>List of Machines</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        //[HttpGet("filter")]
        //public ActionResult<List<string>> GetMachineNamesByAssetName([FromQuery] string? assetName)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(assetName))
        //        {
        //            assetName = assetName.Trim().ToLower();
        //            var result = _machineService.GetMachinesByAssetName(assetName);
        //            return Ok(result);
        //        }
        //        return BadRequest();
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, "Internal Server Error");
        //    }
            
        //}

        /// <summary>
        /// Gets the List of Machine names that are ussing specific Asset
        /// </summary>
        /// <param name="assetName">Asset name you want to look for</param>
        /// <response code="200">Information retrieved</response>
        /// <response code="404">Asset name does not exist</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>List of machines that uses specific Asset</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{assetName}/machine")]
        public ActionResult<List<string>> GetMachinesByAssetName(string? assetName)
        {
            try
            {
                if (!string.IsNullOrEmpty(assetName))
                {
                    assetName = assetName.ToLower().Trim();
                }
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

        /// <summary>
        /// Gets the list of machine names that uses latest Assets
        /// </summary>
        /// <response code="200">Information retrieved</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>List of Machine names</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("latest-asset")]
        public ActionResult<List<string>> GetMachinesThatUsesLatestAssets()
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
