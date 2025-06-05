using LabApp.Dtos;
using LabApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LabApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<DeviceDto>>> GetAll()
        {
            var devices = await _deviceService.GetAllAsync();
            return Ok(devices);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<DeviceDto>> GetById(int id)
        {
            var device = await _deviceService.GetByIdAsync(id);
            if (device == null)
                return NotFound();

            return Ok(device);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<DeviceDto>> Create([FromBody] CreateDeviceDto dto)
        {
            var created = await _deviceService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDeviceDto dto)
        {
            var success = await _deviceService.UpdateAsync(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }
        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _deviceService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
