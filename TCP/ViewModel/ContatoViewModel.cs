using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TCP.Model;

namespace TCP.ViewModel
{
    public class ContatoViewModel : ObservableCollection<Contato>
    {
        public ContatoViewModel()
        {
            PreparaContatoCollection();
        }

        private void PreparaContatoCollection()
        {
            var Contato1 = new Contato
            {
                Nome = "Jose Carlos",
                Sobrenome = "Macoratti",
                Email = "macoratti@yahoo.com",
                Telefone = "012-8789-0200"
            };
            Add(Contato1);
            var Contato2 = new Contato
            {
                Nome = "Bruno",
                Sobrenome = "Silveira",
                Email = "brunosilv@email.com",
                Telefone = "021-9987-3458"
            };
            Add(Contato2);
            var Contato3 = new Contato
            {
                Nome = "Jonatas",
                Sobrenome = "Sanches",
                Email = "jonsanch@email.com",
                Telefone = "011-8975-57898"
            };
            Add(Contato3);
        }
    }
}