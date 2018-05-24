using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Text.RegularExpressions;


namespace FlashTest
{
    public partial class frmMain : Form
    {
        public string fullPath = string.Empty;
        public string filename = string.Empty;
        private Boolean TypeFirstChangeFlag = false;
        private Boolean AlgFirstChangeFlag = false;
        private Boolean RecRespFlag = false;

        BindingSource DeveiceTypeBS = new BindingSource();
        BindingSource AlgBS = new BindingSource();

        private const int SendBufferSize = 1 * 1024;
        private const int ReceiveBufferSize = 1 * 1024;
        //private Boolean Ackflag = false;
        Socket socketClient = null;
        Thread threadClientRec = null;
        Thread threadDownloadPat = null;

        private List<string> objListCmd = new List<string>(); //define cmd list info variable

        public frmMain()
        {
            InitializeComponent();
            //Control.CheckForIllegalCrossThreadCalls = false;
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
        private void btnExecute_Click(object sender, EventArgs e)// send list commmand
        {
            if (dgvCmd.RowCount == 0) return;
            if (dgvCmd.CurrentRow.Index < dgvCmd.RowCount - 1)
            {
                string currentCmd = dgvCmd.CurrentRow.Cells[0].Value.ToString();
                ClientSendMsg(currentCmd, 0);
                dgvCmd.CurrentCell = dgvCmd.Rows[dgvCmd.CurrentRow.Index + 1].Cells[0];

            }
            else if (dgvCmd.CurrentRow.Index == dgvCmd.RowCount - 1)
            {
                string currentCmd = dgvCmd.CurrentRow.Cells[0].Value.ToString();
                ClientSendMsg(currentCmd, 0);
                dgvCmd.CurrentCell = dgvCmd.Rows[0].Cells[0];
            }
            ModifyButton(btnExecute, false);
            Thread.Sleep(1);
        }
        private void btnEndCmd_Click(object sender, EventArgs e)// end command execute, index back to 0
        {
            dgvCmd.CurrentCell = dgvCmd.Rows[0].Cells[0];
        }
        private void txtCMsg_KeyDown(object sender, KeyEventArgs e)//Enter send message
        {   //当光标位于输入文本框上的情况下 发送信息的热键为回车键Enter 
            if (e.KeyCode == Keys.Enter)
            {
                //则调用客户端向服务端发送信息的方法
                ClientSendMsg(txtCMsg.Text, 0);
            }
        }
        private void cboDeviceType_SelectedIndexChanged(object sender, EventArgs e)// update cmd data grid view when device type change
        {
            if (TypeFirstChangeFlag)
            {
                UpdateAlgList();
                GetStatus();
            }
            else TypeFirstChangeFlag = true;
        }
        private void cboAlg_SelectedIndexChanged(object sender, EventArgs e)//upadte cmd data grid view when alg change
        {
            if (AlgFirstChangeFlag)
            {
                GetStatus();
            }
            else AlgFirstChangeFlag = true;
        }
        //textbox invoke
        private delegate void InvokeCallback(string msg);
        private void ShowMsg(string msg)//show meassage to txt box
        {
            if (txtMsg.InvokeRequired)
            {
                InvokeCallback msgCallback = new InvokeCallback(ShowMsg);
                txtMsg.Invoke(msgCallback, new object[] { msg });

            }
            else
            {
                txtMsg.AppendText(msg + "\r\n");
                ModifyButton(btnExecute, true);
            }
        }
        //button invoke
        private delegate void ModifyButton_dg(Button _btnName, bool _b);
        private void ModifyButton(Button _btnName, bool _b)// enable or disable button
        {
            _btnName.Enabled = _b;
        }


        //menu action
        private void tsmAbout_Click(object sender, EventArgs e)
        {
            frmAbout aboutbox = new frmAbout();
            aboutbox.ShowDialog();
        }
        private void tsmImportCmd_Click(object sender, EventArgs e)// import cmd file to database
        {

            //1.select file
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "TXT File(*.txt)|*.txt|CSV File(*.csv)|*.csv|All Files(*.*)|*.txt";
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                fullPath = openfile.FileName;//send the filename to global Variable
                filename = Path.GetFileNameWithoutExtension(fullPath);
                if (!IsValidFileName(filename))
                {
                    MessageBox.Show("File Name Only Contains [A-Za-z_0-9]", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
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


            //
            GetStatus();

        }
        private void tsmExit_Click(object sender, EventArgs e)//close program
        {
            Application.Exit();
        }
        private void tsmShowDatabase_Click(object sender, EventArgs e)//show database path
        {
            using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    DataTable dt = sh.ShowDatabase();
                    string DatabasePath = dt.Rows[0].ItemArray[2].ToString();
                    DatabasePath = DatabasePath.Replace(@"\\", @"\");
                    MessageBox.Show(DatabasePath);
                    conn.Close();
                }
            }
        }
        private void tsmDeleteCmd_Click(object sender, EventArgs e)//delete current combo device type
        {
            DeleteTable(cboDeviceType.SelectedValue.ToString());
            LoadCmdToGrid();
        }
        private void tsmPatternDownload_Click(object sender, EventArgs e)//download pattern
        {
            //创建一个新线程 用于监听服务端发来的信息
            threadDownloadPat = new Thread(DownloadPattern);
            threadDownloadPat.SetApartmentState(ApartmentState.STA);
            //将窗体线程设置为与后台同步
            threadDownloadPat.IsBackground = true;
            //启动线程
            threadDownloadPat.Start();

        }

        //define user method
        private void DownloadPattern()//download pattern action
        {
            //1.select file
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "TXT File(*.txt)|*.txt";
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                fullPath = openfile.FileName;//send the filename to global Variable
                filename = Path.GetFileNameWithoutExtension(fullPath);
                if (!IsValidFileName(filename))
                {
                    MessageBox.Show("File Name Only Contains [A-Za-z_0-9]", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                ShowMsg(GetCurrentTime() + " Start Download Pattern " + filename);
                //2.用文件流打开用户要发送的文件；
                using (FileStream fs = new FileStream(fullPath, FileMode.Open))
                {
                    long fileLength = new FileInfo(fullPath).Length;//read file size, 10bit for 1 line
                    StreamReader sr = new StreamReader(fs);
                    int LineNum = 1;

                    string line_s = "";
                    string sendStr = "";
                    while ((line_s = sr.ReadLine()) != null)//read line by line
                    {
                        String Hexstr = String.Format("{0:X2}", Convert.ToByte(line_s, 2));
                        if (LineNum % 512 != 0)
                        {
                            sendStr = sendStr + Hexstr;
                        }
                        else
                        {
                            sendStr = sendStr + Hexstr + "FF";
                            ClientSendMsg(sendStr, 1);
                            sendStr = "";
                            RecRespFlag = false;
                            ShowMsg(GetCurrentTime() + " "+ filename+ " download " + string.Format("{0:P}", LineNum * 10.0 / fileLength));
                        }
                        while (!RecRespFlag)
                        {
                            Thread.Sleep(10);
                        }
                        LineNum++;
                    }
                    //send the remainder char and suffix
                    sendStr = sendStr + "00";
                    ClientSendMsg(sendStr, 1);
                    sendStr = "";

                    ShowMsg(GetCurrentTime() + " Finished Download Pattern " + filename);
                }
            }
        }
        private List<string> ReadFileToList(string fullPath)// read file return line list
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
        private void DeleteTable(string filename)// delete database table
        {
            using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    sh.DropTable(filename);
                    conn.Close();
                }
            }
        }
        private Boolean CheckExistTable(string filename)// check if table exist
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
        private void InsertCmdRow(List<string> objListCmd, string filename)// insert one row to database list
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
                        if (Cmd.StartsWith("testinit") | Cmd.StartsWith("get-chipid") |
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
        private void LoadCmdToGrid()//load device type and alg to combo
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
                        if (dt.Rows.Count > 2)
                        {
                            dt.Rows.Remove(dt.Rows[0]);
                            dt.Rows.Remove(dt.Rows[0]);
                            DeveiceTypeBS.DataSource = dt;
                            cboDeviceType.DataSource = DeveiceTypeBS;
                            cboDeviceType.ValueMember = "Tables";  //dt.Columns[0].ColumnName;

                            string Query = string.Format("SELECT distinct cmdtype FROM {0};", cboDeviceType.SelectedValue.ToString());
                            DataTable dt2 = sh.Select(Query);
                            AlgBS.DataSource = dt2;
                            cboAlg.DataSource = AlgBS;
                            cboAlg.ValueMember = dt2.Columns[0].ColumnName;
                        }
                        else
                        {
                            DeveiceTypeBS.DataSource = null;
                            AlgBS.DataSource = null;
                            return;
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private void UpdateAlgList()//update alg combo list based on device type change
        {
            string queryString = cboDeviceType.SelectedValue.ToString();
            if (queryString != "")
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
            else
            {
                return;
            }
        }
        private void GetStatus()// load cmd to data grid view based on device type and alg
        {
            try
            {
                string tableName = cboDeviceType.SelectedValue + "";
                string AlgName = cboAlg.SelectedValue + "";
                if (tableName != "" & AlgName != "")
                {
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
                else
                {
                    dgvCmd.DataSource = null;
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Tcp function
        private void ConnectToServer()//testing tcp connection
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
                threadClientRec = new Thread(RecMsg);
                //将窗体线程设置为与后台同步
                threadClientRec.IsBackground = true;
                //启动线程
                threadClientRec.Start();

                ShowMsg("Connected server, start communication...");
            }
            catch (SocketException ex)
            {
                btnExecute.Enabled = false;
                ShowMsg("Socker error message:" + ex.Message);
            }
        }
        private void RecMsg()// recieve tcp meassage 
        {
            string strRecMsg = null;
            string newMsg = null;
            while (true) //持续监听服务端发来的消息
            {
                int length = 0;
                byte[] buffer = new byte[ReceiveBufferSize];
                try
                {
                    //将客户端套接字接收到的字节数组存入内存缓冲区, 并获取其长度
                    //Thread.Sleep(10000);
                    length = socketClient.Receive(buffer);
                }
                catch (SocketException ex)
                {
                    ShowMsg("Socker error message:" + ex.Message);
                    ShowMsg("Server disconnect...");
                    break;
                }
                catch (Exception ex)
                {
                    ShowMsg("System error message: " + ex.Message);
                    break;
                }
                //将套接字获取到的字节数组转换为人可以看懂的字符串
                strRecMsg = Encoding.UTF8.GetString(buffer, 0, length);

                //将文本框输入的信息附加到txtMsg中  并显示 谁,什么时间,换行,发送了什么信息 再换行
                if (Regex.IsMatch(strRecMsg, @"\]$"))
                {
                    ShowMsg(newMsg + strRecMsg);
                    newMsg = null;
                    RecRespFlag = true;
                }

                else
                {
                    newMsg = newMsg + strRecMsg;
                    //ShowMsg(newMsg+"-----------");
                }
            }
        }
        private void ClientSendMsg(string sendMsg, byte symbol)//send textbox command  
        {
            if (sendMsg.Trim() == string.Empty) return;
            byte[] arrClientMsg = Encoding.UTF8.GetBytes(sendMsg);
            socketClient.Send(arrClientMsg);
            if (symbol == 0)
            {
                ShowMsg(GetCurrentTime() + "  " + sendMsg);
            }
        }
        

        //commom function
        private void timer1_Tick(object sender, EventArgs e)// update timer every second
        {
            tsslTime.Text = GetCurrentTime();
        }
        public String GetCurrentTime()
        {
            DateTime dt = DateTime.Now;
            string currentTime = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return currentTime;
        }
        public static bool IsValidFileName(string filename)//check the file name
        {
            if (Regex.IsMatch(filename, @"^([A-Za-z_0-9]{0,})$"))
            {   // 判断内容（只能是字母、下划线、数字）是否合法
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}