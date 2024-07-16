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
    public partial class frmUpdateEquipment : Form
    {
        private string assetNumber, serialNumber, Type, Manufacture, yearModel, Branch, Description, Department, Status, editBy;
        public frmUpdateEquipment(string assetNumber, string serialNumber, string Type, string Manufacture, string yearModel, string Description, string Branch, string Department, string Status, string editBy)
        {
            InitializeComponent();
            this.assetNumber = assetNumber;
            this.serialNumber = serialNumber;
            this.Type = Type;
            this.Manufacture = Manufacture;
            this.yearModel = yearModel;
            this.Description = Description;
            this.Branch = Branch;
            this.Department = Department;
            this.Status = Status;
            this.editBy = editBy;

        }
        Class1 updateEquipment = new Class1("DESKTOP-108SV7E\\SQLEXPRESS", "CS311-2022", "itc311", "admin");
        private void validateSerial()
        {
            DataTable dt = updateEquipment.GetData("SELECT * FROM tblEquipments WHERE serialNumber = '" + TxtSerial.Text + "'");
            if (TxtSerial.Text == "")
            {
                errorProvider1.SetError(TxtSerial, "Serial Number is Required");
            }

            else if (dt.Rows.Count > 0)
            {
                errorProvider1.SetError(TxtSerial, "Serial Number should be Unique");
            }
            else
            {
                errorProvider1.SetError(TxtSerial, "");
            }
        }
        private void validateType()
        {
            if (cmbTypes.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbTypes, "Type is Required");
            }
            else
            {
                errorProvider1.SetError(cmbTypes, "");
            }
        }
        private void validateManufacture()
        {
            if (txtManufacture.Text == "")
            {
                errorProvider1.SetError(txtManufacture, "Manufacturer is Required");
            }
            else
            {
                errorProvider1.SetError(txtManufacture, "");
            }
        }
        private void validateYear()
        {
            if (txtModel.Text == "")
            {
                errorProvider1.SetError(txtModel, "Year Model is Required");
            }
            else if (int.Parse(txtModel.Text) < 1990 || int.Parse(txtModel.Text) > 3000)
            {
                errorProvider1.SetError(txtModel, "Input should be from 1990 to 3000 only");
            }
            else
            {
                errorProvider1.SetError(txtModel, "");
            }
        }
        private void validateBranch()
        {
            if (cmbBranches.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbBranches, "Branch is Required");
            }
            else
            {
                errorProvider1.SetError(cmbBranches, "");
            }
        }
        private void validateDepartment()
        {
            if (cmbDepartments.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbDepartments, "Department is Required");
            }
            else
            {
                errorProvider1.SetError(cmbDepartments, "");
            }
        }
        private void validateDescription()
        {
            if (txtDesctiptions.Text == "")
            {
                errorProvider1.SetError(txtDesctiptions, "Description is Required");
            }
        }

        private void txtModel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
                errorProvider1.SetError(txtModel, "Input must be a number only");
            }
            else if (txtModel.TextLength > 3 && e.KeyChar != (char)8)
            {
                e.Handled = true;
                errorProvider1.SetError(txtModel, "Input must be 4 characters only");
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
        public void CmbBranch()
        {
            if (Branch == "JUAN SUMULONG CAMPUS")
            {
                cmbBranches.SelectedIndex = 0;
            }
            else if (Branch == "ANDRES BONIFACIO CAMPUS")
            {
                cmbBranches.SelectedIndex = 1;
            }
            else if (Branch == "PLARIDEL CAMPUS")
            {
                cmbBranches.SelectedIndex = 2;
            }
            else if (Branch == "JOSE ABAD SANTOS CAMPUS")
            {
                cmbBranches.SelectedIndex = 3;
            }
            else if (Branch == "APOLINARIO MABINI CAMPUS")
            {
                cmbBranches.SelectedIndex = 4;
            }
            else if (Branch == "ELISA ESGUERRA CAMPUS")
            {
                cmbBranches.SelectedIndex = 5;
            }
            else if (Branch == "JOSE RIZAL CAMPUS")
            {
                cmbBranches.SelectedIndex = 6;
            }
            else
            {
                cmbBranches.SelectedIndex = 7;
            }
        }
        public void CmbType()
        {
            if (Type == "MONITOR")
            {
                cmbTypes.SelectedIndex = 0;
            }
            else if (Type == "CPU")
            {
                cmbTypes.SelectedIndex = 1;
            }
            else if (Type == "KEYBOARD")
            {
                cmbTypes.SelectedIndex = 2;
            }
            else if (Type == "MOUSE")
            {
                cmbTypes.SelectedIndex = 3;
            }
            else if (Type == "AVR")
            {
                cmbTypes.SelectedIndex = 4;
            }
            else if (Type == "MAC")
            {
                cmbTypes.SelectedIndex = 5;
            }
            else if (Type == "PRINTER ")
            {
                cmbTypes.SelectedIndex = 6;
            }
            else
            {
                cmbTypes.SelectedIndex = 7;
            }
        }
        public void CmbDepartment()
        {
            if (Department == "EDUCATION")
            {
                cmbDepartments.SelectedIndex = 0;
            }
            else if (Department == "NURSING")
            {
                cmbDepartments.SelectedIndex = 1;
            }
            else if (Department == "BUSINESS")
            {
                cmbDepartments.SelectedIndex = 2;
            }
            else if (Department == "LIBERAL ARTS, SOCIAL SCIENCES, AND HUMANITIES")
            {
                cmbDepartments.SelectedIndex = 3;
            }
            else if (Department == "BUSINESS, TECHNOLOGY & MANAGEMENT")
            {
                cmbDepartments.SelectedIndex = 4;
            }
            else if (Department == "BUSINESS ADMINISTRATION")
            {
                cmbDepartments.SelectedIndex = 5;
            }
            else if (Department == "SCIENCE AND MATHEMATICS")
            {
                cmbDepartments.SelectedIndex = 6;
            }
            else if (Department == "EDUCATION, INDUSTRIAL ARTS, SKILLS TRAINING AND CONTINUING EDUCATION")
            {
                cmbDepartments.SelectedIndex = 7;
            }
            else if (Department == "HOSPITALITY & TOURISM MGT")
            {
                cmbDepartments.SelectedIndex = 8;
            }
            else if (Department == "INSTITUTE OF ACCOUNTANCY")
            {
                cmbDepartments.SelectedIndex = 9;
            }
            else if (Department == "RADIOLOGIC TECHNOLOGY")
            {
                cmbDepartments.SelectedIndex = 10;
            }
            else if (Department == "COMPUTER SCIENCE")
            {
                cmbDepartments.SelectedIndex = 11;
            }
            else if (Department == "MEDICAL LABORATORY SCIENCE")
            {
                cmbDepartments.SelectedIndex = 12;
            }
            else if (Department == "PHARMACY")
            {
                cmbDepartments.SelectedIndex = 13;
            }
            else if (Department == "PHYSICAL THERAPY")
            {
                cmbDepartments.SelectedIndex = 14;
            }
            else if (Department == "MIDWIFERY")
            {
                cmbDepartments.SelectedIndex = 15;
            }
            else if (Department == "BS PSYCHOLOGY")
            {
                cmbDepartments.SelectedIndex = 16;
            }
            else if (Department == "CRIMINAL JUSTICE")
            {
                cmbDepartments.SelectedIndex = 17;
            }
            else 
            {
                cmbDepartments.SelectedIndex = 18;
            }
        }
        private void frmUpdateEquipment_Load(object sender, EventArgs e)
        {
            TxtAsset.Text = assetNumber;
            TxtSerial.Text = serialNumber;
            txtManufacture.Text = Manufacture;
            txtDesctiptions.Text = Description;
            txtModel.Text = yearModel;
            CmbBranch();
            CmbType();
            CmbDepartment();
            if (Status == "WORKING")
            {
                rbWorking.Checked = true;
            }
            else if ( Status == "PENDING")
            {
                rbPending.Checked = true;
            }
            else
            {
                rbRetire.Checked = true;
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            validateSerial();
            validateType();
            validateManufacture();
            validateYear();
            validateBranch();
            validateDepartment();
            validateDescription();
            ErrorCounts();
            if (errorCount == 0)
            {
                string newStatus;
                if (rbWorking.Checked)
                {
                    newStatus = rbWorking.Text.ToUpper();
                }
                else if (rbPending.Checked)
                {
                    newStatus = rbPending.Text.ToUpper();
                }
                else
                {
                    newStatus = rbRetire.Text.ToUpper();
                }
                try
                {
                    DialogResult answer = MessageBox.Show("Are you sure you want to save this data?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                        updateEquipment.executeSQL("UPDATE tblEquipments SET serialNumber = '" + TxtSerial.Text + "' , Type = '" + cmbTypes.Text.ToUpper() +
                        "' , Manufacturer = '" + txtManufacture.Text + "' , yearModel = '" + txtModel.Text + "' , Description = '" + txtDesctiptions.Text + "' , Branch = '" + cmbBranches.Text.ToUpper() +
                        "' , Department = '" + cmbDepartments.Text.ToUpper() + "' , Status = '" + newStatus + "' , lastUpdatedBy = '" + editBy + "' , lastUpdatedDate = '" + DateTime.Now.ToString("dd-MM-yyyy") +
                        "' WHERE assetNumber = '" + TxtAsset.Text + "'");
                        if (updateEquipment.rowAffected > 0)
                        {
                            MessageBox.Show("Equipment Updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frmEquipments newFrm = (frmEquipments)Application.OpenForms["frmEquipments"];
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
        private void btnClear_Click(object sender, EventArgs e)
        {
            TxtAsset.Clear();
            TxtSerial.Clear();
            txtDesctiptions.Clear();
            txtManufacture.Clear();
            txtModel.Clear();
        }
    }
}
