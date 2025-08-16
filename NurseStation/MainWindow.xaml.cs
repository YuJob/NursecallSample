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
using System.Windows.Threading;

namespace NurseStation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        //サーバーのエンドポイント URL
        static readonly string HubUrl = "http://localhost:5151/Signal";
        //サーバーとの通信を管理する
        HubConnection? connection = null;

        private DispatcherTimer timer1;
        private DispatcherTimer timer2;
        private DispatcherTimer timer3;
        private DispatcherTimer timer4;
        private DispatcherTimer timer5;
        private DispatcherTimer timer6;
        private bool LongPressed1 = false;
        private bool LongPressd2 = false;
        private bool LongPressd3 = false;
        private bool LongPressd4 = false;
        private bool LongPressd5 = false;
        private bool LongPressd6 = false;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            timer1 = new DispatcherTimer();
            timer1.Interval = new TimeSpan(0,0,1);
            timer1.Tick += Timer1_Tick;

            timer2 = new DispatcherTimer();
            timer2.Interval = new TimeSpan(0, 0, 1);
            timer2.Tick += Timer2_Tick;

            timer3 = new DispatcherTimer();
            timer3.Interval = new TimeSpan(0, 0, 1);
            timer3.Tick += Timer3_Tick;

            timer4 = new DispatcherTimer();
            timer4.Interval = new TimeSpan(0, 0, 1);
            timer4.Tick += Timer4_Tick;

            timer5 = new DispatcherTimer();
            timer5.Interval = new TimeSpan(0, 0, 1);
            timer5.Tick += Timer5_Tick;

            timer6 = new DispatcherTimer();
            timer6.Interval = new TimeSpan(0, 0, 1);
            timer6.Tick += Timer6_Tick ;

        }

        private void Timer6_Tick(object? sender, EventArgs e)
        {
            timer6.Stop();
            LongPressd6 = true;
            button6.Background = Brushes.White;
            image6.Visibility = Visibility.Hidden;
            image12.Visibility = Visibility.Hidden;
        }

        private void Timer5_Tick(object? sender, EventArgs e)
        {
            timer5.Stop();
            LongPressd5 = true;
            button5.Background = Brushes.White;
            image5.Visibility = Visibility.Hidden;
            image11.Visibility = Visibility.Hidden;
        }

        private void Timer4_Tick(object? sender, EventArgs e)
        {
            timer4.Stop();
            LongPressd4 = true;
            button4.Background = Brushes.White;
            image4.Visibility = Visibility.Hidden;
            image10.Visibility = Visibility.Hidden;
        }

        private void Timer3_Tick(object? sender, EventArgs e)
        {
            timer3.Stop();
            LongPressd3 = true;
            button3.Background = Brushes.White;
            image3.Visibility = Visibility.Hidden;
            image9.Visibility = Visibility.Hidden;
        }

        private void Timer2_Tick(object? sender, EventArgs e)
        {
            timer2.Stop();
            LongPressd2 = true;
            button2.Background = Brushes.White;
            image2.Visibility = Visibility.Hidden;
            image8.Visibility = Visibility.Hidden;
        }

        private void Timer1_Tick(object? sender, EventArgs e)
        {
            timer1.Stop();
            LongPressed1 = true;
                button1.Background = Brushes.White;
                image1.Visibility = Visibility.Hidden;
                image7.Visibility = Visibility.Hidden;
            
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
                if (message == "接続完了")
                    Dispatcher.Invoke(() => { button1.Foreground = Brushes.Black; button1.Content = user; });
                else if (message == "緊急")
                    Dispatcher.Invoke(() => { button1.Background = Brushes.Red; });
                else if (message == "トイレ")
                    Dispatcher.Invoke(() => { image1.Visibility = Visibility.Visible; });
                else if (message == "その他")
                    Dispatcher.Invoke(() => { image7.Visibility = Visibility.Visible; });
            }
            else if (user.Contains("101-2"))
            {
                if (message == "接続完了")
                    Dispatcher.Invoke(() => { button2.Foreground = Brushes.Black;button2.Content = user; });
                else if (message == "緊急")
                    Dispatcher.Invoke(() => { button2.Background = Brushes.Red; });
                else if (message == "トイレ")
                    Dispatcher.Invoke(() => { image2.Visibility = Visibility.Visible; });
                else if (message == "その他")
                    Dispatcher.Invoke(() => { image8.Visibility = Visibility.Visible; });
            }
            else if (user.Contains("101-3"))
            {
                if (message == "接続完了")
                    Dispatcher.Invoke(() => { button3.Foreground = Brushes.Black; button3.Content = user; });
                else if (message == "緊急")
                    Dispatcher.Invoke(() => { button3.Background = Brushes.Red; });
                else if (message == "トイレ")
                    Dispatcher.Invoke(() => { image3.Visibility = Visibility.Visible; });
                else if (message == "その他")
                    Dispatcher.Invoke(() => { image9.Visibility = Visibility.Visible; });
            }
            else if (user.Contains("101-4"))
            {
                if (message == "接続完了")
                    Dispatcher.Invoke(() => { button4.Foreground = Brushes.Black; button4.Content = user; });
                else if (message == "緊急")
                    Dispatcher.Invoke(() => { button4.Background = Brushes.Red; });
                else if (message == "トイレ")
                    Dispatcher.Invoke(() => { image4.Visibility = Visibility.Visible; });
                else if (message == "その他")
                    Dispatcher.Invoke(() => { image10.Visibility = Visibility.Visible; });
            }
            else if (user.Contains("102-1"))
            {
                if (message == "接続完了")
                    Dispatcher.Invoke(() => { button5.Foreground = Brushes.Black; button5.Content = user; });
                else if (message == "緊急")
                    Dispatcher.Invoke(() => { button5.Background = Brushes.Red; });
                else if (message == "トイレ")
                    Dispatcher.Invoke(() => { image5.Visibility = Visibility.Visible; });
                else if (message == "その他")
                    Dispatcher.Invoke(() => { image11.Visibility = Visibility.Visible; });
            }
            else if (user.Contains("102-2"))
            {
                if (message == "接続完了")
                    Dispatcher.Invoke(() => { button6.Foreground = Brushes.Black; button6.Content = user; });
                else if (message == "緊急")
                    Dispatcher.Invoke(() => { button6.Background = Brushes.Red; });
                else if (message == "トイレ")
                    Dispatcher.Invoke(() => { image6.Visibility = Visibility.Visible; });
                else if (message == "その他")
                    Dispatcher.Invoke(() => { image12.Visibility = Visibility.Visible; });
            }
        }

        private static MainWindow? _Instance;//画面遷移
        public static MainWindow GetInstance()
        {
            _Instance ??= new();
            return _Instance;
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

        private void button1_PreviewMouseDown(object sender, MouseButtonEventArgs e)//解除
        {
            LongPressed1 = false;
            timer1.Start();
        }

        private void button1_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            timer1.Stop();
            if (LongPressed1==false)//その他の中身
            {
                GetInstance().Hide();
                NurseStation2.GetInstance().Show();
            }
        }

        private void button2_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            LongPressd2 = false;
            timer2.Start();
        }

        private void button2_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            timer2.Stop();
            if (LongPressd2==false)//その他の中身
            {
                GetInstance().Hide();
                NurseStation2_2.GetInstance().Show();
            }
        }

        private void button3_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            LongPressd3 = false;
            timer3.Start();
        }

        private void button3_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            timer3.Stop();
            if (LongPressd3==false)//その他の中身
            {
                GetInstance().Hide();
                NurseSation2_3.GetInstance().Show();
            }
        }

        private void button4_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            LongPressd4 = false;
            timer4.Start();
        }

        private void button4_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            timer4.Stop();
            if (LongPressd4 == false)//その他の中身
            {
                GetInstance().Hide();
                NurseSation2_4.GetInstance().Show();
            }
        }

        private void button5_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            LongPressd5 = false;
            timer5.Start();
        }

        private void button5_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            timer5.Stop();
            if (LongPressd5 == false)//その他の中身
            {
                GetInstance().Hide();
                NurseSation2_5.GetInstance().Show();
            }
        }
        private void button6_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            LongPressd6 = false;
            timer6.Start();
        }
        private void button6_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            timer6.Stop();
            if (LongPressd6 == false)//その他の中身
            {
                GetInstance().Hide();
                NurseSation2_6.GetInstance().Show();
            }
        }

    }
}
