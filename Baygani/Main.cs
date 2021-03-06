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
using Baygani.Screens;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;

namespace Baygani
{
 
    
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (ISAdmin())
            {
                designReports.Enabled = true;
                اطلاعاتپایهToolStripMenuItem.Enabled = true;
            }
            else
            { 
                designReports.Enabled = false;
                اطلاعاتپایهToolStripMenuItem.Enabled = false;
            }
            dataGridView1.DataSource = GetAllData();
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["radif"].Width = 35;
            dataGridView1.Columns["شماره نامه"].Width = 80;
            dataGridView1.Columns["تاریخ نامه"].Width = 70;
            dataGridView1.Columns["عنوان نامه"].Width = 100;
            dataGridView1.Columns["فرستنده نامه"].Width = 80;
            dataGridView1.Columns["واحد فرستنده نامه"].Width = 80;
            dataGridView1.Columns["گیرنده نامه"].Width = 80;
            dataGridView1.Columns["واحد گیرنده نامه"].Width = 80;
            dataGridView1.Columns["نوع نامه"].Width = 80;
            labelDate.Text = GetDate.Today();
            label6.Text = LoginInfo.UserID;
        }

        private bool ISAdmin()
        {
            if (LoginInfo.UserID == "Admin")
            {
                return true;
            }
            return false;
        }

        private DataTable GetAllData()
        {
            DataTable dtrecord = new DataTable();
            using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
            {
                using (SqlCommand cmd=new SqlCommand("ups_SelectAllLetterForDataGridView", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    dtrecord.Load(sdr);
                }
            }
            return dtrecord;
        }

        private void لیستاداراتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Edarat eds = new Edarat();
            eds.ShowDialog();
        }

        private void نامهجدیدToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            NewLetter nl = new NewLetter();
            nl.ShowDialog();
            dataGridView1.DataSource = GetAllData();
        }

        private void بروزرسانیToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetAllData();
        }


        private void تغییرToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    NewLetter nl = new NewLetter();
                    nl.LetterID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);
                    nl.IsUpdate = true;
                    nl.ShowDialog();
                    dataGridView1.DataSource = GetAllData();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private void لیستپیوستهاینامهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    Attachment at = new Attachment();
                    at.LetterID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);
                    at.Shomare_Name = dataGridView1.SelectedRows[0].Cells["شماره نامه"].Value.ToString();
                    at.ShowDialog();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void پوشهپیشفرضپیوستهاToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DefaultAttachFolder daf = new DefaultAttachFolder();
            daf.ShowDialog();
        }

        private void لیستواحدهاToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Vahedha v = new Vahedha();
            v.ShowDialog();
        }

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult a = new DialogResult();
            a = MessageBox.Show("آیا مایل به خروج از برنامه می باشید ؟", "خروج", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (a == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void گزارشکلینامههاToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (designReports.CheckState == CheckState.Checked)
            {
                StiReport report = new StiReport();
                report.Load("Report/ReportAll.mrt");
                ((StiSqlDatabase)report.Dictionary.Databases["MS SQL"]).ConnectionString = ApplicationSetting.ConnectionString();
                report.Compile();
                report.Design();
            }
            else
            {
                StiReport report = new StiReport();
                report.Load("Report/ReportAll.mrt");
                ((StiSqlDatabase)report.Dictionary.Databases["MS SQL"]).ConnectionString = ApplicationSetting.ConnectionString();
                report.Compile();
                report.ShowWithRibbonGUI();
            }
        }

        private void گزارشنامههایصادرهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (designReports.CheckState == CheckState.Checked)
            {
                StiReport report = new StiReport();
                report.Load("Report/ReportByType.mrt");
                ((StiSqlDatabase)report.Dictionary.Databases["MS SQL"]).ConnectionString = ApplicationSetting.ConnectionString();
                report.Compile();
                report.Design();
            }
            else
            {
                StiReport report = new StiReport();
                report.Load("Report/ReportByType.mrt");
                ((StiSqlDatabase)report.Dictionary.Databases["MS SQL"]).ConnectionString = ApplicationSetting.ConnectionString();
                report.Compile();
                report["@id_type"] = 1;
                report.ShowWithRibbonGUI();
            }
        }

        private void designReports_Click(object sender, EventArgs e)
        {
            if (designReports.CheckState == CheckState.Checked)
            {
                designReports.CheckState = CheckState.Unchecked;
            }
            else
            {
                designReports.CheckState = CheckState.Checked;
            }
        }

        private void گزارشنامههایواردهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (designReports.CheckState == CheckState.Checked)
            {
                StiReport report = new StiReport();
                report.Load("Report/ReportByType.mrt");
                ((StiSqlDatabase)report.Dictionary.Databases["MS SQL"]).ConnectionString = ApplicationSetting.ConnectionString();
                report.Compile();
                report.Design();
            }
            else
            {
                StiReport report = new StiReport();
                report.Load("Report/ReportByType.mrt");
                ((StiSqlDatabase)report.Dictionary.Databases["MS SQL"]).ConnectionString = ApplicationSetting.ConnectionString();
                report.Compile();
                report["@id_type"] = 2;
                report.ShowWithRibbonGUI();
            }
        }

        private void حذفنامهToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void حذفنامهToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                DialogResult a = new DialogResult();
                a = MessageBox.Show("هستید ؟" + " " + dataGridView1.SelectedRows[0].Cells["شماره نامه"].Value.ToString() + " " + "آیا مایل به حذف نامه","حذف نامه",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
                        {
                            using (SqlCommand cmd = new SqlCommand("delete from tblBayegani where id=" + dataGridView1.SelectedRows[0].Cells["id"].Value.ToString(), con))
                            {
                                con.Open();
                                cmd.ExecuteNonQuery();
                            }
                        }
                        MessageBox.Show("عملیات مورد نظر با موفقیت انجام شد", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView1.DataSource = GetAllData();
                    }
                    catch (Exception ex)
                    {
                       MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            if (radioButtonDateLetter.Checked)
            {
                if(textBox_FromDate_Search.Text !=string.Empty && textBox_ToDate_Search.Text != string.Empty)
                {
                    dataGridView1.DataSource = Filter_By_Type_Date(textBox_FromDate_Search.Text, textBox_ToDate_Search.Text);
                }
                else if(textBox_FromDate_Search.Text != string.Empty && textBox_ToDate_Search.Text == string.Empty)
                {
                    dataGridView1.DataSource = Filter_By_Type_Date(textBox_FromDate_Search.Text, "9999/99/99");
                }
                else if (textBox_FromDate_Search.Text == string.Empty && textBox_ToDate_Search.Text != string.Empty)
                {
                    dataGridView1.DataSource = Filter_By_Type_Date("0000/00/00", textBox_ToDate_Search.Text);
                }
            }
            else if (radioButtonTypeLetter.Checked)
            {
                if (radioButton_LetterSadere.Checked)
                {
                   dataGridView1.DataSource= Filter_By_Type_Sadere(2);
                }
                else
                {
                    dataGridView1.DataSource = Filter_By_Type_Sadere(1);
                }
            }
        }

        private DataTable Filter_By_Type_Date(string v1, string v2)
        {
            DataTable dtrecord = new DataTable();
            using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("ups_SelectAllLetterByDate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@date_From", v1);
                    cmd.Parameters.AddWithValue("@date_To", v2);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    dtrecord.Load(sdr);
                }
            }
            return dtrecord;
        }

        private DataTable Filter_By_Type_Sadere(int v)
        {
            DataTable dtrecord = new DataTable();
            using (SqlConnection con=new SqlConnection(ApplicationSetting.ConnectionString()))
            {
                using(SqlCommand cmd=new SqlCommand("ups_SelectAllLetterByType", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_type", v);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    dtrecord.Load(sdr);
                }
            }
            return dtrecord;
        }

        private void radioButtonLetter_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonLetter.Checked)
            {
                textBox_ShomareName_search.Enabled = true;
                textBox_TitleName_search.Enabled = true;
            }
            else
            {
                textBox_ShomareName_search.Enabled = false;
                textBox_TitleName_search.Enabled = false;
            }

        }

        private void radioButtonDateLetter_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDateLetter.Checked)
            {
                textBox_FromDate_Search.Enabled = true;
                textBox_ToDate_Search.Enabled = true;
            }
            else
            {
                textBox_FromDate_Search.Enabled = false;
                textBox_ToDate_Search.Enabled = false;
            }
        }

        private void radioButtonTypeLetter_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonTypeLetter.Checked)
            {
                radioButton_LetterSadere.Enabled = true;
                radioButton_LetterVarede.Enabled = true;
            }
            else
            {
                radioButton_LetterSadere.Enabled = false;
                radioButton_LetterVarede.Enabled = false;
            }
        }

        private void buttonUnFilter_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetAllData();
        }

        private void textBox_ShomareName_search_TextChanged(object sender, EventArgs e)
        {
            if (textBox_ShomareName_search.Text.Trim() == string.Empty)
            {
                dataGridView1.DataSource = GetAllData();
            }
            else
            {
                dataGridView1.DataSource = Filter_By_Shomare_name(textBox_ShomareName_search.Text.Trim());
            }
        }

        private DataTable Filter_By_Shomare_name(string v)
        {
            DataTable dtrecord = new DataTable();
            using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("ups_SelectAllLetterByShomare_Name", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Shomare_name", v);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    dtrecord.Load(sdr);
                }
            }
            return dtrecord;
        }

        private void textBox_TitleName_search_TextChanged(object sender, EventArgs e)
        {
            if (textBox_TitleName_search.Text.Trim() == string.Empty)
            {
                dataGridView1.DataSource = GetAllData();
            }
            else
            {
                dataGridView1.DataSource = Filter_By_Title_name(textBox_TitleName_search.Text.Trim());
            }
        }

        private DataTable Filter_By_Title_name(string v)
        {
            DataTable dtrecord = new DataTable();
            using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("ups_SelectAllLetterByTitle_Name", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@title", v);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    dtrecord.Load(sdr);
                }
            }
            return dtrecord;
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Cells["radif"].Value = e.RowIndex + 1;
        }


        private void دربارهسیستمToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About ab = new About();
            ab.ShowDialog();
        }

        private void قفلکردنسیستمToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Login lg = new Login();
            lg.Show();
        }

        private void کاربرانToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Users us = new Users();
            us.ShowDialog();
        }
    }
}
