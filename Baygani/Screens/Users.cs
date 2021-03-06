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
using Baygani.General;

namespace Baygani.Screens
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Users_Load(object sender, EventArgs e)
        {
            LoadAllData();
        }

        private void LoadAllData()
        {
            if (ISAdmin())
            {
                dataGridView1.DataSource = Select_Users_for_Admin();
            }
            else
            {
                dataGridView1.DataSource = Select_Users_for_Users(LoginInfo.UserID);
                buttonNew.Enabled = false;
                buttonDelete.Enabled = false;
            }
            SelectedCheck();
        }

        private DataTable Select_Users_for_Users(string userName)
        {
            DataTable dtrecord = new DataTable();
            using (SqlConnection con = new SqlConnection(General.ApplicationSetting.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from Users where Username="+"'"+userName+"'", con))
                {
                    con.Open();
                    SqlDataReader dtr = cmd.ExecuteReader();
                    dtrecord.Load(dtr);
                }
            }
            return dtrecord;
        }

        private DataTable Select_Users_for_Admin()
        {
            DataTable dtrecord = new DataTable();
            using (SqlConnection con = new SqlConnection(General.ApplicationSetting.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from Users", con))
                {
                    con.Open();
                    SqlDataReader dtr = cmd.ExecuteReader();
                    dtrecord.Load(dtr);
                }
            }
            return dtrecord;
        }

        private bool ISAdmin()
        {
            if (LoginInfo.UserID == "Admin")
            {
                return true;
            }
            return false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedCheck();
        }

        private void SelectedCheck()
        {
            textBoxUserName.Text = dataGridView1.SelectedRows[0].Cells["user"].Value.ToString();
            textBoxPassword.Text = dataGridView1.SelectedRows[0].Cells["Password"].Value.ToString();
            if (dataGridView1.SelectedRows[0].Cells["user"].Value.ToString() == "Admin")
            {
                textBoxUserName.Enabled = false;
                buttonDelete.Enabled = false;
            }
            else if (dataGridView1.SelectedRows[0].Cells["user"].Value.ToString() != "Admin")
            {
                textBoxUserName.Enabled = true;
                buttonDelete.Enabled = true;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (ISvalid())
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_Users_Update", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@UserName", textBoxUserName.Text.Trim());
                            cmd.Parameters.AddWithValue("@Password", textBoxPassword.Text.Trim());
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("عملیات مورد نظر با موفقیت انجام شد", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAllData();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            if (ISvalid())
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_Users_Insert", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@UserName", textBoxUserName.Text.Trim());
                            cmd.Parameters.AddWithValue("@Password", textBoxPassword.Text.Trim());
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("عملیات مورد نظر با موفقیت انجام شد", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAllData();
                }
                catch (SqlException ex)
                {

                    switch (ex.Number)
                    {
                        case 2627:
                            MessageBox.Show("نام کاربری نمیتواند تکراری باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        default:
                            MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw;
                    }
                }
            }
        }

        private bool ISvalid()
        {
            if (textBoxUserName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("وارد کردن نام کاربری الزامی می باشد", "ورود اطلاعات ناقص", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBoxPassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("وارد کردن کلمه عبور الزامی می باشد", "ورود اطلاعات ناقص", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DialogResult a = new DialogResult();
            a = MessageBox.Show("کاربر مورد نظر حذف خواهد شد آیا مطمئن هستید", "حذف کاربر", MessageBoxButtons.YesNo);
            if (a == DialogResult.Yes)
            {
                try
                {
                    using(SqlConnection con=new SqlConnection(ApplicationSetting.ConnectionString()))
                    {
                        using(SqlCommand cmd=new SqlCommand("Delete from Users where Username="+"'"+textBoxUserName.Text.Trim() + "'", con))
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("عملیات مورد نظر با موفقیت انجام شد", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAllData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
