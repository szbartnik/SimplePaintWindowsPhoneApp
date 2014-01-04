using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using DrawOnMe.Resources;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Coding4Fun.Toolkit.Controls;

namespace DrawOnMe
{
    public partial class MainPage : PhoneApplicationPage
    {
        private MasterViewModel _viewModel;
        private Point _point;
        private Point _oldPoint;
        private bool _draw = false;
        private double _opacity;
        private List<int> _undoMemory = new List<int>();

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            _oldPoint = _point;
            _opacity = 1;

            BuildLocalizedApplicationBar();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            paint.MouseMove += new MouseEventHandler(FingerMove);
            paint.MouseLeftButtonUp += new MouseButtonEventHandler(FingerUp);
            paint.MouseLeftButtonDown += new MouseButtonEventHandler(FingerDown);

            bgColorEllipse.MouseLeftButtonDown += bgColorEllipse_MouseLeftButtonDown;
            lineColorEllipse.MouseLeftButtonDown += lineColorEllipse_MouseLeftButtonDown;
            _viewModel = new MasterViewModel();
            DataContext = _viewModel;
        }

        void lineColorEllipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        void bgColorEllipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FingerDown(object sender, MouseButtonEventArgs e)
        {
            _undoMemory.Add(paint.Children.Count);
            _point = e.GetPosition(paint);
            _oldPoint = _point;
            _draw = true;
        }

        private void FingerUp(object sender, MouseButtonEventArgs e)
        {
            _draw = false;
        }

        private void FingerMove(object sender, MouseEventArgs e)
        {
            if (_draw)
            {
                _point = e.GetPosition(paint);

                Line line = new Line()
                {
                    Stroke = _viewModel.LineColor,
                    X1 = _point.X,
                    Y1 = _point.Y,
                    X2 = _oldPoint.X,
                    Y2 = _oldPoint.Y,

                    StrokeStartLineCap = PenLineCap.Round,
                    StrokeEndLineCap = PenLineCap.Round,
                    StrokeThickness = _viewModel.Thickness,
                    Opacity = _opacity,
                };

                paint.Children.Add(line);
                _oldPoint = _point;
            }
            _oldPoint = _point;
        }

        private void BuildLocalizedApplicationBar()
        {
            ApplicationBar = new ApplicationBar();

            ApplicationBarIconButton undoButton = new ApplicationBarIconButton(new Uri("/Assets/back.png", UriKind.Relative)) { Text = "Undo" };
            undoButton.Click += undoButton_Click;
            ApplicationBar.Buttons.Add(undoButton);

            ApplicationBarIconButton clearContentButton = new ApplicationBarIconButton(new Uri("/Assets/close.png", UriKind.Relative)) { Text = "Clear" };
            clearContentButton.Click += clearContentButton_Click;
            ApplicationBar.Buttons.Add(clearContentButton);

            ApplicationBarIconButton saveButton = new ApplicationBarIconButton(new Uri("/Assets/save.png", UriKind.Relative)) { Text = "Save" };
            saveButton.Click += saveButton_Click;
            ApplicationBar.Buttons.Add(saveButton);
        }

        void saveButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void clearContentButton_Click(object sender, EventArgs e)
        {
            _undoMemory.Clear();
            paint.Children.Clear();
        }

        void undoButton_Click(object sender, EventArgs e)
        {
            if (_undoMemory.Count == 0) return;

            int i = 0;

            foreach (var line in paint.Children.OfType<Line>().ToList())
            {
                if (i++ >= _undoMemory.Last())
                {
                    paint.Children.Remove(line);
                }
            }
            _undoMemory.Remove(_undoMemory.Last());
        }

        private void thicknessPlus_Tap(object sender, GestureEventArgs e)
        {
            _viewModel.Thickness++;
        }

        private void thicknessMinus_Tap(object sender, GestureEventArgs e)
        {
            _viewModel.Thickness--;
        }
    }
}