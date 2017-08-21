using System;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace EZPost.Web.Common
{
    public class ExcelHelper
    {
        public static DataTable ImportExcelFileForTable(string filePath,int startRow)
        {
            XSSFWorkbook xssfworkbook;
            #region//初始化信息
            try
            {
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    xssfworkbook = new XSSFWorkbook(file);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            #endregion

            NPOI.SS.UserModel.ISheet sheet = xssfworkbook.GetSheetAt(0);
            DataTable table = new DataTable();
            IRow headerRow = sheet.GetRow(0);//第一行为标题行
            int cellCount = headerRow.LastCellNum;
            int rowCount = sheet.LastRowNum;

            //handling header.
            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }
            for (int i = (sheet.FirstRowNum + startRow); i <= rowCount; i++) 
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = table.NewRow();

                if (row != null)
                {
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                            dataRow[j] = GetCellValue(row.GetCell(j));
                    }
                }
                table.Rows.Add(dataRow);
            }
            return table;
        }

        public static DataSet ImportExcelFileForDataSet(string filePath, int startRow)
        {
            XSSFWorkbook xssfworkbook;
            #region//初始化信息
            try
            {
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    xssfworkbook = new XSSFWorkbook(file);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            #endregion

            NPOI.SS.UserModel.ISheet sheet = xssfworkbook.GetSheetAt(0);
            DataTable table = new DataTable();
            IRow headerRow = sheet.GetRow(0);//第一行为标题行
            int cellCount = headerRow.LastCellNum;
            int rowCount = sheet.LastRowNum;

            //handling header.
            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }
            for (int i = (sheet.FirstRowNum + startRow); i <= rowCount; i++) //第二行为标题行说明 所以sheet.FirstRowNum + 2
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = table.NewRow();

                if (row != null)
                {
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                            dataRow[j] = GetCellValue(row.GetCell(j));
                    }
                    table.Rows.Add(dataRow);
                }
            }

            NPOI.SS.UserModel.ISheet sheet2 = xssfworkbook.GetSheetAt(1);
            DataTable table2 = new DataTable();
            IRow headerRow2 = sheet2.GetRow(0);//第一行为标题行
            int cellCount2 = headerRow2.LastCellNum;
            int rowCount2 = sheet2.LastRowNum;

            //handling header.
            for (int i = headerRow2.FirstCellNum; i < cellCount2; i++)
            {
                DataColumn column = new DataColumn(headerRow2.GetCell(i).StringCellValue);
                table2.Columns.Add(column);
            }
            for (int i = (sheet2.FirstRowNum + 2); i <= rowCount2; i++) //第二行为标题行说明 所以sheet.FirstRowNum + 2
            {
                IRow row = sheet2.GetRow(i);
                DataRow dataRow = table2.NewRow();

                if (row != null)
                {
                    for (int j = row.FirstCellNum; j < cellCount2; j++)
                    {
                        if (row.GetCell(j) != null)
                            dataRow[j] = GetCellValue(row.GetCell(j));
                    }
                    table2.Rows.Add(dataRow);
                }
            }

            var ds = new DataSet();
            ds.Tables.Add(table);
            ds.Tables.Add(table2);
            return ds;
        }

        private static string GetCellValue(ICell cell)
        {
            if (cell == null)
                return string.Empty;
            switch (cell.CellType)
            {
                case CellType.Blank:
                    return string.Empty;
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                case CellType.Error:
                    return cell.ErrorCellValue.ToString();
                case CellType.Numeric:
                case CellType.Unknown:
                default:
                    return cell.ToString();//This is a trick to get the correct value of the cell. NumericCellValue will return a numeric value no matter the cell value is a date or a number
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Formula:
                    try
                    {
                        HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(cell.Sheet.Workbook);
                        e.EvaluateInCell(cell);
                        return cell.ToString();
                    }
                    catch
                    {
                        return cell.NumericCellValue.ToString();
                    }
            }
        }
    }
}