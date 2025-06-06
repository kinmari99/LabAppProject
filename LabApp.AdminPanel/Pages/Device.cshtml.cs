using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Grpc.Net.Client;
using LabApp.gRPC;

namespace LabApp.AdminPanel.Pages
{
    public class DeviceModel : PageModel
    {
        [BindProperty]
        public int DeviceId { get; set; }

        public DeviceReply? Device { get; set; }

        public async Task<IActionResult> OnPostAsync()
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

            return Page();
        }
    }
}
