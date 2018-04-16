using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data;

namespace FlashTest
{
    public partial class frmMain : Form
    {
        public string fullPath = string.Empty;
        public string filename = string.Empty;
        private List<string> objListCmd = new List<string>(); //define student info variable

        public frmMain()
        {
            InitializeComponent();
            LoadCmdToGrid();
        }

        //define control method
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)//closing action
        {
            Application.Exit();
        }
        private void btnImportCmd_Click(object sender, EventArgs e)//import cmd file
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
            config.DatabaseFile = "database.sqlite";
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
            config.DatabaseFile = "database.sqlite";
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
            config.DatabaseFile = "database.sqlite";
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
                        cboDeviceType.DataSource = dt;
                        cboDeviceType.ValueMember = dt.Columns[0].ColumnName;
                        
                        string Query = string.Format("SELECT cmdtype FROM {0};", cboDeviceType.SelectedValue.ToString());
                        SQLiteCommand createCommand = new SQLiteCommand(Query, conn);

                        createCommand.ExecuteNonQuery();
                        SQLiteDataReader dr = createCommand.ExecuteReader();
                        //cboAlg.DataSource = dr;
                        //cboAlg.ValueMember = dr.Columns[0].ColumnName;

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private void cboDeviceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetStatus();
        }
        private void cboAlg_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetStatus();
        }
        private void GetStatus()
        {
            try
            {
                string tableName = cboDeviceType.SelectedValue + "";

                using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        conn.Open();
                        cmd.Connection = conn;

                        SQLiteHelper sh = new SQLiteHelper(cmd);

                        DataTable dt = sh.GetColumnStatus(tableName);
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
    }
}