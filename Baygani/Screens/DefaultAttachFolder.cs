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
    public partial class DefaultAttachFolder : Form
    {
        public string DefaultFolder { get; set; }
        public DefaultAttachFolder()
        {
            InitializeComponent();
        }

        private void DefaultAttachFolder_Load(object sender, EventArgs e)
        {
            GetDefaultFolder();
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
            label1.Text = DefaultFolder;
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            if(folderBrowserDialog1.SelectedPath != null)
            {
                textBoxPatch.Text = folderBrowserDialog1.SelectedPath.ToString();
                DefaultFolder = textBoxPatch.Text;
            }
        }



        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if(textBoxPatch.Text != null)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_tblOption_update_defaultAttachFolder", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@id", 1);
                            cmd.Parameters.AddWithValue("@patch", DefaultFolder);
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    label1.Text = DefaultFolder;
                    MessageBox.Show("عملیات مورد نظر با موفقیت انجام شد", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
