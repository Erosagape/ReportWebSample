Imports System
Imports System.Drawing
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraGrid
Public Module Extension
    Public sub GridPrintPreview (sender As GridControl,landscape As Boolean,paperkind As Printing.PaperKind,title As string)
        Dim printLink As New PrintableComponentLink
        Dim printSys =New PrintingSystem
        printLink.PrintingSystem = printSys
        printLink.Landscape = Landscape
        printLink.Component = sender
        printLink.PaperKind = PaperKind         
        printLink.Margins =New Printing.Margins(60,45,60,45)

        Dim phf As PageHeaderFooter = DirectCast(printLink.PageHeaderFooter,PageHeaderFooter)
        phf.Header.Content.AddRange (New String(){"",title})
        phf.Header.Font =New Font("Tahoma",10,FontStyle.Bold)
        phf.Footer.Content.AddRange (New String(){"Print Date :" & Convert.ToString(DateTime.now),"","หน้า  [Page # of Pages #]"})

        printLink.CreateDocument

        Dim frm As New FrmViewer
        frm.Text =title
        frm.printLink =printLink
        frm.printingSystem1=printsys
        frm.printingSystem1.Document.AutoFitToPagesWidth = 1
        frm.printingSystem1.Links.Add(printLink)
        frm.DocumentViewerRibbonController1.DocumentViewer.DocumentSource=frm.printingSystem1
        frm.Show()
    End sub
End Module
