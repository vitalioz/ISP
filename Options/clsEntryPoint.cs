using System;
using Core;

namespace Options
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
                    frmMiscLists locMiscLists = new frmMiscLists();
                    locMiscLists.Left = 2;
                    locMiscLists.Top = 54;
                    locMiscLists.RightsLevel = Convert.ToInt32(tokens[1]);
                    locMiscLists.Extra = tokens[2];
                    locMiscLists.Show();
                    break;                    
                case 2:
                    frmComissionsCategories locComissionsCategories = new frmComissionsCategories();
                    locComissionsCategories.Left = 2;
                    locComissionsCategories.Top = 54;
                    locComissionsCategories.RightsLevel = Convert.ToInt32(tokens[1]);
                    locComissionsCategories.Extra = tokens[2];
                    locComissionsCategories.ShowDialog();
                    break;
                case 3:
                    frmServiceProviders locServiceProviders = new frmServiceProviders();
                    locServiceProviders.RightsLevel = Convert.ToInt32(tokens[1]);
                    locServiceProviders.Extra = tokens[2];
                    locServiceProviders.Show();
                    break;
                case 4:
                    frmServicePackages locServicePackages = new frmServicePackages();
                    locServicePackages.RightsLevel = Convert.ToInt32(tokens[1]);
                    locServicePackages.Extra = tokens[2];
                    locServicePackages.Show();
                    break;
                case 6:
                    frmOptions locOptions = new frmOptions();
                    locOptions.RightsLevel = Convert.ToInt32(tokens[1]);
                    locOptions.Extra = tokens[2];
                    locOptions.Show();
                    break;
                case 8:
                    frmUsersList locUsersList = new frmUsersList();
                    locUsersList.Left = 2;
                    locUsersList.Top = 54;
                    locUsersList.RightsLevel = Convert.ToInt32(tokens[1]);
                    locUsersList.Extra = tokens[2];
                    locUsersList.ShowDialog();
                    break;
            }

        }
    }
}
