﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using OfficeOpenXml;

namespace EZPost.Dto
{
    #region 需要用到的实体

    public class XlsEntity
    {
        /// <summary>
        /// 实体名称
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// 列名称
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 列下标
        /// </summary>
        public int ColumnIndex { get; set; }

        /// <summary>
        /// 转换方法
        /// </summary>
        public Func<string, object> ConvertFunc { get; set; }
    }

    public class XlsRow
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public List<string> ErrMessage { get; set; }

        /// <summary>
        /// 错误列名
        /// </summary>
        public List<string> ErrColumn { get; set; }

        /// <summary>
        /// 错误内容
        /// </summary>
        public List<string> ErrValue { get; set; }

        /// <summary>
        /// 是否转换出错（false:未出错，true：出错）
        /// </summary>
        public bool IsErr { get; set; }

        /// <summary>
        /// 出错行数
        /// </summary>
        public  int RowIndex { set; get; }
    }

    #endregion

    public class ECEntityCOM<T> where T : XlsRow, new()
    {
        private  List<XlsEntity> xlsHeader = new List<XlsEntity>();

        #region 初始化转换形式


        public  void ForMember(Expression<Func<T, object>> entityExpression, Func<string, object> func)
        {
            XlsEntity xlsEntity = new XlsEntity();
            xlsEntity.EntityName = GetPropertyName(entityExpression);
            xlsEntity.ColumnName = xlsEntity.EntityName;
            xlsEntity.ConvertFunc = func;
            xlsHeader.Add(xlsEntity);
        }


        public  void ForMember(string columnName, Expression<Func<T, object>> entityExpression)
        {
            XlsEntity xlsEntity = new XlsEntity();
            xlsEntity.ColumnName = columnName;
            xlsEntity.EntityName = GetPropertyName(entityExpression);
            xlsHeader.Add(xlsEntity);
        }

        public  void ForMember(string columnName, string entityName)
        {
            XlsEntity xlsEntity = new XlsEntity();
            xlsEntity.ColumnName = columnName;
            xlsEntity.EntityName = entityName;
            xlsHeader.Add(xlsEntity);
        }

        public  void ForMember(string columnName, string entityName, Func<string, object> func)
        {
            XlsEntity xlsEntity = new XlsEntity();
            xlsEntity.ColumnName = columnName;
            xlsEntity.EntityName = entityName;
            xlsEntity.ConvertFunc = func;
            xlsHeader.Add(xlsEntity);
        }

        public  void ForMember(string columnName, Expression<Func<T, object>> entityExpression, Func<string, object> func)
        {
            XlsEntity xlsEntity = new XlsEntity();
            xlsEntity.ColumnName = columnName;
            xlsEntity.EntityName = GetPropertyName(entityExpression);
            xlsEntity.ConvertFunc = func;
            xlsHeader.Add(xlsEntity);
        }

        #endregion

        #region 从Excel中加载数据（泛型）

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="skipRow">跳过第几行</param>
        /// <returns></returns>
        public  List<T> LoadFromExcel(string filePath, int sheetIndex,int skipRow)
        {
            FileInfo existingFile = new FileInfo(filePath);
            List<T> resultList = new List<T>();

            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetIndex];

                int colStart = worksheet.Dimension.Start.Column;
                int colEnd = worksheet.Dimension.End.Column;
                int rowStart = worksheet.Dimension.Start.Row;
                int rowEnd = worksheet.Dimension.End.Row;

                PropertyInfo[] propertyInfoList = typeof(T).GetProperties();
                XlsEntity xlsEntity;

                #region 将实体和excel列标题进行对应绑定,添加到集合中

                for (int i = colStart; i <= colEnd; i++)
                {
                    string columnName = worksheet.Cells[rowStart, i].Value.ToString();

                    xlsEntity = xlsHeader.FirstOrDefault(e => e.ColumnName == columnName);

                    for (int j = 0; j < propertyInfoList.Length; j++)
                    {

                        if (xlsEntity != null && xlsEntity.ColumnName == columnName)
                        {
                            xlsEntity.ColumnIndex = i;
                            xlsHeader.Add(xlsEntity);
                        }
                        else if (propertyInfoList[j].Name == columnName)
                        {
                            xlsEntity = new XlsEntity();
                            xlsEntity.ColumnName = columnName;
                            xlsEntity.EntityName = propertyInfoList[j].Name;
                            xlsEntity.ColumnIndex = i;
                            xlsHeader.Add(xlsEntity);
                            break;
                        }
                    }
                }

                #endregion

                #region 根据对应的实体名列名的对应绑定就行值的绑定

                for (int row = rowStart + skipRow; row <= rowEnd; row++)
                {
                    T result = new T();
                    ExcelRange tempCell = worksheet.Cells[row, 1];
                    if(tempCell.Value==null) break;

                    foreach (PropertyInfo p in propertyInfoList)
                    {
                        var xlsRow = xlsHeader.FirstOrDefault(e => e.EntityName == p.Name);
                        if (xlsRow == null) continue;

                        ExcelRange cell = worksheet.Cells[row, xlsRow.ColumnIndex];
                        if (cell.Value == null) continue;
                        try
                        {
                            if (xlsRow.ConvertFunc != null)
                            {
                                object entityValue = xlsRow.ConvertFunc(cell.Value.ToString());
                                p.SetValue(result, entityValue);
                            }
                            else
                            {
                                cellBindValue(result, p, cell);
                            }
                       }
                        catch (Exception ex)
                        {
                            if (result.ErrColumn == null) result.ErrColumn = new List<string>();
                            if (result.ErrMessage == null) result.ErrMessage = new List<string>();
                            if (result.ErrValue == null) result.ErrValue = new List<string>();
                            result.ErrColumn.Add(p.Name);
                            result.ErrMessage.Add(ex.Message);
                            result.ErrValue.Add(cell.Value.ToString());
                            result.RowIndex = row;
                            result.IsErr = true;
                        }
                }
                    resultList.Add(result);
                }

                #endregion
            }
            return resultList;
        }

        #endregion

        #region 给实体绑定值

        private static void cellBindValue(T result, PropertyInfo p, ExcelRange cell)
        {
            switch (p.PropertyType.Name.ToLower())
            {
                case "string":
                    p.SetValue(result, cell.GetValue<String>());
                    break;
                case "int16":
                    p.SetValue(result, cell.GetValue<Int16>());
                    break;
                case "int32":
                    p.SetValue(result, cell.GetValue<Int32>());
                    break;
                case "int64":
                    p.SetValue(result, cell.GetValue<Int64>());
                    break;
                case "decimal":
                    p.SetValue(result, cell.GetValue<Decimal>());
                    break;
                case "double":
                    p.SetValue(result, cell.GetValue<Double>());
                    break;
                case "datetime":
                    p.SetValue(result, cell.GetValue<DateTime>());
                    break;
                case "boolean":
                    p.SetValue(result, cell.GetValue<Boolean>());
                    break;
                case "byte":
                    p.SetValue(result, cell.GetValue<Byte>());
                    break;
                case "char":
                    p.SetValue(result, cell.GetValue<Char>());
                    break;
                case "single":
                    p.SetValue(result, cell.GetValue<Single>());
                    break;
                default:
                    p.SetValue(result, cell.Value);
                    break;
            }
        }

        #endregion

        #region 获取Lambda的属性名称

        private static string GetPropertyName(Expression<Func<T, object>> expression)
        {
            Expression expressionToCheck = expression;
            bool done = false;
            while (!done)
            {
                switch (expressionToCheck.NodeType)
                {
                    case ExpressionType.Convert:
                        expressionToCheck = ((UnaryExpression)expressionToCheck).Operand;
                        break;
                    case ExpressionType.Lambda:
                        expressionToCheck = ((LambdaExpression)expressionToCheck).Body;
                        break;
                    case ExpressionType.MemberAccess:
                        var memberExpression = ((MemberExpression)expressionToCheck);
                        string propertyName = memberExpression.Member.Name;
                        return propertyName;
                    default:
                        done = true;
                        break;
                }
            }
            return "";
        }

        #endregion
    }
}