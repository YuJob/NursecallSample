using Server1;


//Web �T�[�o�[���N�����邽�߂̏����I�u�W�F�N�g�����
var builder = WebApplication.CreateBuilder(args);

//DI �R���e�i�� SignalR �T�[�o�[�@�\��ǉ�
builder.Services.AddSignalR();

//�T�[�o�[�A�v���̃C���X�^���X�\�z
var app = builder.Build();

// ���[�e�B���O�~�h���E�F�A�L����
app.UseRouting();

//WPF �N���C�A���g�͂��� URL�ɐڑ����Ă���
app.MapHub<SignalHub>("/Signal");

app.Run();//�T�[�o�[�N��
