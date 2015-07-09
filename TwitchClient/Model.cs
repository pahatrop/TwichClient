using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace TwitchClient
{
    [DataContract]
    public class MainModel
    {
        [DataMember]
        public int _total;
        [DataMember]
        public LinkModel _links;
        [DataMember]
        public List<TopModel> top;
    }
     [DataContract]
    public class LinkModel
    {
         [DataMember]
        public string self;
         [DataMember]
        public string next;
    }
     [DataContract]
    public class TopModel
    {
         [DataMember]
        public int viewers;
         [DataMember]
        public int channels;
         [DataMember]
        public GameModel game;
    }
     [DataContract]
    public class GameModel
    {
         [DataMember]
        public string name;
         [DataMember]
        public int _id;
         [DataMember]
        public int giantbomb_id;
         [DataMember]
        public BoxModel box;
         [DataMember]
        public LogoModel logo;
    }
     [DataContract]
    public class BoxModel
    {
        [DataMember]
        public string large;
        [DataMember]
        public string medium;
        [DataMember]
        public string small;
        [DataMember]
        public string template;
    }
     [DataContract]
    public class LogoModel
    {
        [DataMember]
        public string large;
        [DataMember]
        public string medium;
        [DataMember]
        public string small;
        [DataMember]
        public string template;
    }
}
