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
    public partial class frmUpdateTicketUser : Form
    {
        private string ticketNumber, Problem, Details, Status, editBy;
        public frmUpdateTicketUser(string ticketNumber, string Problem, string Details, string Status, string editBy)
        {
            InitializeComponent();
            this.ticketNumber = ticketNumber;
            this.Problem = Problem;
            this.Details = Details;
            this.Status = Status;
            this.editBy = editBy;
        }
        Class1 updateTicket = new Class1("DESKTOP-108SV7E\\SQLEXPRESS", "CS311-2022", "itc311", "admin");
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDetails.Clear();
        }
        private void validateProblem()
        {
            if (cmbProblem.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbProblem, "Problem is Required");
            }
            else
            {
                errorProvider1.SetError(cmbProblem, "");
            }
        }    
        private void validateDetails()
        {
            if (txtDetails.Text == "")
            {
                errorProvider1.SetError(txtDetails, "Details is Required");
            }
            else
            {
                errorProvider1.SetError(txtDetails, "");

            }
        }
        private int errorCount;    
        public void ErrorCounts()
        {
            errorCount = 0;
            foreach (Control c in errorProvider1.ContainerControl.Controls)
            {
                if (errorProvider1.GetError(c) != "")
                {
                    errorCount++;
                }
            }
        }
        private void frmUpdateTicketUser_Load(object sender, EventArgs e)
        {
            TxtTicket.Text = ticketNumber;
            txtDetails.Text = Details;
            if (Problem == "HARDWARE")
            {
                cmbProblem.SelectedIndex = 0;
            }
            else if (Problem == "SOFTWARE")
            {
                cmbProblem.SelectedIndex = 1;
            }
            else
            {
                cmbProblem.SelectedIndex = 2;
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            validateProblem();
            validateDetails();
            ErrorCounts();
            if (errorCount == 0)
            {
                if (Status == "PENDING")
                {
                    try
                    {
                        DialogResult answer = MessageBox.Show("Are you sure you want to save this data?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (answer == DialogResult.Yes)
                        {
                            updateTicket.executeSQL("UPDATE tblTicketUser SET Details = '" + txtDetails.Text + "' , Problem = '" + cmbProblem.Text.ToUpper() +
                            "' , lastUpdateDate = '" + DateTime.Now.ToString("dd-MM-yyyy") + "' WHERE ticketNumber = '" + TxtTicket.Text + "'");
                            if (updateTicket.rowAffected > 0)
                            {
                                MessageBox.Show("Ticket Updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                frmTicketsUser newFrm = (frmTicketsUser)Application.OpenForms["frmTicketsUser"];
                                newFrm.dgvLoad();
                                this.Close();
                            }
                        }
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show(error.Message, "Error on Update new Ticket", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Status is Not PENDING!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
