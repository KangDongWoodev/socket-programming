using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        private const int PORT1 = 8000; //포트번호
        private const int PORT2 = 9000; //포트번호
        private const int BUFFER_SIZE = 1024;
        private TcpClient client;
        private TcpClient client2;
        private NetworkStream stream;
        private NetworkStream stream2;
        private Thread serialThread;
        private Thread receiveThread;
        string save_path;

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            btnCon.Enabled = false; //버튼비활성화
        }

        private void btnCon_Click(object sender, EventArgs e)
        {
            ConnectToServer();

        }

        private void ConnectToServer()
        {
            client = new TcpClient();
            client.Connect("192.168.0.86", PORT2);
            stream = client.GetStream();
            try
            {
                receiveThread = new Thread(new ThreadStart(ReceiveFiles));
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to server: " + ex.Message);
            }
        }

        private void ReceiveFiles()
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                for (int j =0; j< 600; j++)
                {
                    stopwatch.Start();
                    for (int i = 0; i < 60; i++)
                        {
                            // 파일 사이즈를 받기
                            byte[] sizeBytes = new byte[sizeof(int)];
                            int sizeReceived = stream.Read(sizeBytes, 0, sizeof(int));
                            if (sizeReceived != sizeof(int))
                            {
                                throw new Exception("Failed to receive file size");
                            }
                            int fileSize = BitConverter.ToInt32(sizeBytes, 0);

                            // 파일 데이터를 받기
                            byte[] buffer = new byte[BUFFER_SIZE];
                            int totalBytesReceived = 0;
                            MemoryStream memoryStream = new MemoryStream();
                            while (totalBytesReceived < fileSize)
                            {
                                int bytesReceived = stream.Read(buffer, 0, Math.Min(fileSize - totalBytesReceived, BUFFER_SIZE));
                                if (bytesReceived == 0)
                                {
                                    throw new Exception("Connection closed unexpectedly");
                                }
                                memoryStream.Write(buffer, 0, bytesReceived);
                                totalBytesReceived += bytesReceived;
                            }
                            Image image = Image.FromStream(memoryStream);
                            Bitmap bmp = new Bitmap(image, 320, 240);

                            if (button_red.Enabled == false)
                            {
                                for (int x = 0; x < bmp.Width; x++)
                                {
                                    for (int y = 0; y < bmp.Height; y++)
                                    {
                                        Color pixelColor = bmp.GetPixel(x, y);
                                        int red = pixelColor.R;
                                        Color newColor = Color.FromArgb(red, 0, 0);
                                        bmp.SetPixel(x, y, newColor);
                                    }
                                }
                                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                                pictureBox1.BackgroundImage = bmp;
                            }
                            else if (button_green.Enabled == false)
                            {
                                for (int x = 0; x < bmp.Width; x++)
                                {
                                    for (int y = 0; y < bmp.Height; y++)
                                    {
                                        Color pixelColor = bmp.GetPixel(x, y);
                                        int green = pixelColor.G;
                                        Color newColor = Color.FromArgb(0, green, 0);
                                        bmp.SetPixel(x, y, newColor);
                                    }
                                }
                                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;     //Make picture fill in the panel
                                pictureBox1.BackgroundImage = bmp;
                            }
                            else if (button_blue.Enabled == false)
                            {
                                for (int x = 0; x < bmp.Width; x++)
                                {
                                    for (int y = 0; y < bmp.Height; y++)
                                    {
                                        Color pixelColor = bmp.GetPixel(x, y);
                                        int blue = pixelColor.B;
                                        Color newColor = Color.FromArgb(0, 0, blue);
                                        bmp.SetPixel(x, y, newColor);
                                    }
                                }
                                // 이미지 보여주기
                                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;     //Make picture fill in the panel
                                pictureBox1.BackgroundImage = bmp;
                            }
                            else
                            {
                                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;     //Make picture fill in the panel
                                pictureBox1.BackgroundImage = bmp;
                                Thread.Sleep(1000);
                            }
                            // 이미지 저장
                            string date = DateTime.Now.ToString("yyyy.MM.dd HH시mm분ss초fff");
                            string savefile = save_path + "\\" + date + ".bmp";
                            textBox1.Text = savefile;
                            image.Save(savefile);
                            memoryStream.Dispose();
                        }
                    stopwatch.Stop();
                    TimeSpan ts = stopwatch.Elapsed;
                    double second = ts.Seconds;
                    double msecond = ts.Milliseconds;
                    int fps = (int)(60 / (second+(msecond/1000))); 
                    textBox2.Text = "이미지 파일 60 개받는데 걸린 시간 " + ts.Seconds + "초 " + ts.Milliseconds + "밀리초 "+fps+"프레임";
                    stopwatch.Reset();
                }

                MessageBox.Show("All files received successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error receiving file: " + ex.Message);
            }
            finally
            {
                stream.Close();
                client.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnCon.Enabled = true; //버튼비활성화
            select_folder();
        }

        private void select_folder() // 폴터00 셀렉트
        {
            FolderBrowserDialog folder = new FolderBrowserDialog(); //폴더띄우는 함수선언
            folder.ShowDialog(); // 폴더 띄움
            string save_folder = folder.SelectedPath + "\\";//경로지정

            string date = DateTime.Now.ToString("yyyy.MM.dd HH시mm분"); //폴더 생성
            save_path = save_folder + date;
            Directory.CreateDirectory(save_path);

            textBox1.Text = "저장 위치: " + save_path;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            client.Close();
            client2.Close();
            textBox1.Text = "접속 종료";
            btnCon.Enabled = true;
            btnClose.Enabled = false;
        }

        private void btnSerialConn_Click(object sender, EventArgs e)
        {
          
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            
        }

        private void button_red_Click(object sender, EventArgs e)
        {
            button_red.Enabled = false; //버튼비활성화
            button_green.Enabled = true; //버튼활성화
            button_blue.Enabled = true; //버튼활성화
            button_orginal.Enabled = true; //버튼활성화
        }


        private void button_reds()
        {
            button_red.Enabled = false; //버튼비활성화
            button_green.Enabled = true; //버튼활성화
            button_blue.Enabled = true; //버튼활성화
            button_orginal.Enabled = true; //버튼활성화
        }

        private void button_green_Click(object sender, EventArgs e)
        {
            button_red.Enabled = true; //버튼비활성화
            button_green.Enabled = false; //버튼활성화
            button_blue.Enabled = true; //버튼활성화
            button_orginal.Enabled = true; //버튼활성화
        }


        private void button_greens()
        {
            button_red.Enabled = true; //버튼비활성화
            button_green.Enabled = false; //버튼활성화
            button_blue.Enabled = true; //버튼활성화
            button_orginal.Enabled = true; //버튼활성화
        }

        private void button_blue_Click(object sender, EventArgs e)
        {
            button_red.Enabled = true; //버튼비활성화
            button_green.Enabled = true; //버튼활성화
            button_blue.Enabled = false; //버튼활성화
            button_orginal.Enabled = true; //버튼활성화
        }

        private void button_blues()
        {
            button_red.Enabled = true; //버튼비활성화
            button_green.Enabled = true; //버튼활성화
            button_blue.Enabled = false; //버튼활성화
            button_orginal.Enabled = true; //버튼활성화
        }


        private void button_orginal_Click(object sender, EventArgs e)
        {
            button_red.Enabled = true; //버튼비활성화
            button_green.Enabled = true; //버튼활성화
            button_blue.Enabled = true; //버튼활성화
            button_orginal.Enabled = false; //버튼활성화
        }

        private void button_orginals()
        {
            button_red.Enabled = true; //버튼비활성화
            button_green.Enabled = true; //버튼활성화
            button_blue.Enabled = true; //버튼활성화
            button_orginal.Enabled = false; //버튼활성화
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnectToSerial();
        }

        private void ConnectToSerial()
        {
            try
            {
                client2 = new TcpClient();
                client2.Connect("192.168.0.86", PORT1);
                stream2 = client2.GetStream();
                serialThread = new Thread(new ThreadStart(ReceiveSerialData));
                serialThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to server: " + ex.Message);
            }
        }

        private void ReceiveSerialData()
        {
            byte[] buffer = new byte[1024];
            stream2.BeginRead(buffer, 0, buffer.Length, OnDataReceived, buffer);
        }

        private void OnDataReceived(IAsyncResult ar)
        {
            byte[] buffer = (byte[])ar.AsyncState;
            int bytesRead = stream2.EndRead(ar);

            if (bytesRead > 0)
            {
                switch ((char)buffer[0])
                {
                    case 'r':
                        button_reds();
                        textBox3.Text = "red";
                        break;
                    case 'g':
                        button_greens();
                        textBox3.Text = "green";
                        break;
                    case 'b':
                        button_blues();
                        textBox3.Text = "blue";
                        break;
                    case 'o':
                        button_orginals();
                        textBox3.Text = "oringal";
                        break;
                    default:
                        textBox3.Text = "not";
                        break;
                }
            }

            ReceiveSerialData();
        }
    }
}
