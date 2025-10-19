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
        private CIconList iniicon;
        private CEnemyTemplateList enemyList;
        public MainWindow()
        {
            InitializeComponent();
            enemyList = new CEnemyTemplateList();
            UpdateEnemyListBox();
            
            InitializeIconSystem();
            Listof.SelectionChanged += Listof_SelectionChanged;
        }

        private void InitializeIconSystem()
        {

            iniicon = new CIconList(128, 128, 3, 10); 

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string imagesPath = System.IO.Path.Combine(basePath, "icons/Monsters");

            if (Directory.Exists(imagesPath))
            {
                iniicon.Load(imagesPath);
                DisplayIcons();
            }
            else
            {
                MessageBox.Show("Папка с иконками не найдена", "Ошибка");
            }
        }
   

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
            if (enemy == null) return; 

            _name.Text = enemy.Name;
            _iconName.Text = enemy.IconName;
            _life.Text = enemy.BaseLife.ToString();
            _lifeMod.Text = enemy.LifeModifier.ToString();
            _gold.Text = enemy.BaseGold.ToString();
            _goldMod.Text = enemy.GoldModifier.ToString();
            _spawn.Text = enemy.SpawnChance.ToString();

            string icon_name = enemy.IconName.Replace(".png", "");
            CIcon iconnn = iniicon.findByName(icon_name);


            if (iconnn != null)
            {
                
                enemyImage.Source = new BitmapImage(new Uri(iconnn.Path));

            }



        }

        private void btvRemove_Click(object sender, RoutedEventArgs e)
        {
            if (Listof.SelectedItem is CEnemyTemplate selectedEnemy)
            {
                enemyList.RemoveEnemy(selectedEnemy);
                UpdateEnemyListBox();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Выберите противника для удаления");
            }
        }



        private void btvAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               var enemy1 = enemyImage.Source;

                iconsListBox.Items.Refresh();
                var enemy = new CEnemyTemplate(
                    _name.Text,
                    _iconName.Text,
                    int.Parse(_life.Text),
                    double.Parse(_lifeMod.Text),
                    int.Parse(_gold.Text),
                    double.Parse(_goldMod.Text),
                    double.Parse(_spawn.Text)
                );



                enemyList.AddEnemy(enemy);
                UpdateEnemyListBox();
                ClearForm();
            }
            catch (FormatException)
            {
                MessageBox.Show("Проверьте правильность введенных числовых значений");
            }
        }

        private void btvSave(object sender, RoutedEventArgs e)
        {
            //создание диалога
            OpenFileDialog dlg = new OpenFileDialog();
            //настройка параметров диалога
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".json"; // Default file extension
            dlg.Filter = "JSON files (.json)|*.json"; // Filter files by extension
                                                      //вызов диалога
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    // Загружаем список противников
                    enemyList.LoadFromFile(dlg.FileName);
                    UpdateEnemyListBox();
                    
                }
                catch (Exception ex)
                {
                   
                }
            }

        }

        private void btvLoad(object sender, RoutedEventArgs e)
        {
            {
                //создание диалога
                SaveFileDialog dlg = new SaveFileDialog();
                //настройка параметров диалога
                dlg.FileName = "Document"; // Default file name
                dlg.DefaultExt = ".json"; // Default file extension
                dlg.Filter = "JSON files (.json)|*.json"; // Filter files by extension
                                                          //вызов диалога
                if (dlg.ShowDialog() == true)
                {
                    try
                    {
                        // Сохраняем список противников
                        enemyList.SaveToFile(dlg.FileName);
                       
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
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

        private void UpdateEnemyListBox()
        {
            Listof.Items.Clear();
            foreach (var enemy in enemyList.GetEnemies())
            {
                Listof.Items.Add(enemy);
            }
        }

        private void ClearForm()
        {
            _name.Text = "";
            _iconName.Text = "";
            _life.Text = "0";
            _lifeMod.Text = "0";
            _gold.Text = "0";
            _goldMod.Text = "0";
            _spawn.Text = "0";
         
        }

        private void Listof_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Listof.SelectedItem is CEnemyTemplate selectedEnemy)
            {
                DisplayEnemyInfo(selectedEnemy);
            }
        }

       

        public void DisplayIcons() 
        {
            var icons = iniicon.GetIcons();
            foreach (var icon in icons)
            {
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(icon.Path));
                image.Tag = icon.Name;
                iconsListBox.Items.Add(image);
                
            }
        }
     

        private void LoadIcons()
        {
            string[] iconNames = { "Sword", "Axe", "Bow", "Staff" };
            Color[] colors = { Colors.Red, Colors.Blue, Colors.Green, Colors.Orange };

            double x = 10;
            double y = 10;

            for (int i = 0; i < iconNames.Length; i++)
            {
                Image img = new Image
                {
                    Width = 50,
                    Height = 50,
                    Source = new BitmapImage(new Uri("путь_к_иконке")), // здесь у вас должен быть путь к изображению
                    Tag = iconNames[i] // сохраняем название
                };
                iconsListBox.Items.Add(img);
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

                

                // Сдвигаем позицию для следующей иконки
                x += 60;
            }

          
        }

        private void iconsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            if (iconsListBox.SelectedItem is Image selectedImage)
            {
                enemyImage.Source = selectedImage.Source;
                if (selectedImage.Tag != null)
                {
                    _iconName.Text = selectedImage.Tag.ToString();
                }
            }



        }

        private void _iconName_TextChanged(object sender, TextChangedEventArgs e)

        {





        }
    } 
}