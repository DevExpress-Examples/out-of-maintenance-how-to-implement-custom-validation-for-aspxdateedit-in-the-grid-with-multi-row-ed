<!-- default file list -->
*Files to look at*:

* [Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx](./VB/WebSite/Default.aspx))
* [Default.aspx.cs](./CS/WebSite/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/WebSite/Default.aspx.vb))
<!-- default file list end -->
# How to implement custom validation for ASPxDateEdit in the grid with multi-row editing


<p>This example illustrates how to implement a custom client-side and server-side validation in the ASPxGridView with multi-row editing. This functionality allows you to check the difference between dates in the HireDate and BirthDate columns.</p><p>If the variation between these columns is less than 18, inputted data is invalid and data update is not allowed. <br />
All DateEdits use the same PopupWindowCalendar to increase the time of page load. See the <a href="https://www.devexpress.com/Support/Center/p/E1452">How to share the same calendar across all the date editors within the ASPxGridView</a> example to learn more about this feature. <br />
For more information on how to implement multi - row editing, refer to the <a href="https://www.devexpress.com/Support/Center/p/E324">How to implement the multi-row editing feature in the ASPxGridView</a> example.</p>

<br/>


