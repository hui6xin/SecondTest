using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Xps.Packaging;

namespace WpfFlipPageDemo
{
    /// <summary>
    /// CtrlDocumentViewer.xaml 的交互逻辑
    /// </summary>
    public partial class CtrlDocumentViewer : UserControl
    {
        static string fileName = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "CSharp 3.0 Specification.xps");
        public CtrlDocumentViewer()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(CtrlDocumentViewer_Loaded);
        }

        void CtrlDocumentViewer_Loaded(object sender, RoutedEventArgs e)
        {
            XpsDocument document = new XpsDocument(fileName, System.IO.FileAccess.Read);

            FixedDocument doc = new FixedDocument();
            FixedDocumentSequence fixedDoc = document.GetFixedDocumentSequence();

            this.dvDocument.Document = fixedDoc;
            this.dvDocument.FitToHeight();
        }
    }
}
