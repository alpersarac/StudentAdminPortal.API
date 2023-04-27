using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.DataModels;

namespace StudentAdminPortal.API.Repositories
{
    public class SqlStudentRepository : IStudentRepository
    {
        private readonly StudentAdminContext _context;
        public SqlStudentRepository(StudentAdminContext context)
        {
            _context= context;
        }

        public async Task<Student> GetStudentAsync(Guid studentId)
        {
            return await _context.Student.Where(p => p.Id == studentId).FirstOrDefaultAsync();
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            return  await _context.Student.ToListAsync();
            //test nasıl oldu
        }
    }
}
