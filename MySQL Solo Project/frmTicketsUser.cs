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
    public partial class frmTicketsUser : Form
    {
        private string username;
        public frmTicketsUser(string username)
        {
            InitializeComponent();
            this.username = username;
        }
        public void dgvLoad()
        {
            try
            {
                DataTable dt = ticket.GetData("SELECT ticketNumber, Problem, Details, Status, Date, Time, createdBy FROM tblTicketUser WHERE ticketNumber <> '" + username + "' ORDER BY ticketNumber DESC");
                dataGridView1.DataSource = dt;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Add Ticket", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        Class1 ticket = new Class1("DESKTOP-108SV7E\\SQLEXPRESS", "CS311-2022", "itc311", "itc311");

        private void frmTickets_Load(object sender, EventArgs e)
        {
            dgvLoad();
            dataGridView1.Columns[2].Visible = false;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = ticket.GetData("SELECT ticketNumber, Problem, Details, Status, Date, Time, createdBy FROM tblTicketUser WHERE ticketNumber <> '" + username +
                    "'AND(ticketNumber LIKE '%" + txtSearch.Text + "%' OR Problem LIKE '%" + txtSearch.Text + "%' OR Status LIKE '%" + txtSearch.Text + "%' ) ORDER BY ticketNumber ");
                dataGridView1.DataSource = dt;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Search in button", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            frmTickets_Load(sender, e);
        }   
        private int rowSelected;
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string editTicketNumber, editProblem, editDetails, editStatus;
            editTicketNumber = dataGridView1.Rows[rowSelected].Cells[0].Value.ToString();
            editProblem = dataGridView1.Rows[rowSelected].Cells[1].Value.ToString();
            editDetails = dataGridView1.Rows[rowSelected].Cells[2].Value.ToString();
            editStatus = dataGridView1.Rows[rowSelected].Cells[3].Value.ToString();
            frmUpdateTicketUser updateTicket = new frmUpdateTicketUser(editTicketNumber, editProblem, editDetails, editStatus, username);
            updateTicket.Show();
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            frmAddTicketUser createTicket = new frmAddTicketUser(username);
            createTicket.Show();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string Status;
            Status = dataGridView1.Rows[rowSelected].Cells[3].Value.ToString();
            if (Status == "WAITING FOR APPROVAL")
            {
                try
                {
                    DialogResult answer = MessageBox.Show("Are you sure you want to delete this data?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                        ticket.executeSQL("DELETE FROM tblTicket WHERE ticketNumber ='" + dataGridView1.Rows[rowSelected].Cells[0].Value.ToString() + "' ");
                        if (ticket.rowAffected > 0)
                        {
                            MessageBox.Show("Ticket deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ticket.executeSQL("INSERT INTO tblDeleteTicket VALUES('" + DateTime.Now.ToString("MM/dd/yyyy ") + "', '" + DateTime.Now.ToLongTimeString() + "', 'Ticket Management', '" + dataGridView1.Rows[rowSelected].Cells[0].Value.ToString() + "', '" + username + "')");
                            frmTicketsUser newFrm = (frmTicketsUser)Application.OpenForms["frmTicketsUser"];
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            rowSelected = dataGridView1.CurrentCell.RowIndex;
        }
    }
}
