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

namespace MySQL_Solo_Project
{   
    public partial class frmTicketAdminAssign : Form
    {
        private string ticketnumber, Problem, Status, Details, createdBy, date, assignedBy;
        public frmTicketAdminAssign(string ticketnumber, string Problem, string Status, string Details, string createdBy, string date, string assignedBy)
        {
            InitializeComponent();
            this.ticketnumber = ticketnumber;
            this.Problem = Problem;
            this.Status = Status;
            this.Details = Details;
            this.createdBy = createdBy;
            this.date = date;
            this.assignedBy = assignedBy;
        }
        Class1 assignTicket = new Class1("DESKTOP-108SV7E\\SQLEXPRESS", "CS311-2022", "itc311", "itc311");
        private void validateAssign()
        {
            if (cmbTechnical.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbTechnical, "Technical is Required");
            }
            else
            {
                errorProvider1.SetError(cmbTechnical, "");
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
        private void frmTicketAdminAssign_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_CS311_2022DataSet.tblAccounts' table. You can move, or remove it, as needed.
            this.tblAccountsTableAdapter.Fill(this._CS311_2022DataSet.tblAccounts);
            TxtTicket.Text = ticketnumber;
            txtProblem.Text = Problem;
            txtDetails.Text = Details;
            txtCreatedBy.Text = createdBy;
            txtDateCreated.Text = date;
            try
            {
                DataTable dt = assignTicket.GetData("SELECT * FROM tblAccounts WHERE userType = 'TECHNICAL'");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string technicals;
                        technicals = dt.Rows[i]["username"].ToString();
                        cmbTechnical.Items.Add(technicals);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Assign Form Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAssign_Click(object sender, EventArgs e)
        {
            validateAssign();
            ErrorCounts();
            if(errorCount == 0)
            {
                if(Status == "PENDING")
                {
                    try
                    {
                        DialogResult answer = MessageBox.Show("Are you sure you want to assign this Ticket?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (answer == DialogResult.Yes)
                        {
                            assignTicket.executeSQL("UPDATE tblTicketUser SET Status = 'ON-GOING', assignedTo = '" + cmbTechnical.Text.ToUpper() + "', assignedBy = '" + assignedBy.ToUpper() +
                                "', dateAssigned = '" + DateTime.Now.ToString("dd-MM-yyyy") + "' WHERE TicketNumber = '" + TxtTicket.Text + "' ");
                            if (assignTicket.rowAffected > 0)
                            {
                                MessageBox.Show("Ticket Assigned!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                frmTicketAdmin newFrm = (frmTicketAdmin)Application.OpenForms["frmTicketAdmin"];
                                newFrm.dgvLoad();
                                this.Close();
                            }
                        }
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show(error.Message, "Error on Assigned Ticket!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Status must be pending!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }       
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
