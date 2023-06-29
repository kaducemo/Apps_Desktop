using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP_Server_Test
{
    public partial class Form1 : Form
    {



        Thread threadEnviaUdp = null;
        bool threadC1Ligada = false;
        bool threadC2Ligada = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!threadC1Ligada)
            {
                textBox5.Enabled = false;
                checkBox11.Enabled = false;
                textBox1.Enabled = textBox2.Enabled = textBox3.Enabled = textBox4.Enabled = false; //Desativa Textbox para não mudar IP durante operação
                button1.Text = "Parar";
                threadC1Ligada = true;
                threadEnviaUdp = new Thread(() => EnviaDadosUDP(Convert.ToInt32(textBox5.Text), 1));
                threadEnviaUdp.Start();
            }
            else
            {
                if (!threadC2Ligada)
                {                    
                    checkBox11.Enabled = true;
                    if (!checkBox11.Checked)
                    {
                        textBox1.Enabled = textBox2.Enabled = textBox3.Enabled = textBox4.Enabled = true; //Desativa Textbox para não mudar IP durante operação
                    }
                }
                textBox5.Enabled = true;
                threadC1Ligada = false;
                button1.Text = "Iniciar";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!threadC2Ligada)
            {
                textBox6.Enabled = false;
                checkBox11.Enabled = false;
                textBox1.Enabled = textBox2.Enabled = textBox3.Enabled = textBox4.Enabled = false; //Desativa Textbox para não mudar IP durante operação
                button2.Text = "Parar";
                threadC2Ligada = true;
                threadEnviaUdp = new Thread(() => EnviaDadosUDP(Convert.ToInt32(textBox6.Text), 2));
                threadEnviaUdp.Start();
            }
            else
            {                
                if (!threadC1Ligada)
                {                    
                    checkBox11.Enabled = true;
                    if (!checkBox11.Checked)
                    {
                        textBox1.Enabled = textBox2.Enabled = textBox3.Enabled = textBox4.Enabled = true; //Desativa Textbox para não mudar IP durante operação
                    }
                }
                threadC2Ligada = false;
                button2.Text = "Iniciar";
                textBox6.Enabled = true;
            }
        }


        void EnviaDadosUDP(Int32 port, int cam)
        {
            int count = 0;
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPAddress broadcast;

            if (checkBox11.Checked)
            {
                broadcast = IPAddress.Loopback;
            }
            else
            {
                String tmp = textBox1.Text + "." + textBox2.Text + "." + textBox3.Text + "." + textBox4.Text;
                broadcast = IPAddress.Parse(tmp);
            }


            byte[] sendbuf = { 0 };

            IPEndPoint ep = new IPEndPoint(broadcast, port);

            try
            {
                if (cam == 1)
                {
                    while (threadC1Ligada)
                    {
                        s.SendTo(sendbuf, ep);


                        if (checkBox9.Checked)
                        {
                            sendbuf[0] = 0;
                            if (checkBox1.Checked) sendbuf[0] += 1;
                            if (checkBox2.Checked) sendbuf[0] += 2;
                            if (checkBox3.Checked) sendbuf[0] += 4;
                            if (checkBox4.Checked) sendbuf[0] += 8;
                            Thread.Sleep(100);
                        }
                        else
                        {
                            count = (count + 1) % 16;
                            sendbuf[0] = Convert.ToByte(count);
                            Thread.Sleep(250);
                        }
                    }
                }
                else
                {
                    while (threadC2Ligada)
                    {
                        s.SendTo(sendbuf, ep);


                        if (checkBox10.Checked)
                        {
                            sendbuf[0] = 0;
                            if (checkBox8.Checked) sendbuf[0] += 1;
                            if (checkBox7.Checked) sendbuf[0] += 2;
                            if (checkBox6.Checked) sendbuf[0] += 4;
                            if (checkBox5.Checked) sendbuf[0] += 8;
                            Thread.Sleep(100);
                        }
                        else
                        {
                            count = (count + 1) % 16;
                            sendbuf[0] = Convert.ToByte(count);
                            Thread.Sleep(250);
                        }

                    }
                }

            }
            catch
            {
                MessageBox.Show("Erro ao enviar Dado pela da port " + port.ToString());
            }
            finally
            {
                s.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            threadC1Ligada = false;
            threadC2Ligada = false;
            Thread.Sleep(500);
            Application.Exit();
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox11.Checked)
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
            }
            else
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
            }
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked)
            {
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                checkBox4.Enabled = true;
            }
            else
            {
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
            }
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked)
            {
                checkBox5.Enabled = true;
                checkBox6.Enabled = true;
                checkBox7.Enabled = true;
                checkBox8.Enabled = true;
            }
            else
            {
                checkBox5.Enabled = false;
                checkBox6.Enabled = false;
                checkBox7.Enabled = false;
                checkBox8.Enabled = false;
            }
        }
    }
}