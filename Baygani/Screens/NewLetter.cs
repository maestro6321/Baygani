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

    public partial class NewLetter : Form
    {
        public NewLetter()
        {
            InitializeComponent();
        }
        public bool IsUpdate { get; set; }
        public int LetterID { get; set; }
        private void NewLetter_Load(object sender, EventArgs e)
        {
            LoadDataIntoComboBoxes();
            if (IsUpdate)
            {
                LoadAllDataByID();
                this.Text = "تغییر مشخصات نامه";
            }
        }

        private void LoadAllDataByID()
        {
            DataTable dtrecord = GetAllDataByID();
            DataRow row;
            row = dtrecord.Rows[0];
            LetterNumberTextBox.Text = row["Shomare_name"].ToString();
            LetterDateTextBox.Text = row["Tarikh"].ToString();
            LetterTitletextBox.Text = row["Title"].ToString();
            TypecomboBox.SelectedValue = Text = row["type"].ToString();
            CategorycomboBox.SelectedValue = Text = row["category"].ToString();
            OlaviatcomboBox.SelectedValue = Text = row["olaviat"].ToString();
            EdareFerestandecomboBox.SelectedValue = Text = row["id_ferestande"].ToString();
            VahedFerestandecomboBox.SelectedValue = Text = row["id_vahed_ferestande"].ToString();
            EdareGirandecomboBox.SelectedValue = Text = row["id_girande"].ToString();
            VahedGirandecomboBox.SelectedValue = Text = row["id_vahed_girande"].ToString();
        }
        private DataTable GetAllDataByID()
        {
            DataTable dtrecord = new DataTable();
            using (SqlConnection con=new SqlConnection(ApplicationSetting.ConnectionString()))
            {
                using (SqlCommand cmd=new SqlCommand("sp_tblBayegani_select_ByID", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", LetterID);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    dtrecord.Load(sdr);

                }
            }
            return dtrecord;
        }

        private void LoadDataIntoComboBoxes()
        {
            TypecomboBox.DataSource = GetComboBoxData("tblType");
            TypecomboBox.DisplayMember = "Name";
            TypecomboBox.ValueMember = "ID";
            CategorycomboBox.DataSource = GetComboBoxData("tblCategory");
            CategorycomboBox.DisplayMember = "CategoryName";
            CategorycomboBox.ValueMember = "ID";
            OlaviatcomboBox.DataSource = GetComboBoxData("tblOlaviat");
            OlaviatcomboBox.DisplayMember = "Olaviat_name";
            OlaviatcomboBox.ValueMember = "ID";
            EdareFerestandecomboBox.DataSource = GetComboBoxData("tblEdarat");
            EdareFerestandecomboBox.DisplayMember = "name_edare";
            EdareFerestandecomboBox.ValueMember = "ID";
            VahedFerestandecomboBox.DataSource = GetComboBoxData("tblVahed");
            VahedFerestandecomboBox.DisplayMember = "name_vahed";
            VahedFerestandecomboBox.ValueMember = "ID";
            EdareGirandecomboBox.DataSource = GetComboBoxData("tblEdarat");
            EdareGirandecomboBox.DisplayMember = "name_edare";
            EdareGirandecomboBox.ValueMember = "ID";
            VahedGirandecomboBox.DataSource = GetComboBoxData("tblVahed");
            VahedGirandecomboBox.DisplayMember = "name_vahed";
            VahedGirandecomboBox.ValueMember = "ID";

        }

        private DataTable GetComboBoxData(string v)
        {
            DataTable dtrecord = new DataTable();

            using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("select * from "+v, con))
                {
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    dtrecord.Load(sdr);
                }
            }
            return dtrecord;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                if (IsUpdate)
                {
                    try
                    {
                        using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
                        {
                            using (SqlCommand cmd = new SqlCommand("sp_tblBayegani_update", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@id", LetterID);
                                cmd.Parameters.AddWithValue("@Shomare_name", LetterNumberTextBox.Text.Trim());
                                cmd.Parameters.AddWithValue("@tarikh", LetterDateTextBox.Text);
                                cmd.Parameters.AddWithValue("@title", LetterTitletextBox.Text);
                                cmd.Parameters.AddWithValue("@id_ferestande", EdareFerestandecomboBox.SelectedValue);
                                cmd.Parameters.AddWithValue("@id_vahed_ferestande", VahedFerestandecomboBox.SelectedValue);
                                cmd.Parameters.AddWithValue("@id_girande", EdareGirandecomboBox.SelectedValue);
                                cmd.Parameters.AddWithValue("@id_vahed_girande", VahedGirandecomboBox.SelectedValue);
                                cmd.Parameters.AddWithValue("@type", TypecomboBox.SelectedValue);
                                cmd.Parameters.AddWithValue("@category", CategorycomboBox.SelectedValue);
                                cmd.Parameters.AddWithValue("@olaviat", OlaviatcomboBox.SelectedValue);
                                con.Open();
                                cmd.ExecuteNonQuery();
                            }
                        }
                        ResetAllObject();
                        MessageBox.Show("ثبت نامه با موفقیت انجام شد", "ثبت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    try
                    {
                        using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
                        {
                            using (SqlCommand cmd = new SqlCommand("sp_tblBayegani_insert", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@Shomare_name", LetterNumberTextBox.Text.Trim());
                                cmd.Parameters.AddWithValue("@tarikh", LetterDateTextBox.Text);
                                cmd.Parameters.AddWithValue("@title", LetterTitletextBox.Text);
                                cmd.Parameters.AddWithValue("@id_ferestande", EdareFerestandecomboBox.SelectedValue);
                                cmd.Parameters.AddWithValue("@id_vahed_ferestande", VahedFerestandecomboBox.SelectedValue);
                                cmd.Parameters.AddWithValue("@id_girande", EdareGirandecomboBox.SelectedValue);
                                cmd.Parameters.AddWithValue("@id_vahed_girande", VahedGirandecomboBox.SelectedValue);
                                cmd.Parameters.AddWithValue("@type", TypecomboBox.SelectedValue);
                                cmd.Parameters.AddWithValue("@category", CategorycomboBox.SelectedValue);
                                cmd.Parameters.AddWithValue("@olaviat", OlaviatcomboBox.SelectedValue);
                                con.Open();
                                cmd.ExecuteNonQuery();
                            }
                        }
                        ResetAllObject();
                        MessageBox.Show("ثبت نامه با موفقیت انجام شد", "ثبت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private bool IsValid()
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (Controls[i].GetType() == typeof(TextBox) || Controls[i].GetType() == typeof(ComboBox) || Controls[i].GetType() == typeof(MaskedTextBox))
                {
                    if (Controls[i].Text == "")
                    {
                        MessageBox.Show("لطفا مغادیر خواسته شده را به دقت وارد نمایید", "مغایرت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            ResetAllObject();
        }

        private void ResetAllObject()
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (Controls[i].GetType() == typeof(TextBox) || Controls[i].GetType() == typeof(ComboBox) || Controls[i].GetType() == typeof(MaskedTextBox))
                {
                    Controls[i].Text = "";
                }
            }
        }
    }
}
