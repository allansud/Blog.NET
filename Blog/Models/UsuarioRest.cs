using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Blog.Models
{
    [DataContract]
    [Serializable]
    public class UsuarioRest
    {
        [DataMember]
        public int codigo { get; set; }

        [DataMember]
        public string nome { get; set; }

        [DataMember]
        public string email { get; set; }

        [DataMember]
        public string senha { get; set; }

        [DataMember]
        public string telefone { get; set; }

        [DataMember]
        public string cidade { get; set; }
    }
}