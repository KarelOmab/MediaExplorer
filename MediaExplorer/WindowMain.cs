using MediaInfoLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaExplorer
{
    public partial class WindowMain : Form
    {
        private string _path;
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        MediaInfo MI = new MediaInfo();
        

        public WindowMain()
        {
            InitializeComponent();
            listView1.CheckBoxes = true;
        }

        private void WindowMain_Load(object sender, EventArgs e)
        {

            string v = MI.Option("Info_Version", "0.7.0.0;MediaInfoDLL_Example_CS;0.7.0.0");
            if (v.Length == 0)
            {
                MessageBox.Show("MediaInfo.Dll: this version of the DLL is not compatible");
                return;
            }

            LoadMediaData();
        }

        private void LoadMediaData()
        {
            if (Path == null)
                Path = TextBoxPath.Text;

            listView1.Items.Clear();
            //listView1.Columns.Clear();

            if (Directory.Exists(Path))
                foreach(string f in Directory.GetFiles(Path))
                    HandleFile(f);

            if (listView1.Items.Count > 0)
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void HandleFile(string f)
        {
            MediaQuery(f);
        }

        private void MediaQuery(string f, bool isComplete=true)
        {
            if (File.Exists(f))
            {
                try
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.UseItemStyleForSubItems = false;
                    lvi.Text = System.IO.Path.GetFileName(f);

                    MI.Open(f);

                    if (isComplete) MI.Option("Complete", "1");

                    string info = MI.Inform();
                    StreamKind sk = new StreamKind();

                    foreach (string line in info.Split('\n'))
                    {
                        if (line.Contains(":"))
                        {
                            string[] kv = line.Split(':');
                            string k = kv[0].Trim();
                            string v = string.Join(":", kv.Skip(1)).Trim(); //properties like 'Display aspect ratio' includes colon so we have to put it back

                            if (!listView1.Columns.ContainsKey(k))
                            {
                                ColumnHeader ch = new ColumnHeader();
                                ch.Tag = sk;
                                ch.Text = k;
                                ch.Name = k;
                                listView1.Columns.Add(ch);
                                
                            }

                            lvi.SubItems.Add("");   //add empty sub item, we will use indexes to set the value below

                            int ci = listView1.Columns.IndexOfKey(k);   //get column index

                            while (lvi.SubItems.Count <= ci)
                                lvi.SubItems.Add("");   //add blank (filler) subitems if/where necessary
                                
                            lvi.SubItems[ci].Text = v;  //set value
                            lvi.SubItems[ci].BackColor = GetBackgroundColor(sk);    //set cell background color

                        } else
                        {
                            switch (line.Trim())
                            {
                                case "Audio":
                                    sk = StreamKind.Audio;
                                    break;
                                case "General":
                                    sk = StreamKind.General;
                                    break;
                                case "Image":
                                    sk = StreamKind.Image;
                                    break;
                                case "Menu":
                                    sk = StreamKind.Menu;
                                    break;
                                case "Other":
                                    sk = StreamKind.Other;
                                    break;
                                case "Text":
                                    sk = StreamKind.Text;
                                    break;
                                case "Video":
                                    sk = StreamKind.Video;
                                    break;
                            }
                        } 
                    }
                    MI.Close();
                    listView1.Items.Add(lvi);
                }
                catch (Exception ex) {
                    Console.WriteLine(listView1.GetType());
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private Color GetBackgroundColor(StreamKind sk)
        {
            if (sk == StreamKind.Audio)
                return Color.Beige;
            else if (sk == StreamKind.General)
                return Color.White;
            else if (sk == StreamKind.Image)
                return Color.Azure;
            else if (sk == StreamKind.Menu)
                return Color.Firebrick;
            else if (sk == StreamKind.Other)
                return Color.ForestGreen;
            else if (sk == StreamKind.Text)
                return Color.Lavender;
            else if (sk == StreamKind.Video)
                return Color.SkyBlue;

            return Color.White;
        }

        private void ExampleQuery(string f)
        {
            //Test if version of DLL is compatible : 3rd argument is "version of DLL tested;Your application name;Your application version"
            String ToDisplay = string.Empty;

            //Information about MediaInfo
            //ToDisplay += "\r\n\r\nInfo_Parameters\r\n";
            //ToDisplay += MI.Option("Info_Parameters");

            //ToDisplay += "\r\n\r\nInfo_Capacities\r\n";
            //ToDisplay += MI.Option("Info_Capacities");

            //ToDisplay += "\r\n\r\nInfo_Codecs\r\n";
            //ToDisplay += MI.Option("Info_Codecs");

            //An example of how to use the library
            ToDisplay += "\r\n\r\nOpen\r\n";
            MI.Open(f);

            ToDisplay += "\r\n\r\nInform with Complete=false\r\n";
            MI.Option("Complete");
            ToDisplay += MI.Inform();

            //ToDisplay += "\r\n\r\nInform with Complete=true\r\n";
            //MI.Option("Complete", "1");
            //ToDisplay += MI.Inform();

            //ToDisplay += "\r\n\r\nCustom Inform\r\n";
            //MI.Option("Inform", "General;File size is %FileSize% bytes");
            //ToDisplay += MI.Inform();

            //ToDisplay += "\r\n\r\nGet with Stream=General and Parameter='FileSize'\r\n";
            //ToDisplay += MI.Get(0, 0, "FileSize");

            //ToDisplay += "\r\n\r\nGet with Stream=General and Parameter=46\r\n";
            //ToDisplay += MI.Get(0, 0, 46);

            //ToDisplay += "\r\n\r\nCount_Get with StreamKind=Stream_Audio\r\n";
            //ToDisplay += MI.Count_Get(StreamKind.Audio);

            //ToDisplay += "\r\n\r\nGet with Stream=General and Parameter='AudioCount'\r\n";
            //ToDisplay += MI.Get(StreamKind.General, 0, "AudioCount");

            //ToDisplay += "\r\n\r\nGet with Stream=Audio and Parameter='StreamCount'\r\n";
            //ToDisplay += MI.Get(StreamKind.Audio, 0, "StreamCount");

            //ToDisplay += "\r\n\r\nClose\r\n";
            MI.Close();

            //Example with a stream
            //ToDisplay+="\r\n"+ExampleWithStream()+"\r\n";

            //Displaying the text
            Console.WriteLine(ToDisplay);
            //richTextBox1.Text = ToDisplay;
        }
        String ExampleWithStream(string f)
        {
            //Initilaizing MediaInfo
            MediaInfo MI = new MediaInfo();

            //From: preparing an example file for reading
            FileStream From = new FileStream(f, FileMode.Open, FileAccess.Read);

            //From: preparing a memory buffer for reading
            byte[] From_Buffer = new byte[64 * 1024];
            int From_Buffer_Size; //The size of the read file buffer

            //Preparing to fill MediaInfo with a buffer
            MI.Open_Buffer_Init(From.Length, 0);

            //The parsing loop
            do
            {
                //Reading data somewhere, do what you want for this.
                From_Buffer_Size = From.Read(From_Buffer, 0, 64 * 1024);

                //Sending the buffer to MediaInfo
                System.Runtime.InteropServices.GCHandle GC = System.Runtime.InteropServices.GCHandle.Alloc(From_Buffer, System.Runtime.InteropServices.GCHandleType.Pinned);
                IntPtr From_Buffer_IntPtr = GC.AddrOfPinnedObject();
                Status Result = (Status)MI.Open_Buffer_Continue(From_Buffer_IntPtr, (IntPtr)From_Buffer_Size);
                GC.Free();
                if ((Result & Status.Finalized) == Status.Finalized)
                    break;

                //Testing if MediaInfo request to go elsewhere
                if (MI.Open_Buffer_Continue_GoTo_Get() != -1)
                {
                    Int64 Position = From.Seek(MI.Open_Buffer_Continue_GoTo_Get(), SeekOrigin.Begin); //Position the file
                    MI.Open_Buffer_Init(From.Length, Position); //Informing MediaInfo we have seek
                }
            }
            while (From_Buffer_Size > 0);

            //Finalizing
            MI.Open_Buffer_Finalize(); //This is the end of the stream, MediaInfo must finnish some work

            //Get() example
            Console.WriteLine("Container format is " + MI.Get(StreamKind.General, 0, "Format"));
            return "Container format is " + MI.Get(StreamKind.General, 0, "Format");
        }
    }
}
