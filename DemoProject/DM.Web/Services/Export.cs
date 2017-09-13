using DM.Service.IServices;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DM.Web.Service
{
    public class Export
    {
        private IDMService _dmService;

        public Export(IDMService dmService)
        {
            this._dmService = dmService;
        }
        public Stream CreateExcelClassFile()
        {
            var list = _dmService.getAllClass();
            using (var excelPackage = new ExcelPackage(new MemoryStream()))
            {
                // Tạo author cho file Excel
                excelPackage.Workbook.Properties.Author = "Khanh Le";
                // Tạo title cho file Excel
                excelPackage.Workbook.Properties.Title = "Excel_AllClass";
                // thêm tí comments vào làm màu 
                //excelPackage.Workbook.Properties.Comments = "This is my fucking generated Comments";
                // Add Sheet vào file Excel
                excelPackage.Workbook.Worksheets.Add("Class Sheet");
                // Lấy Sheet bạn vừa mới tạo ra để thao tác 
                var workSheet = excelPackage.Workbook.Worksheets[1];
                // Đổ data vào Excel file
                workSheet.Cells[1, 1].LoadFromCollection(list, true, TableStyles.Dark9);
                // BindingFormatForExcel(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }
    }
}