using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace NurseCall
{
    /// <summary>
    /// NurseCall2.xaml の相互作用ロジック
    /// </summary>
    public partial class NurseCall2 : Window, INotifyPropertyChanged
    {
        //サーバーのエンドポイント URL
        static readonly string HubUrl = "http://localhost:5151/Signal";
        //サーバーとの通信を管理する
        HubConnection? connection = null;

        public NurseCall2()
        {
            InitializeComponent();
            this.DataContext = this;

            messageData = new MessageData();
        }
        private static NurseCall2? _Instance;//画面遷移
        public static NurseCall2 GetInstance()
        {
            _Instance ??= new();
            return _Instance;
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
                string username = Application.Current.Properties["username"]?.ToString() ?? "";
                User = username;

            // 起動時にサーバーアプリへの接続を確立する．
            try
            {
                ConnectionAvaliable = false;

                var conn = new HubConnectionBuilder()
                    .WithUrl(HubUrl)
                    .Build();

                this.connection = conn;


                await this.connection.StartAsync(); //サーバーに接続

                ConnectionAvaliable = true;

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"failed to connect to \"{HubUrl}\"");
            }
        }
        private void button1_Click(object sender, RoutedEventArgs e)//戻る
        {
            GetInstance().Hide();
            MainWindow.GetInstance().Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void button2_Click(object sender, RoutedEventArgs e)//送信
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) || checkBox1.IsChecked == true || checkBox2.IsChecked == true || checkBox3.IsChecked == true || checkBox4.IsChecked == true || checkBox5.IsChecked == true || slider1.Value != 50)
            {
                MessageBox.Show("ナースステーションへ通知しました", "通知", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                Message = "その他";
                messageData.Question = textBox1.Text;
                messageData.Bath = checkBox1.IsChecked;
                messageData.Eat = checkBox2.IsChecked;
                messageData.Move = checkBox3.IsChecked;
                messageData.Drip = checkBox4.IsChecked;
                messageData.Drug = checkBox5.IsChecked;
                messageData.Mind = slider1.Value;
                await this.connection.InvokeAsync("SendMessage", User, Message, messageData);//送る
            }

        }

        private string user = "";
        public string User
        {
            get { return user; }
            set { SetProperty(ref user, value); }
        }

        private string message = "";
        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }
        public MessageData messageData;

        private bool connectionAvaliable = false;

        public bool ConnectionAvaliable
        {
            get { return connectionAvaliable; }
            set { SetProperty(ref connectionAvaliable, value); }
        }

        private void SetProperty<T>(ref T backingField, T value, [CallerMemberName] string prop = "")
        {
            backingField = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}