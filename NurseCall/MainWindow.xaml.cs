using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged //プロパティ変更通知を行う
    {
        //サーバーのエンドポイント URL
        static readonly string HubUrl = "http://localhost:5151/Signal";
        //サーバーとの通信を管理する
        HubConnection? connection = null;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            User= label1.Content.ToString();
            messageData = new MessageData();//インスタンス生成
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 起動時にサーバーアプリへの接続を確立する．
            try
            {
                ConnectionAvaliable = false;//接続していない時は、操作不可

                var conn = new HubConnectionBuilder()
                    .WithUrl(HubUrl)
                    .Build();

                this.connection = conn;


                await this.connection.StartAsync(); //サーバーに接続

                ConnectionAvaliable = true;
                Message = "接続完了";
                await this.connection.InvokeAsync("SendMessage", User, Message, messageData);//送る

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"failed to connect to \"{HubUrl}\"");
            }

        }
        private async void button1_Click(object sender, RoutedEventArgs e)//緊急
        {
            MessageBox.Show("ナースステーションへ通知しました", "通知", MessageBoxButton.OKCancel, MessageBoxImage.Information);

            Message = "緊急";
            await this.connection.InvokeAsync("SendMessage", User, Message, messageData);//送る
        }

        private async void button3_Click(object sender, RoutedEventArgs e)//トイレ
        {
            MessageBox.Show("ナースステーションへ通知しました", "通知", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            Message = "トイレ";
            await this.connection.InvokeAsync("SendMessage", User, Message, messageData);//送る
        }

        private static MainWindow? _Instance;//画面遷移
        public static MainWindow GetInstance()
        {
            _Instance ??= new();
            return _Instance;
        }

        private void button2_Click(object sender, RoutedEventArgs e)//その他ボタン
        {
            Application.Current.Properties["username"] = User;//その他ウィンドウへ名前受け渡し
            GetInstance().Hide();
            NurseCall2.GetInstance().Show();
            
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

        private void SetProperty<T>(ref T backingField, T value, [CallerMemberName] string prop = "")//自動で画面を更新できるようにする
        {
            backingField = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler? PropertyChanged;//プロパティ（状態）が変化した
    }
}