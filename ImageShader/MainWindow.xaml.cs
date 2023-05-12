using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using ImageShader.ViewModel;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MessageBox = System.Windows.MessageBox;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private MainVm vm = null;
        
        public MainWindow()
        {
            InitializeComponent();
            vm = new MainVm();
            this.DataContext = vm;

        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择文件";
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "jpg文件|*.jpg|png文件|*.png|bmp文件|*.bmp|所有文件|*.*";
            if ((bool)openFileDialog.ShowDialog())
                return; //被点了取消
            string filename = openFileDialog.FileName;
            BitmapImage bit2 = new BitmapImage();
            bit2.BeginInit();
            bit2.UriSource = new Uri(filename);
            bit2.CacheOption = BitmapCacheOption.OnLoad;
            bit2.EndInit();
            bit2.Freeze();
            ImageControl.Source = bit2;
            ImageControl.Effect = null;
            vm.Background = Readjson(Path.GetDirectoryName(filename) + "\\" + Path.GetFileNameWithoutExtension(filename) + ".json", "Background");
            // 压缩图片的大小
            vm.size2 = new FileInfo(filename).Length / 1024;
        }

        private void AddBack_Click(object sender, RoutedEventArgs e)
        {
            // 颜色
            string colorString = colorName.Text;
            Byte r, g, b;
            try
            {
                if (colorString.Length == 7)
                {
                    r = Convert.ToByte(colorString.Substring(1, 2), 16);
                    g = Convert.ToByte(colorString.Substring(3, 2), 16);
                    b = Convert.ToByte(colorString.Substring(5, 2), 16);
                }
                else if (colorString.Length == 9)
                {
                    r = Convert.ToByte(colorString.Substring(3, 2), 16);
                    g = Convert.ToByte(colorString.Substring(5, 2), 16);
                    b = Convert.ToByte(colorString.Substring(7, 2), 16);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("没有这种颜色！");
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择文件";
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "jpg文件|*.jpg|png文件|*.png|bmp文件|*.bmp|所有文件|*.*";
            if (!(bool)openFileDialog.ShowDialog())
            {
                return; //被点了取消
            }
            string filename = openFileDialog.FileName;
            System.Windows.Controls.Image im = new System.Windows.Controls.Image();
            im.Source = new BitmapImage(new Uri(filename));
            VisualBrush vb = new VisualBrush();
            vb.Visual = im;
            
            vm.eff.Input2 = vb;
            vm.eff.ColorKey = System.Windows.Media.Color.FromArgb(255, r, g, b);
            if (r >= g && r >= b)
                vm.eff.MaxColor = 1;
            else if (g >= r && g >= b)
                vm.eff.MaxColor = 2;
            else if(b >= g && b >= r)
                vm.eff.MaxColor = 3;
            
            ImageControl.Effect = vm.eff;
        }

        public static string? Readjson(string jsonFile, string key)
        {
            try
            {
                using StreamReader file = File.OpenText(jsonFile);
                using JsonTextReader reader = new JsonTextReader(file);
                JObject o = (JObject)JToken.ReadFrom(reader);
                var value = o[key]?.ToString();
                return value;
            }
            catch/* (Exception e)*/
            {
                //Console.WriteLine(e);
                //throw;
            }
        }

        private void RefreshShader_Click(object sender, RoutedEventArgs e)
        {
            vm.eff.Init();
            ImageControl.Effect = vm.eff;
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string filepath = "/Resource/" + vm.ResourceNameItem;
            BitmapImage bit2 = new BitmapImage();
            bit2.BeginInit();
            bit2.UriSource = new Uri("pack://application:,,," + filepath);
            bit2.CacheOption = BitmapCacheOption.OnLoad;
            bit2.EndInit();
            bit2.Freeze();
            ImageControl.Source = bit2;
            ImageControl.Effect = null;

            string filename = vm.ResourceNameItem.Split(".")[0];
            vm.Background = Readjson(".\\json\\" + filename + ".json", "Background");
        }
    }

}
