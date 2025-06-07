using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Grpc.Net.Client;
using LabApp.gRPC;
using LabApp.AdminPanel.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace LabApp.AdminPanel.Pages
{
    public class DeviceModel : PageModel
    {
        [BindProperty]
        public int DeviceId { get; set; }

        [BindProperty]
        public DeviceViewModel InputDevice { get; set; } = new();

        public List<DeviceViewModel> Devices { get; set; } = new();

        public DeviceReply? Device { get; set; }

        public async Task OnGetAsync()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7199");
            var client = new DeviceServiceGrpc.DeviceServiceGrpcClient(channel);

            var response = await client.GetAllDevicesAsync(new EmptyRequest());

            Devices = response.Devices.Select(d => new DeviceViewModel
            {
                Id = d.Id,
                Name = d.Name,
                Model = d.Model,
                SerialNumber = d.SerialNumber,
                IsOperational = d.IsOperational
            }).ToList();
        }

        public async Task<IActionResult> OnPostSearchAsync()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7199");
            var client = new DeviceServiceGrpc.DeviceServiceGrpcClient(channel);

            try
            {
                Device = await client.GetDeviceByIdAsync(new DeviceRequest { Id = DeviceId });
            }
            catch (Grpc.Core.RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
            {
                ModelState.AddModelError(string.Empty, "Nie znaleziono urz¹dzenia.");
            }

            await OnGetAsync();

            return Page();
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> OnPostAddAsync()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7199");
            var client = new DeviceServiceGrpc.DeviceServiceGrpcClient(channel);

            await client.AddDeviceAsync(new DeviceCreateRequest
            {
                Name = InputDevice.Name,
                Model = InputDevice.Model,
                SerialNumber = InputDevice.SerialNumber,
                IsOperational = InputDevice.IsOperational
            });

            return RedirectToPage();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> OnPostEditAsync()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7199");
            var client = new DeviceServiceGrpc.DeviceServiceGrpcClient(channel);

            await client.UpdateDeviceAsync(new DeviceUpdateRequest
            {
                Id = InputDevice.Id,
                Name = InputDevice.Name,
                Model = InputDevice.Model,
                SerialNumber = InputDevice.SerialNumber,
                IsOperational = InputDevice.IsOperational
            });

            return RedirectToPage();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7199");
            var client = new DeviceServiceGrpc.DeviceServiceGrpcClient(channel);

            await client.DeleteDeviceAsync(new DeviceRequest { Id = id });

            return RedirectToPage();
        }
    }
}
