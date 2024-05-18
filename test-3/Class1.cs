using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace test_3
{
    class DataServices
    {
    private static SqlConnection mySqlConnection;
    private SqlDataAdapter myDataAdapter;
    //hàm kết nối với CSDL
    public bool OpenDB()
    {
        string conStr = "Data Source=I9-20900K-RTX-5\\PROJECT1;Initial Catalog=test;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";
        try
        {
            mySqlConnection = new SqlConnection(conStr);
            mySqlConnection.Open();
        }
        catch (SqlException ex)
        {
            DisplayError(ex);
            mySqlConnection = null;
            return false;
        }
        return true;
    }
    //Hàm truy vấn dữ liệu
    public DataTable RunQuery(string sSql)
    {
        DataTable myDataTable = new DataTable();
        try
        {
            myDataAdapter = new SqlDataAdapter(sSql, mySqlConnection);
            SqlCommandBuilder mySqlCommandBuilder = new SqlCommandBuilder(myDataAdapter);
            myDataAdapter.Fill(myDataTable);
        }
        catch (SqlException ex)
        {
            DisplayError(ex);
            return null;
        }
        return myDataTable;
    }
    //Hàm nhập nhật một DataTable vào một bảng của CSDL
    public void Update(DataTable myDataTable)
    {
        try
        {
            myDataAdapter.Update(myDataTable);
        }
        catch (SqlException ex)
        {
            DisplayError(ex);
        }
    }
    //Hàm thực hiện một câu lệnh SQL dựa trên SqlCommand
    public void ExecuteNonQuery(string sSql)
    {
        SqlCommand mySqlCommand = new SqlCommand(sSql, mySqlConnection);
        try
        {
            mySqlCommand.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            DisplayError(ex);
        }
    }
        public void DisplayError(SqlException ex)
        {
            string sSql = "SELECT * FROM Errors WHERE Number = " + ex.Number;
            DataTable dtError = RunQuery(sSql);
            if (dtError.Rows.Count > 0)
                MessageBox.Show(dtError.Rows[0][2].ToString().Trim(), "Lỗi " + ex.Number.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);   
            else
            
                MessageBox.Show(ex.Message, "Error " + ex.Number.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
              
           
    }
}
}

