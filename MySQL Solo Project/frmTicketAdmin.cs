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
    public partial class frmTicketAdmin : Form
    {
        private string username;
        public frmTicketAdmin(string username)
        {
            InitializeComponent();
            this.username = username;
        }
        public void dgvLoad()
        {
            try
            {
                DataTable dt = ticket.GetData("SELECT ticketNumber, Problem, Date, Time, Status, Details, createdBy, completedBy, dateCompleted FROM tblTicketUser WHERE ticketNumber <> '" + username + "' ORDER BY ticketNumber DESC ");
                dataGridView1.DataSource = dt;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on log in button", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        Class1 ticket = new Class1("DESKTOP-108SV7E\\SQLEXPRESS", "CS311-2022", "itc311", "itc311");
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = ticket.GetData("SELECT ticketNumber, Problem, Details, Status, Date, Time, createdBy, completedBy, dateCompleted FROM tblTicketUser WHERE ticketNumber <> '" + username +
                    "'AND(ticketNumber LIKE '%" + txtSearch.Text + "%' OR Problem LIKE '%" + txtSearch.Text + "%' OR Status LIKE '%" + txtSearch.Text + "%' ) ORDER BY ticketNumber ");
                dataGridView1.DataSource = dt;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Search in button", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmTicketAdmin_Load(object sender, EventArgs e)
        {
            dgvLoad();
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
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
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string Status;
            Status = dataGridView1.Rows[rowSelected].Cells[4].Value.ToString();
            if (Status == "WAITING FOR APPROVAL")
            {
                try
                {
                    DialogResult answer = MessageBox.Show("Are you sure you want to delete this data?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                        ticket.executeSQL("DELETE FROM tblTicketUser WHERE ticketNumber ='" + dataGridView1.Rows[rowSelected].Cells[0].Value.ToString() + "' ");
                        if (ticket.rowAffected > 0)
                        {
                            MessageBox.Show("Ticket deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ticket.executeSQL("INSERT INTO tblDelelteLogsTicketUser VALUES('" + DateTime.Now.ToString("MM/dd/yyyy ") + "', '" + DateTime.Now.ToLongTimeString() + "', 'Ticket Management', '" + dataGridView1.Rows[rowSelected].Cells[0].Value.ToString() + "', '" + username + "')");
                            frmTicketAdmin newFrm = (frmTicketAdmin)Application.OpenForms["frmTicketAdmin"];
                            newFrm.dgvLoad();
                        }
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error on Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Status is Not PENDING!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }

        private void btnAssign_Click(object sender, EventArgs e)
        {
                string TicketNumber, Problem, Status, Details, createdBy, date;
                TicketNumber = dataGridView1.Rows[rowSelected].Cells[0].Value.ToString();
                Problem = dataGridView1.Rows[rowSelected].Cells[1].Value.ToString();
                Status = dataGridView1.Rows[rowSelected].Cells[4].Value.ToString();
                Details = dataGridView1.Rows[rowSelected].Cells[5].Value.ToString();
                createdBy = dataGridView1.Rows[rowSelected].Cells[6].Value.ToString();
                date = dataGridView1.Rows[rowSelected].Cells[2].Value.ToString();
                frmTicketAdminAssign updateTicket = new frmTicketAdminAssign(TicketNumber, Problem, Status, Details, createdBy, date, username);
                updateTicket.Show();
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            string Status = dataGridView1.Rows[rowSelected].Cells[4].Value.ToString();
            if (Status == "WAITING FOR APPROVAL")
            {
                string TicketNumber, Problem, Details, completedBy, dateCompleted;
                TicketNumber = dataGridView1.Rows[rowSelected].Cells[0].Value.ToString();
                Problem = dataGridView1.Rows[rowSelected].Cells[1].Value.ToString();
                Details = dataGridView1.Rows[rowSelected].Cells[5].Value.ToString();
                completedBy = dataGridView1.Rows[rowSelected].Cells[7].Value.ToString();
                dateCompleted = dataGridView1.Rows[rowSelected].Cells[8].Value.ToString();
                frmTicketAdminApprove updateTicket = new frmTicketAdminApprove(TicketNumber, Problem, Details, completedBy, dateCompleted, username);
                updateTicket.Show();
            }
            else
            {
                MessageBox.Show("Status must be WAITING FOR APPROVAL!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            frmTicketAdmin_Load(sender, e);
        }
    }
}
