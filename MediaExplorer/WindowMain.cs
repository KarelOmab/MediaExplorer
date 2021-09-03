/*
 * MediaExplorer - A windows application that effectively combines explorer and MediaInfo library.
 * 
 *  https://github.com/KarelOmab/MediaExplorer
 *
 * Author: Karel Tutsu
 * Date: September 3, 2021
 *
 * Copyright (C) 2021 Karel Tutsu
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software
 * and associated documentation files (the "Software"), to deal in the Software without restriction,
 * including without limitation the rights to use, copy, modify, merge, publish, distribute, 
 * sublicense, and/or sell copies of the Software, 
 * and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE 
 * OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */


using MediaInfoLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MediaExplorer
{
    public partial class WindowMain : Form
    {
        public enum ViewMode
        {
            DATAGRID,
            LISTVIEW
        }

        private ViewMode _view;
        public ViewMode View
        {
            get { return _view; }
            set 
            {
                _view = value;

                if (value == ViewMode.DATAGRID)
                {
                    
                    if (splitContainer1.Panel2.Controls.Contains(listView1))
                    {
                        splitContainer1.Panel2.Controls.Remove(listView1);
                        LoadColumns();  //add all unique columns
                        LoadRows(); //add rows from media files
                    }
                        

                    splitContainer1.Panel2.Controls.Add(dataGridView1);
                    dataGridView1.BringToFront();

                    ToolStripMenuItemDGW.Checked = true;
                    ToolStripMenuItemLW.Checked = false;


                } else if (value == ViewMode.LISTVIEW)
                {
                    if (splitContainer1.Panel2.Controls.Contains(dataGridView1))
                    {
                        splitContainer1.Panel2.Controls.Remove(dataGridView1);
                        LoadColumns();  //add all unique columns
                        LoadRows(); //add rows from media files
                    }
                        

                    splitContainer1.Panel2.Controls.Add(listView1);
                    listView1.BringToFront();

                    ToolStripMenuItemDGW.Checked = false;
                    ToolStripMenuItemLW.Checked = true;
                }
            }
        }
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
        private readonly Dictionary<string, string> DictKeysDef = new Dictionary<string, string>();

        private readonly ListView listView1;
        private readonly DataGridView dataGridView1;

        public WindowMain()
        {
            InitializeComponent();

            listView1 = new ListView() { View = System.Windows.Forms.View.Details , Dock = DockStyle.Fill } ;
            System.Reflection.PropertyInfo p = typeof(ListView).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            p.SetValue(listView1, true, null);

            dataGridView1 = new DataGridView() { Dock = DockStyle.Fill };
            p = typeof(DataGridView).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            p.SetValue(dataGridView1, true, null);

            View = ViewMode.DATAGRID;
        }

        private void WindowMain_Load(object sender, EventArgs e)
        {
            string v = MI.Option("Info_Version", "0.7.0.0;MediaInfoDLL_Example_CS;0.7.0.0");
            if (v.Length == 0)
            {
                MessageBox.Show("MediaInfo.Dll: this version of the DLL is not compatible");
                return;
            }

            System.Reflection.PropertyInfo aProp = typeof(ListView).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            aProp.SetValue(listView1, true, null);

            Text += " - " + Application.ProductVersion;

            //Path = @"D:\Disk1\Example\child";
            LoadTreeview();
        }

        private void LoadInformComplete(string f)
        {
            StreamKind sk = StreamKind.General;
            if (File.Exists(f))
            {
                try
                {
                    MediaFile mf = new MediaFile(f);
                    List<string> lKeys = new List<string>();
                    MI.Open(f);
                    MI.Option("Complete", "1");

                    foreach (string line in MI.Inform().Split('\n'))
                    {
                        if (line.Contains(":"))
                        {
                            string[] kv = line.Split(':');
                            string k = kv[0].Trim();
                            string v = string.Join(":", kv.Skip(1)).Trim(); //properties like 'Display aspect ratio' includes colon so we have to put it back
                            string modkey = string.Format("{0}_{1}{2}", sk.ToString(), k, lKeys.FindAll(x => x == k).Count.ToString());

                            bool isMatch = false;
                            foreach (KeyValuePair<string, string> keyval in DictKeysDef)
                            {
                                if (keyval.Key == modkey)
                                {
                                    isMatch = true;
                                    break;
                                }
                            }

                            if (!isMatch)
                                DictKeysDef.Add(modkey, k);

                            lKeys.Add(k);

                            mf.lParams.Add(new Parameter(0, modkey, string.Empty, v, sk));

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
                                        mf.lParams.Add(new Parameter(0, line, string.Empty, string.Empty, sk));
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
#if DEBUG       

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
#endif
            //process files in directory
            if (Directory.Exists(Path))
            {
                lMediaFiles.Clear();

                if (View == ViewMode.DATAGRID)
                    dataGridView1.Visible = false;
                else if (View == ViewMode.LISTVIEW)
                    listView1.Visible = false;

                splitContainer1.Panel2.Refresh();
                TextBoxPath.Refresh();

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

                //get all unique columns and media files
                foreach (string f in Directory.GetFiles(Path))
                    LoadInformComplete(f);

                //add all unique columns
                LoadColumns();

                //add rows from media files
                LoadRows();

            }

#if DEBUG
            watch.Stop();

            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");

#endif
        }
        private void LoadColumns()
        {
            if (View == ViewMode.DATAGRID)
            {
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();

                foreach (KeyValuePair<string, string> keyval in DictKeysDef)
                    dataGridView1.Columns.Add(keyval.Key, keyval.Value);
            }
            else if (View == ViewMode.LISTVIEW)
            {
                listView1.Items.Clear();
                listView1.Columns.Clear();

                foreach (KeyValuePair<string, string> keyval in DictKeysDef)
                    listView1.Columns.Add(new ColumnHeader() { Text = keyval.Value, Name = keyval.Key });
            }
        }

        private void LoadRows()
        {
            if (View == ViewMode.DATAGRID)
            {
                foreach (MediaFile mf in lMediaFiles)
                {
                    //Create the new row first and get the index of the new row
                    int rowIndex = this.dataGridView1.Rows.Add();

                    //Obtain a reference to the newly created DataGridViewRow 
                    var row = this.dataGridView1.Rows[rowIndex];

                    foreach (Parameter p in mf.lParams)
                    {
                        Console.WriteLine(p.Index + " [ " + p.StreamKind + " ] " + p.Key + " -> " + p.Value);
                        row.Cells[p.Key].Value = p.Value;
                    }
                }

                dataGridView1.AutoResizeColumns();
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.Visible = true;
                dataGridView1.BringToFront();
            }  else if (View == ViewMode.LISTVIEW)
            {
                foreach (MediaFile mf in lMediaFiles)
                {
                    ListViewItem lvi = new ListViewItem() { UseItemStyleForSubItems = false };

                    foreach (Parameter p in mf.lParams)
                    {
#if DEBUG   
                        Console.WriteLine(p.Index + " [ " + p.StreamKind + " ] " + p.Key + " -> " + p.Value);
#endif
                        int ci = listView1.Columns.IndexOfKey(p.Key);

                        while (lvi.SubItems.Count <= ci)
                            lvi.SubItems.Add("");

                        lvi.SubItems[ci].Text = p.Value;
                        lvi.SubItems[ci].BackColor = GetBackgroundColor(p.StreamKind);
                    }

                    listView1.Items.Add(lvi);
                }
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                listView1.Visible = true;
                listView1.BringToFront();
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
        private void LoadTreeview()
        {
            CreateDirectoryNode(null, new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)));    //list all common folders

            foreach (DriveInfo di in DriveInfo.GetDrives())
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

        private void ToolStripMenuItemDGW_Click(object sender, EventArgs e)
        {
            View = ViewMode.DATAGRID;
        }

        private void ToolStripMenuItemLW_Click(object sender, EventArgs e)
        {
            View = ViewMode.LISTVIEW;
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
    }
}
