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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
             dataGridView1.DataSource = GetAllData();
             dataGridView1.Columns[0].Width = 35;
             dataGridView1.Columns[1].Width = 80;
             dataGridView1.Columns[2].Width = 80;
             dataGridView1.Columns[4].Width = 80;
             dataGridView1.Columns[5].Width = 80;
             dataGridView1.Columns[6].Width = 80;
             dataGridView1.Columns[7].Width = 80;
             dataGridView1.Columns[8].Width = 80;
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
                    nl.LetterID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                    nl.IsUpdate = true;
                    nl.ShowDialog();
                    dataGridView1.DataSource = GetAllData();
                }
                catch (Exception)
                {


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
                    at.LetterID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                    at.Shomare_Name = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    at.ShowDialog();
                }
                catch (Exception)
                {


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
                report["@id_type"] = 2;
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
                report["@id_type"] = 1;
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
                a = MessageBox.Show("هستید ؟" + " " + dataGridView1.SelectedRows[0].Cells[1].Value.ToString() + " " + "آیا مایل به حذف نامه","حذف نامه",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
                        {
                            using (SqlCommand cmd = new SqlCommand("delete from tblBayegani where id=" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), con))
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

        }
    }
}
