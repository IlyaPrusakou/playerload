using Audioplayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using AudioPlayer;
using System.Media;
using System.Threading;
using System.Runtime.ExceptionServices;

namespace Audioplayer
{
    public class Player : GenericPlayer<Song>, IDisposable
    {
        private bool disposed = false;
        private SoundPlayer soundplayer;
        public Exception exceptionfield { get; set; }
        public event Action OnError;
        public event Action OnWarning;
        public event Action ItemListChangedEvent;
        public Player()
        {
            soundplayer = new SoundPlayer();
          
            IsLock = true;
        }

        public Player(Skin skn)
        {
            soundplayer = new SoundPlayer(); 
            SkinForm = skn;

            IsLock = true;
        }
        public override void Play(bool Loop = false)//
        {

            if (Loop == false)//
            {
                ShufleExtension.ExtenShufle(Items);//
            }
            else//
            {
                for (int i = 0; i < 5; i++)//
                {
                    ShufleExtension.ExtenShufle(Items);//
                }
            }
            if (Playing == true)//
            {
                for (int i = 0; i < Items.Count; i++)
                {

                    Data = GetItemData(Items[i]);
                    soundplayer.SoundLocationChanged += PlayNowItem;

                    soundplayer.SoundLocation = Items[i].Path;//
                    soundplayer.Load();//
                    soundplayer.PlaySync();//
                }
            }
        }

        private  void InnerPlay(CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                soundplayer.Stop();
                return;
            }
            soundplayer.Load();
            try
            {
                soundplayer.PlaySync();
            }
            //catch (FileNotFoundException fex)   AL5-Player1/2. ExceptionHandling.
            //{
            //exceptionfield = fex;   AL5-Player1/2. ExceptionHandling.
            //OnError();   AL5-Player1/2. ExceptionHandling.
            //}
            //catch (InvalidOperationException iex)   AL5-Player1/2. ExceptionHandling.
            //{
            //exceptionfield = iex;   AL5-Player1/2. ExceptionHandling.
            //OnError();   AL5-Player1/2. ExceptionHandling.
            //}
            catch (FileNotFoundException fex)    //AL5 - Player1 / 2.CustomExceptions.
            {
                //ExceptionDispatchInfo di = ExceptionDispatchInfo.Capture(new FailedToPlayException(soundplayer.SoundLocation, fex.StackTrace, fex.Message, fex));
                //di.Throw();
                throw new FailedToPlayException(soundplayer.SoundLocation, fex.StackTrace, fex.Message, fex);
            }
            catch (InvalidOperationException iex)    //AL5 - Player1 / 2.CustomExceptions.
            {

                ExceptionDispatchInfo di = ExceptionDispatchInfo.Capture(new FailedToPlayException(soundplayer.SoundLocation, iex.StackTrace, iex.Message, iex));
                di.Throw();     //AL5 - Player1 / 2.CustomExceptions.
                //throw new FailedToPlayException(soundplayer.SoundLocation, iex.StackTrace, iex.Message, iex);
                //<summury>
                // вот именно в месте повторной генерации исключения(строчка 100 или 101) исключение генерируется
                // и приложение подает(то есть повторное исключение не обрабатывается!!!),
                // хотя это исключение должно всплывать  на строчке 143 и обрабатываться try-catch основного потока.
                // я прописал логику обработки исключения как будто исключение всплывает в основном потоке
                // ввел события onerror и onwarning и изменил метод vizualizer в классе Programm
            }

            
        }
        public async override Task PlayAsync(bool Loop = false)
        {
            
            if (Loop == false)
            {
                ShufleExtension.ExtenShufle(Items);
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    ShufleExtension.ExtenShufle(Items);
                }
            }
            if (Playing == true)
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    
                    if (token.IsCancellationRequested)
                    {
                        return;
                    }
                    source = new CancellationTokenSource();
                    token = source.Token;
                    Data = GetItemData(Items[i]);
                    soundplayer.SoundLocationChanged += PlayNowItem;
                    soundplayer.SoundLocation = Items[i].Path;
                    Task task = Task.Run(() => InnerPlay(token),token);
                    try    //AL5 - Player1 / 2.CustomExceptions.
                    {
                        await task;
                    }
                    catch (PlayerException pex)    //AL5 - Player1 / 2.CustomExceptions.
                    {
                        exceptionfield = pex;
                        OnWarning();
                    }
                    catch (Exception ex)    //AL5 - Player1 / 2.CustomExceptions.
                    {
                        exceptionfield = ex;
                        OnError();
                    }
                    source.Dispose();
                    exceptionfield = null;
                } 
             }
        }
        
        private FileInfo[] GetWav(string directoryPath, string pattern = null) 
        {
            DirectoryInfo dir = new DirectoryInfo(directoryPath); 
            FileInfo[] files = null;
            if (pattern == null) { files = dir.GetFiles(); }
            else if (pattern != null) { files = dir.GetFiles(pattern); }
            return files; 
        }
         public override void Load(string directoryPath, string pattern = null) 
        {
            if (token == null && token.IsCancellationRequested == false)
            {
                //soundplayer.Stop();
                source.Cancel();
            }
            List<Song> listOfLoadedSongs = new List<Song>(); 
            FileInfo[] files = GetWav(directoryPath); 
            foreach (FileInfo item in files) 
            {
                Song itemSong = new Song();
                
                itemSong.Path = item.FullName;
                var audioFile = TagLib.File.Create(itemSong.Path);
                itemSong.Title = audioFile.Tag.Title;
                listOfLoadedSongs.Add(itemSong);
                audioFile.Dispose();
            }
            Items = listOfLoadedSongs;
            ItemListChangedEvent?.Invoke();
        }
        public override void Clear()
        {
             Items.Clear();
        }
        
        public override void SavePlaylist(string directory, string name) 
        {
            if (Items.Count != 0)
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<Song>));
                XmlWriterSettings set = new XmlWriterSettings();
                set.Indent = true;
                string filepath = directory + @"\" + name + @".xml";
                using (FileStream fs = File.Create(filepath))
                {
                    using (XmlWriter str = XmlWriter.Create(fs, set))
                    {
                        xs.Serialize(str, Items);
                    }
                }
            }
            else if (Items.Count == 0) { Console.WriteLine("Playlist is empty"); }
        }
        public override void LoadPlayList(string filepath) 
        {

            XmlSerializer xs = new XmlSerializer(typeof(List<Song>)); 
            List<Song> listOfLoadedSongs; 
            using (FileStream fs = new FileStream(filepath, FileMode.Open))
            {
                using (XmlReader str = XmlReader.Create(fs))
                {
                    listOfLoadedSongs = (List<Song>)xs.Deserialize(str);
                }
            }
            Items = listOfLoadedSongs;
            ItemListChangedEvent?.Invoke();
        }
        //public void LyricsOutput() 
        //{
            //foreach (Song item in Items) 
            //{
                //SkinForm.Render($"{item.Title} --- {item.Lyrics}"); 
            //}
        //}
        public void Dispose()
        {
            DisposeAlgo(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void DisposeAlgo(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Items = null;
                    Rnd = null;
                    SkinForm = null;
                }
                soundplayer.Dispose();
                source.Dispose();
                disposed = true;
            }
        }
        ~Player()
        {
           
            DisposeAlgo(false);
        }
    }
}