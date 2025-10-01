using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Laba1_2
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();




        }

        private void InitializeIconSystem()
        {

            iniicon = new CIconList(128, 128, 1, 1);

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string imagesPath = System.IO.Path.Combine(basePath, "icons/Monsters");

            if (Directory.Exists(imagesPath))
            {
                iniicon.Load(imagesPath);
                DisplayIcons();
            }
            else
            {
                MessageBox.Show("Папка с иконками не найдена", "ЧТО-ТО НЕ ТАК!!!");
            }
        }
        public IniIconManager iniicon;

        public class IniIconManager
        {
            private List<CIcon> icons = new List<CIcon>();
            public CIcon findByName(string name)
            {
                return icons.FirstOrDefault(icon => icon.Name == name);

            }
        }
        public void DisplayEnemyInfo(CEnemyTemplate enemy)
        {
            _name.Text = enemy.Name;
            _iconName.Text = enemy.IconName;
            _life.Text = enemy.BaseLife.ToString();
            _lifeMod.Text = enemy.LifeModifier.ToString();
            _gold.Text = enemy.BaseGold.ToString();
            _goldMod.Text = enemy.GoldModifier.ToString();
            _spawn.Text = enemy.SpawnChance.ToString();

            string icon_name = enemy.IconName.Replace(".png", "");

            CIcon iconnn = iniicon.findByName(icon_name);



            scene.Children.Clear();



            if (iconnn != null)
            {
                Rectangle samaicon = iconnn.getIcon();
                Rectangle twoiconss = new Rectangle
                {
                    Width = samaicon.Width,
                    Height = samaicon.Height,
                    Fill = samaicon.Fill
                };

                scene.Children.Add(twoiconss);
            }



        }

        static void TestSerialization()
        {
            // Создаем список экземпляров класса Person
            List<Person> people = new List<Person>();
            people.Add(new Person(20, "Ivan", "Ivanov", 177.65));
            people.Add(new Person(30, "Aleksey", "Alekseevich", 166.99));
            people.Add(new Person(25, "Oleg", "Olegovich", 180.01));

            // Сериализация списка в JSON
            string jsonString = JsonSerializer.Serialize(people);

            // Сохранение JSON в файл
            File.WriteAllText("people.json", jsonString);

            Console.WriteLine("Сериализация завершена. Файл people.json создан.");
        }

        private void btvRemove_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btvAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btvSave(object sender, RoutedEventArgs e)
        {
            //создание диалога
            OpenFileDialog dlg = new OpenFileDialog();
            //настройка параметров диалога
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension
                                                        //вызов диалога
            dlg.ShowDialog();
            //получение выбранного имени файла
            //lb1.Content = dlg.FileName;

        }

        private void btvLoad(object sender, RoutedEventArgs e)
        {
            {
                //создание диалога
                SaveFileDialog dlg = new SaveFileDialog();
                //настройка параметров диалога
                dlg.FileName = "Document"; // Default file name
                dlg.DefaultExt = ".txt"; // Default file extension
                dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension
                                                            //вызов диалога
                dlg.ShowDialog();
                //получение выбранного имени файла
                // lb1.Content = dlg.FileName;
            }
        }

        void TestDeserialization()
        {
            // Чтение JSON из файла
            string jsonFromFile = File.ReadAllText("people.json");
            List<Person> people = new List<Person>();

            // Парсинг JSON
            JsonDocument doc = JsonDocument.Parse(jsonFromFile);

            // Добавление новой записи в список класса из json
            foreach (JsonElement element in doc.RootElement.EnumerateArray())
            {
                int age = element.GetProperty("age").GetInt32();
                string firstName = element.GetProperty("first_name").GetString();
                string secondName = element.GetProperty("second_name").GetString();
                double height = element.GetProperty("height").GetDouble();

                // Создание нового экземпляра класса Person с помощью конструктора
                Person person = new Person(age, firstName, secondName, height);

                // Добавление объекта в список
                people.Add(person);
            }

            // Вывод данных на экран
            foreach (var person in people)
            {

                Listof.Items.Add($"Age: {person.getAge()}, Name: {person.getFirstName()} {person.getSecondName()}, Height: {person.getHeight()}");
            }

        }

        public void Load(string path)
        {
            //путь до папки содержащей изображения
            string folder = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + path;
            //фильтр расширения изображения
            string filter = "*.png";
            //получение массива строк содержащих пути до изображений
            string[] files = Directory.GetFiles(folder, filter);
            foreach (string file in files)
            {
                MessageBox.Show($"Найдено изображение: {file}"); //в file содержится путь до изображения с расширением .png
            }
        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //получение координат мыши в координатах объекта Canvas с именем scene
            Point mousePosition = Mouse.GetPosition(scene);
            CIcon cliIcon = iniicon.isMouseOver(mousePosition);
            if (cliIcon != null)
            {
                string iconName = cliIcon.Name();
                _iconName.Text = iconName + ".png";
                scene.Children.Clear();

                Rectangle twoiconss = new Rectangle
                {
                    Width = cliIcon.getIcon().Width,
                    Height = cliIcon.getIcon().Height,
                    Fill = cliIcon.getIcon().Fill
                };
                scene.Children.Add(twoiconss);
            }
        }
        private void LoadIcons()
        {
            // Проверяем доступность Canvas
            if (scene == null)
            {
                MessageBox.Show("Canvas 'scene' не найден!");
                return;
            }

            // Очищаем Canvas
            scene.Children.Clear();

            // Создаем простые цветные иконки
            string[] iconNames = { "Sword", "Axe", "Bow", "Staff" };
            Color[] colors = { Colors.Red, Colors.Blue, Colors.Green, Colors.Orange };

            double x = 10;
            double y = 10;

            for (int i = 0; i < iconNames.Length; i++)
            {
                // Создаем прямоугольник как иконку
                Rectangle icon = new Rectangle
                {
                    Width = 50,
                    Height = 50,
                    Fill = new SolidColorBrush(colors[i]),
                    Stroke = Brushes.Black,
                    StrokeThickness = 2,
                    Tag = iconNames[i]
                };

                // Позиционируем на Canvas
                Canvas.SetLeft(icon, x);
                Canvas.SetTop(icon, y);

                // Добавляем на Canvas
                scene.Children.Add(icon);

                // Добавляем текст с названием
                TextBlock text = new TextBlock
                {
                    Text = iconNames[i],
                    Foreground = Brushes.Black,
                    FontSize = 10,
                    Width = 50,
                    TextAlignment = TextAlignment.Center
                };

                Canvas.SetLeft(text, x);
                Canvas.SetTop(text, y + 55);

                scene.Children.Add(text);

                // Сдвигаем позицию для следующей иконки
                x += 60;
            }

            MessageBox.Show($"Добавлено {iconNames.Length} иконок на Canvas!");
        }
    } 
}