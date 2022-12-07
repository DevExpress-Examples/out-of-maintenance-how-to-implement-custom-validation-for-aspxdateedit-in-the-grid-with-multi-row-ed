Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web

Partial Public Class [Default]
	Inherits System.Web.UI.Page
	Protected Sub DateEditInit(ByVal sender As Object, ByVal e As EventArgs)
		Dim de As ASPxDateEdit = TryCast(sender, ASPxDateEdit)
		InitDateEdit(de, "client")
	End Sub
	Private Sub InitDateEdit(ByVal de As ASPxDateEdit, ByVal clientPrefixName As String)
		Dim index As Integer = (CType(de.NamingContainer, GridViewDataItemTemplateContainer)).VisibleIndex
        de.ID += index.ToString()
		de.ClientInstanceName = clientPrefixName & de.ID
		If de.ID.Contains("BirthDate") Then
			de.ClientSideEvents.Validation = String.Format("function(s,e){{OnValidation(s,e,{0}{1});}}", clientPrefixName & "HireDate", index)
		Else
			de.ClientSideEvents.Validation = String.Format("function(s,e){{OnValidation(s,e,{0}{1});}}", clientPrefixName & "BirthDate", index)
		End If
		SetupCalendarOwner(de)
	End Sub
	Protected Sub ASPxGridView1_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
		If e.Parameters = "Cancel" Then
			ASPxEdit.ClearEditorsInContainer(ASPxGridView1, "DateGroup")
			ASPxGridView1.DataBind()
		Else
			If ASPxEdit.ValidateEditorsInContainer(ASPxGridView1) Then
				'Your update operations here                        
			End If
		End If
	End Sub
	Protected Sub Date_Validation(ByVal sender As Object, ByVal e As ValidationEventArgs)
		Dim date1 As DateTime = CDate(e.Value)
		Dim de As ASPxDateEdit = TryCast(sender, ASPxDateEdit)
		Dim visibleIndex As Integer = (CType(de.NamingContainer, GridViewDataItemTemplateContainer)).VisibleIndex
		If de.ID.Contains("HireDate") Then
			Dim birthDate As DateTime = CheckDateEdit("BirthDate", visibleIndex)
			If birthDate<>DateTime.MinValue Then
				CheckYears(e, date1, birthDate)
			Else
				e.IsValid=False
			End If
		Else
			Dim hireDate As DateTime = CheckDateEdit("HireDate", visibleIndex)
			If hireDate <> DateTime.MinValue Then
				CheckYears(e, hireDate, date1)
			Else
				e.IsValid = False
			End If
		End If
	End Sub

	Private Function CheckDateEdit(ByVal dateEditName As String, ByVal visibleIndex As Integer) As DateTime
		Dim de2 As ASPxDateEdit = TryCast(ASPxGridView1.FindRowCellTemplateControl(visibleIndex, CType(ASPxGridView1.Columns(dateEditName), GridViewDataColumn), dateEditName & visibleIndex), ASPxDateEdit)
		If de2 IsNot Nothing Then
			Dim date2 As DateTime = CDate(de2.Value)
			Return date2
		End If
		Return DateTime.MinValue
	End Function
	Private Sub CheckYears(ByVal e As ValidationEventArgs, ByVal hireDate As DateTime, ByVal birthDate As DateTime)
		Dim span As TimeSpan = hireDate.Subtract(birthDate)
		If span.TotalDays / 365 < 18 Then
			e.IsValid = False
		End If

	End Sub
	Private Sub SetupCalendarOwner(ByVal editor As ASPxDateEdit)
		editor.PopupCalendarOwnerID = "ReferenceDateEdit"
	End Sub

End Class
