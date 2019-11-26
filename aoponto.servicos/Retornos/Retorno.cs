using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aoponto.lib;

namespace aoponto.servicos
{
    public class Retorno
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        private List<KeyValuePair<string, object>> ListaDados { get; set; }
        public ETipoFalha TipoFalha { get; set; }



        public Retorno()
        {
            this.Sucesso = true;
            this.Mensagem = "";
            this.ListaDados = null;
            this.TipoFalha = ETipoFalha.SemFalhas;
        }

        public Retorno(bool sucesso, string mensagem)
        {
            this.Sucesso = sucesso;
            this.Mensagem = mensagem;
            this.ListaDados = null;
            this.TipoFalha = ETipoFalha.NaoDefinido;
        }

        public Retorno(ETipoFalha falha)
        {
            this.Sucesso = false;
            this.Mensagem = falha.Descricao();
            this.ListaDados = null;
            this.TipoFalha = falha;
        }

        public Retorno(Exception ex)
        {
            this.Sucesso = false;
            this.Mensagem = ex.Message;
            this.ListaDados = null;
            this.TipoFalha = ETipoFalha.Excessao;
        }

        public Retorno(object dados)
        {
            this.Sucesso = true;
            this.Mensagem = "";            
            this.AdicionarDados(dados);
            this.TipoFalha = ETipoFalha.SemFalhas;
        }

        public Retorno(object dados, string mensagem)
        {
            this.Sucesso = true;
            this.Mensagem = mensagem;
            this.AdicionarDados(dados);
            this.TipoFalha = ETipoFalha.SemFalhas;
        }               

        public void AdicionarDados(object dados)
        {
            this.AdicionarDados(dados, "xyz");
        }

        public void AdicionarDados(object dados, string chave)
        {
            if (this.ListaDados == null)
            {
                this.ListaDados = new List<KeyValuePair<string, object>>();
            }

            this.ListaDados.Add(new KeyValuePair<string, object>(chave, dados));
        }

        public T ObterDados<T>()
        {
            return this.ObterDados<T>("xyz");
        }

        public T ObterDados<T>(string chave)
        {
            if (this.ListaDados == null)
            {
                return default(T);
            }

            try
            {                                
                return (T)Convert.ChangeType(this.ListaDados.FirstOrDefault(x => x.Key == chave).Value, typeof(T));
            }
            catch (Exception)
            {
                return default(T);
            }
        }

    }
}
