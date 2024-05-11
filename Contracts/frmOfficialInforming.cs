using Core;
using System;
using System.Windows.Forms;

namespace Contracts
{
    public partial class frmOfficialInforming : Form
    {
        int iRightsLevel;
        ucOfficialInforming_Commands ucOfficialInforming_Commands = new ucOfficialInforming_Commands();
        ucOfficialInforming_RTO ucOfficialInforming_RTO = new ucOfficialInforming_RTO();
        ucOfficialInforming_FX ucOfficialInforming_FX = new ucOfficialInforming_FX();
        ucOfficialInforming_ManFees ucOfficialInforming_ManFees = new ucOfficialInforming_ManFees();
        ucOfficialInforming_AdminFees ucOfficialInforming_AdminFees = new ucOfficialInforming_AdminFees();
        ucOfficialInforming_PerformanceFees ucOfficialInforming_PerfFees = new ucOfficialInforming_PerformanceFees();
        ucOfficialInforming_CustodyFees ucOfficialInforming_CustodyFees = new ucOfficialInforming_CustodyFees();
        ucOfficialInforming_ExPostCost ucOfficialInforming_ExPostCost = new ucOfficialInforming_ExPostCost();
        ucOfficialInforming_PeriodicalEvaluation ucOfficialInforming_PeriodicalEvaluation = new ucOfficialInforming_PeriodicalEvaluation();

        public frmOfficialInforming()
        {
            InitializeComponent();

            ucOfficialInforming_Commands.Top = -2000;
            ucOfficialInforming_RTO.Top = -2000;
            ucOfficialInforming_FX.Top = -2000;
            ucOfficialInforming_ManFees.Top = -2000;
            ucOfficialInforming_AdminFees.Top = -2000;
            ucOfficialInforming_PerfFees.Top = -2000;
            ucOfficialInforming_CustodyFees.Top = -2000;
            ucOfficialInforming_ExPostCost.Top = -2000;
            ucOfficialInforming_PeriodicalEvaluation.Top = -2000;
        }

        private void frmOfficialInforming_Load(object sender, EventArgs e)
        {
            this.Controls.Add(ucOfficialInforming_Commands);
            ucOfficialInforming_Commands.Top = -2000;
            ucOfficialInforming_Commands.Left = 6;

            this.Controls.Add(ucOfficialInforming_RTO);
            ucOfficialInforming_RTO.Top = -2000;
            ucOfficialInforming_RTO.Left = 6;

            this.Controls.Add(ucOfficialInforming_FX);
            ucOfficialInforming_FX.Top = -2000;
            ucOfficialInforming_FX.Left = 6;

            this.Controls.Add(ucOfficialInforming_ManFees);
            ucOfficialInforming_ManFees.Top = -2000;
            ucOfficialInforming_ManFees.Left = 6;

            this.Controls.Add(ucOfficialInforming_AdminFees);
            ucOfficialInforming_AdminFees.Top = -2000;
            ucOfficialInforming_AdminFees.Left = 6;

            this.Controls.Add(ucOfficialInforming_PerfFees);
            ucOfficialInforming_PerfFees.Top = -2000;
            ucOfficialInforming_PerfFees.Left = 6;

            this.Controls.Add(ucOfficialInforming_CustodyFees);
            ucOfficialInforming_CustodyFees.Top = -2000;
            ucOfficialInforming_CustodyFees.Left = 6;

            this.Controls.Add(ucOfficialInforming_ExPostCost);
            ucOfficialInforming_ExPostCost.Top = -2000;
            ucOfficialInforming_ExPostCost.Left = 6;

            this.Controls.Add(ucOfficialInforming_PeriodicalEvaluation);
            ucOfficialInforming_PeriodicalEvaluation.Top = -2000;
            ucOfficialInforming_PeriodicalEvaluation.Left = 6;

            lblType.Text = Global.GetLabel("update_type");
            cmbTypos.SelectedIndex = 0;
        }
        protected override void OnResize(EventArgs e)
        {
            ucOfficialInforming_Commands.Width = this.Width - 28;
            ucOfficialInforming_Commands.Height = this.Height - 86;

            ucOfficialInforming_RTO.Width = this.Width - 28;
            ucOfficialInforming_RTO.Height = this.Height - 86;

            ucOfficialInforming_FX.Width = this.Width - 28;
            ucOfficialInforming_FX.Height = this.Height - 86;

            ucOfficialInforming_ManFees.Width = this.Width - 28;
            ucOfficialInforming_ManFees.Height = this.Height - 86;

            ucOfficialInforming_AdminFees.Width = this.Width - 28;
            ucOfficialInforming_AdminFees.Height = this.Height - 86;

            ucOfficialInforming_PerfFees.Width = this.Width - 28;
            ucOfficialInforming_PerfFees.Height = this.Height - 86;

            ucOfficialInforming_CustodyFees.Width = this.Width - 28;
            ucOfficialInforming_CustodyFees.Height = this.Height - 86;

            ucOfficialInforming_ExPostCost.Width = this.Width - 28;
            ucOfficialInforming_ExPostCost.Height = this.Height - 86;

            ucOfficialInforming_PeriodicalEvaluation.Width = this.Width - 28;
            ucOfficialInforming_PeriodicalEvaluation.Height = this.Height - 86;
        }

        private void cmbTypos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ucOfficialInforming_Commands.Top = -2000;
            ucOfficialInforming_RTO.Top = -2000;
            ucOfficialInforming_FX.Top = -2000;
            ucOfficialInforming_ManFees.Top = -2000;
            ucOfficialInforming_AdminFees.Top = -2000;
            ucOfficialInforming_PerfFees.Top = -2000;
            ucOfficialInforming_CustodyFees.Top = -2000;
            ucOfficialInforming_ExPostCost.Top = -2000;
            ucOfficialInforming_PeriodicalEvaluation.Top = -2000;

            switch (cmbTypos.SelectedIndex)
            {

                case 0:
                    ucOfficialInforming_Commands.Top = 40;
                    break;
                case 1:
                    ucOfficialInforming_RTO.Top = 40;
                    break;
                case 2:
                    ucOfficialInforming_FX.Top = 40;
                    break;
                case 3:
                    ucOfficialInforming_ManFees.Top = 40;
                    break;
                case 4:
                    ucOfficialInforming_AdminFees.Top = 40;
                    break;
                case 5:
                    ucOfficialInforming_PerfFees.Top = 40;
                    break;
                case 6:
                    ucOfficialInforming_CustodyFees.Top = 40;
                    break;
                case 7:
                    break;
                case 8:
                    ucOfficialInforming_PeriodicalEvaluation.Top = 40;
                    break;
                case 9:
                    ucOfficialInforming_ExPostCost.Top = 40;
                    break;
                case 10:
                    //ucOfficialInforming_Misc.Top = 40;
                    break;
            }
        }

        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
    }
}
