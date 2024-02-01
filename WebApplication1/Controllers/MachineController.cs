using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
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

        [HttpGet("{machineName}")]
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
        public ActionResult<IEnumerable<Asset>> GetAssets()
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
    }
}
