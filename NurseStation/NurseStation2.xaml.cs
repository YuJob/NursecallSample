using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Reflection.Emit;
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

namespace NurseStation
{
    /// <summary>
    /// NurseStation2.xaml の相互作用ロジック
    /// </summary>
    public partial class NurseStation2 : Window, INotifyPropertyChanged
    {
        //サーバーのエンドポイント URL
        static readonly string HubUrl = "http://localhost:5151/Signal";
        //サーバーとの通信を管理する
        HubConnection? connection = null;



        public NurseStation2()
        {
            InitializeComponent();
            this.DataContext = this;

            User = label3.Content.ToString();
            messageData = new MessageData();


        }
        private static NurseStation2? _Instance;//画面遷移
        public static NurseStation2 GetInstance()
        {
            _Instance ??= new();
            return _Instance;
        }


        
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 起動時にサーバーアプリへの接続を確立する．
            try
            {
                ConnectionAvaliable = false;

                var conn = new HubConnectionBuilder()
                    .WithUrl(HubUrl)
                    .Build();

                this.connection = conn;

                // サーバーアプリからのメッセージを受信したときのハンドラを登録する
                this.connection.On<string, string, MessageData>("ReceiveMessage", OnReceiveMessage);
                await this.connection.StartAsync(); //サーバーに接続

                ConnectionAvaliable = true;

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"failed to connect to \"{HubUrl}\"");
            }
        }
        private void OnReceiveMessage(string user, string message, MessageData messageData)//受信（ナースステーション）
        {
            if (user.Contains("101-1"))
            {
                if (message == "その他")
                    Dispatcher.Invoke(() => {
                        textBox1.Text = messageData.Question;
                        checkBox1.IsChecked = messageData.Bath;
                        if (messageData.Bath == true)
                        {
                            checkBox1.Foreground = Brushes.Red;
                        }
                        else checkBox1.Foreground = Brushes.Black;

                        checkBox2.IsChecked = messageData.Eat;
                        if (messageData.Eat == true)
                        {
                            checkBox2.Foreground = Brushes.Red;
                        }
                        else checkBox2.Foreground = Brushes.Black;

                        checkBox3.IsChecked = messageData.Move;
                        if (messageData.Move == true)
                        {
                            checkBox3.Foreground = Brushes.Red;
                        }
                        else checkBox3.Foreground = Brushes.Black;

                        checkBox4.IsChecked = messageData.Drip;
                        if (messageData.Drip == true)
                        {
                            checkBox4.Foreground = Brushes.Red;
                        }
                        else checkBox4.Foreground = Brushes.Black;

                        checkBox5.IsChecked = messageData.Drug;
                        if (messageData.Drug == true)
                        {
                            checkBox5.Foreground = Brushes.Red;
                        }
                        else checkBox5.Foreground = Brushes.Black;
                        slider1.Value = messageData.Mind;
                    });

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
