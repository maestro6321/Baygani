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
    public partial class Vahedha : Form
    {
        public int Id_Vahed;
        public string name_Vahed;
        public Vahedha()
        {
            InitializeComponent();
        }

        private void Vahedha_Load(object sender, EventArgs e)
        {
            loadlistbox();
        }

        private void loadlistbox()
        {
            EdaratlistBox.DataSource = GetAllData();
            EdaratlistBox.DisplayMember = "name_vahed";
            EdaratlistBox.ValueMember = "ID";
        }

        private DataTable GetAllData()
        {
            DataTable dtrecord = new DataTable();
            using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from tblVahed", con))
                {
                    con.Open();
                    SqlDataReader dtr = cmd.ExecuteReader();
                    dtrecord.Load(dtr);
                }
            }
            return dtrecord;
        }

        private void EdaratlistBox_Click(object sender, EventArgs e)
        {
            Id_Vahed = Convert.ToInt32(EdaratlistBox.SelectedValue.ToString());
            textBoxName.Text = EdaratlistBox.Text;
            name_Vahed = textBoxName.Text;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (Id_Vahed > 0 && textBoxName.Text != "")
            {
                try
                {
                    name_Vahed = textBoxName.Text;
                    using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_tblVahed_update", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@name_vahed", name_Vahed);
                            cmd.Parameters.AddWithValue("@id", Id_Vahed);
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("عملیات مورد نظر با موفقیت انجام شد", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Id_Vahed = 0;
                    name_Vahed = null;
                    textBoxName.Text = "";
                    loadlistbox();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text != "")
            {
                try
                {
                    name_Vahed = textBoxName.Text;
                    using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_tblVahed_Insert", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@name_vahed", name_Vahed);
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("عملیات مورد نظر با موفقیت انجام شد", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Id_Vahed = 0;
                    name_Vahed = null;
                    textBoxName.Text = "";
                    loadlistbox();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (Id_Vahed > 0)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("delete from tblVahed where id=" + Convert.ToString(Id_Vahed), con))
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("عملیات مورد نظر با موفقیت انجام شد", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Id_Vahed = 0;
                    name_Vahed = null;
                    loadlistbox();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
