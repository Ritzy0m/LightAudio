using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Windows.Forms;
using System.IO.Ports;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Drawing.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RGB_Control
{
    public partial class form1 : Form
    {
        List<string> portnames = new List<string>();
        Point lastPoint;
        bool sidebarExpand;
        bool LEDPanelExpand;
        bool readstate;
        string monitor;
        string ledUpdate;
        string SelectedCom;
        private BackgroundWorker backgroundThreading;

        public form1()
        {
            backgroundThreading = new BackgroundWorker();
            backgroundThreading.WorkerReportsProgress = true;
            backgroundThreading.DoWork += new DoWorkEventHandler(backgroundThreading_DoWork);
            backgroundThreading.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundTheading_Complete);
            backgroundThreading.ProgressChanged += new ProgressChangedEventHandler(backgroundThreading_InProgress);
            InitializeComponent();
            portnames = GetPorts();

            foreach (string port in portnames)
            {
                textBox1.AppendText(port + "\r\n");
            }
            foreach (string COMID in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(COMID);
            }
            
        }
        public List<string> GetPorts()
        {
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM " + "Win32_PnPEntity WHERE Caption like '%(COM%'"))
            {
                string[] portnames = SerialPort.GetPortNames();

                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList().Select(p => p["Caption"].ToString());

                List<string> portList = portnames.Select(n => n + " - " + ports.FirstOrDefault(s => s.Contains(n))).ToList();

                return portList;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            portnames = GetPorts();
            textBox1.Clear();
            comboBox1.Items.Clear();
            foreach (string port in portnames)
            {
                textBox1.AppendText(port + "\r\n");

            }
            foreach (string COMID in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(COMID);
            }

        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (LEDPanelExpand)
            {
                LEDTimer.Start();
               
            }
            if (sidebar.Width == sidebar.MaximumSize.Width)
            {
                MenuTimer.Start();
            }
            else
            {
                return;
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                Application.Exit();
            }
            else
            {
                Application.Exit();
            }

        }

        private void button10_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width == sidebar.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    MenuTimer.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width == sidebar.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    MenuTimer.Stop();
                }
            }
        }

        private void panel4_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            MenuTimer.Start();
        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void LEDTimer_Tick(object sender, EventArgs e)
        {
            if (LEDPanelExpand)
            {
                LEDPanel.Width -= 100;
                if (LEDPanel.Width == LEDPanel.MinimumSize.Width)
                {
                    LEDPanelExpand = false;
                    LEDTimer.Stop();
                }
            }
            else
            {
                LEDPanel.Width += 100;
                if (LEDPanel.Width == LEDPanel.MaximumSize.Width)
                {
                    LEDPanelExpand = true;
                    LEDTimer.Stop();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LEDTimer.Start();
            if (sidebar.Width == sidebar.MaximumSize.Width)
            {
                MenuTimer.Start();
            }
            else
            {
                return;
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                Form2 openform = new Form2();
                openform.Show();
                return;
            }
            if (comboBox1 != null)
            {
                string selected = comboBox1.SelectedItem.ToString();
                richTextBox1.AppendText("COM Port " + selected + "\r\n");
                if (comboBox2.SelectedItem == null)
                {
                    Form2 openform = new Form2();
                    openform.Show();
                    return;
                }
                if (comboBox2.SelectedItem != null)
                {
                    int Baude = Int32.Parse(comboBox2.SelectedItem.ToString());
                    richTextBox1.AppendText("Buade Rate " + Baude + "\r\n");
                    button3.SendToBack();
                    label6.Text = string.Empty;
                    label6.Text = "Connected";
                    serialPort1.PortName = selected;
                    serialPort1.BaudRate = Baude;
                    readstate = true;
                    backgroundThreading.RunWorkerAsync();
                }
            }
        }

        private void LEDPanel_SizeChanged(object sender, EventArgs e)
        {
            if (LEDPanel.Width == LEDPanel.MinimumSize.Width)
            {
                button4.BackColor = Color.FromArgb(25, 25, 25);
                button5.BackColor = Color.FromArgb(43, 43, 43);
            }
            if (LEDPanel.Width == LEDPanel.MaximumSize.Width)
            {
                button4.BackColor = Color.FromArgb(43, 43, 43);
                button5.BackColor = Color.FromArgb(25, 25, 25);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            button9.SendToBack();
            label6.Text = string.Empty;
            serialPort1.Close();
            label6.Text = "Disconnected";
            readstate = false;
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //Thread newThread = new Thread(new ThreadStart(SerialThreading)); newThread.Start();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void panel17_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
               serialPort1.Write(textBox3.Text.ToString());
                textBox3.Clear();
            }
            else
            {
                return;
            }

        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Write(textBox3.Text.ToString());
                    textBox3.Clear();
                }
                else
                {
                    return;
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel18_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem.ToString() == "WS2812B")
            {
                comboBox5.Text = "RGB";
            }
            else
            {
                return;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ledUpdate = "U";
            if (numericUpDown2.Value <= 9)
            {
                ledUpdate = ledUpdate + "0" + "0" + numericUpDown2.Value.ToString();
            }
            if (numericUpDown2.Value > 9 && numericUpDown2.Value <= 99)
            {
                ledUpdate = ledUpdate + "0" + numericUpDown2.Value.ToString();
            }
            if (numericUpDown2.Value > 99)
            {
                ledUpdate = ledUpdate + numericUpDown2.Value.ToString();
            }
            if (numericUpDown1.Value <= 9)
            {
                ledUpdate = ledUpdate + "P" + "0" + numericUpDown1.Value.ToString();
            }
            if (numericUpDown1.Value <= 99 && numericUpDown1.Value > 9)
            {
                ledUpdate = ledUpdate + "P" + numericUpDown1.Value.ToString();
            }
            if (comboBox3.Text.Length != 7)
            {
                ledUpdate = ledUpdate + "C" + comboBox3.SelectedItem;
                while (ledUpdate.Length != 15)
                {
                    ledUpdate = ledUpdate + "I";
                }
            }
            else
            {
                ledUpdate = ledUpdate + "C" + comboBox3.SelectedItem;
            }
            if (comboBox5.Text.Length == 3)
            {
                ledUpdate = ledUpdate + "O" + comboBox5.SelectedItem.ToString();
            }
            else
            {
                Form3 form3 = new Form3();
                form3.Show();
                return;
            }
            if (serialPort1.IsOpen)
            {
                serialPort1.Write(ledUpdate);
            }
            else
            {
                Form3 form3 = new Form3();
                form3.Show();
                return;
            }
            serialPort1.Write(ledUpdate);
            Console.WriteLine(ledUpdate);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
           // if ()
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown2.Value <= 99)
            {
               // numericUpDown2.Value = NumericUpDown2();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
 
        public void backgroundThreading_DoWork(object sender, DoWorkEventArgs e)
        {
            serialPort1.Open();
            if (serialPort1.IsOpen)
            {
                while (serialPort1.IsOpen)
                {
                    if (serialPort1.IsOpen)
                    {
                        //backgroundThreading.ReportProgress();
                        //richTextBox1.Invoke((MethodInvoker)(() => richTextBox1.AppendText(serialPort1.ReadLine() + " \r\n"))); //need to be changed due to changing UI in thread
                    }
                }
            }
            else
            {
                serialPort1.Close();
                label6.Text = string.Empty;
                label6.Text = "Disconnected";
                return;
            }
        }

        public void backgroundTheading_Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("Complete");
            label6.Text = string.Empty;
            label6.Text = "Disconnected";
            button9.SendToBack();
            portnames = GetPorts();
            textBox1.Clear();
            comboBox1.Items.Clear();
            foreach (string port in portnames)
            {
                textBox1.AppendText(port + "\r\n");

            }
            foreach (string COMID in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(COMID);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {

        }

        private void backgroundThreading_InProgress(object sender, ProgressChangedEventArgs e)
        {
            richTextBox1.AppendText(serialPort1.ReadLine() + "\r\n");
        }

        private void Refresh_Tick(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                SelectedCom = comboBox1.SelectedItem.ToString();
            }
            portnames = GetPorts();
            textBox1.Clear();
            comboBox1.Items.Clear();
            foreach (string port in portnames)
            {
                textBox1.AppendText(port + "\r\n");

            }
            foreach (string COMID in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(COMID);
            }
            comboBox1.SelectedItem = SelectedCom;
        }
    }

}
