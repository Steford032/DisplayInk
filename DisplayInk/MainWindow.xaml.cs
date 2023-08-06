﻿using System;
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
				Point mousePos = e.GetPosition(paintCanvas);
				paintPoint(brushCol, mousePos);
			}
		}

		private void paintCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			isPaint = false;
			points.Reset();
		}

		private void paintCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			isPaint = true;

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
