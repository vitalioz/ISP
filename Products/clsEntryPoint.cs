using System;
using Core;

namespace Products
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
                    frmProductsList locProductsList = new frmProductsList();
                    locProductsList.RightsLevel = Convert.ToInt32(tokens[1]);
                    locProductsList.Extra = tokens[2];
                    locProductsList.Show();
                    break;
                case 2:
                    frmSelectedProducts locSelectedProducts = new frmSelectedProducts();
                    locSelectedProducts.RightsLevel = Convert.ToInt32(tokens[1]);
                    locSelectedProducts.Extra = tokens[2];
                    locSelectedProducts.Show();
                    break;
                case 3:
                    frmProductsAccordance locProductsAccordance = new frmProductsAccordance();
                    locProductsAccordance.RightsLevel = Convert.ToInt32(tokens[1]);
                    locProductsAccordance.Extra = tokens[2];
                    locProductsAccordance.Show();
                    break;
                case 4:
                    frmStandardPortfolios locStandardPortfolios = new frmStandardPortfolios();
                    locStandardPortfolios.RightsLevel = Convert.ToInt32(tokens[1]);
                    locStandardPortfolios.Extra = tokens[2];
                    locStandardPortfolios.Show();
                    break;
                case 5:
                    frmProductsPrices locProductsPrices = new frmProductsPrices();
                    locProductsPrices.RightsLevel = Convert.ToInt32(tokens[1]);
                    locProductsPrices.Extra = tokens[2];
                    locProductsPrices.Show();
                    break;
                case 6:
                    frmInvestmentCommetties locInvestmentCommetties = new frmInvestmentCommetties();
                    locInvestmentCommetties.RightsLevel = Convert.ToInt32(tokens[1]);
                    locInvestmentCommetties.Extra = tokens[2];
                    locInvestmentCommetties.Show();
                    break;
                case 7:
                    frmProductDataDownloader locProductDataDownloader = new frmProductDataDownloader();
                    locProductDataDownloader.RightsLevel = Convert.ToInt32(tokens[1]);
                    locProductDataDownloader.Extra = tokens[2];
                    locProductDataDownloader.Show();
                    break;
            }
        }
    }
}
