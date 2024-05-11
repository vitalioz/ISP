using System;
using System.Windows.Forms;
using Core;

namespace Accounting
{
    public partial class frmContractDiscounts : Form
    {
        int iMode, iLastAktion;
        string sTemp = "";
        public frmContractDiscounts()
        {
            InitializeComponent();
        }

        private void frmContractDiscounts_Load(object sender, EventArgs e)
        {
            iLastAktion = 0;

            switch (iMode)
            {
                case 1:                  // 1 - Brokerage Discounts
                    grpBrokerage.Top = 12;
                    grpBrokerage.Left = 12;
                    grpBrokerage.Visible = true;
                    btnOK.Left = 145;
                    btnOK.Top = 246;
                    btnCancel.Left = 295;
                    btnCancel.Top = 246;
                    this.Width = 522;
                    this.Height = 320;
                    break;

                case 2:
                case 3:
                case 4:
                case 5:                                           // 2 - Advisory, 3 - Discret, 4 - Custody, 5 - DealAdvisory
                    grpAdvisory.Top = 12;
                    grpAdvisory.Left = 12;
                    grpAdvisory.Visible = true;
                    btnOK.Left = 130;
                    btnOK.Top = 172;
                    btnCancel.Left = 280;
                    btnCancel.Top = 172;
                    this.Width = 480;
                    this.Height = 256;
                    break;
                case 6:
                    grpFX.Top = 12;
                    grpFX.Left = 12;
                    grpFX.Visible = true;
                    btnOK.Left = 140;
                    btnOK.Top = 120;
                    btnCancel.Left = 292;
                    btnCancel.Top = 120;
                    this.Width = 546;
                    this.Height = 200;
                    break;
                case 11:                                         // 11 - Brokerage Multi
                    grpBrokerage_Multi.Top = 12;
                    grpBrokerage_Multi.Left = 12;
                    grpBrokerage_Multi.Visible = true;
                    btnOK.Left = 56;
                    btnOK.Top = 180;
                    btnCancel.Left = 208;
                    btnCancel.Top = 180;
                    this.Width = 386;
                    this.Height = 260;
                    break;
                case 21:
                case 31:
                case 41:
                case 51:                                          // 21 - Advisory Multi, 31 - Discret Multi, 41 - Custody Multi, 51 - DealAdvisory Multi
                    grpAdvisory_Multi.Top = 12;
                    grpAdvisory_Multi.Left = 12;
                    grpAdvisory_Multi.Visible = true;
                    btnOK.Left = 90;
                    btnOK.Top = 172;
                    btnCancel.Left = 242;
                    btnCancel.Top = 172;
                    this.Width = 458;
                    this.Height = 248;
                    break;
            }
            this.Refresh();
        }
        protected override void OnResize(EventArgs e)
        {
            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
        }
        //--- 1 - Brokerage Fees Edit -----------------------
        private void txtBuyPercent_LostFocus(object sender, EventArgs e)
        {
            txtBuyFinish.Text = ((100 - Convert.ToDouble(txtBuyPercent.Text)) * Convert.ToDouble(lblBuy.Text.Replace("%", "")) / 100.0).ToString();
            txtSellFinish.Text = ((100 - Convert.ToDouble(txtBuyPercent.Text)) * Convert.ToDouble(lblSell.Text.Replace("%", "")) / 100.0).ToString();
        }

        private void txtBuyFinish_LostFocus(object sender, EventArgs e)
        {
            if (Convert.ToDouble(lblBuy.Text)  > 0)
            {
                if (Convert.ToDouble(lblBuy.Text) != 0) txtBuyPercent.Text = (100 - Math.Round(100 * Convert.ToDouble(txtBuyFinish.Text) / Convert.ToDouble(lblBuy.Text), 2)).ToString();
                else txtBuyPercent.Text = "0";
            }
            else {
                txtBuyPercent.Text = "0";
                txtBuyFinish.Text = "0";
            }
        }

        private void txtSellFinish_LostFocus(object sender, EventArgs e)
        {
            if (Convert.ToDouble(lblSell.Text) > 0)
            {
                if (Convert.ToDouble(lblSell.Text) != 0) txtBuyPercent.Text = (100 - Math.Round(100 * Convert.ToDouble(txtBuyFinish.Text) / Convert.ToDouble(lblSell.Text), 2)).ToString();
                else txtBuyPercent.Text = "0";
            }
            else
            {
                txtBuyPercent.Text = "0";
                txtBuyFinish.Text = "0";
            }
        }

        private void txtTicketFeesPercent_LostFocus(object sender, EventArgs e)
        {
            txtTicketFeesBuy.Text = ((100 - Convert.ToDouble(txtTicketFeesPercent.Text)) * Convert.ToDouble(lblTicketFeesBuy.Text.Replace("%", "")) / 100.0).ToString();
            txtTicketFeesSell.Text = ((100 - Convert.ToDouble(txtTicketFeesPercent.Text)) * Convert.ToDouble(lblTicketFeesSell.Text.Replace("%", "")) / 100.0).ToString();
        }
        //--- 2 - Advisory Fees Edit -----------------------
        private void txtFeesDiscount_LostFocus(object sender, EventArgs e)
        {
            if (Global.IsNumeric(txtFeesDiscount.Text.Replace("%", "")))                 
                txtFinalFees.Text = ((100 - Convert.ToDouble(txtFeesDiscount.Text)) * Convert.ToDouble(lblFees.Text.Replace("%", "")) / 100.0).ToString();
        }
        private void txtFinalFees_LostFocus(object sender, EventArgs e)
        {
            if (Global.IsNumeric(lblFees.Text.Replace("%", "")))
                if ((Convert.ToDouble(lblFees.Text.Replace("%", ""))) != 0)
                    txtFeesDiscount.Text = (((Convert.ToDouble(lblFees.Text.Replace("%", "")) - Convert.ToDouble(txtFinalFees.Text)) * 100.0) / Convert.ToDouble(lblFees.Text.Replace("%", ""))).ToString("0.####");
                else txtFeesDiscount.Text = "0";
        }
        private void txtMinFeesDiscount_LostFocus(object sender, EventArgs e)
        {
            if (Global.IsNumeric(txtMinFeesDiscount.Text.Replace("%", "")))
                txtFinalMinFees.Text = ((100 - Convert.ToDouble(txtMinFeesDiscount.Text)) * Convert.ToDouble(lblMinFees.Text.Replace("%", "")) / 100.0).ToString();
        }
        private void txtFinalMinFees_LostFocus(object sender, EventArgs e)
        {
            if (Global.IsNumeric(lblMinFees.Text.Replace("%", "")))
                if ((Convert.ToDouble(lblMinFees.Text.Replace("%", ""))) != 0)
                    txtMinFeesDiscount.Text = (((Convert.ToDouble(lblMinFees.Text.Replace("%", "")) - Convert.ToDouble(txtFinalMinFees.Text)) * 100.0) / Convert.ToDouble(lblMinFees.Text.Replace("%", ""))).ToString("0.####");
                else txtMinFeesDiscount.Text = "0";
        }

        //--- 6 - DealAdvisory &  FX Fees Edit -----------------------
        private void txtFeesDiscountFX_LostFocus(object sender, EventArgs e)
        {
            txtFinalFeesFX.Text = ((100 - Convert.ToDouble(txtFeesDiscountFX.Text)) * Convert.ToDouble(lblFeesFX.Text.Replace("%", "")) / 100.0).ToString();
        }
        public int Mode { get { return this.iMode; } set { this.iMode = value; } }

        private void btnOK_Click(object sender, EventArgs e)
        {
            iLastAktion = 1;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            iLastAktion = 0;
            this.Close();
        }

        public int LastAktion { get { return this.iLastAktion; } set { this.iLastAktion = value; } }
    }
}

