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
using System.Configuration;


namespace KETHUCHPHSK
{
    public partial class BOOK : Form
    {
        public BOOK()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            load_dgvSach();
            cboMon();
        }
        public void cboMon()
        {
            
            string constr = ConfigurationManager.ConnectionStrings["db_qls"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Select sMaMon from Subject", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter adp = new SqlDataAdapter("Select sMaMon from Subject", cnn))
                    {
                        DataTable db = new DataTable();
                        adp.Fill(db);
                        cbomaMon.DataSource = db;
                        cbomaMon.DisplayMember = "sMaMon";
                    }
                }
            }
        }
        void load_dgvSach()
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qls"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Select *from BOOK", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        DataTable db = new DataTable();
                        adp.Fill(db);
                        dataGridView1.DataSource = db;
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qls"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "insert_Book";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add("@masach", SqlDbType.Char).Value = txtmasach.Text;
                    cmd.Parameters.Add("@tensach", SqlDbType.NVarChar).Value = txttieude.Text;
                    cmd.Parameters.Add("@fGia", SqlDbType.Float).Value = txtgia.Text;
                    cmd.Parameters.Add("@sMaMon", SqlDbType.Char).Value = (cbomaMon.Text);
                    cmd.Parameters.Add("@sMoTa", SqlDbType.NVarChar).Value = txtmoTa.Text;
                    try
                    {
                        var tieude = new List<string>() { "Hướng đối tượng", "Toán rời rạc", "Giải bài tập tiếng anh", "Lộ trình HSK1", "Tiếng Nga cho người mới", "Học học học", "abcxyz" } ;
                        foreach(string TD in tieude)
                        {
                            if(TD != txttieude.Text)
                            {
                                cnn.Open();
                                cmd.ExecuteNonQuery();
                                cnn.Close();
                                load_dgvSach();
                            }
                            else
                            {
                                MessageBox.Show("khong them duọcw");
                            }
                        }
                           
                    }
                    catch
                    {
                        MessageBox.Show("Can't insert duplicate key", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                    }
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintBook prB = new PrintBook();
            prB.Show();
        }
    }
}
