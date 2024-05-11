using Core;
using System;

namespace Contracts
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
                    frmPreContracts locPreContracts = new frmPreContracts();
                    locPreContracts.RightsLevel = Convert.ToInt32(tokens[1]);
                    locPreContracts.Extra = tokens[2];
                    locPreContracts.Show();
                    break;
                case 2:
                    frmClientsList locClientsList2 = new frmClientsList();                              // 2 - Clients List
                    locClientsList2.Mode = 1;
                    locClientsList2.RightsLevel = Convert.ToInt32(tokens[1]);
                    locClientsList2.Extra = tokens[2];
                    locClientsList2.Show();
                    break;
                case 3:
                    frmClientsList locClientsList3 = new frmClientsList();
                    locClientsList3.Mode = 3;                                                           // 3 - Influence Centers
                    locClientsList3.RightsLevel = Convert.ToInt32(tokens[1]);
                    locClientsList3.Extra = tokens[2];
                    locClientsList3.Show();
                    break;
                case 4:
                    frmClientsList locClientsList4 = new frmClientsList();
                    locClientsList4.Mode = 4;                                                           //  4 - Introducers List
                    locClientsList4.RightsLevel = Convert.ToInt32(tokens[1]);
                    locClientsList4.Extra = tokens[2];
                    locClientsList4.Show();
                    break;
                case 5:
                    frmClientsList locClientsList5 = new frmClientsList();
                    locClientsList5.Mode = 5;                                                           //  5 - User
                    locClientsList5.RightsLevel = Convert.ToInt32(tokens[1]);
                    locClientsList5.Extra = tokens[2];
                    locClientsList5.Show();
                    break;
                case 7:
                    frmCandidatesSearch locCandidatesSearch = new frmCandidatesSearch();
                    locCandidatesSearch.RightsLevel = Convert.ToInt32(tokens[1]);
                    locCandidatesSearch.Show();
                    break;
                case 8:
                    frmClientsSearch locClientsSearch = new frmClientsSearch();
                    locClientsSearch.RightsLevel = Convert.ToInt32(tokens[1]);
                    locClientsSearch.Show();
                    break;
                case 9:
                    frmContractsSearch locContractsSearch = new frmContractsSearch();
                    locContractsSearch.RightsLevel = Convert.ToInt32(tokens[1]);
                    locContractsSearch.Show();
                    break;
                case 15:
                    frmClientsBlackList locClientsBlackList = new frmClientsBlackList();
                    locClientsBlackList.Left = 2;
                    locClientsBlackList.Top = 54;
                    locClientsBlackList.RightsLevel = Convert.ToInt32(tokens[1]);
                    locClientsBlackList.ShowDialog();
                    break;
                case 18:
                    frmOfficialInforming locOfficialInforming = new frmOfficialInforming();
                    locOfficialInforming.RightsLevel = Convert.ToInt32(tokens[1]);
                    locOfficialInforming.Show();
                    break;
                case 19:
                    frmClientsRequests locClientsRequests = new frmClientsRequests();
                    locClientsRequests.RightsLevel = Convert.ToInt32(tokens[1]);
                    locClientsRequests.Show();
                    break;
            }
        }
    }
}
