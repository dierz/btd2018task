using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BecomeTheDeveloper2018
{
    public partial class Form1 : Form
    {
        public String file1name=String.Empty, file2name=String.Empty;
        public List<String> file1names;
        public List<String> file2names;
        public int c = 0;
        public Form1()
        {
            InitializeComponent();
            checkFileStatus();
            file2names = new List<String>();
            file1names = new List<String>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "C:\\Users\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                file1name = openFileDialog1.FileName;
            }
            checkFileStatus();
            richTextBox2.Text += "file1 has loaded\n";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            openFileDialog2.InitialDirectory = "C:\\Users\\";
            openFileDialog2.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog2.FilterIndex = 2;
            openFileDialog2.RestoreDirectory = true;

            if (openFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                file2name = openFileDialog2.FileName;
            }
            checkFileStatus();
            richTextBox2.Text += "file2 has loaded\n";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            if (file2name == String.Empty)
            {
                richTextBox2.Text += "file2 is not loaded. Error!\n";
                return;
            }
            String[] lines2 = File.ReadAllLines(file2name);
            int cnt = lines2.Length;
            Analyze2(lines2, cnt);
            lines2 = null;
            if (file1name == String.Empty)
            {
                richTextBox2.Text += "file1 is not loaded. Error!\n";
                return;
            }
            String[] lines1 = File.ReadAllLines(file1name);
            cnt = lines1.Length;
            Analyze1(lines1, cnt);
            foreach(String s in file1names)
            {
                richTextBox1.Text += s + "\n";
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            richTextBox2.Text += elapsedTime + "\n";
        }


        public void checkFileStatus()
        {
            if (file1name.Equals(String.Empty))
            {
                label3.Visible = false;
            }
            else
            {
                label3.Visible = true;
            }
            if (file2name.Equals(String.Empty))
            {
                label4.Visible = false;
            }
            else
            {
                label4.Visible = true;
            }
        }
        public void FindOccurences2(string line)
        {
            String ph;
            String pattern = @"(user:)\s(\w+)";
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
            Match m = r.Match(line);
            while (m.Success)
            {
                Group g = m.Groups[2];
                ph = g.Value;
                if (!IsFile2NameInList(ph))
                {
                    file2names.Add(ph);
                    //richTextBox2.Text += ph + "\n";
                }
                m=m.NextMatch();
            }
        }
        public void FindOccurences1(string line)
        {
            String ph;
            String pattern = @"(user:)\s(\w+)";
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
            Match m = r.Match(line);
            while (m.Success)
            {
                Group g = m.Groups[2];
                ph = g.Value;
                if (!IsFile2NameInList(ph)&&!IsFile1NameInList(ph))
                {
                    file1names.Add(ph);
                }
                m = m.NextMatch();
            }
        }
        public bool IsFile2NameInList(string name)
        {
            if (file2names.Contains(name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsFile1NameInList(string name)
        {
            if (file1names.Contains(name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Analyze2(String[] lines2, int cnt)
        {
            for (c = 0; c < cnt; c++)
            {
                FindOccurences2(lines2.ElementAt(c));
            }
        }
        public void Analyze1(string[] lines1, int cnt)
        {
            for (c = 0; c < cnt; c++)
            {
                FindOccurences1(lines1.ElementAt(c));
            }
        }
    }
}
