using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySQL_Solo_Project
{
    public partial class tblDeleteLogsTicket : Form
    {
        private string username;
        public tblDeleteLogsTicket(string username)
        {
            InitializeComponent();
            this.username = username;
        }
        Class1 deleteTicket = new Class1("DESKTOP-108SV7E\\SQLEXPRESS", "CS311-2022", "itc311", "admin");
        private void tblDeleteLogsTicket_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = deleteTicket.GetData("SELECT date, time, module, ID, deletedFile FROM tblDelelteLogsTicketUser WHERE date <> '" + username + "' ORDER BY date DESC ");
                dataGridView1.DataSource = dt;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on table account", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
