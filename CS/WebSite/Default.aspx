<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript" language="javascript">
        function OnValidation(s, e, dename) {

            if (!clientChkb.GetChecked()) {
                return;
            }
            var date1 = e.value;
            if (!date1)
                return;
            var date2 = dename.GetValue();
            var msecPerYear = 1000 * 60 * 60 * 24 * 365;
            var years = dename.name.indexOf("Birth") > -1 ? (date1.getTime() - date2.getTime()) / msecPerYear : (date2.getTime() - date1.getTime()) / msecPerYear;
            if (years < 18) {
                e.isValid = false;
            }
        }
        function updateClick(s, e) {
           if(ASPxClientEdit.ValidateEditorsInContainer(grid.GetMainElement()))           
                grid.PerformCallback(s.GetText());
        }
        function cancelClick(s, e) {
            grid.PerformCallback(s.GetText());
        }
    </script>
    <div>
        <dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server" ClientInstanceName="clientChkb"
            Checked="true" Text="ClientValidation">
        </dx:ASPxCheckBox>
        <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
            DataSourceID="AccessDataSource1" KeyFieldName="EmployeeID" OnCustomCallback="ASPxGridView1_CustomCallback" >
            <Columns>
                <dx:GridViewDataTextColumn FieldName="EmployeeID" ReadOnly="True" VisibleIndex="0">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="LastName" VisibleIndex="2">
                    <DataItemTemplate>
                        <dx:ASPxTextBox ID="LastName" runat="server" Text='<%# Eval("LastName") %>'>
                        </dx:ASPxTextBox>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="FirstName" VisibleIndex="1">
                    <DataItemTemplate>
                        <dx:ASPxTextBox ID="FirstName" runat="server" Text='<%# Eval("FirstName") %>'>
                        </dx:ASPxTextBox>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Address" VisibleIndex="3">
                    <DataItemTemplate>
                        <dx:ASPxTextBox ID="Address" runat="server" Text='<%# Eval("Address") %>'>
                        </dx:ASPxTextBox>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="BirthDate" VisibleIndex="4">
                    <DataItemTemplate>
                        <dx:ASPxDateEdit ID="BirthDate" runat="server" Value='<%# Eval("BirthDate") %>' OnInit="DateEditInit"
                            OnValidation="Date_Validation">
                            <ValidationSettings ValidateOnLeave="false" ErrorDisplayMode="ImageWithTooltip" EnableCustomValidation="true"
                                ValidationGroup="DateGroup" ErrorText="Invalid difference between Hire Date and Birth Date">
                            </ValidationSettings>
                        </dx:ASPxDateEdit>
                    </DataItemTemplate>
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataDateColumn FieldName="HireDate" VisibleIndex="5">
                    <DataItemTemplate>
                        <dx:ASPxDateEdit ID="HireDate" runat="server" Value='<%# Eval("HireDate") %>' OnInit="DateEditInit"
                            OnValidation="Date_Validation">
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" EnableCustomValidation="true"
                                ValidationGroup="DateGroup" ValidateOnLeave="false" ErrorText="Invalid difference between Hire Date and Birth Date">
                            </ValidationSettings>
                        </dx:ASPxDateEdit>
                    </DataItemTemplate>
                </dx:GridViewDataDateColumn>
            </Columns>
        </dx:ASPxGridView>
        <table border="0">
            <tr>
                <td>
                    <dx:ASPxButton ID="UpdateBtn" CausesValidation="false" AutoPostBack="false" ValidationGroup="DateGroup" 
                        runat="server" Text="Update">
                        <ClientSideEvents Click="updateClick" />
                    </dx:ASPxButton>
                </td>
                <td>
                    <dx:ASPxButton ID="CancelBtn" runat="server" AutoPostBack="false" Text="Cancel">
                        <ClientSideEvents Click="cancelClick" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <asp:AccessDataSource ID="AccessDataSource1" runat="server" DataFile="~/App_Data/nwind.mdb"
        SelectCommand="SELECT [EmployeeID], [LastName], [FirstName], [Address], [BirthDate], [HireDate] FROM [Employees]">
    </asp:AccessDataSource>
    <dx:ASPxDateEdit runat="server" ID="ReferenceDateEdit" ClientVisible="false">
        <CalendarProperties>
            <Style BackColor="LightYellow" />
        </CalendarProperties>
    </dx:ASPxDateEdit>
    </form>
</body>
</html>
