using DataAccess.DTO.Req;
using DataAccess.DTO.Res;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service.IService
{
    public interface ITestService
    {
        Task<ResFormat<IEnumerable<ResTestDTO>>> GetAllTest();
        Task<ResFormat<ResTestDTO>> GetTestById(int id);
        Task<ResFormat<bool>> DeleteTest(int id);
        Task<ResFormat<ResTestDTO>> UpdateTest(TestUpdateDTO test, int id);
        Task<ResFormat<ResTestDTO>> CreateTest(TestCreateDTO test);
    }
}
