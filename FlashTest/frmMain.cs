using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
            CreateTable(filename);

            //3.record the student data to list
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
        private void CreateTable(string filename)// read file return student list
        {
            config.DatabaseFile = "database.sqlite";
            using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;
                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    SQLiteTable tb = new SQLiteTable(filename);
                    tb.Columns.Add(new SQLiteColumn("ID"));
                    sh.CreateTable(tb);
                    conn.Close();
                }
            }
        }
    }
}