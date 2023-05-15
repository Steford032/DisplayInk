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

namespace displayInk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int diameter = (int)Sizes.small;
        private Brush brushCol = Brushes.Black;

        private bool isPaint = false;
        private bool isErase = false;

        private enum Sizes
        {
            small = 5,
            med = 10,
            large = 15
        }
        private class ArrayPoints
        {
            private int index = 0;
            private Point[] points;
            public ArrayPoints(int size)
            {
                if (size <= 0) size = 2;
                points = new Point[size];
            }

            public void SetPoint(int x, int y)
            {
                if (index >= points.Length) index = 0;
                points[index] = new Point(x, y);
                index++;
            }

            public void Reset()
            {
                index = 0;
            }

            public int Index()
            {
                return index;
            }
            public Point[] getPoints()
            {
                return points;
            }

        }
        private ArrayPoints points = new ArrayPoints(2);

        public MainWindow()
        {

            InitializeComponent();
            Height = SystemParameters.WorkArea.Height;
        }

        private void paintPoint(Brush color, Point position)
        {
            points.SetPoint((int)position.X, (int)position.Y);
            if (points.Index() >= 2)
            {
                for (int i = 0; i < points.Index() - 1; i++)
                {
                    Line line = new Line();
                    line.Stroke = brushCol;
                    line.StrokeThickness = 5;
                    line.X1 = points.getPoints()[i].X;
                    line.Y1 = points.getPoints()[i].Y;
                    line.X2 = points.getPoints()[i + 1].X;
                    line.Y2 = points.getPoints()[i + 1].Y;
                    line.StrokeEndLineCap = PenLineCap.Round;
                    paintCanvas.Children.Add(line);
                }
                points.SetPoint((int)position.X, (int)position.Y);
            }
            // paintCanvas.Children.Add(newEllipse);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPaint)
            {
                Point mousePos = e.GetPosition(paintCanvas);
                paintPoint(brushCol, mousePos);
            }
            if (isErase)
            {
                Point mousePos = e.GetPosition(paintCanvas);
                paintPoint(brushCol, mousePos);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            brushCol = Brushes.Red;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            brushCol = Brushes.Black;
        }

        private void Slider_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            diameter = (int)slider.Value;
        }

        private void paintCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isPaint = true;
            Line ln = new Line();
            ln.Stroke = Brushes.Black;
            ln.Fill = Brushes.Black;
            ln.StrokeThickness = 30;
            ln.X1 = 1;
            ln.Y1 = 1;
            ln.X2 = 1000;
            ln.Y2 = 1000;
        }

        private void paintCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isPaint = false;
            points.Reset();
        }

        private void slider_MouseDown(object sender, MouseButtonEventArgs e)
        {
            diameter = (int)slider.Value;
        }

    }
}
