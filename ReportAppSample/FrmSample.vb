Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraReports
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraTab
Public Class FrmSample
    Private Function GetRptMat(strFilter As String) As Report1
        Dim rptMat As New Report1
        rptMat.DataSource = GetDataSource(strFilter)
        Return rptMat
    End Function
    Private Function GetDataSource(Optional strFilter As String = "") As DataTable
        Using cn As OleDb.OleDbConnection = GetCn()
            Dim da As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("select * from StockMat " & If(strFilter <> "", String.Format(" where [TYPEMAT]='{0}'", strFilter), ""), cn)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        End Using
    End Function
    Private Function GetCn() As OleDb.OleDbConnection
        Dim cn As New OleDb.OleDbConnection(My.Settings.DbConnectionString)
        cn.Open
        Return cn
    End Function
    Private sub LoadGrid()
        Dim dt As DataTable = GetDataSource(ComboBox1.Text)
        GridControl1.DataSource = dt
        dim grid As DevExpress.XtraGrid.Views.Grid.GridView = DirectCast (GridControl1.MainView,DevExpress.XtraGrid.Views.Grid.GridView)
        for each  c As DevExpress.XtraGrid.Columns.GridColumn in grid.Columns
            c.BestFit()
        Next                        
    End sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Dim rpt As Report1 = GetRptMat(ComboBox1.Text)
        Dim printtool As New ReportPrintTool(rpt)
        printtool.ShowRibbonPreviewDialog()
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Using cn As OleDb.OleDbConnection = GetCn()
            Dim rd As OleDb.OleDbDataReader = New OleDb.OleDbCommand("select distinct TYPEMAT from StockMat order by TYPEMAT", cn).ExecuteReader()
            While rd.Read
                ComboBox1.Items.Add(rd.GetString(0))
            End While
        End Using
    End Sub
    Private Sub XtraTabControl1_SelectedPageChanging(sender As Object, e As TabPageChangingEventArgs) Handles XtraTabControl1.SelectedPageChanging
        If e.Page.Equals(XtraTabPage2) Then
            LoadGrid()
        End If
    End Sub

    Private Sub GridControl1_Click(sender As Object, e As EventArgs) Handles GridControl1.Click

    End Sub

    Private Sub GridView1_RowClick(sender As Object, e As RowClickEventArgs) Handles GridView1.RowClick
        Dim dr As DataRow =GridView1.GetDataRow(e.RowHandle)
        MessageBox.Show (dr("ID").ToString())
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Extension.GridPrintPreview(GridControl1,true,Printing.PaperKind.A4,"Preview Grid")        
    End Sub
End Class