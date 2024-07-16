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
    public partial class frmTicketTechnical : Form
    {
        private string username;
        public frmTicketTechnical(string username)
        {
            InitializeComponent();
            this.username = username;
        }
        Class1 ticket = new Class1("DESKTOP-108SV7E\\SQLEXPRESS", "CS311-2022", "itc311", "itc311");
        private void frmTicketTechnical_Load(object sender, EventArgs e)
        {
            dgvLoad();
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[6].Visible = false;
        }
        public void dgvLoad()
        {
            try
            {
                DataTable dt = ticket.GetData("SELECT ticketNumber, Problem, Details, Date, Time, Status, createdBy FROM tblTicketUser WHERE assignedTo = '" +
                    username + "' ORDER BY ticketNumber DESC");
                dataGridView1.DataSource = dt;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on log in button", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int rowSelected;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            rowSelected = dataGridView1.CurrentCell.RowIndex;
        }
        private void btnComplete_Click(object sender, EventArgs e)
        {
            string status = dataGridView1.Rows[rowSelected].Cells[5].Value.ToString();
            if(status == "ON-GOING")
            {
                string TicketNumber, Problem, Details, createdBy, date;
                TicketNumber = dataGridView1.Rows[rowSelected].Cells[0].Value.ToString();
                Problem = dataGridView1.Rows[rowSelected].Cells[1].Value.ToString();
                Details = dataGridView1.Rows[rowSelected].Cells[2].Value.ToString();
                date = dataGridView1.Rows[rowSelected].Cells[3].Value.ToString();
                createdBy = dataGridView1.Rows[rowSelected].Cells[6].Value.ToString();
                frmTicketTechnicalComplete updateTicket = new frmTicketTechnicalComplete(TicketNumber, Problem, Details, createdBy, date, username);
                updateTicket.Show();
            }
            else
            {
                MessageBox.Show("Status must be ON-GOING!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = ticket.GetData("SELECT ticketNumber, Problem, Details, Date, Time, Status, createdBy FROM tblTicketUser WHERE assignedTo = '" + username +
                    "'AND(ticketNumber LIKE '%" + txtSearch.Text + "%' OR Problem LIKE '%" + txtSearch.Text + "%' OR Status LIKE '%" + txtSearch.Text + "%' ) ORDER BY ticketNumber ");
                dataGridView1.DataSource = dt;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Search in button", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
