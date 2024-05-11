using System;
using Core;

namespace Reports
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
                    frmTRS locInvestProposalsList = new frmTRS();
                    locInvestProposalsList.Show();
                    break;
                case 2:
                    frmPeriodicalEvaluation locPeriodicalEvaluation = new frmPeriodicalEvaluation();
                    locPeriodicalEvaluation.Show();
                    break;
                case 3:
                    frmExPostCost locExPostCost = new frmExPostCost();
                    locExPostCost.RightsLevel = Convert.ToInt32(tokens[1]);
                    locExPostCost.Show();
                    break;
            }
        }
    }
}
