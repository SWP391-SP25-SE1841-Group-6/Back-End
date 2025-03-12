using AutoMapper;
using BusinessObject;
using DataAccess.DTO.Req;
using DataAccess.DTO.Res;
using DataAccess.Repo.IRepo;
using DataAccess.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class TestResultDetailService :ITestResultDetailService
    {
        private readonly ITestResultDetailRepo _testResultDetailRepo;
        private readonly IMapper _mapper;

        public TestResultDetailService(ITestResultDetailRepo testResultDetailRepo, IMapper mapper)
        {
            _testResultDetailRepo = testResultDetailRepo;
            _mapper = mapper;
        }

        public async Task<ResFormat<IEnumerable<ResTestResultDetailDTO>>> GetTestResultDetailsByTestResultAsync(int testResultId)
        {
            var res = new ResFormat<IEnumerable<ResTestResultDetailDTO>>();
            try
            {
                var testResultDetails = await _testResultDetailRepo.GetTestResultDetailsByTestResultAsync(testResultId);
                var resultDTOs = _mapper.Map<IEnumerable<ResTestResultDetailDTO>>(testResultDetails);
                res.Success = true;
                res.Data = resultDTOs;
                res.Message = "Test Result Details Retrieved Successfully";
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Failed to retrieve Test Result Details: {ex.Message}";
            }

            return res;
        }

        public async Task<ResFormat<bool>> AddTestResultDetailAsync(TestResultDetailCreateDTO testResultDetailCreateDTO)
        {
            var res = new ResFormat<bool>();
            try
            {
                var testResultDetail = _mapper.Map<TestResultDetail>(testResultDetailCreateDTO);
                await _testResultDetailRepo.AddAsync(testResultDetail);
                res.Success = true;
                res.Data = true;
                res.Message = "Test Result Detail Created Successfully";
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Failed to create Test Result Detail: {ex.Message}";
            }

            return res;
        }
    }
}
