using System;
using System.Collections.Generic;
using System.IO;
using Abp.Collections.Extensions;
using Abp.Dependency;
using EZPost.Dto;
using OfficeOpenXml;

namespace EZPost.ServiceComm
{
    public class EpPlusExcelExporterBase
    {

        public static FileDto CreateExcelPackage(string fileName, Action<ExcelPackage> creator)
        {
            var file = new FileDto(fileName,"xlxs");

            using (var excelPackage = new ExcelPackage())
            {
                creator(excelPackage);
                Save(excelPackage, file);
            }

            return file;
        }

        public static void AddHeader(ExcelWorksheet sheet, int rowIndex = 1, params string[] headerTexts)
        {
            if (headerTexts.IsNullOrEmpty())
            {
                return;
            }

            for (var i = 0; i < headerTexts.Length; i++)
            {
                AddHeader(sheet, i + 1, headerTexts[i],rowIndex);
            }
        }

        private static void AddHeader(ExcelWorksheet sheet, int columnIndex, string headerText,int rowIndex=1)
        {
            sheet.Cells[rowIndex, columnIndex].Value = headerText;
            sheet.Cells[rowIndex, columnIndex].Style.Font.Bold = true;
        }

        public  static void AddObjects<T>(ExcelWorksheet sheet, int startRowIndex, IList<T> items, params Func<T, object>[] propertySelectors)
        {
            if (items.IsNullOrEmpty() || propertySelectors.IsNullOrEmpty())
            {
                return;
            }

            for (var i = 0; i < items.Count; i++)
            {
                for (var j = 0; j < propertySelectors.Length; j++)
                {
                    sheet.Cells[i + startRowIndex, j + 1].Value = propertySelectors[j](items[i]);
                }
            }
        }

        private static void Save(ExcelPackage excelPackage, FileDto file)
        {
            excelPackage.SaveAs(new FileInfo(file.FileName));
        }
    }
}