using Grpc.Core;
using LabApp.Data;
using LabApp.gRPC;
using LabApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LabApp.Services
{

    public class GrpcDiagnosticianService : DiagnosticianServiceGrpc.DiagnosticianServiceGrpcBase
    {
        private readonly ApplicationDbContext _context;

        public GrpcDiagnosticianService(ApplicationDbContext context)
        {
            _context = context;
        }

        public override async Task<DiagnosticianListReply> GetAllDiagnosticians(DiagnosticianRequest request, ServerCallContext context)
        {
            var list = await _context.Diagnosticians.ToListAsync();

            var reply = new DiagnosticianListReply();
            reply.Diagnosticians.AddRange(list.Select(d => new DiagnosticianReply
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Email = d.Email,
                Pwzdl = d.PWZDL,
                Specialization = d.Specialization
            }));

            return reply;
        }

        public override async Task<DiagnosticianReply> GetDiagnosticianById(DiagnosticianRequest request, ServerCallContext context)
        {
            var d = await _context.Diagnosticians.FindAsync(request.Id);
            if (d == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Nie znaleziono diagnosty"));

            return new DiagnosticianReply
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Email = d.Email,
                Pwzdl = d.PWZDL,
                Specialization = d.Specialization
            };
        }

        public override async Task<DiagnosticianReply> AddDiagnostician(DiagnosticianCreateRequest request, ServerCallContext context)
        {
            var d = new Models.Diagnostician
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PWZDL = request.Pwzdl,
                Specialization = request.Specialization
            };
            _context.Diagnosticians.Add(d);
            await _context.SaveChangesAsync();

            return new DiagnosticianReply
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Email = d.Email,
                Pwzdl = d.PWZDL,
                Specialization = d.Specialization
            };
        }

        public override async Task<DiagnosticianReply> UpdateDiagnostician(DiagnosticianUpdateRequest request, ServerCallContext context)
        {
            var d = await _context.Diagnosticians.FindAsync(request.Id);
            if (d == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Nie znaleziono diagnosty"));

            d.FirstName = request.FirstName;
            d.LastName = request.LastName;
            d.Email = request.Email;
            d.PWZDL = request.Pwzdl;
            d.Specialization = request.Specialization;

            await _context.SaveChangesAsync();

            return new DiagnosticianReply
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Email = d.Email,
                Pwzdl = d.PWZDL,
                Specialization = d.Specialization
            };
        }

        public override async Task<DiagnosticianEmptyReply> DeleteDiagnostician(DiagnosticianRequest request, ServerCallContext context)
        {
            var d = await _context.Diagnosticians.FindAsync(request.Id);
            if (d == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Nie znaleziono diagnosty"));

            _context.Diagnosticians.Remove(d);
            await _context.SaveChangesAsync();

            return new DiagnosticianEmptyReply();
        }
    }
}