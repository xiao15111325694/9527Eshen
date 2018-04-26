using System.Collections.Generic;
using System.Linq;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data;
using System.IO;
using NPOI.XSSF.UserModel;

namespace MVCEchartsManager.DataServer
{

    public class ExportServer
    {
        private readonly HelpDAl _helpDAl;

        public ExportServer()
        {

        }
        public ExportServer(HelpDAl helpDAl)
        {
            _helpDAl = helpDAl;
        }

        public HSSFWorkbook Export<T>(string ExportTableName, List<T> list)
        {
            HSSFWorkbook book = new HSSFWorkbook();
            ISheet sheet = book.CreateSheet(ExportTableName);
            IRow row = sheet.CreateRow(0);
            HelpDAl helpDal = new HelpDAl(); 
            var listViewDisplayName = helpDal.GetDisplayName(list.FirstOrDefault()); //得到表头信息

            //设置表格样式
            ICellStyle cellStyle = book.CreateCellStyle();
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderTop = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.VerticalAlignment = VerticalAlignment.Center;
            cellStyle.Alignment = HorizontalAlignment.Center;

            //设置表头
            for (int i = 0; i < listViewDisplayName.Count; i++)
            {
                row.CreateCell(i).SetCellValue(listViewDisplayName[i]);
            }
            //数据填充
            for (int i = 0; i < list.Count; i++)
            {
                IRow rowtemp = sheet.CreateRow(i + 1);
                var result = helpDal.GetValue(list[i]);
                for (int j = 0; j < result.Count; j++)
                {
                    rowtemp.CreateCell(j).SetCellValue(result[j]);
                }
            }
            return book;
        }

        public  DataTable ExcelToTable(string file)
        {
            DataTable dt = new DataTable();
            IWorkbook workbook;


            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {

                workbook = new XSSFWorkbook(fs);

                if (workbook == null) { return null; }
                ISheet sheet = workbook.GetSheetAt(0);
                List<int> columns = new List<int>();
                for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                {
                    DataRow dr = dt.NewRow();
                    bool hasValue = false;
                    foreach (int j in columns)
                    {
                        dr[j] = GetValueType(sheet.GetRow(i).GetCell(j));
                        if (dr[j] != null && dr[j].ToString() != string.Empty)
                        {
                            hasValue = true;
                        }
                    }
                    if (hasValue)
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }

        private static object GetValueType(ICell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Blank: //BLANK:  
                    return null;
                case CellType.Boolean: //BOOLEAN:  
                    return cell.BooleanCellValue;
                case CellType.Numeric: //NUMERIC:  
                    return cell.NumericCellValue;
                case CellType.String: //STRING:  
                    return cell.StringCellValue;
                case CellType.Error: //ERROR:  
                    return cell.ErrorCellValue;
                case CellType.Formula: //FORMULA:  
                default:
                    return "=" + cell.CellFormula;
            }
        }
    }
}