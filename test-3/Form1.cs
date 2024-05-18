using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace test_3
{
    public partial class Form1 : Form
    {
        private static SqlConnection mySqlConnection;
        private SqlDataAdapter myDataAdapter;
        private DataServices myDataServices;
        private DataTable dtatable;

        public Form1()
        {
            InitializeComponent();
            guna2TextBox2.UseSystemPasswordChar = true;
           
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string conStr = "Data Source=I9-20900K-RTX-5\\PROJECT1;Initial Catalog=test;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";
            try
            {
                mySqlConnection = new SqlConnection(conStr);
                mySqlConnection.Open();
                MessageBox.Show("Kết nối thành công");
                myDataAdapter = new SqlDataAdapter("Select * from userlogin where email='"+guna2TextBox1+ "' AND  hashbytes('SHA2_512','"+guna2TextBox2+"')= password", mySqlConnection);
                dtatable = new DataTable();
                myDataAdapter.Fill(dtatable);
                if(dtatable.Rows.Count > 0)
                {
                    MessageBox.Show("Đăng nhập thành công");
                }
                else
                {
                    MessageBox.Show("Đăng nhập thất bại");
                }
            }
            catch 
            {
              MessageBox.Show("Lỗi kết nối CSDL");
                mySqlConnection = null;
            }


         

        }

        private void ShowPassButton_CheckedChanged(object sender, EventArgs e)
        {
            //Nếu checkbox được check thì hiện mật khẩu
            if (ShowPassButton.Checked)
            {
                guna2TextBox2.UseSystemPasswordChar = false;
            }
            else
            {
                guna2TextBox2.UseSystemPasswordChar = true;
            }
        }
    }


}
