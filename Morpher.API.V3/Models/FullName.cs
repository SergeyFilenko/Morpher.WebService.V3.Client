﻿namespace Morpher.API.V3.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class FullName
    {
        [DataMember(Name = "Ф")]
        public string Surname { get; set; }

        [DataMember(Name = "И")]
        public string Name { get; set; }

        [DataMember(Name = "О")]
        public string Pantronymic { get; set; }
    }
}