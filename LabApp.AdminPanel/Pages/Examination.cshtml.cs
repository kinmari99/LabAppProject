using Grpc.Net.Client;
using LabApp.AdminPanel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LabApp.gRPC;
using LabApp.Models;

namespace LabApp.AdminPanel.Pages
{
    public class ExaminationModel : PageModel
    {
        [BindProperty]
        public int ExaminationId { get; set; }

        [BindProperty]
        public ExaminationViewModel InputExamination { get; set; } = new();

        public List<ExaminationViewModel> Examinations { get; set; } = new();

        public ExaminationReply? Examination { get; set; }

        public async Task OnGetAsync()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7199");
            var client = new ExaminationServiceGrpc.ExaminationServiceGrpcClient(channel);

            var response = await client.GetAllExaminationsAsync(new ExaminationRequest { Id = 0 });


            Examinations = response.Examinations.Select(d => new ExaminationViewModel
            {

                Id = d.Id,
                Name=d.Name,
                Description=d.Description,
                Unit=d.Unit,
                LowerRange=d.LowerRange,
                UpperRange=d.UpperRange,
                Price=d.Price,
                
            }).ToList();
        }

        public async Task<IActionResult> OnPostSearchAsync()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7199");
            var client = new ExaminationServiceGrpc.ExaminationServiceGrpcClient(channel);

            try
            {
                Examination = await client.GetExaminationByIdAsync(new ExaminationRequest { Id = ExaminationId });
            }
            catch (Grpc.Core.RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
            {
                ModelState.AddModelError(string.Empty, "Nie znaleziono badania.");
            }

            await OnGetAsync();

            return Page();
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> OnPostAddAsync()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7199");
            var client = new ExaminationServiceGrpc.ExaminationServiceGrpcClient(channel);

            await client.AddExaminationAsync(new ExaminationCreateRequest
            {
               Name=InputExamination.Name,
               Description=InputExamination.Description,
               Unit=InputExamination.Unit,
               LowerRange=InputExamination.LowerRange,
               UpperRange=InputExamination.UpperRange,
               Price=InputExamination.Price,
            });

            return RedirectToPage();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> OnPostEditAsync()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7199");
            var client = new ExaminationServiceGrpc.ExaminationServiceGrpcClient(channel);

            await client.UpdateExaminationAsync(new ExaminationUpdateRequest
            {
                Id = InputExamination.Id,
                Name = InputExamination.Name,
                Description = InputExamination.Description,
                Unit = InputExamination.Unit,
                LowerRange = InputExamination.LowerRange,
                UpperRange = InputExamination.UpperRange,
                Price = InputExamination.Price,



            });

            return RedirectToPage();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7199");
            var client = new ExaminationServiceGrpc.ExaminationServiceGrpcClient(channel);

            await client.DeleteExaminationAsync(new ExaminationRequest { Id = id });

            return RedirectToPage();
        }

    }
}

          

