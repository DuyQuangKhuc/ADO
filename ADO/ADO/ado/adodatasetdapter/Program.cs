using System;
using System.Data;

class Program
{
    static void ShowDataTable(DataTable table)
    {
        Console.WriteLine("Bảng: " + table.TableName);
        // Hiện thị các cột
        foreach (DataColumn column in table.Columns)
        {
            Console.Write($"{column.ColumnName,15}");
        }
        Console.WriteLine();

        // Hiện thị các dòng dữ liệu
        int number_cols = table.Columns.Count;
        foreach (DataRow row in table.Rows)
        {
            for (int i = 0; i < number_cols; i++)
            {
                Console.Write($"{row[i],15}");
            }
            Console.WriteLine();
        }

    }
    public static void Main(string[] args)
    {
        DataTable table = new DataTable("MyTable");

        // Thêm các cột vào bảng
        table.Columns.Add("STT");
        table.Columns.Add("HoTen");
        table.Columns.Add("Tuoi");


        // Thêm dòng liệu vào cột
        table.Rows.Add(1, "XuanThuLab", 25);      
        table.Rows.Add(2, "Nguyen Van A", 23);   
        table.Rows.Add(3, "Nguyen Van B", 20);   
        //DataSet dataSet = new DataSet();
        //dataSet.Tables.Add(table);
        // Duyệt qua các cột, in tên cột
        Console.WriteLine($"Bảng {table.TableName}");
        foreach (DataColumn c in table.Columns)
        {
            Console.Write($"{c.ColumnName,20}");
        }
        Console.WriteLine();

        // Duyệt qua các dòng và in  dữ liệu cột
        for (int i = 0; i < table.Rows.Count; i++)
        {
            Console.Write($"{table.Rows[i][0],20}");           
            Console.Write($"{table.Rows[i]["HoTen"],20}");      
            Console.Write($"{table.Rows[i]["Tuoi"],20}");        
            Console.WriteLine();
        }

        // Gán giá trị dữ liệu vào trường (cell)
        table.Rows[2]["HoTen"] = "Họ tên mới";
        
        // Hoặc duyệt bằng foreach liệt kê các dòng
        Console.WriteLine();
        ShowDataTable(table);
    }
}