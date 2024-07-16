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
    public partial class frmAddEquipment : Form
    {
        private string createdBy;
        public frmAddEquipment(string createdBy)
        {
            InitializeComponent();
            this.createdBy = createdBy;
        }
        Class1 addNewEquipment = new Class1("DESKTOP-108SV7E\\SQLEXPRESS", "CS311-2022", "itc311", "admin");
        private void validateAsset()
        {
            DataTable dt = addNewEquipment.GetData("SELECT * FROM tblEquipments WHERE assetNumber = '" + TxtAsset.Text + "'");
            if (TxtAsset.Text == "")
            {
                errorProvider1.SetError(TxtAsset, "Asset Number is Required");
            }

            else if (dt.Rows.Count > 0)
            {
                errorProvider1.SetError(TxtAsset, "Asset Number should be Unique");
            }
            else
            {
                errorProvider1.SetError(TxtAsset, "");
            }
        }
        private void validateSerial()
        {
            DataTable dt = addNewEquipment.GetData("SELECT * FROM tblEquipments WHERE serialNumber = '" + TxtSerial.Text + "'");
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
            if (cmbType.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbType, "Type is Required");
            }
            else
            {
                errorProvider1.SetError(cmbType, "");
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
            if (cmbBranch.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbBranch, "Branch is Required");
            }
            else
            {
                errorProvider1.SetError(cmbBranch, "");
            }
        }
        private void validateDescription()
        {
            if (txtDescrip.Text == "")
            {
                errorProvider1.SetError(txtDescrip, "Description is Required");
            }
            else
            {
                errorProvider1.SetError(txtDescrip, "");
            }
        }
        private void validateDepartment()
        {
            if (cmbDepartment.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbDepartment, "Department is Required");
            }
            else
            {
                errorProvider1.SetError(cmbDepartment, "");
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
        private void txtModel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
                errorProvider1.SetError(txtModel, "Input must be a number only");
            }
            else if(txtModel.TextLength > 3 && e.KeyChar != (char)8)
            {
                e.Handled = true;
                errorProvider1.SetError(txtModel, "Input must be 4 characters only");
            }

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            validateAsset();
            validateSerial();
            validateType();
            validateManufacture();
            validateYear();
            validateBranch();
            validateDescription();
            validateDepartment();
            ErrorCounts();
            if(errorCount == 0)
            {
                try
                {                  
                    DialogResult answer = MessageBox.Show("Are you sure you want to save this data?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                        addNewEquipment.executeSQL("INSERT INTO tblEquipments (assetNumber, serialNumber, Type, Manufacturer, yearModel, Description, Branch, Department, Status, createdBy, dateCreated) VALUES ('" + 
                            TxtAsset.Text + "','" + TxtSerial.Text + "','" + cmbType.Text.ToUpper() + "','" + txtManufacture.Text + "','" + txtModel.Text + "','" + txtDescrip.Text + "','" + cmbBranch.Text.ToUpper() + "','" + cmbDepartment.Text.ToUpper() + "','WORKING','" + createdBy + "','" + DateTime.Now.ToString("MM/dd/yyyy ") + "')");
                        if (addNewEquipment.rowAffected > 0)
                        {
                            MessageBox.Show("New Equipment Added!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            TxtAsset.Clear();
            TxtSerial.Clear();
            txtModel.Clear();
            txtDescrip.Clear();
            txtManufacture.Clear();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddEquipment_Load(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int num = rnd.Next(1, 1000);
            int num2 = rnd.Next(1000, 5000);           
            TxtAsset.Text = "22 - " + num.ToString();
            TxtSerial.Text = num2.ToString();
        }
    }
}
