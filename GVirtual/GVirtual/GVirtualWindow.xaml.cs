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
using libSMARTMultiTouch.Table;

namespace GVirtual
{
    /// <summary>
    /// Interaction logic for GVirtualWindow.xaml
    /// </summary>
    public partial class GVirtualWindow : Window
    {
        public GVirtualWindow()
        {
            InitializeComponent();

            TableManager.Initialize(this, LayoutRoot);

            LayoutRoot.Children.Add(new TableControl());

            TableManager.IsFullScreen = false;
        }
    }
}
