﻿using System;
using MimeKit;
using MailKit.Net.Smtp;

namespace TodoApi.Services
{
    public class EmailService : IEmailService
    {
        const string DEFAULT_EMAIL_HOST = "smtp.office365.com";
        const int DEFAULT_EMAIL_PORT = 587;
        const bool DEFAULT_SSL_REQUIRE = false;
        const string DEFAULT_EMAIL_USER = "italo.luz@softplan.com.br";
        const string DEFAULT_EMAIL_PASSWORD = "sKYLINE321";
        const string DEFAULT_FROM_EMAIL_ADDRESS = "italo.luz@softplan.com.br";

        private SmtpClient _smtpClient;
        private string _host;
        private int _porta;
        private bool _requerSSL;
        private string _usuarioAutenticacao;
        private string _senhaAutenticacao;

        public EmailService()
        {
            _smtpClient = new SmtpClient();
            _host = DEFAULT_EMAIL_HOST;
            _porta = DEFAULT_EMAIL_PORT;
            _requerSSL = DEFAULT_SSL_REQUIRE;
            _usuarioAutenticacao = DEFAULT_EMAIL_USER;
            _senhaAutenticacao = DEFAULT_EMAIL_PASSWORD;
        }

        public EmailService(string host,int porta, bool requerSSL, string usuarioAutenticacao, string senhaAutenticacao)
        {
            _smtpClient = new SmtpClient();
            _host = host;
            _porta = porta;
            _requerSSL = requerSSL;
            _usuarioAutenticacao = usuarioAutenticacao;
            _senhaAutenticacao = senhaAutenticacao;
        }

        public bool Enviar(string emailDestinatario, string mensagemHTML, string assunto)
        {
            return Enviar(DEFAULT_FROM_EMAIL_ADDRESS, emailDestinatario, mensagemHTML, assunto);
        }

        public bool Enviar(string emailRemetente, string emailDestinatario, string mensagemHTML, string assunto)
        {
            try
            {
                _smtpClient.Connect(_host, _porta, _requerSSL);
                _smtpClient.Authenticate(_usuarioAutenticacao, _senhaAutenticacao);
                _smtpClient.Send(getMimeMessage(mensagemHTML, emailRemetente, emailDestinatario, assunto));
                _smtpClient.Disconnect(true);
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private MimeMessage getMimeMessage(string htmlMessage, string remente, string destinatario, string assunto)
        {
            BodyBuilder htmlBody = new BodyBuilder();
            htmlBody.HtmlBody = htmlMessage;

            var mensagem = new MimeMessage();
            mensagem.From.Add(new MailboxAddress("", remente));
            mensagem.To.Add(new MailboxAddress("", destinatario));
            mensagem.Subject = assunto;
            mensagem.Body = htmlBody.ToMessageBody();
            return mensagem;
        }

        public string getTeste()
        {
            return "teste";
        }
    }
}