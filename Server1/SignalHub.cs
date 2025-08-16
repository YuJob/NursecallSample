using Microsoft.AspNetCore.SignalR;

namespace Server1
{
    //SignalR のハブで、サーバーとクライアントの間の リアルタイム通信の中心
    public class SignalHub : Hub<IsignalClient>
    {
        //ASP.NET Core の依存性注入（DI）でロガーを取得
        //サーバー側でログ出力できるようにする
        private readonly ILogger<SignalHub> _logger;

        public SignalHub(ILogger<SignalHub> logger)
        {
            _logger = logger;
        }
        //クライアントからのメッセージ受信
        public async Task SendMessage(string user, string message, MessageData messageData)//送信者の名前と送信されたメッセージ内容
        {
            _logger.LogInformation($"Client \"{user}\" call \"{message}\"."); //ログ出力
            await Clients.All.ReceiveMessage(user, message, messageData);
        }
    }
}
