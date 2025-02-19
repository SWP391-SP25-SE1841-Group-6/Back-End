using AutoMapper;
using BusinessObject;
using DataAccess.DTO.Req;
using DataAccess.DTO.Res;
using DataAccess.Repo;
using DataAccess.Repo.IRepo;
using DataAccess.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class TestService : ITestService
    {
        private readonly IBaseRepo<Test> _testRepo;
        private readonly IMapper _mapper;

        public TestService(IBaseRepo<Test> testRepo, IMapper mapper)
        {
            _testRepo = testRepo;
            _mapper = mapper;
        }
        public async Task<ResFormat<ResTestDTO>> CreateTest(TestCreateDTO test)
        {
            var res = new ResFormat<ResTestDTO>();
            try
            {
                var list = await _testRepo.GetAllAsync();
                if (list.Any(t => t.TestName == test.TestName))
                {
                    res.Success = false;
                    res.Message = "Duplicate value";
                    return res;
                }
                else
                {
                    var mapp = _mapper.Map<Test>(test);
                    mapp.IsDeleted = false;
                    await _testRepo.AddAsync(mapp);
                    var result = _mapper.Map<ResTestDTO>(mapp);
                    res.Success = true;
                    res.Data = result;
                    res.Message = "Test Created Successfully";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Create failed: {ex.Message}";
                return res;
            }
        }

        public Task<ResFormat<bool>> DeleteTest(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResFormat<IEnumerable<ResTestDTO>>> GetAllTest()
        {
            throw new NotImplementedException();
        }

        public Task<ResFormat<ResTestDTO>> GetTestById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResFormat<ResTestDTO>> UpdateTest(TestCreateDTO test, int id)
        {
            var res = new ResFormat<ResTestDTO>();
            try
            {

                var existingTest = await _testRepo.GetByIdAsync(id);
                if (existingTest.Any(a => a.QuestionId == id && a.IsDeleted == false))
                {
                    var q = list.FirstOrDefault(a => a.QuestionId == id);
                    if (list.Any(b => b.Question1 == question.Question1))
                    {

                        res.Success = false;
                        res.Message = "Duplicate Question";
                        return res;
                    }
                    else
                    {
                        q.Question1 = question.Question1;
                        _questionRepo.Update(q);
                        var q2 = _mapper.Map<ResQuestionDTO>(q);
                        res.Success = true;
                        res.Data = q2;
                        res.Message = "Success";
                        return res;
                    }
                }
                else
                {
                    res.Success = false;
                    res.Message = "No question with this Id";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Failed :{ex.Message}";
                return res;
            }
        }
    }
}
