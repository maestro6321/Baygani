using Baygani.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Baygani.Screens
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
                {
                    con.Open();


                    if (con.State == ConnectionState.Open)
                    {
                        btnstatus.BackColor = Color.Green;
                        UserNamecomboBox.Enabled = true;
                        PasswodTextBox.Enabled = true;
                        LoginButton.Enabled = true;
                        label4.Text = "ارتباط برقرار است";
                        label4.ForeColor = Color.Green;
                        timer1.Enabled = false;
                        UserNamecomboBox.DataSource = Load_UserNames();
                        UserNamecomboBox.DisplayMember = "Username";
                        UserNamecomboBox.ValueMember = "Username";
                    }
                }
            }
            catch
            {
                btnstatus.BackColor = Color.Red;
                UserNamecomboBox.Enabled = false;
                PasswodTextBox.Enabled = false;
                LoginButton.Enabled = false;
                label4.Text = "ارتباط برقرار نیست";
                label4.ForeColor = Color.Red;
            }
        }

        private DataTable Load_UserNames()
        {
            DataTable dtrecord = new DataTable();
            using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Select Username from Users", con))
                {
                    con.Open();
                    SqlDataReader dtr = cmd.ExecuteReader();
                    dtrecord.Load(dtr);
                }
            }
            return dtrecord;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Users_VerifyLoginDetails", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserName", UserNamecomboBox.SelectedValue);
                        cmd.Parameters.AddWithValue("@Password", PasswodTextBox.Text.Trim());
                        con.Open();
                        SqlDataReader sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            this.Hide();
                            Main frm = new Main();
                            LoginInfo.UserID = UserNamecomboBox.SelectedValue.ToString();
                            frm.Show();
                        }
                        else
                        {
                            MessageBox.Show("نام کاربری یا کلمه عبور اشتباه است", "ورود ناموفق", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private bool isValid()
        {
            if (UserNamecomboBox.SelectedValue == null)
            {
                MessageBox.Show("وارد کردن نام کاربری الزامی می باشد", "ورود اطلاعات ناقص", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (PasswodTextBox.Text.Trim() == string.Empty)
            {
                MessageBox.Show("وارد کردن کلمه عبور الزامی می باشد", "ورود اطلاعات ناقص", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
