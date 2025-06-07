using Grpc.Core;
using LabApp.Data;
using LabApp.gRPC;
using LabApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LabApp.Services
{
    public class GrpcExaminationService: ExaminationServiceGrpc.ExaminationServiceGrpcBase
    {
        private readonly ApplicationDbContext _context;

        public GrpcExaminationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public override async Task<ExaminationListReply> GetAllExaminations(ExaminationRequest request, ServerCallContext context)
        {
            var list = await _context.Examinations.ToListAsync();

            var reply = new ExaminationListReply();
            reply.Examinations.AddRange(list.Select(d => new ExaminationReply
            {

                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Unit = d.Unit,
                LowerRange = d.LowerRange,
                UpperRange = d.UpperRange,
                Price=d.Price
            }));

            return reply;
        }

        public override async Task<ExaminationReply> GetExaminationById(ExaminationRequest request, ServerCallContext context)
        {
            var d = await _context.Examinations.FindAsync(request.Id);
            if (d == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Nie znaleziono badania"));

            return new ExaminationReply
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Unit = d.Unit,
                LowerRange = d.LowerRange,
                UpperRange = d.UpperRange,
                Price = d.Price
            };
        }

        public override async Task<ExaminationReply> AddExamination(ExaminationCreateRequest request, ServerCallContext context)
        {
            var d = new Models.Examination
            {Name=request.Name,
            Description=request.Description,
            Unit=request.Unit,
            LowerRange=request.LowerRange,
            UpperRange=request.UpperRange,
            Price=request.Price
               
            };
            _context.Examinations.Add(d);
            await _context.SaveChangesAsync();

            return new ExaminationReply
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Unit = d.Unit,
                LowerRange = d.LowerRange,
                UpperRange = d.UpperRange,
                Price = d.Price
            };
        }

        public override async Task<ExaminationReply> UpdateExamination(ExaminationUpdateRequest request, ServerCallContext context)
        {
            var d = await _context.Examinations.FindAsync(request.Id);
            if (d == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Nie znaleziono badania"));

            d.Name = request.Name;
            d.Description= request.Description;
            d.Unit = request.Unit;
            d.LowerRange = request.LowerRange;
            d.UpperRange = request.UpperRange;
            d.Price = request.Price;

            await _context.SaveChangesAsync();

            return new ExaminationReply
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Unit = d.Unit,
                LowerRange = d.LowerRange,
                UpperRange = d.UpperRange,
                Price = d.Price
            };
        }

        public override async Task<ExaminationEmptyReply> DeleteExamination(ExaminationRequest request, ServerCallContext context)
        {
            var d = await _context.Examinations.FindAsync(request.Id);
            if (d == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Nie znaleziono badania"));

            _context.Examinations.Remove(d);
            await _context.SaveChangesAsync();

            return new ExaminationEmptyReply();
        }
    }
}
