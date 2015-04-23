using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace StudentData
{
    class Program
    {
        static void Main(string[] args)
        {
            int ignoreLine;
            string[] files;
            int filesCount;
            System.Data.DataTable dt = new System.Data.DataTable();
            Console.WriteLine("请输入TXT文件内容中需要忽略的行数\n");
            ignoreLine = int.Parse(Console.ReadLine());
            files = Directory.GetFiles(System.Environment.CurrentDirectory, "*.txt", SearchOption.AllDirectories);
            filesCount = files.Count();
            for (int i=0;i<filesCount;i++)
            {
                dt = DataTableUnion(dt, txtToDataTable(files[i], ignoreLine));
            }
            DataTabletoExcel(dt, System.Environment.CurrentDirectory + "\\output.xlsx");
        }

        public static System.Data.DataTable txtToDataTable(string filepath,int ignoreLine)
        {
            try
            {
                System.Data.DataTable dt;
                FileStream fs;
                StreamReader sr;
                string[] inputrow;
                string filename;
                string classname;
                fs = new FileStream(filepath, FileMode.Open);
                sr = new StreamReader(fs, Encoding.GetEncoding("GBK"));
                filename = filepath.Split('\\').Last();
                classname = sr.ReadLine().Split(' ')[0];
                for (int i = 1; i < ignoreLine; i++)
                {
                    sr.ReadLine();
                }
                dt = new System.Data.DataTable();
                inputrow = sr.ReadLine().Split('\t');
                inputrow[17] = "Course";
                foreach (string columnsname in inputrow)
                {
                    dt.Columns.Add(columnsname);
                }
                for (; ; )
                {
                    inputrow = sr.ReadLine().Split('\t');
                    if (inputrow[0] == "")
                        break;
                    inputrow[17] = filename.Split('.').First();
                    dt.Rows.Add(inputrow);
                }
                dt.Columns.Add("Class");
                foreach (DataRow dr in dt.Rows)
                {
                    dr["Class"] = classname;
                }
                return dt;
            }
            catch (NoNullAllowedException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static void DataTabletoExcel(System.Data.DataTable tmpDataTable, string strFileName)
        {
            if (tmpDataTable == null)
                return;
            int rowNum = tmpDataTable.Rows.Count;
            int columnNum = tmpDataTable.Columns.Count;
            int rowIndex = 1;
            int columnIndex = 0;

            Microsoft.Office.Interop.Excel.Application xlApp = new ApplicationClass();
            xlApp.DefaultFilePath = "";
            xlApp.DisplayAlerts = true;
            xlApp.SheetsInNewWorkbook = 1;
            Workbook xlBook = xlApp.Workbooks.Add(true);

            //将DataTable的列名导入Excel表第一行
            foreach (DataColumn dc in tmpDataTable.Columns)
            {
                columnIndex++;
                xlApp.Cells[rowIndex, columnIndex] = dc.ColumnName;
            }

            //将DataTable中的数据导入Excel中
            for (int i = 0; i < rowNum; i++)
            {
                rowIndex++;
                columnIndex = 0;
                for (int j = 0; j < columnNum; j++)
                {
                    columnIndex++;
                    xlApp.Cells[rowIndex, columnIndex] = tmpDataTable.Rows[i][j].ToString();
                }
            }
            xlBook.SaveCopyAs(strFileName);
            xlBook.Close();
            xlApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
            GC.Collect();
            GC.WaitForPendingFinalizers();  
        }

        public static System.Data.DataTable DataTableUnion(System.Data.DataTable dataTable1, System.Data.DataTable dataTable2)
        {
            if (dataTable2==null)
            {
                Console.WriteLine("Input Data Error: DataTable2 Is Null!\n");
                return null;
            }
            if (dataTable1.Columns.Count==0)
            {
                dataTable1 = dataTable2.Clone();
            }
            object[] obj = new object[dataTable1.Columns.Count];
            for (int i=0;i<dataTable2.Rows.Count;i++)
            {
                dataTable2.Rows[i].ItemArray.CopyTo(obj, 0);
                dataTable1.Rows.Add(obj);
            }
            return dataTable1;
        }
    }
}
