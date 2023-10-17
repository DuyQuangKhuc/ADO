using System;
using System.Data;
using System.Data.SqlClient;

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
        // Tạo kết nối
        var sqlconnectstring = "Data Source=DESKTOP-O6LJBT8\\LONG;database=QuanlyNhanvien;uid=sa;pwd=long;" +
            "Integrated Security=True;TrustServerCertificate=True;Encrypt = False";
        var connection = new SqlConnection(sqlconnectstring);
        connection.Open();


        // TẠO DataAdapter
        SqlDataAdapter adapter = new SqlDataAdapter();

        // Thiết lập bảng trong DataSet ánh xạ tương ứng có tên là Nhanvien
        adapter.TableMappings.Add("Table", "Nhanvien");

        // SelectCommand - Thực thi khi gọi Fill lấy dữ liệu về DataSet
        adapter.SelectCommand = new SqlCommand(@"SELECT NhanviennID,Ten,Ho FROM Nhanvien", connection);

        int id = 1;

        adapter.SelectCommand = new SqlCommand(@"SELECT NhanviennID,Ten,Ho FROM Nhanvien WHERE NhanviennID = @NhanviennID and Ho= @Ho", connection);
        adapter.SelectCommand.Parameters.Add(new SqlParameter("@NhanviennID", id));
        adapter.SelectCommand.Parameters.Add(new SqlParameter("@Ho", "Hoang"));
        //getsv.SourceColumn = "NhanviennID";
        //getsv.SourceVersion = DataRowVersion.Original;  // Giá trị ban đầu

        // InsertCommand - Thực khi khi gọi Update, nếu DataSet có chèn dòng mới (vào DataTable)
        // Dữ liệu lấy từ DataTable, như cột Ten tương  ứng với tham số @Ten
        adapter.InsertCommand = new SqlCommand(@"INSERT INTO Nhanvien (Ten, Ho) VALUES (@Ten, @Ho)", connection);
        adapter.InsertCommand.Parameters.Add("@Ten", SqlDbType.NVarChar, 200, "Ten");
        adapter.InsertCommand.Parameters.Add("@Ho", SqlDbType.NVarChar, 200, "Ho");

        // DeleteCommand  - Thực thi khi gọi Update, nếu có remove dòng nào đó của DataTable
        adapter.DeleteCommand = new SqlCommand(@"DELETE FROM Nhanvien WHERE NhanviennID = @NhanviennID", connection);
        var pr1 = adapter.DeleteCommand.Parameters.Add(new SqlParameter("@NhanviennID", SqlDbType.Int));
        pr1.SourceColumn = "NhanviennID";
        pr1.SourceVersion = DataRowVersion.Original;  // Giá trị ban đầu


        // UpdateCommand -  Thực thi khi gọi Update, nếu có chỉnh sửa trường dữ liệu nào đó
        adapter.UpdateCommand = new SqlCommand(@"UPDATE Nhanvien SET Ho=@Ho, Ten = @Ten
                                         WHERE NhanviennID = @NhanviennID", connection);
        adapter.UpdateCommand.Parameters.Add("@Ten", SqlDbType.NVarChar, 200, "Ten");
        adapter.UpdateCommand.Parameters.Add("@Ho", SqlDbType.NVarChar, 200, "Ho");
        var pr2 = adapter.UpdateCommand.Parameters.Add(
            new SqlParameter("@NhanviennID", SqlDbType.Int)
            { SourceColumn = "NhanviennID" });
        pr2.SourceVersion = DataRowVersion.Original;



        DataSet dataSet = new DataSet();

        // Thực hiện lấy dữ liệu từ nguồn về DataSet
        adapter.Fill(dataSet);
        // Lấy DataTable kết quả và hiện thị
        DataTable dataTable = dataSet.Tables["Nhanvien"];
        ShowDataTable(dataTable);



        // Ví dụ thêm một dòng dữ liệu
        //var rowadd = dataTable.Rows.Add();
        //rowadd["Ho"] = "Họ mới";
        //rowadd["Ten"] = "Tên mới";
        //adapter.Update(dataSet);
        //// Nạp lại
        //adapter.Fill(dataSet);

        //// Ví dụ cập nhật dòng thứ 10
        //var rowedit = dataTable.Rows[10];
        //rowedit["Ho"] = "Nguyễn";
        //adapter.Update(dataSet);



        //// Ví dụ xóa một dòng đữ thứ 10
        //var rowdelete = dataTable.Rows[10];
        //rowdelete.Delete();
        //adapter.Update(dataSet);




        connection.Close();
    }
}