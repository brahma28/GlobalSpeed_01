<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


    <div>
        <script runat="server">
            private void Page_Load(object sender, System.EventArgs e)
            {                
                ReportViewer1.LocalReport.ReportPath = Server.MapPath(ViewBag.ReportName);
                ReportViewer1.LocalReport.DataSources.Add(ViewBag.ReportDataSource);
                if (ViewBag.parametrizastoru != null)
                    ReportViewer1.LocalReport.SetParameters(ViewBag.parametrizastoru);
                      
                                      
                //ReportViewer1.LocalReport.GetParameters();
   
                ReportViewer1.LocalReport.Refresh();
            }
        </script>
        <form id="Form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">          
        </asp:ScriptManager>
        <rsweb:reportviewer id="ReportViewer1" runat="server"  Width="100%" AsyncRendering="false" 
            ShowFindControls="false" 
            ShowPageNavigationControls="false"
            ShowRefreshButton="false"
            ShowBackButton="false"
            ShowZoomControl="false" BackColor="White"
            >
        </rsweb:reportviewer>
        </form>        
    </div>

