using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Laba1_2
{
    public class CIconList
    {
        private List<CIcon> icons;
        private int iconWidth;
        private int iconHeight;
        private int columns;
        private int rows;

        public CIconList(int width, int height, int cols, int rows)
        {
            icons = new List<CIcon>();
            iconWidth = width;
            iconHeight = height;
            columns = cols;
            this.rows = rows;
        }

        public void Load(string folderPath)
        {
            icons.Clear();

            if (!Directory.Exists(folderPath))
            {
                MessageBox.Show($"Папка не существует: {folderPath}");
                return;
            }

            string filter = "*.png";
            string[] files = Directory.GetFiles(folderPath, filter);

            if (files.Length == 0)
            {
                MessageBox.Show($"В папке {folderPath} не найдено PNG файлов");
                return;
            }

            double x = 10;
            double y = 10;
            int count = 0;

            foreach (string file in files)
            {
                Point position = new Point(x, y);
                CIcon icon = new CIcon(iconWidth, iconHeight, file, position);
                icons.Add(icon);

                x += iconWidth + 10;
                count++;

                if (count >= columns)
                {
                    x = 10;
                    y += iconHeight + 10;
                    count = 0;
                }
            }

            MessageBox.Show($"Загружено {icons.Count} иконок");
        }

        public CIcon findByName(string name)
        {
            return icons.Find(icon => icon.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public CIcon IsMouseOver(Point position)
        {
            foreach (var icon in icons)
            {
                if (icon.IsPointInside(position))
                {
                    return icon;
                }
            }
            return null;
        }

        public List<CIcon> GetIcons()
        {
            return icons;
        }
    }
}
