using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Baygani.General;
using System.Data.SqlClient;

namespace Baygani.Screens
{
    public partial class Attachment : Form
    {
        public int LetterID { get; set; }
        public string Shomare_Name { get; set; }
        public string DefaultFolder { get; set; }

        public string File_name;

        public Attachment()
        {
            InitializeComponent();
        }

        private void Attachment_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = LoadAllData();
                GetDefaultFolder();
                label5.Text = Shomare_Name;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private object LoadAllData()
        {
            DataTable dtrecord = new DataTable();
            using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("select Attachment from tblAttachment where Letter_ID=" + Convert.ToString(LetterID), con))
                {
                    con.Open();
                    SqlDataReader dtr = cmd.ExecuteReader();
                    dtrecord.Load(dtr);
                }
            }
            return dtrecord;
        }

        private void GetDefaultFolder()
        {
            using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("select DefaultAttachFolder from tblOption", con))
                {
                    con.Open();
                    SqlDataReader dtr = cmd.ExecuteReader();
                    while (dtr.Read())
                    {
                        DefaultFolder = dtr["DefaultAttachFolder"].ToString();
                    }
                }
            }
            label3.Text = DefaultFolder;
        }


        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(DefaultFolder+@"\"+Shomare_Name + @"\" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (File_name !=null)
            {
                try
                {
                    if (!System.IO.Directory.Exists(DefaultFolder + @"\" + Shomare_Name))
                        System.IO.Directory.CreateDirectory(DefaultFolder + @"\" + Shomare_Name);
                    File_name = LetterID + "_" + File_name;
                    System.IO.File.Copy(openFileDialog1.FileName, DefaultFolder + @"\" + Shomare_Name + @"\" + File_name, false);
                    using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_tblAttachment_insert", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Letter_ID", LetterID);
                            cmd.Parameters.AddWithValue("@Attachment", File_name);
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("عملیات مورد نظر با موفقیت انجام شد", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = LoadAllData();
                    File_name = null;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != null)
            {
                textBoxPatch.Text = openFileDialog1.FileName;
                File_name = openFileDialog1.SafeFileName;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DialogResult a;
            a = MessageBox.Show("رکورد مورد نظر حذف خواهد شد آیا مطمئن هستید", "حذف رکورد", MessageBoxButtons.YesNo);
            if (a == DialogResult.Yes)
            {
                try
                {
                    File_name = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_tblAttachment_delete", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Letter_ID", LetterID);
                            cmd.Parameters.AddWithValue("@Attachment", File_name);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            if (System.IO.File.Exists(DefaultFolder + @"\" + Shomare_Name + @"\" + File_name))
                            {
                                DialogResult i;
                                i = MessageBox.Show("آیا مایل به حذف فایل مورد نظر از دایرکتوری هستید ؟", "حذف فایل اصلی", MessageBoxButtons.YesNo);
                                if (i == DialogResult.Yes)
                                {
                                    System.IO.File.Delete(DefaultFolder + @"\" + Shomare_Name + @"\" + File_name);
                                }
                            }
                        }
                    }
                    MessageBox.Show("عملیات مورد نظر با موفقیت انجام شد", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = LoadAllData();
                    File_name = null;
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
