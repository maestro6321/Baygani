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
    public partial class Edarat : Form
    {
        public int Id_Edare;
        public string name_Edare;
        public Edarat()
        {
            InitializeComponent();
        }

        private void Edarat_Load(object sender, EventArgs e)
        {
            loadlistbox();
        }

        private void loadlistbox()
        {
            EdaratlistBox.DataSource = GetAllData();
            EdaratlistBox.DisplayMember = "name_edare";
            EdaratlistBox.ValueMember = "ID";
        }

        private DataTable GetAllData()
        {
            DataTable dtrecord = new DataTable();
            using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
            {
                using(SqlCommand cmd=new SqlCommand("Select * from tblEdarat", con))
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
            Id_Edare =Convert.ToInt32( EdaratlistBox.SelectedValue.ToString());
            textBoxName.Text = EdaratlistBox.Text;
            name_Edare = textBoxName.Text;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (Id_Edare > 0 && textBoxName.Text != "")
            {
                try
                {
                    name_Edare = textBoxName.Text;
                    using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_tblEdarat_update", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@name_edare", name_Edare);
                            cmd.Parameters.AddWithValue("@id", Id_Edare);
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("عملیات مورد نظر با موفقیت انجام شد", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Id_Edare = 0;
                    name_Edare = null;
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
                    name_Edare = textBoxName.Text;
                    using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_tblEdarat_insert", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@name_edare", name_Edare);
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("عملیات مورد نظر با موفقیت انجام شد", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Id_Edare = 0;
                    name_Edare = null;
                    textBoxName.Text = "";
                    loadlistbox();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (Id_Edare > 0)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("delete from tblEdarat where id=" + Convert.ToString(Id_Edare), con))
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("عملیات مورد نظر با موفقیت انجام شد", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Id_Edare = 0;
                    name_Edare = null;
                    loadlistbox();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
