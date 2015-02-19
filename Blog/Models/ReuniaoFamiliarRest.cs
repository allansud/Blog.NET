using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Blog.Models
{
    [DataContract]
    public class ReuniaoFamiliarRest
    {
        [DataMember]
        public int Codigo { get; set; }

        [DataMember]
        public String Titulo { get; set; }

        [DataMember]
        public String Palestrante { get; set; }

        [DataMember]
        public String Data { get; set; }

    }
}