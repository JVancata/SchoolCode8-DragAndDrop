using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
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
using FileHelpers;

namespace DragAndDrop
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Point startPoint;
        public bool Moving = false;
        public int indexCounter = 0;
        public string path = "infoColor.csv";
        Random rnd = new Random();

        public MainWindow()
        {
            InitializeComponent();
            
            if (!System.IO.File.Exists(@path))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(@path))
                {
                    fs.WriteByte(0);
                }
            }
            //Board.Children.Add();

            var engine = new FileHelperEngine<Ctverec>();
            var result = engine.ReadFile(path);
            List<Ctverec> seznamCtvercu = new List<Ctverec>(result);
            
            //seznamCtvercu.Add(new Ctverec(100, 100, System.Windows.Media.Colors.Blue, 0, 0));
            //seznamCtvercu.Add();
            renderRectangles(seznamCtvercu);

        }
        private void renderRectangles(List<Ctverec> ctverce)
        {
            foreach(Ctverec ctverec in ctverce)
            {
                Board.Children.Add(createRectangle(ctverec.Width, ctverec.Height, ctverec.X, ctverec.Y));
            }
            //Board.Children.Add(createRectangle(ctverec.Width, ctverec.Height, ctverec.Color, ctverec.X, ctverec.Y));
        }
        //private Rectangle createRectangle(int width, int height, System.Windows.Media.Color color, int x, int y)
        private Rectangle createRectangle(int width, int height, int x, int y)
        {
            Rectangle theRect = new Rectangle();

            theRect.Margin = new Thickness(x, y, 0, 0);

            theRect.StrokeThickness = 1;
            
            SolidColorBrush brush = new SolidColorBrush(System.Windows.Media.Colors.Blue);
            int next = rnd.Next(1, 7);
            switch (next)
            {
                case 1:
                    brush = new SolidColorBrush(System.Windows.Media.Colors.Blue);
                    break;
                case 2:
                    brush = new SolidColorBrush(System.Windows.Media.Colors.Green);
                    break;
                case 3:
                    brush = new SolidColorBrush(System.Windows.Media.Colors.Purple);
                    break;
                case 4:
                    brush = new SolidColorBrush(System.Windows.Media.Colors.Yellow);
                    break;
                case 5:
                    brush = new SolidColorBrush(System.Windows.Media.Colors.Magenta);
                    break;
                case 6:
                    brush = new SolidColorBrush(System.Windows.Media.Colors.Red);
                    break;
                default:
                    break;
            }
            theRect.Fill = brush;

            theRect.PreviewMouseLeftButtonDown += List_PreviewMouseLeftButtonDown;
            theRect.PreviewMouseLeftButtonUp += List_PreviewMouseLeftButtonUp;
            theRect.PreviewMouseMove += List_MouseMove;

            theRect.HorizontalAlignment = HorizontalAlignment.Left;
            theRect.VerticalAlignment = VerticalAlignment.Top;

            theRect.Height = width;
            theRect.Width = height;
            return theRect;
        }
        private void List_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Store the mouse position
            startPoint = e.GetPosition(null);
            Debug.WriteLine(startPoint);

            Rectangle ctverec = (Rectangle)sender;
            Panel.SetZIndex(ctverec, indexCounter++);
            Moving = true;
        }
        private void List_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Store the mouse position
            startPoint = e.GetPosition(null);
            Debug.WriteLine(startPoint);
            Moving = false;
        }
        private void List_MouseMove(object sender, MouseEventArgs e)
        {
            // Get the current mouse position
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;
            Rectangle ctverec = (Rectangle)sender;
            //Panel ctverecPanel = (Panel)sender;
            if (Moving)
            {
                Thickness margin = ctverec.Margin;
                margin.Left = mousePos.X - ctverec.Width/2;
                margin.Top = mousePos.Y - ctverec.Height/2;
                ctverec.Margin = margin;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Ctverec> seznamCtvercu = new List<Ctverec>();
            var engine = new FileHelperEngine<Ctverec>();
            var result = engine.ReadFile(path);
            foreach (Object ctverec in Board.Children)
            {
                if(ctverec is Rectangle)
                {
                    Rectangle ctverecxd = (Rectangle)ctverec;
                    seznamCtvercu.Add(new Ctverec((int)ctverecxd.Width, (int)ctverecxd.Height, (int)ctverecxd.Margin.Left, (int)ctverecxd.Margin.Top));

                }
            }
            engine.WriteFile(path, seznamCtvercu);
        }
    }
}