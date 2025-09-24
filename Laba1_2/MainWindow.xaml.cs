using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.Win32;
using System.Runtime.CompilerServices;

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
            lb1.Content = dlg.FileName;

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
                lb1.Content = dlg.FileName;
            }
        }
    }

    public class Person
    {
        [JsonInclude] //Атрибут будет сохранен в json
        int age;
        [JsonInclude]
        string first_name;
        [JsonInclude]
        string second_name;
        [JsonInclude]
        double height;

        public Person(int Age, string FName, string SName, double Height)
        {
            age = Age;
            first_name = FName;
            second_name = SName;
            height = Height;
        }

        public int getAge() { return age; }
        public string getFirstName() { return first_name; }
        public string getSecondName() { return second_name; }
        public double getHeight() { return height; }
    }

    internal class Program
    {
        [STAThread] // Добавьте этот атрибут для WPF приложений
        static void Main(string[] args)
        {
            // Тестирование сериализации
            TestSerialization();

            // Тестирование десериализации
            TestDeserialization();

            // Запуск WPF приложения
            var app = new Application();
            app.Run(new MainWindow());
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

        static void TestDeserialization()
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
                Console.WriteLine($"Age: {person.getAge()}, Name: {person.getFirstName()} {person.getSecondName()}, Height: {person.getHeight()}");
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
                Console.WriteLine($"Найдено изображение: {file}"); //в file содержится путь до изображения с расширением .png
            }
        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //получение координат мыши в координатах объекта Canvas с именем scene
            Point mousePosition = Mouse.GetPosition(scene);

        }
       


    }
}