using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bit8.Students.Domain.Models;
using Bit8.Students.Persistence;
using FluentResults;

namespace Bit8.Students.Services.Students
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _uow;
        
        public StudentService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        
        public async Task<Result<int>> CreateAsync(CreateStudentRequest request)
        {
            var student = new Student { Name = request.Name };
            
            await _uow.StudentRepository.AddAsync(student);
            _uow.Commit();

            return Result.Ok(student.Id);
        }

        public async Task<Result<IEnumerable<GetAllSemestersDto>>> GetAllAsync()
        {
            var disciplines = await _uow.StudentRepository.AllAsync();
            var mapped = disciplines.Select(x => new GetAllSemestersDto
            {
                Id = x.Id,
                Name = x.Name
            });
            return Result.Ok(mapped);
        }

        public async Task<Result> AssignToSemesterAsync(AssignToSemesterRequest request)
        {
            var result = Result.Ok();

            if (!await _uow.SemesterRepository.ExistsAsync(request.SemesterId))
            {
                result.WithError("Semester with given id does not exist");
            }
            
            if (!await _uow.StudentRepository.ExistsAsync(request.StudentId))
            {
                result.WithError("Student with given id does not exist");
            }

            if (result.IsFailed)
            {
                return result;
            }

            var student = await _uow.StudentRepository.Get(request.StudentId);
            
            student.AssignToSemester(request.SemesterId);
            await _uow.StudentRepository.UpdateAsync(student);
            
            _uow.Commit();
            return result;
        }
    }
}