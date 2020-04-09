using EasyQuizApi.Data.RepositoryBase;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using EasyQuizApi.Model;
using EasyQuizApi.Share.Dto;
using EasyQuizApi.Share.Enums;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace EasyQuizApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeThiController : ControllerBase
    {
        static string path;
        static BaseFont _arialCustomer;
        readonly Font _generalFont ;
        readonly Font _generalFontBold;
        readonly Font _sectionHeaderFont;
        readonly Font _questionFontBold;
        readonly Color _headerColor = WebColors.GetRGBColor("#c5c5c9");
        readonly Color _answerColor = WebColors.GetRGBColor("#fc0303");
        private readonly IDeThiRepository _deThiRepository;

        public DeThiController(IDeThiRepository deThiRepository)
        {
             path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "font-times-new-roman.ttf");
            _arialCustomer = BaseFont.CreateFont(path, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            _generalFont = new Font(_arialCustomer, 10f, Font.NORMAL);
            _generalFontBold = new Font(_arialCustomer, 9f, Font.BOLD);
            _sectionHeaderFont = new Font(_arialCustomer, 12f, Font.BOLD);
            _questionFontBold = new Font(_arialCustomer, 10f, Font.BOLD);
            _deThiRepository = deThiRepository;
        }

        [HttpGet("getListKyThi")]
        public async Task<IActionResult> GetLisKyThi()
        {
            try
            {
                var result = await _deThiRepository.GetListKiThi();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("getListLopHoc")]
        public async Task<IActionResult> GetListLopHoc()
        {
            try
            {
                var result = await _deThiRepository.GetListLopHoc();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("getListDeThi")]
        public IActionResult GetListDeThi([FromQuery] DeThiFilterDto filter)
        {
            try
            {
                var result = _deThiRepository.GetListDeThi(filter);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetDetail(int id)
        {
            try
            {
                var result =  _deThiRepository.GetDeThiDetail(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [HttpGet("getListMonHoc")]
        public async Task<IActionResult> GetListMonHoc(string filter)
        {
            try
            {
                var result = await _deThiRepository.GetListMonHoc(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("createDeThi")]
        public IActionResult CreateDeThi([FromBody]JObject data)
        {
            var response = new ResponseBase();
            var vm = new DeThiNewDto();
            try
            {
                if ( data.SelectToken("kyThi.id") == null || data.SelectToken("cauHois") == null ||
                    data.SelectToken("monHoc.id") == null || data.SelectToken("thoiGian") == null )
                {
                    response.Success = false;
                    response.Message = "Dữ liệu không hợp lệ";
                    return Ok(response);
                }

                vm = MappingDeThiDto(data);
                int createdId = _deThiRepository.CreateDeThi(vm);
                if (createdId > 0)
                {
                    response.Success = true;
                    response.Message = "Tạo mới thành công";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Tạo mới đề thi thất bại";
                }

                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
        
        [HttpPost("updateDeThi")]
        public IActionResult UpdateDethi([FromBody]JObject data)
        {
            var response = new ResponseBase();
            var vm = new DeThiNewDto();
            try
            {
                if ( data.SelectToken("kyThi.id") == null || data.SelectToken("cauHois") == null ||
                     data.SelectToken("monHoc.id") == null || data.SelectToken("thoiGian") == null )
                {
                    response.Success = false;
                    response.Message = "Dữ liệu không hợp lệ";
                    return Ok(response);
                }

                vm = MappingDeThiDto(data);
                int status = _deThiRepository.UpdateDeThi(vm);
                if (status == 1)
                {
                    response.Success = true;
                    response.Message = "Tạo mới thành công";
                }
                else if(status == -2)
                {
                    response.Success = false;
                    response.Message = $"Đẫ tồn tại đề thi này, không thể cập nhật!";
                } else if (status == -1)
                {
                    response.Success = false;
                    response.Message = $"Không tìm thấy đề thi tương ứng";
                }
                else
                {
                    response.Success = false;
                    response.Message = $"Cập nhật thất bại!";
                }

                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        private DeThiNewDto MappingDeThiDto(JObject data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            var vm = new DeThiNewDto();
            vm.Id = data.SelectToken("id") != null ? data.SelectToken("id").Value<int>() : 0;
            if (data.SelectToken("lopHoc.id") != null)
            {
                vm.LopId = data.SelectToken("lopHoc.id").Value<int>();
            }
            vm.KyThiId = data.SelectToken("kyThi.id").Value<int>();
            vm.MonHocId = data.SelectToken("monHoc.id").Value<int>();
            vm.SoCau = data.SelectToken("soCau").Value<int>();
            vm.ThoiGian = data.SelectToken("thoiGian").Value<int>();
            vm.Status = Status.GuiDuyet;
            vm.NgayThi = data.SelectToken("ngayThi").Value<DateTime>();
            vm.GiaoVienId = 1;
            vm.SoLuongDeTuSinh = data.SelectToken("soLuongDe").Value<int>();
            vm.GhiChu = data.SelectToken("ghiChu").Value<string>();
            vm.CauHois =
                JsonConvert.DeserializeObject<List<QuestionListItemDto>>(data.SelectToken("cauHois").ToString());
            vm.KieuDanTrang = data.SelectToken("kieuDanTrang") != null
                ? (KieuDanTrang)Enum.ToObject(typeof(KieuDanTrang) , data.SelectToken("kieuDanTrang").Value<int>())
                : KieuDanTrang.FullPage;
            return vm;
        }

        [HttpPost("viewDeThi1")]
        public ActionResult ViewDeThi([FromBody]JObject data)
        {
            try
            {
                var vm = new DeThiPdfDto();
                if (data.SelectToken("lopHoc.value") != null)
                {
                    vm.LopThi = data.SelectToken("lopHoc.value").Value<string>();
                }
                vm.HocPhan = data.SelectToken("monHoc.value").Value<string>();
                vm.ThoiGianThi = data.SelectToken("thoiGian").Value<int>();
                vm.SoDe = data.SelectToken("soDe") !=  null ? data.SelectToken("soDe").Value<int>(): 1;
                var ngayThi = data.SelectToken("ngayThi").Value<DateTime>();
                vm.NgayThi = ngayThi;
                vm.KyThi = data.SelectToken("kyThi.value").Value<string>() + ", "  + ngayThi.Year;
                vm.CauHois =
                    JsonConvert.DeserializeObject<List<QuestionListItemDto>>(data.SelectToken("cauHois").ToString());
                vm.KieuDanTrang = data.SelectToken("kieuDanTrang") != null
                    ? (KieuDanTrang)Enum.ToObject(typeof(KieuDanTrang) , data.SelectToken("kieuDanTrang").Value<int>())
                    : KieuDanTrang.FullPage;
                vm.IsContainDapAn = data.SelectToken("isContainDapAn") != null && data.SelectToken("isContainDapAn").Value<bool>();
                var bytes = CreatePdfStream(vm);
                var memory = new MemoryStream(bytes) {Position = 0};
                return new FileStreamResult(memory, new MediaTypeHeaderValue("application/octet-stream"));
            }
            catch
            {
            }
            return BadRequest("Lỗi không thể khởi tạo được nội dung");
        }

        private byte[] CreatePdfStream(DeThiPdfDto data)
        {
            // ========================= General config ===================================
            var pdfDoc = new Document(PageSize.A4, 20, 20, 20, 20);
            MemoryStream streamResult = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, streamResult);
            pdfDoc.Open();

            // ===================== Header ==============================================
            var headerTable = CreateHeader(data);
            pdfDoc.Add(headerTable);

            // ===================== Section 2: Đáp án ========================================= 
            CreateDapAnSection(pdfDoc, data.CauHois, data.IsContainDapAn);

            // =================== Body ======================================
            CreateDeThiBody(pdfDoc, data.CauHois, data.KieuDanTrang, data.IsContainDapAn);
            pdfDoc.Close();
            return streamResult.GetBuffer();
        }
        
        private PdfPTable CreateHeader(DeThiPdfDto vm)
        {
            PdfPTable headertable = new PdfPTable(3);
            headertable.HorizontalAlignment = 0;
            headertable.WidthPercentage = 100;
            headertable.SetWidths(new [] { 100f, 320f, 100f });
            headertable.DefaultCell.Border = Rectangle.BOX;

            {
                PdfPCell firstCell = new PdfPCell(new Phrase("Viện CNTT-TT", _generalFont));
                firstCell.VerticalAlignment = Element.ALIGN_TOP;
                firstCell.HorizontalAlignment = Element.ALIGN_CENTER;
                headertable.AddCell(firstCell);
            }

            {
                PdfPTable tableInfo = new PdfPTable(4);
                tableInfo.HorizontalAlignment = 0;
                tableInfo.WidthPercentage = 100;
                tableInfo.SetWidths(new[] { 100f, 275f, 100f, 175f });
                headertable.DefaultCell.Border = Rectangle.NO_BORDER;

                PdfPCell cell11 = new PdfPCell(new Phrase("Học phần:", _generalFont));
                PdfPCell cell12 = new PdfPCell(new Phrase(vm.HocPhan, _generalFontBold));
                PdfPCell cell13 = new PdfPCell(new Phrase("Kì thi:", _generalFont));
                PdfPCell cell14 = new PdfPCell(new Phrase(vm.KyThi, _generalFont));
 
                PdfPCell cell21 = new PdfPCell(new Phrase("Lớp thi:", _generalFont));
                PdfPCell cell22;
                if (!string.IsNullOrEmpty(vm.LopThi))
                {
                    cell22 = new PdfPCell(new Phrase(vm.LopThi, _generalFont));
                }
                else
                {
                    cell22 = new PdfPCell(new Phrase("...................................................", _generalFont));
                }
                PdfPCell cell23 = new PdfPCell(new Phrase("MSSV:", _generalFont));
                PdfPCell cell24 = new PdfPCell(new Phrase(".............................", _generalFont));
                PdfPCell cell31 = new PdfPCell(new Phrase("Họ và tên:", _generalFont));
                PdfPCell cell32 = new PdfPCell(new Phrase("...................................................", _generalFont));
                PdfPCell cell33 = new PdfPCell(new Phrase("Số thứ tự:", _generalFont));
                PdfPCell cell34 = new PdfPCell(new Phrase(".............................", _generalFont));


                var arrayCell = new[] { cell11, cell12, cell13, cell14, cell21, cell22, cell23, cell24, cell31, cell32, cell33, cell34};
                foreach(PdfPCell cell in arrayCell)
                {
                    cell.Border = Rectangle.NO_BORDER;
                    tableInfo.AddCell(cell);
                }


                PdfPCell middlecell = new PdfPCell(tableInfo);
                middlecell.PaddingBottom = 10f;
                middlecell.PaddingLeft = 7f;
                middlecell.PaddingRight = 5f;
                headertable.AddCell(middlecell);
            }

            {
                PdfPCell lastColumn = new PdfPCell(new Phrase($"Mã đề thi\n\n {vm.SoDe}", _generalFont));
                lastColumn.VerticalAlignment = Element.ALIGN_TOP;
                lastColumn.HorizontalAlignment = Element.ALIGN_CENTER;
                headertable.AddCell(lastColumn);
            }

            // ===== line 2 =====

            {
                PdfPCell firstCell = new PdfPCell(new Phrase("Giám thị", _generalFont));
                firstCell.VerticalAlignment = Element.ALIGN_TOP;
                firstCell.HorizontalAlignment = Element.ALIGN_CENTER;
                headertable.AddCell(firstCell);
            }

            {
                PdfPTable emb = new PdfPTable(2);
                emb.HorizontalAlignment = 0;
                emb.WidthPercentage = 100;
                emb.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell cell1 = new PdfPCell(new Paragraph($"Thời gian làm bài: {vm.ThoiGianThi} phút", _generalFont));
                PdfPCell cell2 = new PdfPCell(new Phrase($"Ngày thi: {vm.NgayThi.ToString("dd/M/yyyy", CultureInfo.InvariantCulture)}", _generalFont));
                cell1.Border = Rectangle.NO_BORDER;
                cell2.Border = Rectangle.NO_BORDER;
                cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                emb.AddCell(cell1);
                emb.AddCell(cell2);
                PdfPCell cell21 = new PdfPCell(new Phrase(" - Không sử dụng tài liệu, không sử dụng máy tính cầm tay. ", _generalFont));
                cell21.Colspan = 2;
                PdfPCell cell22 = new PdfPCell(new Phrase(
                    " - Mỗi câu hỏi trắc nghiệm chỉ có một đáp án đúng và điền vào bảng trả lời", _generalFont));
                cell22.Colspan = 2;
                cell21.Border = Rectangle.NO_BORDER;
                cell22.Border = Rectangle.NO_BORDER;
                emb.AddCell(cell21);
                emb.AddCell(cell22);
                PdfPCell middlecell = new PdfPCell(emb);
                middlecell.Border = Rectangle.NO_BORDER;
                middlecell.PaddingBottom = 10f;
                middlecell.Border = Rectangle.BOTTOM_BORDER;
                headertable.AddCell(middlecell);
            }

            {
                PdfPCell lastColumn = new PdfPCell();
                headertable.AddCell(lastColumn);
            }

            return headertable;
        }
        
         private void CreateDapAnSection(Document pdfDoc, List<QuestionListItemDto> data, bool isContainDapAn)
        {
            PdfPTable dapAnSection = new PdfPTable(1);
            dapAnSection.WidthPercentage = 100;
            dapAnSection.HorizontalAlignment = 0;
            dapAnSection.DefaultCell.Border = Rectangle.NO_BORDER;

            // ======= Header 1 =================================================
            PdfPCell dapAnHeaderCell = new PdfPCell(new Phrase("PHẦN CÂU TRẢ LỜI TRẮC NGHIỆM", _sectionHeaderFont));
            dapAnHeaderCell.BackgroundColor = _headerColor;
            dapAnHeaderCell.PaddingBottom = 3f;
            dapAnHeaderCell.PaddingTop = 3f;
            dapAnHeaderCell.Border = Rectangle.NO_BORDER;
            dapAnHeaderCell.VerticalAlignment = Element.ALIGN_MIDDLE;

            // =========  Grid đáp án ===========================================
            PdfPTable dapAnGridTable = new PdfPTable(16);
            int soCauHoi = data.Count;
            int rows = (int)Math.Ceiling(soCauHoi * 1f / 16);
            var dapAnKeyMap = new[] {"A", "B", "C", "D"};
            for (int i = 0; i < rows; i++)
            {
                PdfPCell cell;
                for(int j = 0; j < 16; j++)
                {
                    if(j == 0)
                    {
                        cell = new PdfPCell(new Phrase("Câu hỏi", _generalFontBold));
                    } else
                    {
                        if( i == 2 && j >= 11)
                        {
                            cell = new PdfPCell();
                        } else
                        {
                            cell = new PdfPCell(new Phrase(((i * 15) + j).ToString(), _generalFontBold));
                        }
                    }
                    cell.PaddingTop = 5f;
                    cell.PaddingBottom = 5f;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    dapAnGridTable.AddCell(cell);
                }
                for (int k = 0; k < 16; k++)
                {
                    if (k == 0)
                    {
                        cell = new PdfPCell(new Phrase("Trả lời", _generalFontBold));
                        cell.PaddingTop = 5f;
                        cell.PaddingBottom = 5f;
                    }
                    else
                    {
                        var dapAnKey = string.Empty;
                        var index = (i * 15) + k -1;
                        if (isContainDapAn && index < soCauHoi)
                        {
                            var listDapAn = data[index].Options;
                            for (int keyIndex = 0; keyIndex < 4; keyIndex++)
                            {
                                if (listDapAn[keyIndex].IsDapAn)
                                {
                                    dapAnKey = dapAnKeyMap[keyIndex];
                                    break;
                                }
                            }
                        }
                        cell = new PdfPCell(new Phrase(dapAnKey, _generalFontBold));
                        cell.FixedHeight = 25f;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    }
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    dapAnGridTable.AddCell(cell);
                }
            }

            PdfPCell cellGrid = new PdfPCell(dapAnGridTable);
            cellGrid.PaddingTop = 5f;
            cellGrid.PaddingBottom = 5f;
            cellGrid.Border = Rectangle.NO_BORDER;

            // ===== Cell Heder phần câu hỏi ==================================
            PdfPCell cauHoiSectionHeaderCell = new PdfPCell(new Phrase("PHẦN CÂU HỎI TRẮC NGHIỆM", _sectionHeaderFont));
            cauHoiSectionHeaderCell.BackgroundColor = _headerColor;
            cauHoiSectionHeaderCell.PaddingBottom = 3f;
            cauHoiSectionHeaderCell.PaddingTop = 3f;
            cauHoiSectionHeaderCell.Border = Rectangle.NO_BORDER;
            cauHoiSectionHeaderCell.VerticalAlignment = Element.ALIGN_MIDDLE;

            // ======= Add cell to main table =================================
            dapAnSection.AddCell(dapAnHeaderCell);
            dapAnSection.AddCell(cellGrid);
            dapAnSection.AddCell(cauHoiSectionHeaderCell);

            // === Paragraph để cách dòng =====================================
            Paragraph p1 = new Paragraph();
            p1.SpacingBefore = 10f;

            pdfDoc.Add(p1);
            pdfDoc.Add(dapAnSection);
        }

         /// <summary>
         /// 
         /// </summary>
         /// <param name="pdfDoc"></param>
         /// <param name="data"></param>
         /// <param name="kieuDanTrang"></param>
         /// <param name="isContainDapAn">  </param>
        private void CreateDeThiBody(Document pdfDoc, List<QuestionListItemDto> data, KieuDanTrang kieuDanTrang, bool isContainDapAn)
        {
            MultiColumnText columns = new MultiColumnText();
            columns.AddRegularColumns(36f, pdfDoc.PageSize.Width - 36f, 24f, (int)kieuDanTrang);
            var dapAnKeyMap = new[] {"A", "B", "C", "D"};
            for (int index = 1; index <= data.Count; index++)
            {
                var question = data[index - 1];
                Paragraph blockQuestion = new Paragraph();
                Phrase questionContent = new Phrase($"Câu {index}: {question.Content} \n", _questionFontBold);
                blockQuestion.Add(questionContent);

                for (int i = 0; i < question.Options.Count; i++)
                {
                    var optionData = question.Options[i];
                    Phrase option = new Phrase();
                    var dapAnKey = dapAnKeyMap[i];
                    Chunk key = new Chunk(dapAnKey, _generalFont);
                    if (optionData.IsDapAn && isContainDapAn)
                    {
                        key.SetBackground(_answerColor);
                    }
                    Chunk content = new Chunk($" .{optionData.Content} \n", _generalFont);
                    option.Add(key);
                    option.Add(content);
                    
                    blockQuestion.Add(option);
                }

                blockQuestion.SpacingAfter = 12f;
                blockQuestion.Alignment = Element.ALIGN_JUSTIFIED;
                columns.AddElement(blockQuestion);
            };
            pdfDoc.Add(columns);
        }
    }
}