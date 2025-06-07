using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Grpc.Net.Client;
using LabApp.gRPC;
using LabApp.AdminPanel.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace LabApp.AdminPanel.Pages
{
    public class DiagnosticianModel : PageModel
    {
        [BindProperty]
        public int DiagnosticianId { get; set; }

        [BindProperty]
        public DiagnosticianViewModel InputDiagnostician { get; set; } = new();

        public List<DiagnosticianViewModel> Diagnosticians { get; set; } = new();

        public DiagnosticianReply? Diagnostician { get; set; }

        public async Task OnGetAsync()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7199");
            var client = new DiagnosticianServiceGrpc.DiagnosticianServiceGrpcClient(channel);

            var response = await client.GetAllDiagnosticiansAsync(new DiagnosticianRequest { Id = 0 });


            Diagnosticians = response.Diagnosticians.Select(d => new DiagnosticianViewModel
            {
    
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                PWZDL = d.Pwzdl,
                Email = d.Email,
                Specialization = d.Specialization
            }).ToList();
        }

        public async Task<IActionResult> OnPostSearchAsync()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7199");
            var client = new DiagnosticianServiceGrpc.DiagnosticianServiceGrpcClient(channel);

            try
            {
                Diagnostician = await client.GetDiagnosticianByIdAsync(new DiagnosticianRequest { Id = DiagnosticianId });
            }
            catch (Grpc.Core.RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
            {
                ModelState.AddModelError(string.Empty, "Nie znaleziono diagnosty.");
            }

            await OnGetAsync();

            return Page();
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> OnPostAddAsync()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7199");
            var client = new DiagnosticianServiceGrpc.DiagnosticianServiceGrpcClient(channel);

            await client.AddDiagnosticianAsync(new DiagnosticianCreateRequest
            {
                FirstName = InputDiagnostician.FirstName,
                LastName = InputDiagnostician.LastName,
                Pwzdl = InputDiagnostician.PWZDL,
                Email = InputDiagnostician.Email,
                Specialization = InputDiagnostician.Specialization
            });

            return RedirectToPage();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> OnPostEditAsync()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7199");
            var client = new DiagnosticianServiceGrpc.DiagnosticianServiceGrpcClient(channel);

            await client.UpdateDiagnosticianAsync(new DiagnosticianUpdateRequest
            {
                Id = InputDiagnostician.Id,
                FirstName=InputDiagnostician.FirstName,
                LastName=InputDiagnostician.LastName,
                Pwzdl=InputDiagnostician.PWZDL,
                Email=InputDiagnostician.Email,
                Specialization=InputDiagnostician.Specialization
           
            });

            return RedirectToPage();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7199");
            var client = new DiagnosticianServiceGrpc.DiagnosticianServiceGrpcClient(channel);

            await client.DeleteDiagnosticianAsync(new DiagnosticianRequest { Id = id });

            return RedirectToPage();
        }
    }
}
