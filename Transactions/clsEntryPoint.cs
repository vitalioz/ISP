using System;
using Core;

namespace Transactions
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
                    frmInvestProposalsList locInvestProposalsList = new frmInvestProposalsList();
                    locInvestProposalsList.RightsLevel = Convert.ToInt32(tokens[1]);
                    locInvestProposalsList.Extra = tokens[2];
                    locInvestProposalsList.Show();
                    break;
                case 2:
                    frmDPMOrdersList locDPMOrdersList = new frmDPMOrdersList();
                    locDPMOrdersList.RightsLevel = Convert.ToInt32(tokens[1]);
                    locDPMOrdersList.Extra = tokens[2];
                    locDPMOrdersList.Show();
                    break;
                case 4:
                    frmDailySecurities locDailySecurities = new frmDailySecurities();
                    locDailySecurities.Mode = 1;                                                // 1 - Dialy, 2 - Search
                    locDailySecurities.RightsLevel = Convert.ToInt32(tokens[1]);
                    locDailySecurities.Extra = tokens[2];
                    locDailySecurities.Show();
                    break;
                case 5:
                    frmDailySecurities locCommandsSearch = new frmDailySecurities();
                    locCommandsSearch.Mode = 2;                                                 // 1 - Dialy, 2 - Search
                    locCommandsSearch.RightsLevel = Convert.ToInt32(tokens[1]);
                    locCommandsSearch.Extra = tokens[2];
                    locCommandsSearch.Show();
                    break;
                case 6:
                    frmSecuritiesCheck locSecuritiesCheck = new frmSecuritiesCheck();
                    locSecuritiesCheck.RightsLevel = Convert.ToInt32(tokens[1]);
                    locSecuritiesCheck.Extra = tokens[2];
                    locSecuritiesCheck.Show();
                    break;
                case 8:
                    frmDailyFX locDailyFX = new frmDailyFX();
                    locDailyFX.Mode = 1;                                                          // 1 - Dialy, 2 - Search
                    locDailyFX.RightsLevel = Convert.ToInt32(tokens[1]);
                    locDailyFX.Extra = tokens[2];
                    locDailyFX.Show();
                    break;
                case 9:
                    frmDailyFX locSearchFX = new frmDailyFX();
                    locSearchFX.Mode = 2;                                                         // 1 - Dialy, 2 - Search
                    locSearchFX.RightsLevel = Convert.ToInt32(tokens[1]);
                    locSearchFX.Extra = tokens[2];
                    locSearchFX.Show();
                    break;
                case 11:
                    frmDailyLL locDailyLL = new frmDailyLL();
                    locDailyLL.Mode = 1;                                                          // 1 - Dialy, 2 - Search
                    locDailyLL.RightsLevel = Convert.ToInt32(tokens[1]);
                    locDailyLL.Extra = tokens[2];
                    locDailyLL.Show();
                    break;
                case 12:
                    frmDailyLL locLLSearch = new frmDailyLL();
                    locLLSearch.Mode = 2;                                                        // 1 - Dialy, 2 - Search
                    locLLSearch.RightsLevel = Convert.ToInt32(tokens[1]);
                    locLLSearch.Extra = tokens[2];
                    locLLSearch.Show();
                    break;
            }
        }
    }
}
