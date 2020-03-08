using System;
using System.Threading.Tasks;
using EasyQuizApi.Data.RepositoryBase;
using EasyQuizApi.Share.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyQuizApi.API.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController: ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;
        public QuestionController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }
        
        [HttpPost("createQuestion")]
        public async Task<IActionResult> CreateQuestion( QuestionCreateModel data)
        {
            try
            {
                var createdId = await  _questionRepository.CreateQuestion(data);
                if (createdId == 0)
                {
                    return BadRequest();
                }

                return Ok();
            } catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("getListQuestion")]
        public async Task<IActionResult> GetListQuestion([FromQuery]ListQuestionPageDto data)
        {
            try
            {
                var result = await _questionRepository.GetListQuestion(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        
        [HttpGet("getListMonHocLookup")]
        public async Task<IActionResult> getListMonHoc()
        {
            try
            {
                var result = await _questionRepository.GetListSubjectLookup();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}