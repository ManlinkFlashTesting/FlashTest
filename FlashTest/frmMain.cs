﻿using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace FlashTest
{
    public partial class frmMain : Form
    {
        public string fullPath = string.Empty;
        public string filename = string.Empty;
        private Boolean TypeFirstChangeFlag = false;
        private Boolean AlgFirstChangeFlag = false;

        BindingSource DeveiceTypeBS = new BindingSource();
        BindingSource AlgBS = new BindingSource();

        private const int SendBufferSize = 2 * 1024;
        private const int ReceiveBufferSize = 8 * 1024;
        private string strRecMsg = null;
        Socket socketClient = null;
        Thread threadClient = null;

        private List<string> objListCmd = new List<string>(); //define student info variable

        public frmMain()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            timer1.Start();
            LoadCmdToGrid();
            GetStatus();
            ConnectToServer();
        }

        //define control method
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)//closing action
        {
            Application.Exit();
        }
        private void btnClearLog_Click(object sender, EventArgs e)//clear command log window
        {
            txtMsg.Text = null;
        }
        private void btnSaveLog_Click(object sender, EventArgs e)//Save command log window
        {
            // Text from the rich textbox rtfMain
            string str = txtMsg.Text;
            // Create a new SaveFileDialog object
            using (SaveFileDialog dlgSave = new SaveFileDialog())
                try
                {
                    // Available file extensions
                    dlgSave.Filter = "Text Files (*.txt)|*.txt";
                    // SaveFileDialog title
                    dlgSave.Title = "Save";
                    // Show SaveFileDialog
                    if (dlgSave.ShowDialog() == DialogResult.OK && dlgSave.FileName.Length > 0)
                    {
                        // Save file as utf8 without byte order mark (BOM)
                        UTF8Encoding utf8 = new UTF8Encoding();
                        StreamWriter sw = new StreamWriter(dlgSave.FileName, false, utf8);
                        sw.Write(str);
                        sw.Close();
                    }
                }
                catch (Exception errorMsg)
                {
                    MessageBox.Show(errorMsg.Message);
                }
        }
        private void btnCSend_Click(object sender, EventArgs e)// send manual command
        {
            if (txtCMsg.Text.Trim() == string.Empty) return;
            ClientSendMsg(txtCMsg.Text.Trim(), 0);
            Thread.Sleep(10);
        }
        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (dgvCmd.RowCount == 0) return;
            if ( dgvCmd.CurrentRow.Index< dgvCmd.RowCount-1 )
            {
                string currentCmd = dgvCmd.CurrentRow.Cells[0].Value.ToString();
                ClientSendMsg(currentCmd, 0);
                dgvCmd.CurrentCell = dgvCmd.Rows[dgvCmd.CurrentRow.Index + 1].Cells[0];

            }
            else if(dgvCmd.CurrentRow.Index == dgvCmd.RowCount-1)
            {
                string currentCmd = dgvCmd.CurrentRow.Cells[0].Value.ToString();
                ClientSendMsg(currentCmd, 0);
                dgvCmd.CurrentCell = dgvCmd.Rows[0].Cells[0];
            }
            btnExecute.Enabled = false;
            Thread.Sleep(10);

        }

        private void btnEndCmd_Click(object sender, EventArgs e)
        {
            dgvCmd.CurrentCell = dgvCmd.Rows[0].Cells[0];
        }

        private void txtCMsg_KeyDown(object sender, KeyEventArgs e)//快捷键 Enter 发送信息
        {   //当光标位于输入文本框上的情况下 发送信息的热键为回车键Enter 
            if (e.KeyCode == Keys.Enter)
            {
                //则调用客户端向服务端发送信息的方法
                ClientSendMsg(txtCMsg.Text, 0);
            }
        }
        private void cboDeviceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TypeFirstChangeFlag)
            {
                UpdateAlgList();
                GetStatus();
            }
            else TypeFirstChangeFlag = true;
        }
        private void cboAlg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AlgFirstChangeFlag)
            {
                GetStatus();
            }
            else AlgFirstChangeFlag = true;
        }

        //menu action
        private void tsmAbout_Click(object sender, EventArgs e)
        {
            frmAbout aboutbox = new frmAbout();
            aboutbox.ShowDialog();
        }
        private void tsmImportCmd_Click(object sender, EventArgs e)
        {

            //1.select file
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "TXT File(*.txt)|*.txt|CSV File(*.csv)|*.csv|All Files(*.*)|*.txt";
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                fullPath = openfile.FileName;//send the filename to global Variable
                filename = Path.GetFileNameWithoutExtension(fullPath);
            }
            else return;
            //2.create database table based on filename
            //check table is exist? and create table
            if (CheckExistTable(filename))
            {
                DeleteTable(filename);
            }

            CreateTable(filename);

            //3.record Cmd to list
            try
            {
                //read file
                objListCmd = ReadFileToList(fullPath);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Read File Error, " + ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //4.Record list to database
            InsertCmdRow(objListCmd, filename);

            //update device list and alg list
            LoadCmdToGrid();
        }
        private void tsmExit_Click(object sender, EventArgs e)//close program
        {
            Application.Exit();
        }
        private void tsmShowDatabase_Click(object sender, EventArgs e)//show database path
        {

        }

        //define user method
        private List<string> ReadFileToList(string fullPath)// read file return student list
        {
            List<string> objList = new List<string>();
            string line = string.Empty;
            try
            {
                StreamReader file = new StreamReader(fullPath, Encoding.Default);
                line = file.ReadLine();
                while (line != null)
                {
                    if (line != "")
                    {
                        objList.Add(line);

                    }
                    line = file.ReadLine();
                }
                file.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return objList;
        }
        private void CreateTable(string filename)// read file create database table
        {
            using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    DataTable dt = sh.GetTableList();
                    dt.ToString();
                    SQLiteTable tb = new SQLiteTable(filename);
                    tb.Columns.Add(new SQLiteColumn("CmdID", ColType.Integer, false, true, true, "0"));
                    tb.Columns.Add(new SQLiteColumn("CmdType", ColType.Text, false, false, true, ""));
                    tb.Columns.Add(new SQLiteColumn("CmdValue", ColType.Text, false, false, true, ""));

                    sh.CreateTable(tb);
                    conn.Close();
                }
            }
        }
        private void DeleteTable(string filename)// read file create database table
        {
            config.DatabaseFile = "database.sqlite";
            using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;

                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    //SQLiteTable tb = new SQLiteTable(filename);
                    sh.DropTable(filename);
                    //sh.CreateTable(tb);
                    conn.Close();
                }
            }
        }
        private Boolean CheckExistTable(string filename)// read file create database table
        {
            using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;
                    //string Query = string.Format("SELECT count(*) FROM sqlite_master WHERE name=`{0}`;", filename);
                    string Query = string.Format("SELECT * FROM sqlite_master WHERE name='{0}';", filename);
                    SQLiteCommand createCommand = new SQLiteCommand(Query, conn);

                    createCommand.ExecuteNonQuery();
                    SQLiteDataReader dr = createCommand.ExecuteReader();

                    int count = 0;
                    while (dr.Read())
                    {
                        count++;
                    }

                    conn.Close();

                    if (count == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        private void InsertCmdRow(List<string> objListCmd, string filename)// read file return student list
        {
            using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    int count = sh.ExecuteScalar<int>(string.Format("select count(*) from {0};", filename)) + 1;
                    string CmdType = "common";//false as common, true as alg
                    var dic = new Dictionary<string, object>();

                    foreach (var Cmd in objListCmd)
                    {
                        if (Cmd.StartsWith("testinit") | Cmd.StartsWith("get-chipid")|
                            Cmd.StartsWith("set-clock") | Cmd.StartsWith("set-vccvolt") | Cmd.StartsWith("get-chipid"))
                        {
                            dic["CmdID"] = count;
                            dic["CmdType"] = CmdType;
                            dic["CmdValue"] = Cmd;
                            sh.Insert(filename, dic);
                        }
                        else if (Cmd.StartsWith("alg"))
                        {
                            CmdType = Cmd;
                        }
                        else
                        {
                            string[] split = Cmd.Split(new Char[] { ':' });
                            dic["CmdID"] = count;
                            dic["CmdType"] = CmdType;
                            dic["CmdValue"] = Cmd;
                            sh.Insert(filename, dic);
                        }
                        count++;
                    }
                    conn.Close();
                }
            }
        }

        private void LoadCmdToGrid()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        conn.Open();
                        cmd.Connection = conn;

                        SQLiteHelper sh = new SQLiteHelper(cmd);

                        DataTable dt = sh.GetTableList();
                        dt.Rows.Remove(dt.Rows[0]);
                        dt.Rows.Remove(dt.Rows[0]);
                        DeveiceTypeBS.DataSource = dt;
                        cboDeviceType.DataSource = DeveiceTypeBS;
                        cboDeviceType.ValueMember = dt.Columns[0].ColumnName;
                        
                        string Query = string.Format("SELECT distinct cmdtype FROM {0};", cboDeviceType.SelectedValue.ToString());
                        DataTable dt2 = sh.Select(Query);
                        AlgBS.DataSource = dt2;
                        cboAlg.DataSource = AlgBS;
                        cboAlg.ValueMember = dt2.Columns[0].ColumnName;

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private void UpdateAlgList()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        conn.Open();
                        cmd.Connection = conn;

                        SQLiteHelper sh = new SQLiteHelper(cmd);

                        string Query = string.Format("SELECT distinct cmdtype FROM {0};", cboDeviceType.SelectedValue.ToString());
                        DataTable dt3 = sh.Select(Query);

                        AlgBS.DataSource = dt3;
                        AlgBS.ResetBindings(false);
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private void GetStatus()
        {
            try
            {
                string tableName = cboDeviceType.SelectedValue + "";
                string AlgName = cboAlg.SelectedValue + "";

                using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        conn.Open();
                        cmd.Connection = conn;

                        SQLiteHelper sh = new SQLiteHelper(cmd);

                        string Query = string.Format("SELECT CmdValue FROM {0} WHERE CmdType = '{1}' or CmdType = 'common';", tableName, AlgName);
                        //SELECT * FROM mircon_1G_cmd WHERE CmdType = 'alg = 1' or CmdType = 'common';
                        DataTable dt = sh.Select(Query);
                        dgvCmd.DataSource = dt;

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        
        //Tcp function
        private void ConnectToServer()
        {
            //定义一个套字节监听  包含3个参数(IP4寻址协议,流式连接,TCP协议)
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //获取文本框输入的服务端IP和Port
            IPEndPoint endpoint = new IPEndPoint(config.clientIP, config.clientPort);
            try
            {
                //向指定的ip和端口号的服务端发送连接请求 用的方法是Connect 不是Bind
                socketClient.Connect(endpoint);

                //创建一个新线程 用于监听服务端发来的信息
                threadClient = new Thread(RecMsg);
                //将窗体线程设置为与后台同步
                threadClient.IsBackground = true;
                //启动线程
                threadClient.Start();
                txtMsg.AppendText("Connected server, start communication...\r\n");

            }
            catch (SocketException ex)
            {
                btnExecute.Enabled = false;
                txtMsg.AppendText("Socker error message:" + ex.Message + "\r\n");
            }
        }
        private void RecMsg()
        {
            while (true) //持续监听服务端发来的消息
            {
                int length = 0;
                byte[] buffer = new byte[ReceiveBufferSize];
                try
                {
                    //将客户端套接字接收到的字节数组存入内存缓冲区, 并获取其长度
                    length = socketClient.Receive(buffer);
                }
                catch (SocketException ex)
                {
                    btnExecute.Enabled = false;
                    txtMsg.AppendText("Socker error message:" + ex.Message + "\r\n");
                    txtMsg.AppendText("Server disconnect...\r\n");
                    break;

                }
                catch (Exception ex)
                {
                    btnExecute.Enabled = false;
                    txtMsg.AppendText("System error message: " + ex.Message + "\r\n");
                    break;
                }
                //将套接字获取到的字节数组转换为人可以看懂的字符串
                strRecMsg = Encoding.UTF8.GetString(buffer, 0, length);

                //将文本框输入的信息附加到txtMsg中  并显示 谁,什么时间,换行,发送了什么信息 再换行
                txtMsg.AppendText( strRecMsg +"\r\n");//"Server " + GetCurrentTime() + " send:\r\n" +
                btnExecute.Enabled = true;
            }
        }

        private void ClientSendMsg(string sendMsg, byte symbol)
        {
            if (sendMsg.Trim() == string.Empty) return;
            byte[] arrClientMsg = Encoding.UTF8.GetBytes(sendMsg);
            socketClient.Send(arrClientMsg);
            txtMsg.AppendText(GetCurrentTime() + "  "+sendMsg + "\r\n");
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            tsslTime.Text = GetCurrentTime();
        }
        public String GetCurrentTime()
        {
            DateTime dt = DateTime.Now;
            string currentTime = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return currentTime;
        }


    }
}