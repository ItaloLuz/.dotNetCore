using MimeKit;

namespace TodoApi.Services
{
    public interface IEmailService
    {
        bool Enviar(string emailDestinatario, string mensagemHTML, string assunto);
        bool Enviar(string emailRemetente, string emailDestinatario, string mensagemHTML, string assunto);
        string getTeste();
    }
}