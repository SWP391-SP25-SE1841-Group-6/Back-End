using BusinessObject;
using DataAccess.DTO.Req;
using DataAccess.DTO.Res;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service.IService
{
    public interface IProgramService
    {
        Task<ResProgramCreateDTO> CreateProgram(ProgramCreateDTO dto);
        Task<List<ResProgramCreateDTO>> GetAllPrograms();
        Task<bool> DeleteProgram(int programId);
        Task<ResProgramCreateDTO?> UpdateProgram(int programId, ProgramUpdateDTO dto);
        Task<ResProgramCreateDTO?> GetProgramById(int programId);
        Task<ResProgramSignupDTO> RegisterProgram(int studentId, int programId);
        Task<List<ResProgramCreateDTO>> GetProgramByStudentId(int studentId);
        Task<List<ResProgramCreateDTO>> GetProgramByPsychologistId(int psychologistId);

    }
}
