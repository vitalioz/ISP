using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Core
{

    public partial class ucDoubleCalendar : UserControl
    {
        int minWidth = 210, maxWidth = 230, minHeight = 22, maxHeight = 171;

        public ucDoubleCalendar()
        {
            InitializeComponent();
            this.Width = minWidth;
            this.Height = minHeight;
        }


        private void picOptions_Click(object sender, EventArgs e)
        {
            this.Width = maxWidth;
            this.Height = maxHeight;
            picClose.Visible = true;
            lbOptions.Visible = true;
        }

        private void lbOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = 0;
            DateTime dTemp, dNow;
            dNow = DateTime.Now;
            switch (lbOptions.SelectedIndex)
            {
                case 0:                               // today
                    dFrom.Value = dNow;
                    dTo.Value = dNow;
                    break;
                case 1:                               // yesterday
                    dFrom.Value = dNow.AddDays(-1);
                    dTo.Value = dNow.AddDays(-1);
                    break;
                case 2:                               // this week
                    i = (int)(dNow.DayOfWeek - 1);
                    if (i < 0) i = i + 7;
                    dTemp = dNow.AddDays(-i);
                    dFrom.Value = dTemp;
                    dTo.Value = dTemp.AddDays(6);
                    break;
                case 3:                              // this month 
                    dFrom.Value = Convert.ToDateTime("01-" + dNow.Month + "-" + dNow.Year);
                    if (dNow.Month < 12) dTemp = Convert.ToDateTime("01-" + (dNow.Month + 1) + "-" + dNow.Year);
                    else dTemp = Convert.ToDateTime("01-01-" + (dNow.Year + 1));
                    dTo.Value = dTemp.AddDays(-1);
                    break;
                case 4:                              // this quarter
                    i = ((dNow.Month - 1) / 3) + 1;
                    switch (i)
                    {
                        case 1:
                            dFrom.Value = Convert.ToDateTime("01-01-" + dNow.Year);
                            dTo.Value = Convert.ToDateTime("31-03-" + dNow.Year);
                            break;
                        case 2:
                            dFrom.Value = Convert.ToDateTime("01-04-" + dNow.Year);
                            dTo.Value = Convert.ToDateTime("30-06-" + dNow.Year);
                            break;
                        case 3:
                            dFrom.Value = Convert.ToDateTime("01-07-" + dNow.Year);
                            dTo.Value = Convert.ToDateTime("30-09-" + dNow.Year);
                            break;
                        case 4:
                            dFrom.Value = Convert.ToDateTime("01-10-" + dNow.Year);
                            dTo.Value = Convert.ToDateTime("31-12-" + dNow.Year);
                            break;
                    }
                    break;
                case 5:                                 // this semester
                    i = ((dNow.Month - 1) / 6) + 1;
                    if (i == 1)
                    {
                        dFrom.Value = Convert.ToDateTime("01-01-" + dNow.Year);
                        dTo.Value = Convert.ToDateTime("30-06-" + dNow.Year);
                    }
                    else
                    {
                        dFrom.Value = Convert.ToDateTime("01-07-" + dNow.Year);
                        dTo.Value = Convert.ToDateTime("31-12-" + dNow.Year);
                    }
                    break;
                case 6:                                // this year
                    dFrom.Value = Convert.ToDateTime("01-01-" + dNow.Year);
                    dTo.Value = Convert.ToDateTime("31-12-" + dNow.Year);
                    break;
                case 7:                               // previous week
                    i = (int)(dNow.DayOfWeek - 1);
                    if (i < 0) i += 7;
                    dTemp = dNow.AddDays(-i);
                    dFrom.Value = dTemp.AddDays(-7);
                    dTo.Value = dTemp.AddDays(-1);
                    break;
                case 8:                                // previous month 
                    if (dNow.Month > 1)
                    {
                        dFrom.Value = Convert.ToDateTime("01-" + (dNow.Month - 1) + "-" + dNow.Year);
                        dTemp = Convert.ToDateTime("01-" + dNow.Month + "-" + dNow.Year);
                        dTo.Value = dTemp.AddDays(-1);
                    }
                    else
                    {
                        dFrom.Value = Convert.ToDateTime("01-12-" + (dNow.Year - 1));
                        dTo.Value = Convert.ToDateTime("31-12-" + (dNow.Year - 1));
                    }
                    break;
                case 9:                                   // previous year
                    dFrom.Value = Convert.ToDateTime("01-01-" + (dNow.Year - 1));
                    dTo.Value = Convert.ToDateTime("31-12-" + (dNow.Year - 1));
                    break;
            }

            picClose.Visible = false;
            lbOptions.Visible = false;
            this.Height = minHeight;
        }
        private void dFrom_ValueChanged(object sender, EventArgs e)
        {
            if (dFrom.Value > dTo.Value) dTo.Value = dFrom.Value;
        }
        private void dTo_ValueChanged(object sender, EventArgs e)
        {
            if (dTo.Value < dFrom.Value) dFrom.Value = dTo.Value;
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            picClose.Visible = false;
            lbOptions.Visible = false;
            this.Width = minWidth;
            this.Height = minHeight;
        }

        public DateTime DateFrom { get { return this.dFrom.Value; } set { this.dFrom.Value = value; } }

        public DateTime DateTo { get { return this.dTo.Value; } set { this.dTo.Value = value; } }

    }    
}
