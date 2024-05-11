using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Core
{
    public partial class frmProductsPricesView : Form
    {
        int iProduct_ID, iShareCodes_ID, iRightsLevel;
        string sTemp;
        public frmProductsPricesView()
        {
            InitializeComponent();
        }

        private void frmProductsPricesView_Load(object sender, EventArgs e)
        {
            dFrom.Value = DateTime.Now.AddDays(-14).Date;
            dTo.Value = DateTime.Now.Date;

            ucPS.StartInit(700, 400, 256, 20, 1);
            ucPS.TextOfLabelChanged += new EventHandler(ucPS_TextChanged);
            ucPS.Mode = 2;
            ucPS.ListType = 1;
            ucPS.Filters = "Aktive >= 0 ";
            ucPS.ShowNonAccord = false;
            ucPS.Visible = true;

            cmbProductType.DataSource = Global.dtProductTypes.Copy();
            cmbProductType.DisplayMember = "Title";
            cmbProductType.ValueMember = "ID";

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");


            if (iShareCodes_ID != 0)
            {
                DefineList();
            }
        }
        protected void ucPS_TextChanged(object sender, EventArgs e)
        {
            Global.ProductData stProduct = new Global.ProductData();
            stProduct = ucPS.SelectedProductData;
            iShareCodes_ID = stProduct.ShareCode_ID;
            /*
            iStockExchange_ID = stProduct.StockExchange_ID;
            sTemp = "";
            if (txtAction.Text == "BUY") sTemp = Global.CheckCompatibility(iContract_ID, iMIFID_2, iMIFIDCategory_ID, iXAA, iShare_ID, iStockExchange_ID);

            lnkISIN.Text = stProduct.ISIN;
            lblShareCode.Text = stProduct.Code;
            //lblProduct.Text = stProduct.Product_Title;
            iProduct_ID = stProduct.Product_ID;
            iProductCategory_ID = stProduct.ProductCategory_ID;
            iShare_ID = stProduct.ShareCode_ID;
            lblCurr.Text = stProduct.Currency;
            sProductTitle = stProduct.Product_Title;
            sStockExchange_Code = stProduct.StockExchange_Code;
            */
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DefineList();
        }
        private void DefineList()
        {
            int i = 0, iOldShareID = -999;
            DateTime dOldDateIns = Convert.ToDateTime("1900/01/01");

            fgList.Rows.Count = 1;
            fgList.Redraw = false;

            clsProductsCodes ProductsCodes = new clsProductsCodes();
            ProductsCodes.DateFrom = dFrom.Value;
            ProductsCodes.DateTo = dTo.Value;
            ProductsCodes.Product_ID = Global.IsNumeric(cmbProductType.SelectedValue)? Convert.ToInt32(cmbProductType.SelectedValue) : 0;
            ProductsCodes.ProductCategory_ID = Global.IsNumeric(cmbProductCategory.SelectedValue) ? Convert.ToInt32(cmbProductCategory.SelectedValue) : 0;            
            ProductsCodes.Record_ID = iShareCodes_ID;                                      // ShareCodes_ID
            sTemp = ucPS.txtShareTitle.Text.Trim();
            if (sTemp.Length > 0) sTemp = "%" + sTemp + "%";
            ProductsCodes.Filter = sTemp;
            ProductsCodes.GetPricesList();
            foreach (DataRow dtRow in ProductsCodes.List.Rows) {
                if ((Convert.ToInt32(dtRow["ShareCodes_ID"]) != iOldShareID) || (Convert.ToDateTime(dtRow["DateIns"]) != dOldDateIns)) {
                    iOldShareID = Convert.ToInt32(dtRow["ShareCodes_ID"]);
                    dOldDateIns = Convert.ToDateTime(dtRow["DateIns"]);

                    i = i + 1;
                    fgList.AddItem(i + "\t" + dtRow["Product_Title"] + "\t" + dtRow["ProductCategory_Title"] + "\t" + dtRow["Code"] + "\t" + dtRow["Code2"] + "\t" + 
                                   dtRow["ISIN"] + "\t" + dtRow["Title"] + "\t" + dtRow["DateIns"] + "\t" + dtRow["Close"] + "\t" + dtRow["Last"] + "\t" + 
                                   dtRow["Currency"] + "\t" + dtRow["ID"] + "\t" + dtRow["ShareCodes_ID"] + "\t" + dtRow["Product_ID"]);
                }
            }
            fgList.Redraw = true;
        }
        public int Product_ID { get { return iProduct_ID; } set { iProduct_ID = value; } }
        public int ShareCodes_ID { get { return iShareCodes_ID; } set { iShareCodes_ID = value; } }
        public int RightsLevel { get { return iRightsLevel; } set { iRightsLevel = value; } }
    }
}
