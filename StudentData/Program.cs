using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace StudentData
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int ignoreLine;
                string[] files;
                int filesCount;
                DataTable dt = new DataTable();
                DataTable all = new DataTable();
                Console.WriteLine("请输入输出文件名（不包括扩展名）：\n");
                string filename = Console.ReadLine();
                ignoreLine = 5;
                files = Directory.GetFiles(Environment.CurrentDirectory, "*.txt", SearchOption.AllDirectories);
                filesCount = files.Count();
                for (int i = 0; i < filesCount; i++)
                {
                    try
                    {
                        switch (files[i].Split('\\').Last())
                        {
                            case "ALL.txt":
                                all = DataTableUnion(all, TxtToDataTable(files[i], ignoreLine));
                                break;
                            default:
                                dt = DataTableUnion(dt, TxtToDataTable(files[i], ignoreLine));
                                break;
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("处理失败，{0}\n当前处理文件为：{1}，请排查此文件及其前后文件是否有问题！", e.Message, files[i]);
                        Console.ReadKey();
                        return;
                    }
                    
                }
                if (DatatableToCSV(dt, filename + "-courses.csv") == true && DatatableToCSV(all, filename + "-all.csv"))
                {
                    Console.WriteLine("Done!");
                }
                else
                {
                    Console.WriteLine("Failed!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("处理失败，{0}。",e.Message);
                return;
            }
        }

        public static DataTable TxtToDataTable(string filepath,int ignoreLine)
        {
            try
            {
                DataTable dt;
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
                dt = new DataTable();
                inputrow = sr.ReadLine().Split('\t');
                inputrow[inputrow.Count()-1] = "Course";
                foreach (string columnsname in inputrow)
                {
                    dt.Columns.Add(columnsname);
                }
                for (; ; )
                {
                    inputrow = sr.ReadLine().Split('\t');
                    if (inputrow[0] == "")
                        break;
                    inputrow[inputrow.Count()-1] = filename.Split('.').First();
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
                Console.WriteLine("处理失败，{0}。", e.Message);
                return null;
            }
        }

        public static DataTable DataTableUnion(DataTable dataTable1, DataTable dataTable2)
        {
            try
            {
                if (dataTable2 == null)
                {
                    Console.WriteLine("Input Data Error: DataTable2 Is Null!\n");
                    return null;
                }
                if (dataTable1.Columns.Count == 0)
                {
                    dataTable1 = dataTable2.Clone();
                }
                object[] obj = new object[dataTable1.Columns.Count];
                for (int i = 0; i < dataTable2.Rows.Count; i++)
                {
                    dataTable2.Rows[i].ItemArray.CopyTo(obj, 0);
                    dataTable1.Rows.Add(obj);
                }
                return dataTable1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool DatatableToCSV(DataTable dt, string fileName)
        {
            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                string data = "";
                //写出列名称
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    data += dt.Columns[i].ColumnName.ToString();
                    if (i < dt.Columns.Count - 1)
                    {
                        data += ",";
                    }
                }
                sw.WriteLine(data);
                //写出各行数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    data = "";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        data += dt.Rows[i][j].ToString();
                        if (j < dt.Columns.Count - 1)
                        {
                            data += ",";
                        }
                    }
                    sw.WriteLine(data);
                }
                sw.Close();
                fs.Close();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
