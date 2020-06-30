using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VennDiagrams
{
    public partial class HelpWindow : Window
    {
        public HelpWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            SetText();
        }
        public void SetText()
        {
            mainTextBlock.Text = "Sets: A B C U \n" +
                                 "Sum of sets: + \n" +
                                 "Intersect of sets: * \n" +
                                 "Subtract of sets: - \n" +
                                 "Complement of set: ' \n" +
                                 "Separate operations: ( ) \n";
        }
    }
}
