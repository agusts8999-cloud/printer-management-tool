using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace WinSDKDemo
{
    public partial class Form1 : Form
    {
        IntPtr printer;
        public Form1()
        {
            InitializeComponent();
            GetComPort();
            cb_lpt.Items.Add("LPT1");
            cb_lpt.Items.Add("LPT2");
            cb_lpt.Items.Add("LPT3");
            PrinterDemo.PrinterCreator(ref printer, "");
            selRb = rb_USB;
        }
        RadioButton selRb;
        bool isOpen = false;
        public void GetComPort()
        {
            string[] str = SerialPort.GetPortNames();
            if (str != null)
            {
                foreach (var item in str)
                {
                    cb_COMName.Items.Add(item);
                }
            }
        }

        private void AddMsg(String msg)
        {
            tb_Msg.Text += $"{DateTime.Now.ToString("G")}: {msg} \r\n";
        }
        private void SampleBtnStatus(bool isOpen)
        {
            btn_Sample.Enabled = isOpen;
            btn_Barcode.Enabled = isOpen;
            btn_Qrcode.Enabled = isOpen;
            btn_Image.Enabled = isOpen;
            btn_PrinterStatus.Enabled = isOpen && selRb != rb_lpt;
            btn_Open.Enabled = !isOpen;
            btn_Close.Enabled = isOpen;
        }

        private void btn_Open_Click(object sender, EventArgs e)
        {
            if (isOpen)
            {
                SampleBtnStatus(false);
                PrinterDemo.ClosePort(printer);
            }
            string info = "";
            if (selRb == rb_USB)
            {
                info = "USB,";
            }
            else if (selRb == rb_COM)
            {
                info = $"{cb_COMName.Text},{cb_BaudRate.Text}";
            }
            else if (selRb == rb_NET)
            {
                info = $"NET,{tb_IP.Text}";
            }
            else if (selRb == rb_lpt)
            {
                info = cb_lpt.Text;
            }
            isOpen = PrinterDemo.OpenPort(printer, info) == 0;
            if (isOpen)
            {
                SampleBtnStatus(true);
                AddMsg("Open port success!");
            }
            else
            {
                AddMsg("Open port fail, please check!");
            }
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            if (isOpen)
            {
                AddMsg("Close port success!");
                PrinterDemo.ClosePort(printer);
                SampleBtnStatus(false);
                isOpen = false;
            }
        }
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            PrinterDemo.ReleasePrinter(printer);
        }
        private void btn_PrinterStatus_Click(object sender, EventArgs e)
        {
            PrinterDemo.GetStatus(printer, msg => AddMsg(msg));
        }

        private void btn_Qrcode_Click(object sender, EventArgs e)
        {
            AddMsg("Print QRCode");
            PrinterDemo.PrintQRCode(printer);
        }

        private void btn_Barcode_Click(object sender, EventArgs e)
        {
            AddMsg("Print Barcode");
            PrinterDemo.PrintBarCode(printer);
        }

        private void btn_Sample_Click(object sender, EventArgs e)
        {
            AddMsg("Print Sample");
            PrinterDemo.PrintSample(printer);
        }

        private void btn_Image_Click(object sender, EventArgs e)
        {
            AddMsg("Print Image");
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files(*.*)|*.*";
            openFileDialog1.InitialDirectory = Environment.CurrentDirectory.ToString();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                PrinterDemo.PrintImage(printer, openFileDialog1.FileName);
            }
        }
        private void tb_Msg_TextChanged(object sender, EventArgs e)
        {
            tb_Msg.SelectionStart = tb_Msg.TextLength;
            tb_Msg.ScrollToCaret();
        }

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            selRb = (RadioButton)sender;
        }
    }
}
