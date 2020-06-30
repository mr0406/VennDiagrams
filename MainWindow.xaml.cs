using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VennDiagrams
{
    public partial class MainWindow : Window
    {
        private Dictionary<Char, CombinedGeometry> simpleShapes;

        public MainWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            CirclesCentersSet();
            SimpleShapesDictionarySet();
        }

        public void CirclesCentersSet()
        {
            int xCenter = 275;
            int yCenter = 275;
            double d = 80;
            double x = Math.Sqrt(3 * Math.Pow(d, 2) / 4);

            int Ax = xCenter;
            int Ay = yCenter - (int)d;

            double Bx = xCenter + (int)x;
            double By = yCenter + (int)(d / 2);

            double Cx = xCenter - (int)x;
            double Cy = yCenter + (int)(d / 2);

            ACircleLeft.Center = new Point(Ax, Ay);
            BCircleLeft.Center = new Point(Bx, By);
            CCircleLeft.Center = new Point(Cx, Cy);

            ACircleRight.Center = new Point(Ax, Ay);
            BCircleRight.Center = new Point(Bx, By);
            CCircleRight.Center = new Point(Cx, Cy);
        }

        public void SimpleShapesDictionarySet()
        {
            simpleShapes = new Dictionary<char, CombinedGeometry>();
            simpleShapes.Add('A', new CombinedGeometry(ACircleLeft, ACircleLeft));
            simpleShapes.Add('B', new CombinedGeometry(BCircleLeft, BCircleLeft));
            simpleShapes.Add('C', new CombinedGeometry(CCircleLeft, CCircleLeft));
            simpleShapes.Add('U', new CombinedGeometry(rectangleLeft, rectangleLeft));
        }

        private void leftCalculateClick(object sender, RoutedEventArgs e)
        {
            String formulaFromInput = leftFormula.Text;
            Input input = new Input(formulaFromInput);

            if (input.IsValid)
            {
                Calculate calculate = new Calculate(input.Formula, simpleShapes);
                ResultLeft.Data = calculate.Result;
            }
            else
            {
                leftErrorBox.Text = input.ErrorType;
            }
        }

        private void rightCalculateClick(object sender, RoutedEventArgs e)
        {
            String formulaFromInput = rightFormula.Text;
            Input input = new Input(formulaFromInput);

            if (input.IsValid)
            {
                Calculate calculate = new Calculate(input.Formula, simpleShapes);
                ResultRight.Data = calculate.Result;
            } else
            {
                rightErrorBox.Text = input.ErrorType;
            }
        }

        private void helpClick(object sender, RoutedEventArgs e)
        {
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.Show();
        }
    }
}
