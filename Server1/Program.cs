using Server1;


//Web サーバーを起動するための準備オブジェクトを作る
var builder = WebApplication.CreateBuilder(args);

//DI コンテナに SignalR サーバー機能を追加
builder.Services.AddSignalR();

//サーバーアプリのインスタンス構築
var app = builder.Build();

// ルーティングミドルウェア有効化
app.UseRouting();

//WPF クライアントはこの URLに接続してくる
app.MapHub<SignalHub>("/Signal");

app.Run();//サーバー起動
