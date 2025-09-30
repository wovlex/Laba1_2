using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;

namespace Laba1_2
{
    internal class Class2
    {
    }
    public class CIcon
    {
        private Point position;
        private string name;
        private Rectangle icon;
        private int width;
        private int height;

        // Свойства для доступа к данным
        public Point Position => position;
        public string Name => name;
        public Rectangle Icon => icon;

        public CIcon(int iconWidth, int iconHeight, string imagePath)
        {
            width = iconWidth;
            height = iconHeight;
            CreateIcon(iconWidth, iconHeight, imagePath);
        }
        public void CreateIcon(int iconWidth, int iconHeight, string imagePath)
        {
            position = new Point(0, 0);
            name = System.IO.Path.GetFileNameWithoutExtension(imagePath);
            icon = new Rectangle();
            //установка цвета линии обводки и цвета заливки при помощи коллекции кистей
            icon.Stroke = Brushes.Black;
            ImageBrush ib = new ImageBrush();
            //позиция изображения будет указана как координаты левого верхнего угла
            //изображение будет растянуто по размерам прямоугольника, описанного вокруг фигуры
            ib.AlignmentX = AlignmentX.Left;
            ib.AlignmentY = AlignmentY.Top;
            //загрузка изображения и назначение кисти
            ib.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
            icon.RenderTransform = new TranslateTransform(position.X, position.Y);
            icon.Fill = ib;
            //параметры выравнивания
            icon.HorizontalAlignment = HorizontalAlignment.Left;
            icon.VerticalAlignment = VerticalAlignment.Center;
            //размеры прямоугольника
            icon.Height = iconHeight;
            icon.Width = iconWidth;
        }

    }


}





