using System;
using Core;

namespace Custody
{
    class clsEntryPoint
    {
        public clsEntryPoint(string sParams)
        {
            string[] tokens = sParams.Split(';');
            if (!Global.IsNumeric(tokens[1])) tokens[1] = "0";
            tokens[2] = tokens[2] + "";

            switch (Convert.ToInt32(tokens[0]))
            {
                case 1:
                    frmExecutionFiles locExecutionFiles = new frmExecutionFiles();
                    locExecutionFiles.RightsLevel = Convert.ToInt32(tokens[1]);
                    locExecutionFiles.Extra = tokens[2];
                    locExecutionFiles.Show();
                    break;
                case 2:
                    frmExecutionFilesFX locExecutionFilesFX = new frmExecutionFilesFX();
                    locExecutionFilesFX.RightsLevel = Convert.ToInt32(tokens[1]);
                    locExecutionFilesFX.Extra = tokens[2];
                    locExecutionFilesFX.Show();
                    break;
                case 3:
                    frmExecutionFiles2 locExecutionFiles2 = new frmExecutionFiles2();
                    locExecutionFiles2.RightsLevel = Convert.ToInt32(tokens[1]);
                    locExecutionFiles2.Extra = tokens[2];
                    locExecutionFiles2.Show();
                    break;
                case 4:
                    frmTrx_Control locTrx_Control = new frmTrx_Control();
                    locTrx_Control.RightsLevel = Convert.ToInt32(tokens[1]);
                    locTrx_Control.Extra = tokens[2];
                    locTrx_Control.Show();
                    break;
                case 6:
                    frmTrx_Invoices locTrx_Invoices = new frmTrx_Invoices();
                    locTrx_Invoices.RightsLevel = Convert.ToInt32(tokens[1]);
                    locTrx_Invoices.Extra = tokens[2];
                    locTrx_Invoices.Show();
                    break;
                case 7:
                    frmTrx_Charges locTrx_Charges = new frmTrx_Charges();
                    locTrx_Charges.RightsLevel = Convert.ToInt32(tokens[1]);
                    locTrx_Charges.Extra = tokens[2];
                    locTrx_Charges.Show();
                    break;
                case 8:
                    frmTrx_Fees locTrx_Fees = new frmTrx_Fees();
                    locTrx_Fees.RightsLevel = Convert.ToInt32(tokens[1]);
                    locTrx_Fees.Extra = tokens[2];
                    locTrx_Fees.Show();
                    break;
            }
        }
    }
}
