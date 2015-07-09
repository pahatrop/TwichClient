using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Net;
using System;
using System.Windows;
using System.ComponentModel;

namespace TwitchClient
{
    public class Game
    {
        public string Channels { get; set; }
        public string Viewers { get; set; }
        public string Name { get; set; }
        public string Small { get; set; }
        public Game(string channels, string viewers, string name, string small)
        {
            Channels = channels;
            Viewers = viewers;
            Name = name;
            Small = small;
        }
    }

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private List<Game> allGames;// = new List<Game>();
        private List<Game> cg = new List<Game>();
        public List<Game> AllGames { get { return allGames; } }
        private int selectedindex = 0;
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public int SelectedIndex
        {
            get { return selectedindex; }
            set
            {
                if (value != -1)
                {
                    selectedindex = value;
                }
            }
        }
        public MainWindowViewModel()
        {
            WebRequest request = WebRequest.Create("https://api.twitch.tv/kraken/games/top");
            request.BeginGetResponse(new AsyncCallback(OnResponse), request);
            RaisePropertyChanged("AllGames");
            RaisePropertyChanged("SelectedIndex");
        }
        private void UpdateList(MainModel mm)
        {
            foreach (TopModel currenGame in mm.top)
            {
                cg.Add(new Game("Channels: " + currenGame.channels.ToString(), "Viewers: " + currenGame.viewers.ToString(), currenGame.game.name , currenGame.game.box.small));
            }
            allGames = cg;
            RaisePropertyChanged("AllGames");
        }
        public void OnResponse(IAsyncResult ar)
        {
            string text = "";
            WebRequest request = (WebRequest)ar.AsyncState;
            WebResponse response = request.EndGetResponse(ar);
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                    text += line + "\n";
            }
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(MainModel));
            MemoryStream ms = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(text));
            MainModel mm = (MainModel)js.ReadObject(ms);
            UpdateList(mm);
        }
    }
}
