using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class Default : System.Web.UI.Page
{
    protected void DateEditInit(object sender, EventArgs e)
    {
        ASPxDateEdit de = sender as ASPxDateEdit;
        InitDateEdit(de, "client");
    }
    private void InitDateEdit(ASPxDateEdit de, string clientPrefixName)
    {
        int index = ((GridViewDataItemTemplateContainer)de.NamingContainer).VisibleIndex;
        de.ID += index;
        de.ClientInstanceName = clientPrefixName + de.ID;
        if (de.ID.Contains("BirthDate"))
            de.ClientSideEvents.Validation = String.Format("function(s,e){{OnValidation(s,e,{0}{1});}}", clientPrefixName + "HireDate", index);
        else
            de.ClientSideEvents.Validation = String.Format("function(s,e){{OnValidation(s,e,{0}{1});}}", clientPrefixName + "BirthDate", index);
        SetupCalendarOwner(de);
    }
    protected void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "Cancel")
        {
            ASPxEdit.ClearEditorsInContainer(ASPxGridView1, "DateGroup");
            ASPxGridView1.DataBind();
        }
        else
        {
            if (ASPxEdit.ValidateEditorsInContainer(ASPxGridView1))
            {
                //Your update operations here                        
            }
        }
    }
    protected void Date_Validation(object sender, ValidationEventArgs e)
    {
        DateTime date1 = (DateTime)e.Value;
        ASPxDateEdit de = sender as ASPxDateEdit;
        int visibleIndex = ((GridViewDataItemTemplateContainer)de.NamingContainer).VisibleIndex;
        if (de.ID.Contains("HireDate"))
        {
            DateTime birthDate = CheckDateEdit("BirthDate", visibleIndex);
            if(birthDate!=DateTime.MinValue)
                CheckYears(e, date1, birthDate);
            else
                e.IsValid=false;
        }
        else
        {
            DateTime hireDate = CheckDateEdit("HireDate", visibleIndex);
            if (hireDate != DateTime.MinValue)
                CheckYears(e, hireDate, date1);
            else
                e.IsValid = false;            
        }
    }

    private DateTime CheckDateEdit(string dateEditName, int visibleIndex)
    {
        ASPxDateEdit de2 = ASPxGridView1.FindRowCellTemplateControl(visibleIndex, (GridViewDataColumn)ASPxGridView1.Columns[dateEditName], dateEditName + visibleIndex) as ASPxDateEdit;
        if (de2 != null)
        {
            DateTime date2 = (DateTime)de2.Value;
            return date2;
        }
        return DateTime.MinValue;
    }
    private void CheckYears(ValidationEventArgs e, DateTime hireDate, DateTime birthDate)
    {
        TimeSpan span = hireDate - birthDate;
        if (span.TotalDays / 365 < 18)
            e.IsValid = false;

    }
    void SetupCalendarOwner(ASPxDateEdit editor)
    {
        editor.PopupCalendarOwnerID = "ReferenceDateEdit";
    }

}
