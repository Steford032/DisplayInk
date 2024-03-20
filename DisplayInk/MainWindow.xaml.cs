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

namespace DisplayInk
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		double diameter;
		private Brush brushCol = Brushes.Black;

		private bool isPaint = false;
		private bool isErase = false;

		private ArrayPoints points = new ArrayPoints(2);

		public MainWindow()
		{
			Height = SystemParameters.WorkArea.Height;
			InitializeComponent();
		}
		private void paintPoint(Brush color, Point position)
		{
			//Creating line as array of sublines
			points.SetPoint((int)position.X, (int)position.Y);
			if (points.Index() >= 2)
			{
				for (int i = 0; i < points.Index() - 1; i++)
				{
					Line line = new Line();
					line.Stroke = brushCol;
					line.StrokeThickness = diameter;
					line.X1 = points.getPoints()[i].X;
					line.Y1 = points.getPoints()[i].Y;
					line.X2 = points.getPoints()[i + 1].X;
					line.Y2 = points.getPoints()[i + 1].Y;
					line.StrokeEndLineCap = PenLineCap.Round;
					paintCanvas.Children.Add(line);
				}
				points.SetPoint((int)position.X, (int)position.Y);
			}
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
                Point mousePosition = e.GetPosition(paintCanvas);

                UIElement elementAtMouse = GetElementAtPoint(paintCanvas, mousePosition);

                if (elementAtMouse != null)
                {
                    paintCanvas.Children.Remove(elementAtMouse);
                }
            }
		}
		private void paintCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			isPaint = true;
		}

		private void paintCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			isPaint = false;
			points.Reset();
		}

        private void paintCanvas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            isErase = false;
        }

        private void paintCanvas_MouseRightButtonDown(object sender, MouseEventArgs e)
        {
			isErase = true;
        }

        private UIElement GetElementAtPoint(Canvas canvas, Point point)
        {
            HitTestResult hitTestResult = VisualTreeHelper.HitTest(canvas, point);

            if (hitTestResult != null && hitTestResult.VisualHit is UIElement element)
            {
                return element;
            }

            return null;
        }

        private void BlackBtn_Click(object sender, RoutedEventArgs e)
		{
			brushCol = Brushes.Black;
		}
		
		private void RedBtn_Click(object sender, RoutedEventArgs e)
		{
			brushCol = Brushes.Red;
		}
		
		private void GreenBtn_Click(object sender, RoutedEventArgs e)
		{
			brushCol = Brushes.LightGreen;
		}
        
		private void GrayBtn_Click(object sender, RoutedEventArgs e)
        {
            brushCol = Brushes.Gray;
        }
       
		private void OrangeBtn_Click(object sender, RoutedEventArgs e)
        {
            brushCol = Brushes.Orange;
        }
        
		private void YellowBtn_Click(object sender, RoutedEventArgs e)
        {
            brushCol = Brushes.Yellow;
        }
        
		private void WhiteBtn_Click(object sender, RoutedEventArgs e)
        {
            brushCol = Brushes.White;
        }
        
		private void VioletBtn_Click(object sender, RoutedEventArgs e)
        {
            brushCol = Brushes.Violet;
        }
       
		private void BlueBtn_Click(object sender, RoutedEventArgs e)
        {
            brushCol = Brushes.Blue;
        }
        
		private void DarkGrayBtn_Click(object sender, RoutedEventArgs e)
        {
            brushCol = Brushes.DarkGray;
        }
		
		private void ClearAllBtn_Click(object sender, RoutedEventArgs e)
		{
			paintCanvas.Children.Clear();
		}

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			//Changing pen's width with Slider
			diameter = this.slider.Value;
		}
	}
}
