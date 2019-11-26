using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using aoponto.dados;
using aoponto.modelos;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace aoponto.servicos
{
    public class PushServico : BaseServico<Push>, IPushServico
    {
        IPushDao _pushDao;

        private class ParametrosExecucaoEnvio
        {
            public Notificacao Notificacao { get; set; }
            public string Destino { get; set; }
        }
        
        const string _apiKey = "AAAAhtnb8xQ:APA91bHuaHLOg3vLfbBBMlKXd34bX-8Lx2XBkl2hln5gcVBkt1jTAhW2a5AS-Mw2SHZVK9lij0hLnew8W8sIyycP9OHMpJKpD4AgJ7ExqklRb1JXMPfztDln7s7nX2U6hMEUyoSifx9S";
        const string _packageApp = "br.com.quadsystems.aoponto";


        public PushServico(AppDbContext contexto, IIdentificacao identificacao, IPushDao pushDao) : base (contexto, identificacao)
        {
            _pushDao = pushDao;
        }

        public Retorno Enviar(Notificacao notificacao, bool enviarParaTodos)
        {
            try
            {
                var thread = new Thread(this.ExecutarEnvio);

                var parametros = new ParametrosExecucaoEnvio() {
                    Notificacao = notificacao,
                    Destino = this.MontarDestino(notificacao, enviarParaTodos)
                };

                if (string.IsNullOrEmpty(parametros.Destino))
                {
                    return Sucesso();
                }

                thread.Start(parametros);

                return Sucesso(new Push());
            }
            catch (Exception ex)
            {
                return Erro(ex);
            }
        }

        private string MontarDestino(Notificacao notificacao, bool enviarParaTodos)
        {
            if (enviarParaTodos)
            {
                return "/topic/all";
            }

            var listaTokens = _pushDao.ObterListaTokenPushPorNotificacao(notificacao.Id);

            if (listaTokens.Any())
            {
                return string.Join(',', listaTokens);
            }
            else
            {
                return "";
            }
        }

        private void ExecutarEnvio(object parametros)
        {

            var dadosEnvioPush = parametros as ParametrosExecucaoEnvio;
            var tokensDestino = dadosEnvioPush.Destino.Split(',');
            var logPush = new List<Push>();
            var retornoEnvio = null as Retorno;
            var dadosRetorno = null as JObject;
            var statusEnvio = 0;
            var mensagemRetorno = "";

            foreach (var token in tokensDestino)
            {
                retornoEnvio = EfetuarPostFirebase(dadosEnvioPush.Notificacao, token);

                if (retornoEnvio.Sucesso)
                {
                    dadosRetorno = JObject.Parse(retornoEnvio.ObterDados<string>());
                    statusEnvio = (int)dadosRetorno["success"];

                    if (statusEnvio == 1)
                        mensagemRetorno = (string)dadosRetorno["results"][0]["message_id"];
                    else
                        mensagemRetorno = (string)dadosRetorno["results"][0]["error"];
                }
                else
                {
                    statusEnvio = 0;
                    mensagemRetorno = retornoEnvio.Mensagem;
                }
                
                logPush.Add(new Push() {
                    DataEnvio = DateTime.Now,
                    NotificacaoId = dadosEnvioPush.Notificacao.Id,
                    Destinatario = token,
                    StatusEnvio = statusEnvio,
                    MensagemRetorno = mensagemRetorno
                });
            }

            using (var contexto = AppDbContextFactory.CreateDbContext())
            {
                contexto.Pushes.AddRange(logPush);
                contexto.SaveChanges();            
            }
        }

        private Retorno EfetuarPostFirebase(Notificacao notificacao, string destino)
        {
            var retorno = Falha();

            try
            {
                var payload = this.CriarPayload(notificacao, destino, "");
                var jsonPush = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                var bytesPush = Encoding.UTF8.GetBytes(jsonPush);

                var request = HttpWebRequest.Create("https://fcm.googleapis.com/fcm/send");

                request.Method = "post";
                request.Headers.Add(string.Format("Authorization: key={0}", _apiKey));
                request.Headers.Add(string.Format("Sender: id={0}", _packageApp));
                request.ContentType = "application/json";
                request.ContentLength = bytesPush.Length;

                using (var dataStream = request.GetRequestStream())
                {
                    dataStream.Write(bytesPush, 0, bytesPush.Length);

                    using (var response = request.GetResponse())
                    {
                        using (var streamResponse = response.GetResponseStream())
                        {
                            using (var streamReader = new StreamReader(streamResponse))
                            {
                                var sResponseFromServer = streamReader.ReadToEnd();
                                retorno = Sucesso(sResponseFromServer);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                retorno = Erro(ex);
            }
            
            return retorno;
        }

        private object CriarPayload(Notificacao notificacao, string tokenPush, string corFundoIconePush)
        {
            ETipoNotificacao tipo = (ETipoNotificacao)notificacao.Tipo;

            return new
            {
                to = tokenPush,
                notification = new
                {
                    title = tipo.TituloPush(),
                    body = tipo.MensagemPush(),
                    sound = "default",
                    click_action = "FCM_PLUGIN_ACTIVITY",
                    icon = "fcm_push_icon",
                    //color = corFundoIconePush
                },
                data = new
                {
                    tipo = notificacao.Tipo,
                    parametros = notificacao.Parametros
                }
            };
        }
    }
}
