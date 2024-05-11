using System;
using Core;

namespace Accounting
{
    class clsEntryPoint
    {   public clsEntryPoint(string sParams)
        {
            string[] tokens = sParams.Split(';');
            if (!Global.IsNumeric(tokens[1])) tokens[1] = "0";
            tokens[2] = tokens[2] + "";

            switch (Convert.ToInt32(tokens[0]))
            {
                case 1:
                    frmAcc_Contracts locAcc_Contracts = new frmAcc_Contracts();
                    locAcc_Contracts.RightsLevel = Convert.ToInt32(tokens[1]);
                    locAcc_Contracts.Extra = tokens[2];
                    locAcc_Contracts.Show();
                    break;
                case 2:
                    frmAcc_InvoicesRTO loc_InvoicesRTO = new frmAcc_InvoicesRTO();
                    loc_InvoicesRTO.RightsLevel = Convert.ToInt32(tokens[1]);
                    loc_InvoicesRTO.Extra = tokens[2];
                    loc_InvoicesRTO.Show();
                    break;
                case 3:
                    frmAcc_InvoicesFX locAcc_InvoicesFX = new frmAcc_InvoicesFX();
                    locAcc_InvoicesFX.RightsLevel = Convert.ToInt32(tokens[1]);
                    locAcc_InvoicesFX.Extra = tokens[2];
                    locAcc_InvoicesFX.Show();
                    break;
                case 4:
                    frmAcc_InvoicesMF locAcc_InvoicesMF = new frmAcc_InvoicesMF();
                    locAcc_InvoicesMF.RightsLevel = Convert.ToInt32(tokens[1]);
                    locAcc_InvoicesMF.Extra = tokens[2];
                    locAcc_InvoicesMF.Show();
                    break;
                case 5:
                    frmAcc_InvoicesAF locAcc_InvoicesAF = new frmAcc_InvoicesAF();
                    locAcc_InvoicesAF.RightsLevel = Convert.ToInt32(tokens[1]);
                    locAcc_InvoicesAF.Extra = tokens[2];
                    locAcc_InvoicesAF.Show();
                    break;
                case 6:
                    frmAcc_InvoicesCF locAcc_InvoicesCF = new frmAcc_InvoicesCF();
                    locAcc_InvoicesCF.RightsLevel = Convert.ToInt32(tokens[1]);
                    locAcc_InvoicesCF.Extra = tokens[2];
                    locAcc_InvoicesCF.Show();
                    break;
                case 7:
                    frmAcc_InvoicesPF locAcc_InvoicesPF = new frmAcc_InvoicesPF();
                    locAcc_InvoicesPF.RightsLevel = Convert.ToInt32(tokens[1]);
                    locAcc_InvoicesPF.Extra = tokens[2];
                    locAcc_InvoicesPF.Show();
                    break;
                case 8:
                    frmAcc_InvoicesRF locAcc_InvoicesRF = new frmAcc_InvoicesRF();
                    locAcc_InvoicesRF.RightsLevel = Convert.ToInt32(tokens[1]);
                    locAcc_InvoicesRF.Extra = tokens[2];
                    locAcc_InvoicesRF.Show();
                    break;
                case 9:
                    frmInvoicesControl locInvoicesControl = new frmInvoicesControl();
                    locInvoicesControl.RightsLevel = Convert.ToInt32(tokens[1]);
                    locInvoicesControl.Extra = tokens[2];
                    locInvoicesControl.Show();
                    break;
                case 10:
                    frmPortfoliosMenu locPortfoliosMenu = new frmPortfoliosMenu();
                    locPortfoliosMenu.Show();
                    break;
            }
        }
    }
}
