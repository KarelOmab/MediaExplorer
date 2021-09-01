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
using static System.Windows.Forms.ListViewItem;

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

        List<Parameter> lParams = new List<Parameter>();


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
            LoadParams();
            LoadColumns();
            LoadMediaData();
        }

        private void LoadParams()
        {
            StreamKind sk = StreamKind.General;
            int i = 0;

            foreach (string line in MI.Option("Info_Parameters").Split('\n'))
            {
                if (line.Contains(":"))
                {
                    string[] kv = line.Split(':');
                    string k = kv[0].Trim();
                    string d = string.Join(":", kv.Skip(1)).Trim(); //properties like 'Display aspect ratio' includes colon so we have to put it back

                    //Inform is a special keyword that returns new dictionary (duplicate)
                    if (k != "Inform")
                        lParams.Add(new Parameter(i, k, d, sk));

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
                        default:
                            if (line.Trim().Length > 0)
                            {
                                lParams.Add(new Parameter(i, line, string.Empty, sk));
                            }
                            break;
                    }
                }
                i++;
            }
        }

        private void LoadColumns()
        {
            listView1.Columns.Add(new ColumnHeader() { Text = "Name"});
            foreach (Parameter p in lParams)
                listView1.Columns.Add(new ColumnHeader() { Text = p.Key, Name = p.Index.ToString(), Width = 0 });
        }

        private void LoadMediaData()
        {
            if (Path == null)
                Path = TextBoxPath.Text;

            listView1.Items.Clear();

            if (Directory.Exists(Path))
                foreach (string f in Directory.GetFiles(Path))
                    HandleFile(f);

            listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void HandleFile(string f)
        {
            if (File.Exists(f))
            {
                try
                {
                    List<int> ints = new List<int>();
                    ListViewItem lvi = new ListViewItem();
                    lvi.UseItemStyleForSubItems = false;
                    lvi.Text = System.IO.Path.GetFileName(f);

                    foreach(ColumnHeader ch in listView1.Columns)
                        lvi.SubItems.Add("");   //fill data

                    MI.Open(f);

                    foreach(StreamKind sk in Enum.GetValues(typeof(StreamKind)))
                    {
                        for (int i=0; i < MI.Count_Get(sk); i++)
                        {
                            foreach (Parameter p in lParams.FindAll(s => s.StreamKind == sk))
                            {
                                string v = MI.Get(sk, i, p.Key);

                                if (v.Trim().Length > 0)
                                {
                                    //Console.WriteLine(string.Format("{0} -> {1}", p.Key, v));
                                    int ci = listView1.Columns.IndexOfKey(p.Index.ToString());
                                    lvi.SubItems[ci].Text = v;  //set cell data
                                    lvi.SubItems[ci].BackColor = GetBackgroundColor(sk);
                                    ints.Add(ci);
                                }
                            }
                        } 
                    }
                    MI.Close();
                    listView1.Items.Add(lvi);

                    foreach(int i in ints)
                    {
                        if (lvi.SubItems[i].Text.Length > listView1.Columns[i].Text.Length)
                            listView1.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
                        else listView1.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.HeaderSize);
                    }
                }
                catch (Exception ex)
                {
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
