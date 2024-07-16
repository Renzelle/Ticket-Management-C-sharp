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
    public partial class frmAddTicketUser : Form
    {
        private string createdBy;
        public frmAddTicketUser(string createdBy)
        {
            InitializeComponent();
            this.createdBy = createdBy;
        }
        Class1 addNewTicket = new Class1("DESKTOP-108SV7E\\SQLEXPRESS", "CS311-2022", "itc311", "admin");
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            validateProblem();
            validateDetails();
            ErrorCounts();
            if(errorCount == 0)
            {
                try
                {
                    DialogResult answer = MessageBox.Show("Are you sure you want to save this data?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                        addNewTicket.executeSQL("INSERT INTO tblTicketUser (ticketNumber, Problem, Details, Status, Date, Time, createdBy) VALUES ('" +
                            TxtTicket.Text + "','" + cmbProblem.Text.ToUpper() + "','" + txtDetails.Text + "','PENDING','" + DateTime.Now.ToString("MM/dd/yyyy ") + "','" + DateTime.Now.ToString("HH:mm:ss") + "','" + createdBy.ToUpper() + "')");
                        if (addNewTicket.rowAffected > 0)
                        {
                            MessageBox.Show("New Ticket Added!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frmTicketsUser newFrm = (frmTicketsUser)Application.OpenForms["frmTicketsUser"];
                            newFrm.dgvLoad();
                            this.Close();
                        }
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error on Add new Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmAddTicketUser_Load(object sender, EventArgs e)
        {
            TxtTicket.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}
