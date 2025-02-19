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
                    mapp.DateCreated = DateTime.Now;
                    mapp.DateUpdated = DateTime.Now;
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

        public async Task<ResFormat<bool>> DeleteTest(int id)
        {
            var res = new ResFormat<bool>();
            try
            {
                var existingTest = await _testRepo.GetByIdAsync(id);
                if (existingTest != null)
                {
                    existingTest.IsDeleted = true;
                    _testRepo.Update(existingTest);
                    res.Success = true;
                    res.Data = true;
                    res.Message = "Success";
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Message = "No Test with this Id";
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

        public async Task<ResFormat<IEnumerable<ResTestDTO>>> GetAllTest()
        {
            var res = new ResFormat<IEnumerable<ResTestDTO>>();
            try
            {
                var list = await _testRepo.GetAllAsync();
                var resList = _mapper.Map<IEnumerable<ResTestDTO>>(list);
                res.Success = true;
                res.Data = resList;
                res.Message = "Retrieved successfully";
                return res;
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Retrieved failed: {ex.Message}";
                return res;
            }
        }

        public async Task<ResFormat<ResTestDTO>> GetTestById(int id)
        {
            var res = new ResFormat<ResTestDTO>();
            try
            {

                var existingTest = await _testRepo.GetByIdAsync(id);
                if (existingTest != null )
                {
                    if (existingTest.IsDeleted == true)
                    {
                        res.Success = false;
                        res.Message = "Test Deleted";
                        return res;
                    }
                    else 
                    {
                        var resList = _mapper.Map<ResTestDTO>(existingTest);
                        res.Success = true;
                        res.Data = resList;
                        res.Message = "Retrieved successfully";
                        res.Message = "Success";
                        return res;
                    }
                    
                }
                else
                {
                    res.Success = false;
                    res.Message = "No Test with this Id";
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

        public async Task<ResFormat<ResTestDTO>> UpdateTest(TestUpdateDTO test, int id)
        {
            var res = new ResFormat<ResTestDTO>();
            try
            {

                var list = await _testRepo.GetAllAsync();
                if (list.Any(a => a.TestId == id && a.IsDeleted == false))
                {
                    var existingTest = list.FirstOrDefault(a => a.TestId == id);
                    if (list.Any(b => b.TestName == test.TestName))
                    {

                        res.Success = false;
                        res.Message = "Duplicate Test Name";
                        return res;
                    }
                    else
                    {
                        existingTest.TestName = test.TestName;
                        existingTest.DateUpdated = DateTime.Now;
                        _testRepo.Update(existingTest);
                        var existingTest2 = _mapper.Map<ResTestDTO>(existingTest);
                        res.Success = true;
                        res.Data = existingTest2;
                        res.Message = "Success";
                        return res;
                    }
                }
                else
                {
                    res.Success = false;
                    res.Message = "No Test with this Id";
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
