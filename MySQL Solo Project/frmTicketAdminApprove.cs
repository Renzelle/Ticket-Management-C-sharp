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
    public partial class frmTicketAdminApprove : Form
    {
        private string ticketnumber, Problem, Details, completedBy, dateCompleted, approvedBy;  
        public frmTicketAdminApprove(string ticketnumber, string Problem, string Details, string completedBy, string dateCompleted, string approvedBy)
        {
            InitializeComponent();
            this.ticketnumber = ticketnumber;
            this.Problem = Problem;
            this.Details = Details;
            this.completedBy = completedBy;
            this.dateCompleted = dateCompleted;
            this.approvedBy = approvedBy;
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        Class1 approveTicket = new Class1("DESKTOP-108SV7E\\SQLEXPRESS", "CS311-2022", "itc311", "itc311");
        private void frmTicketAdminApprove_Load(object sender, EventArgs e)
        {
            TxtTicket.Text = ticketnumber;
            txtProblem.Text = Problem;
            txtDetails.Text = Details;
            txtCompletedBy.Text = completedBy;
            txtDateCompleted.Text = dateCompleted;
        }
        private void btnApprove_Click(object sender, EventArgs e)
        {          
                try
                {
                    DialogResult answer = MessageBox.Show("Are you sure you want to approve this Ticket?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                        approveTicket.executeSQL("UPDATE tblTicketUser SET Status = 'APPROVED', approvedBy = '" + approvedBy.ToUpper() +
                            "', dateApproved = '" + DateTime.Now.ToString("dd-MM-yyyy") + "' WHERE TicketNumber = '" + TxtTicket.Text + "' ");
                        if (approveTicket.rowAffected > 0)
                        {
                            MessageBox.Show("Ticket Approved!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frmTicketAdmin newFrm = (frmTicketAdmin)Application.OpenForms["frmTicketAdmin"];
                            newFrm.dgvLoad();
                            this.Close();
                        }
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error on Approve Ticket!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }
    }
}
