using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EasyQuizApi.Data.RepositoryBase;
using EasyQuizApi.Share.Dto;
using EasyQuizApi.Share.Helper;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace EasyQuizApi.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;

        public QuestionController(IQuestionRepository questionRepository, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _questionRepository = questionRepository;
        }

        [HttpPost("createQuestion")]
        public async Task<IActionResult> CreateQuestion(QuestionCreateModel data)
        {
            try
            {
                var createdId = await _questionRepository.CreateQuestion(data);
                if (createdId == 0)
                {
                    return BadRequest();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        
        [HttpPost("createMonHoc")]
        public async Task<IActionResult> CreateMonHoc(MonHocDto data)
        {
            try
            {
                var createdId = await _questionRepository.CreateMonHoc(data);
                if (createdId == 0)
                {
                    return BadRequest();
                }

                if (createdId == -1)
                {
                    return Ok(new { Message = "Đã tồn tại tên môn học: " + data.Name});
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("getListQuestion")]
        public async Task<IActionResult> GetListQuestion([FromQuery] ListQuestionPageDto data)
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
        
        [HttpGet("getListMonHoc")]
        public async Task<IActionResult> getListMonHoc([FromQuery] ListQuestionPageDto data)
        {
            try
            {
                var result = await _questionRepository.GetListMonHoc(data);
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

        [HttpDelete("deleteQuestion/{questionId}")]
        public async Task<IActionResult> DeleteQuestion(int questionId)
        {
            var result = new ResponseBase();
            try
            {
                var status = _questionRepository.DeleteQuestion(questionId);
                if (status == -1)
                {
                    result.Success = false;
                    result.Message = "Câu hỏi đã được sủ dụng để tạo đề thi, không thể xóa";
                    return Ok(result);
                }

                if (status == 0)
                {
                    result.Success = false;
                    result.Message = "Không tìm thấy câu hỏi cần xóa";
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi server");
            }

            result.Success = true;

            return Ok(result);
        }
        
        [HttpDelete("deleteMonHoc/{monHocId}")]
        public async Task<IActionResult> DeleteMonHoc(int monHocId)
        {
            var result = new ResponseBase();
            try
            {
                var status = _questionRepository.DeleteMonHoc(monHocId);
                if (status == -1)
                {
                    result.Success = false;
                    result.Message = "Môn học đã được sủ dụng để tạo đề thi, không thể xóa";
                    return Ok(result);
                }

                if (status == 0)
                {
                    result.Success = false;
                    result.Message = "Không tìm thấy môn học cần xóa";
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi server");
            }

            result.Success = true;

            return Ok(result);
        }

        [HttpPost("updateQuestion")]
        public async Task<IActionResult> UpdateQuestion(QuestionEditDto data)
        {
            try
            {
                _questionRepository.EditQuestion(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        
        [HttpPost("updateMonHoc")]
        public async Task<IActionResult> updateMonHoc(MonHocDto data)
        {
            try
            {
                _questionRepository.EditMonHoc(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        
        [HttpPost("updateGiaoVien")]
        public async Task<IActionResult> updateGiaoVien(GiaoVienInsertDto data)
        {
            try
            {
                _questionRepository.EditGiaoVien(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        
        [HttpPost("createGiaoVien")]
        public async Task<IActionResult> createGiaoVien(GiaoVienInsertDto data)
        {
            try
            {
                var createdId = await _questionRepository.CreateGiaoVien(data);
                if (createdId == 0)
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        
        [HttpGet("getListGiaoVien")]
        public async Task<IActionResult> getListGiaoVien([FromQuery] ListGiaoVienPageDto data)
        {
            try
            {
                var result = await _questionRepository.GetListGiaoVien(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        
        [HttpDelete("deleteGiaoVien/{id}")]
        public async Task<IActionResult> deleteGiaoVien(int id)
        {
            var result = new ResponseBase();
            try
            {
                var status = _questionRepository.DeleteGiaoVien(id);
                if (status == -1)
                {
                    result.Success = false;
                    result.Message = "Không thể xóa giáo viên!";
                    return Ok(result);
                }

                if (status == 0)
                {
                    result.Success = false;
                    result.Message = "Không tìm thấy giáo viên cần xóa";
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi server");
            }

            result.Success = true;

            return Ok(result);
        }
        [HttpGet("getListKhoa")]
        public async Task<IActionResult> GetListKhoa()
        {
            try
            {
                var result = await _questionRepository.GetListKhoa();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("createAccount")]
        public async Task<IActionResult> CreateAccount(AccountCreateDto data)
        {
            var result = new ResponseBase();
            if (string.IsNullOrEmpty(data.AccountName) || data.GiaoVienId == null)
            {
                result.Message = "Dữ liệu không hợp lệ";
                result.Success = false;
                return Ok(result);
            }
            try
            {
                string password = GenerateToken(6);
                data.Password = PasswordHelper.HashPassword(password);
                var status = await _questionRepository.CreateAccount(data);
                if (status == -1)
                {
                    result.Message = "Đã tồn tại tên tài khoản: " + data.AccountName;
                    result.Success = false;
                }
                else if(status == 1)
                {
                    result.Message = "Tạo tài khoản thành công!";
                    result.Success = true;
                }
                else
                {
                    result.Message = $"Đã tồn tại tài khoản cho giáo viên!";
                    result.Success = false;
                }
                SendEmailPasword(data.Email, data.AccountName, password);
            }
            catch
            {
                result.Message = "Lỗi server";
                result.Success = false;
            }
            return Ok(result);
        }
        
        private string GenerateToken(int length)
        {
            using (RNGCryptoServiceProvider cryptRNG = new RNGCryptoServiceProvider())
            {
                byte[] tokenBuffer = new byte[length];
                cryptRNG.GetBytes(tokenBuffer);
                return Convert.ToBase64String(tokenBuffer);
            }
        }
        
        private async Task SendEmailPasword(string email, string account, string password){
        
            try
            {
                // Credentials
                var credentials = new NetworkCredential("khangpv96nd@gmail.com", "----");
                // Mail message
                var mail = new MailMessage()
                {
                    From = new MailAddress("khangpv96nd@gmail.com"),
                    Subject = "Thông tin mật khẩu",
                    Body = $"Bạn đã được cấp tài khoản: <b>{account}</b> với mật khẩu: \n<b>{password}</b> \n Vui lòng đăng nhập và thay đổi mật khấu của bạn!"
                };
                mail.IsBodyHtml = true;
                mail.To.Add(new MailAddress(email));
                // Smtp client
                var client = new SmtpClient()
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Credentials = credentials
                };
                client.SendMailAsync(mail);
            }
            catch (System.Exception e)
            {
            }
        }
        
        [HttpGet("getListAccount")]
        public async Task<IActionResult> GetLisAccout([FromQuery] ListAccountPageDto data)
        {
            try
            {
                var result = await _questionRepository.GetListAccount(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        
        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto data)
        {
            var result = new ResponseBase();
            try
            {
                if (string.IsNullOrEmpty(data.AccountName) || string.IsNullOrEmpty(data.OldPass) ||
                    string.IsNullOrEmpty(data.NewPass))
                {
                    result.Success = false;
                    result.Message = "Dữ liệu không hợp lệ";
                    return Ok(result);
                }
                var isValidPassword = _userRepository.Authenticate(data.AccountName, data.OldPass);
                if (isValidPassword == null)
                {
                    result.Success = false;
                    result.Message = "Mật khẩu không đúng";
                    result.Data = new
                    {
                        invalidPassword = true
                    };
                    return Ok(result);
                }

                bool status = _userRepository.ChangePassword(data);
                if (status)
                {
                    result.Success = true;
                    result.Message = "Thay đổi mật khẩu thành công";
                }
                else
                {
                    result.Success = false;
                    result.Message = "Thay đổi mật khẩu không thành công";
                }
               
            }
            catch
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}