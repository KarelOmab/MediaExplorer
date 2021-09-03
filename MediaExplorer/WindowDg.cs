﻿using MediaInfoLib;
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
    public partial class WindowDg : Form
    {
        private string _path;
        public string Path
        {
            get { return _path; }
            set { 
                _path = value;
                TextBoxPath.Text = value;
                LoadMediaData();
            }
        }
        private int _iDirHistoryIndex = 0;
        public int DirHistoryIndex
        {
            get { return _iDirHistoryIndex; }
            set
            {
                _iDirHistoryIndex = value;
                Path = lDirHistory[_iDirHistoryIndex].FullName;
            }
        }

        private readonly MediaInfo MI = new MediaInfo();
        private readonly List<MediaFile> lMediaFiles = new List<MediaFile>();
        private readonly List<DirectoryInfo> lDirHistory = new List<DirectoryInfo>();

        public WindowDg()
        {
            InitializeComponent();
        }

        private void WindowDg_Load(object sender, EventArgs e)
        {
            string v = MI.Option("Info_Version", "0.7.0.0;MediaInfoDLL_Example_CS;0.7.0.0");
            if (v.Length == 0)
            {
                MessageBox.Show("MediaInfo.Dll: this version of the DLL is not compatible");
                return;
            }

            Text += " - " + Application.ProductVersion;

            System.Reflection.PropertyInfo aProp = typeof(DataGridView).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            aProp.SetValue(dataGridView1, true, null);

            Path = @"D:\Disk1\Example\child";
            LoadTreeview();
            //Test();
        }

        private void Test()
        {

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            int rows = 10;
            int cols = 400;
            
            for (int i = 0; i < cols; i++)
            {
                dataGridView1.Columns.Add(i.ToString(), i.ToString());
            }


            for (int i=1; i < rows; i++)
            {

                List<object> ob = new List<object>();

                for (int j=1; j < cols; j++)
                {
                    ob.Add(System.IO.Path.GetRandomFileName());
                }
                

                dataGridView1.Rows.Add(ob.ToArray());
            }

            watch.Stop();

            Console.WriteLine(string.Format("Datagrid Rows:{0}, Cols:{1}, Cells:{2}", rows, cols, rows*cols));
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
        }

        private void LoadTreeview()
        {
            CreateDirectoryNode(null, new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)));    //list all common folders

            foreach(DriveInfo di in DriveInfo.GetDrives())
                CreateDirectoryNode(null, di.RootDirectory);
        }
        private void CreateDirectoryNode(TreeNode node, DirectoryInfo di)
        {
            TreeNode directoryNode;

            if (node == null)
            {
                directoryNode = new TreeNode() { Text = di.Name, Tag = di };
                treeView1.Nodes.Add(directoryNode);
            } else directoryNode = node;

            directoryNode.Nodes.Clear();

            try
            {
                foreach (DirectoryInfo d in di.GetDirectories())
                    if ((d.Attributes & FileAttributes.Hidden) == 0)
                    {
                        directoryNode.Nodes.Add(new TreeNode() { Text = d.Name, Tag = d });
                        directoryNode.Expand();
                    }
                        
            }
            catch (Exception) { }
        }
        private void LoadInformComplete(string f)
        {
            StreamKind sk = StreamKind.General;
            int i = 0;
            if (File.Exists(f))
            {
                try
                {
                    MediaFile mf = new MediaFile(f);
                    mf.Type = "";
                    MI.Open(f);

                    MI.Option("Complete", "1");

                    foreach (string line in MI.Inform().Split('\n'))
                    {
                        if (line.Contains(":"))
                        {
                            string[] kv = line.Split(':');
                            string k = kv[0].Trim();
                            string v = string.Join(":", kv.Skip(1)).Trim(); //properties like 'Display aspect ratio' includes colon so we have to put it back

                            mf.lParams.Add(new Parameter(i, k, string.Empty, v, sk));
                            i++;

                        }
                        else
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
                                        mf.lParams.Add(new Parameter(i, line, string.Empty, string.Empty, sk));
                                    }
                                    break;
                            }
                        }
                        
                    }
                    MI.Close();

                    lMediaFiles.Add(mf);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private void LoadMediaData()
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            dataGridView1.DataSource = null;

            //process files in directory
            if (Directory.Exists(Path))
            {
                DirectoryInfo di = new DirectoryInfo(Path);
                bool isMatched = false;
                for (int i = 0; i < lDirHistory.Count; i++)
                {
                    DirectoryInfo die = lDirHistory[i];
                    if (di.FullName == die.FullName)
                    {
                        isMatched = true;
                        break;
                    }
                }

                if (!isMatched)
                    lDirHistory.Add(di);

                foreach (string f in Directory.GetFiles(Path))
                    LoadInformComplete(f);
            }


            //process items
            foreach (MediaFile mf in lMediaFiles)
            {

                List<object> ob = new List<object>();
                foreach (Parameter p in mf.lParams)
                {
#if DEBUG
                    Console.WriteLine(p.Index + " [ " + p.StreamKind + " ] " + p.Key + " -> " + p.Value);
#endif

                    dataGridView1.Columns.Add(p.Key, p.Key);
                    ob.Add(p.Value);


                }
                dataGridView1.Rows.Add(ob.ToArray());
            }
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            watch.Stop();

            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
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
        //private void ExampleQuery(string f)
        //{
        //    //Test if version of DLL is compatible : 3rd argument is "version of DLL tested;Your application name;Your application version"
        //    String ToDisplay = string.Empty;

        //    //Information about MediaInfo
        //    //ToDisplay += "\r\n\r\nInfo_Parameters\r\n";
        //    //ToDisplay += MI.Option("Info_Parameters");

        //    //ToDisplay += "\r\n\r\nInfo_Capacities\r\n";
        //    //ToDisplay += MI.Option("Info_Capacities");

        //    //ToDisplay += "\r\n\r\nInfo_Codecs\r\n";
        //    //ToDisplay += MI.Option("Info_Codecs");

        //    //An example of how to use the library
        //    ToDisplay += "\r\n\r\nOpen\r\n";
        //    MI.Open(f);

        //    ToDisplay += "\r\n\r\nInform with Complete=false\r\n";
        //    MI.Option("Complete");
        //    ToDisplay += MI.Inform();

        //    //ToDisplay += "\r\n\r\nInform with Complete=true\r\n";
        //    //MI.Option("Complete", "1");
        //    //ToDisplay += MI.Inform();

        //    //ToDisplay += "\r\n\r\nCustom Inform\r\n";
        //    //MI.Option("Inform", "General;File size is %FileSize% bytes");
        //    //ToDisplay += MI.Inform();

        //    //ToDisplay += "\r\n\r\nGet with Stream=General and Parameter='FileSize'\r\n";
        //    //ToDisplay += MI.Get(0, 0, "FileSize");

        //    //ToDisplay += "\r\n\r\nGet with Stream=General and Parameter=46\r\n";
        //    //ToDisplay += MI.Get(0, 0, 46);

        //    //ToDisplay += "\r\n\r\nCount_Get with StreamKind=Stream_Audio\r\n";
        //    //ToDisplay += MI.Count_Get(StreamKind.Audio);

        //    //ToDisplay += "\r\n\r\nGet with Stream=General and Parameter='AudioCount'\r\n";
        //    //ToDisplay += MI.Get(StreamKind.General, 0, "AudioCount");

        //    //ToDisplay += "\r\n\r\nGet with Stream=Audio and Parameter='StreamCount'\r\n";
        //    //ToDisplay += MI.Get(StreamKind.Audio, 0, "StreamCount");

        //    //ToDisplay += "\r\n\r\nClose\r\n";
        //    MI.Close();

        //    //Example with a stream
        //    //ToDisplay+="\r\n"+ExampleWithStream()+"\r\n";

        //    //Displaying the text
        //    Console.WriteLine(ToDisplay);
        //    //richTextBox1.Text = ToDisplay;
        //}
        //String ExampleWithStream(string f)
        //{
        //    //Initilaizing MediaInfo
        //    MediaInfo MI = new MediaInfo();

        //    //From: preparing an example file for reading
        //    FileStream From = new FileStream(f, FileMode.Open, FileAccess.Read);

        //    //From: preparing a memory buffer for reading
        //    byte[] From_Buffer = new byte[64 * 1024];
        //    int From_Buffer_Size; //The size of the read file buffer

        //    //Preparing to fill MediaInfo with a buffer
        //    MI.Open_Buffer_Init(From.Length, 0);

        //    //The parsing loop
        //    do
        //    {
        //        //Reading data somewhere, do what you want for this.
        //        From_Buffer_Size = From.Read(From_Buffer, 0, 64 * 1024);

        //        //Sending the buffer to MediaInfo
        //        System.Runtime.InteropServices.GCHandle GC = System.Runtime.InteropServices.GCHandle.Alloc(From_Buffer, System.Runtime.InteropServices.GCHandleType.Pinned);
        //        IntPtr From_Buffer_IntPtr = GC.AddrOfPinnedObject();
        //        Status Result = (Status)MI.Open_Buffer_Continue(From_Buffer_IntPtr, (IntPtr)From_Buffer_Size);
        //        GC.Free();
        //        if ((Result & Status.Finalized) == Status.Finalized)
        //            break;

        //        //Testing if MediaInfo request to go elsewhere
        //        if (MI.Open_Buffer_Continue_GoTo_Get() != -1)
        //        {
        //            Int64 Position = From.Seek(MI.Open_Buffer_Continue_GoTo_Get(), SeekOrigin.Begin); //Position the file
        //            MI.Open_Buffer_Init(From.Length, Position); //Informing MediaInfo we have seek
        //        }
        //    }
        //    while (From_Buffer_Size > 0);

        //    //Finalizing
        //    MI.Open_Buffer_Finalize(); //This is the end of the stream, MediaInfo must finnish some work

        //    //Get() example
        //    Console.WriteLine("Container format is " + MI.Get(StreamKind.General, 0, "Format"));
        //    return "Container format is " + MI.Get(StreamKind.General, 0, "Format");
        //}

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            int ci = lDirHistory.IndexOf(lDirHistory.Find(d => d.FullName == Path));
            if (ci > 0)
                DirHistoryIndex = ci - 1;
        }

        private void ButtonForward_Click(object sender, EventArgs e)
        {
            int ci = lDirHistory.IndexOf(lDirHistory.Find(d => d.FullName == Path));
            if (ci < lDirHistory.Count - 1)
                DirHistoryIndex = ci + 1;
        }

        private void ButtonUp_Click(object sender, EventArgs e)
        {
            DirectoryInfo dInfo = new DirectoryInfo(Path);
            if (dInfo.Parent != null) Path = dInfo.Parent.FullName;
        }

        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            LoadMediaData();
        }

        private void TextBoxPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Path = TextBoxPath.Text;
        }
        private void TreeView1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = e as MouseEventArgs;

            if (me.Button == MouseButtons.Right)
                contextMenuTree.Show(Cursor.Position);
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            CreateDirectoryNode(e.Node, e.Node.Tag as DirectoryInfo);
            e.Node.Expand();
        }

        private void ScanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryInfo di = treeView1.SelectedNode.Tag as DirectoryInfo;
            Path = di.FullName;
        }
    }
}