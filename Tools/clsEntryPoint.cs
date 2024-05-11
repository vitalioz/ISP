using System;
using Core;

namespace Tools
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
                    frmImportData_SchemasList locImportData_SchemasList = new frmImportData_SchemasList();
                    //locImportData_SchemasList.RightsLevel = Convert.ToInt32(tokens[1]);
                    //locImportData_SchemasList.Extra = tokens[2];
                    locImportData_SchemasList.Show();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    frmSystemServices locSystemServices = new frmSystemServices();
                    locSystemServices.RightsLevel = Convert.ToInt32(tokens[1]);
                    locSystemServices.Extra = tokens[2];
                    locSystemServices.Show();
                    break;
            }
        }
    }
}
