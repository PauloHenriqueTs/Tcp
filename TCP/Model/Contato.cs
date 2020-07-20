using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TCP.Model
{
    public class Contato : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Esta rotina é chamada cada vez que o valor da propridade
        // for definida. Isso vai disparar um evento para notificar
        // a WPF via data binding que algo mudou
        private void OnPropertyChanged(string nomePropriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }

        private string _nome;
        private string _sobrenome;
        private string _telefone;
        private string _email;

        public string Nome
        {
            get { return _nome; }
            set
            {
                _nome = value;
                OnPropertyChanged("Nome");
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        public string Sobrenome
        {
            get { return _sobrenome; }
            set
            {
                _sobrenome = value;
                OnPropertyChanged("Sobrenome");
            }
        }

        public string Telefone
        {
            get { return _telefone; }
            set
            {
                _telefone = value;
                OnPropertyChanged("Telefone");
            }
        }
    }
}