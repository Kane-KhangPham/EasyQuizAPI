using EasyQuizApi.Data.RepositoryBase;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using EasyQuizApi.Share.Dto;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;

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
        readonly Color _answerColor = WebColors.GetRGBColor("#c5c5c9");
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

        [HttpPost("viewDeThi1")]
        public  ActionResult ViewDeThi(JObject data)
        {
            var bytes = CreatePdfStream();
            var memory = new MemoryStream(bytes);
            memory.Position = 0;
            return new FileStreamResult(memory, new MediaTypeHeaderValue("application/octet-stream"));
        }

        private byte[] CreatePdfStream()
        {
            // ========================= General config ===================================
            var pdfDoc = new Document(PageSize.A4, 20, 20, 20, 20);
            MemoryStream streamResult = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, streamResult);
            pdfDoc.Open();

            // ===================== Header ==============================================
            var headerTable = CreateHeader();
            pdfDoc.Add(headerTable);

            // ===================== Section 2: Đáp án ========================================= 
            CreateDapAnSection(pdfDoc);

            // =================== Body ======================================
            var listData = new List<QuestionDeThiPdfDto>();
            for (int i = 0; i < 20; i++)
            {
                var question = new QuestionDeThiPdfDto()
                {
                    Question = @"Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Suspendisse blandit blandit turpis. Nam in lectus ut dolor consectetuer bibendum. Morbi neque ipsum, laoreet id; dignissim et, viverra id, mauris.",
                    OptionA = "Nulla risus eros, mollis quis, blandit ut; luctus eget, urna.",
                    OptionB = "Phasellus id lectus! Vivamus laoreet enim et dolor. Integer arcu mauris, ultricies vel, porta quis, venenatis at, libero.",
                    OptionC = "Nulla risus eros, mollis quis, blandit ut; luctus eget, urna.",
                    OptionD = "Phasellus id lectus! Vivamus laoreet enim et dolor. Integer arcu mauris, ultricies vel, porta quis, venenatis at, libero.",
                    Answer = "A"
                };
                listData.Add(question);
            }
            CreateDeThiBody(pdfDoc, listData, 1);
            pdfDoc.Close();
            return streamResult.GetBuffer();
        }
        
        private  PdfPTable CreateHeader()
        {
            PdfPTable headertable = new PdfPTable(3);
            headertable.HorizontalAlignment = 0;
            headertable.WidthPercentage = 100;
            headertable.SetWidths(new float[] { 100f, 320f, 100f });
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
                tableInfo.SetWidths(new float[] { 100f, 275f, 100f, 175f });
                headertable.DefaultCell.Border = Rectangle.NO_BORDER;

                PdfPCell cell11 = new PdfPCell(new Phrase("Học phần:", _generalFont));
                PdfPCell cell12 = new PdfPCell(new Phrase("Tin học đại cương", _generalFontBold));
                PdfPCell cell13 = new PdfPCell(new Phrase("Kì thi:", _generalFont));
                PdfPCell cell14 = new PdfPCell(new Phrase("Giữa kì, 2020", _generalFont));
 
                PdfPCell cell21 = new PdfPCell(new Phrase("Lớp thi:", _generalFont));
                PdfPCell cell22 = new PdfPCell(new Phrase("...................................................", _generalFont));
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
                PdfPCell lastColumn = new PdfPCell(new Phrase("Mã đề thi\n\n 3", _generalFont));
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
                PdfPCell cell1 = new PdfPCell(new Paragraph("Thời gian làm bài: 20 phút", _generalFont));
                PdfPCell cell2 = new PdfPCell(new Phrase("Ngày thi: 12/02/2020", _generalFont));
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
        
         private void CreateDapAnSection(Document pdfDoc)
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
            for(int i = 0; i < 3; i++)
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
                        cell = new PdfPCell();
                        cell.FixedHeight = 25f;
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

        private void CreateDeThiBody(Document pdfDoc, List<QuestionDeThiPdfDto> data, int column)
        {
            MultiColumnText columns = new MultiColumnText();
            columns.AddRegularColumns(36f, pdfDoc.PageSize.Width - 36f, 24f, column);
            for (int index = 1; index <= data.Count; index++)
            {
                var question = data[index - 1];
                Paragraph blockQuestion = new Paragraph();
                Phrase questionContent = new Phrase($"Câu {index}: {question.Question} \n", _questionFontBold);
                blockQuestion.Add(questionContent);

                {
                    var isAnswer = question.Answer == "A";
                    Phrase option = new Phrase();
                    Chunk key = new Chunk("A", _generalFont);
                    if (isAnswer)
                    {
                        key.SetBackground(_answerColor);
                    }
                    Chunk content = new Chunk($" .{question.OptionA} \n", _generalFont);
                    option.Add(key);
                    option.Add(content);

                    blockQuestion.Add(option);
                }

                {
                    var isAnswer = question.Answer == "B";
                    Phrase option = new Phrase();
                    Chunk key = new Chunk("B", _generalFont);
                    if (isAnswer)
                    {
                        key.SetBackground(_answerColor);
                    }
                    Chunk content = new Chunk($" .{question.OptionB} \n", _generalFont);
                    option.Add(key);
                    option.Add(content);

                    blockQuestion.Add(option);
                }

                {
                    var isAnswer = question.Answer == "C";
                    Phrase option = new Phrase();
                    Chunk key = new Chunk("C", _generalFont);
                    if (isAnswer)
                    {
                        key.SetBackground(_answerColor);
                    }
                    Chunk content = new Chunk($" .{question.OptionC} \n", _generalFont);
                    option.Add(key);
                    option.Add(content);

                    blockQuestion.Add(option);
                }

                {
                    var isAnswer = question.Answer == "D";
                    Phrase option = new Phrase();
                    Chunk key = new Chunk("D", _generalFont);
                    if (isAnswer)
                    {
                        key.SetBackground(_answerColor);
                    }
                    Chunk content = new Chunk($" .{question.OptionD}", _generalFont);
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