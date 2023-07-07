using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace POC_Dahua_UDP
{
    public partial class Form1 : Form
    {
        bool threadCam1Rodando = false;
        bool threadCam2Rodando = false;
        private const int listenPort1 = 8001;
        private const int listenPort2 = 8001;


        UInt64[] TaxaOcupacaoC1 = { 0, 0, 0, 0 };
        UInt64[] TaxaOcupacaoC2 = { 0, 0, 0, 0 };

        UInt64 indicePacoteCam1 = 0;
        UInt64 indicePacoteCam2 = 0;

        UInt32[] contaAcionamentosC1 = { 0 , 0, 0 , 0 };
        UInt32[] contaAcionamentosC2 = { 0, 0, 0, 0 };


        Thread threadIniciaCaptura1 = null;
        Thread threadIniciaCaptura2 = null;

        public Form1()
        {
            InitializeComponent();
        }

        void ModificaCheckBox(CheckBox[] cb, bool[] val)
        {
            cb[0].Checked = val[0];
            cb[1].Checked = val[1];
            cb[2].Checked = val[2];
            cb[3].Checked = val[3];
        }

        void ModificaTextBox(TextBox[] tb, String[] val)
        {
            tb[0].Text = val[0];
            tb[1].Text = val[1];
            tb[2].Text = val[2];
            tb[3].Text = val[3];
        }

        void ModificaUmaTextBox(TextBox tb, String val)
        {
            tb.Text = val;
        }





        void CapturaPacotes(int camNumber, int listenPort)
        {
            UdpClient listener = new UdpClient(listenPort);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
            Action<CheckBox[], bool[]> pModificaCheckBox = ModificaCheckBox;
            object[] param1 = new object[2]; //Parametros utilizado para Modificar CheckBox

            Action<TextBox[], String[]> pModificaTextBoxAcionamento = ModificaTextBox;
            object[] param2 = new object[2]; //Parametros utilizado para Modificar Textbox

            Action<TextBox[], String[]> pModificaTextBoxOcupacao = ModificaTextBox;
            object[] param3 = new object[2]; //Parametros utilizado para Modificar Textbox            

            Action<TextBox, String> pModificaTextBoxContagem = ModificaUmaTextBox;
            object[] param4 = new object[2]; //Parametros utilizado para Modificar Textbox Contagem

            Action<TextBox[], String[]> pModificaTextBoxNoAcionamento = ModificaTextBox;
            object[] param5 = new object[2]; //Parametros utilizado para Modificar Textbox



            try
            {
                if (camNumber == 1) //CAM1
                {

                    CheckBox[] cbTemp = { cbL1C1, cbL2C1, cbL3C1, cbL4C1 };
                    bool[] boolTemp = { false, false, false, false };

                    TextBox[] tbTemp1 = { tbAcL1C1, tbAcL2C1, tbAcL3C1, tbAcL4C1 };
                    string[] strTemp1 = { "", "", "", "" };

                    TextBox[] tbTemp2 = { tbOcL1C1, tbOcL2C1, tbOcL3C1, tbOcL4C1 };
                    string[] strTemp2 = { "", "", "", "" };

                    TextBox[] tbTemp3 = { tbQtL1C1, tbQtL2C1, tbQtL3C1, tbQtL4C1 };
                    string[] strTemp3 = { "", "", "", "" };

                    long[] TempoAcionamento = { 0, 0, 0, 0 };
                    Stopwatch[] cronometrosCamera = new Stopwatch[4];
                    cronometrosCamera[0] = new Stopwatch();
                    cronometrosCamera[1] = new Stopwatch();
                    cronometrosCamera[2] = new Stopwatch();
                    cronometrosCamera[3] = new Stopwatch();

                    bool[] permissaoParadaContagem = { false, false, false, false };
                    bool[] AcionamentoNotificado = { false, false, false, false };
                    UInt16 bufferOcupacaoL1 = 0, bufferOcupacaoL2 = 0, bufferOcupacaoL3 = 0, bufferOcupacaoL4 = 0;


                    while (threadCam1Rodando)
                    {
                        byte[] bytes = listener.Receive(ref groupEP); //Bloqueia até Receber o Dado via UDP  

                        indicePacoteCam1++;

                        UInt32 temp = Convert.ToUInt32(bytes[0]); //Guarda o valor do dado em uma variável inteira temporária para manipulação binária
                        UInt32 aux = 0;

                        aux = temp & 0x01;
                        boolTemp[0] = (aux != 0) ? true : false;
                        bufferOcupacaoL1 = (UInt16)(bufferOcupacaoL1 << 1); //Desloca buffer para receber novo valor

                        if (boolTemp[0])
                        {
                            bufferOcupacaoL1 = (UInt16)(bufferOcupacaoL1 | 0x0001);

                            if (cronometrosCamera[0].IsRunning && permissaoParadaContagem[0])
                            {
                                cronometrosCamera[0].Stop();
                                TempoAcionamento[0] = cronometrosCamera[0].ElapsedMilliseconds;
                                strTemp1[0] = TempoAcionamento[0].ToString();
                            }                            
                            else
                            {
                                cronometrosCamera[0].Restart();
                                permissaoParadaContagem[0] = false;
                            }
                            if (!AcionamentoNotificado[0])
                            {
                                AcionamentoNotificado[0] = true;
                                contaAcionamentosC1[0]++;                                
                            }
                        }
                        else 
                        {
                            AcionamentoNotificado[0] = false;
                            if(cronometrosCamera[0].IsRunning)
                                permissaoParadaContagem[0] = true;

                        }


                        aux = temp & 0x02;
                        boolTemp[1] = (aux != 0) ? true : false;
                        bufferOcupacaoL2 = (UInt16)(bufferOcupacaoL2 << 1); //Desloca buffer para receber novo valor

                        if (boolTemp[1])
                        {
                            bufferOcupacaoL2 = (UInt16)(bufferOcupacaoL2 | 0x0001);
                            if (cronometrosCamera[1].IsRunning && permissaoParadaContagem[1])
                            {
                                cronometrosCamera[1].Stop();
                                TempoAcionamento[1] = cronometrosCamera[1].ElapsedMilliseconds;
                                strTemp1[1] = TempoAcionamento[1].ToString();
                            }
                            else
                            {
                                cronometrosCamera[1].Restart();
                                permissaoParadaContagem[1] = false;
                            }
                            if (!AcionamentoNotificado[1])
                            {
                                AcionamentoNotificado[1] = true;
                                contaAcionamentosC1[1]++;
                            }
                        }
                        else
                        {
                            AcionamentoNotificado[1] = false;
                            if (cronometrosCamera[1].IsRunning)
                                permissaoParadaContagem[1] = true;

                        }

                        aux = temp & 0x04;
                        boolTemp[2] = (aux != 0) ? true : false;
                        bufferOcupacaoL3 = (UInt16)(bufferOcupacaoL3 << 1); //Desloca buffer para receber novo valor

                        if (boolTemp[2])
                        {
                            bufferOcupacaoL3 = (UInt16)(bufferOcupacaoL3 | 0x0001);
                            if (cronometrosCamera[2].IsRunning && permissaoParadaContagem[2])
                            {
                                cronometrosCamera[2].Stop();
                                TempoAcionamento[2] = cronometrosCamera[2].ElapsedMilliseconds;
                                strTemp1[2] = TempoAcionamento[2].ToString();
                            }
                            else
                            {
                                cronometrosCamera[2].Restart();
                                permissaoParadaContagem[2] = false;
                            }
                            if (!AcionamentoNotificado[2])
                            {
                                AcionamentoNotificado[2] = true;
                                contaAcionamentosC1[2]++;
                            }
                        }
                        else
                        {
                            AcionamentoNotificado[2] = false;
                            if (cronometrosCamera[2].IsRunning)
                                permissaoParadaContagem[2] = true;

                        }

                        aux = temp & 0x08;
                        boolTemp[3] = (aux != 0) ? true : false;
                        bufferOcupacaoL4 = (UInt16)(bufferOcupacaoL4 << 1); //Desloca buffer para receber novo valor

                        if (boolTemp[3])
                        {
                            bufferOcupacaoL4 = (UInt16)(bufferOcupacaoL4 | 0x0001);
                            if (cronometrosCamera[3].IsRunning && permissaoParadaContagem[3])
                            {
                                cronometrosCamera[3].Stop();
                                TempoAcionamento[3] = cronometrosCamera[3].ElapsedMilliseconds;
                                strTemp1[3] = TempoAcionamento[3].ToString();
                            }
                            else
                            {
                                cronometrosCamera[3].Restart();
                                permissaoParadaContagem[3] = false;
                            }
                            if (!AcionamentoNotificado[3])
                            {
                                AcionamentoNotificado[3] = true;
                                contaAcionamentosC1[3]++;
                            }
                        }
                        else
                        {
                            AcionamentoNotificado[3] = false;
                            if (cronometrosCamera[3].IsRunning)
                                permissaoParadaContagem[3] = true;

                        }


                        param1[0] = cbTemp;
                        param1[1] = boolTemp;
                        BeginInvoke(pModificaCheckBox, param1); //Atualiza Checkbox


                        param2[0] = tbTemp1;
                        param2[1] = strTemp1;
                        BeginInvoke(pModificaTextBoxAcionamento, param2); //Atualiza Textbox Tempo acionamento


                        param3[0] = tbTemp2;
                        param3[1] = strTemp2;
                        strTemp2[0] = Convert.ToString((100 * bufferOcupacaoL1) / 65535); //Define a Taxa de ocupação e coloca em uma string
                        strTemp2[1] = Convert.ToString((100 * bufferOcupacaoL2) / 65535);
                        strTemp2[2] = Convert.ToString((100 * bufferOcupacaoL3) / 65535);
                        strTemp2[3] = Convert.ToString((100 * bufferOcupacaoL4) / 65535);
                        BeginInvoke(pModificaTextBoxOcupacao, param3); //Atualiza TextBox Ocupação

                        param4[0] = tbPacoteC1;
                        param4[1] = indicePacoteCam1.ToString();
                        BeginInvoke(pModificaTextBoxContagem, param4); //Atualiza TextBox Contagem

                        param5[0] = tbTemp3;
                        param5[1] = strTemp3;
                        strTemp3[0] = contaAcionamentosC1[0].ToString(); //Define a Taxa de ocupação e coloca em uma string
                        strTemp3[1] = contaAcionamentosC1[1].ToString();
                        strTemp3[2] = contaAcionamentosC1[2].ToString();
                        strTemp3[3] = contaAcionamentosC1[3].ToString();
                        BeginInvoke(pModificaTextBoxNoAcionamento, param5); //Atualiza TextBox Ocupação

                    }

                }
                else //CAM 2
                {
                    CheckBox[] cbTemp = { cbL1C2, cbL2C2, cbL3C2, cbL4C2 };
                    bool[] boolTemp = { false, false, false, false };

                    TextBox[] tbTemp1 = { tbAcL1C2, tbAcL2C2, tbAcL3C2, tbAcL4C2 };
                    string[] strTemp1 = { "", "", "", "" };

                    TextBox[] tbTemp2 = { tbOcL1C2, tbOcL2C2, tbOcL3C2, tbOcL4C2 };
                    string[] strTemp2 = { "", "", "", "" };

                    TextBox[] tbTemp3 = { tbQtL1C2, tbQtL2C2, tbQtL3C2, tbQtL4C2 };
                    string[] strTemp3 = { "", "", "", "" };

                    long[] TempoAcionamento = { 0, 0, 0, 0 };
                    Stopwatch[] cronometrosCamera = new Stopwatch[4];
                    cronometrosCamera[0] = new Stopwatch();
                    cronometrosCamera[1] = new Stopwatch();
                    cronometrosCamera[2] = new Stopwatch();
                    cronometrosCamera[3] = new Stopwatch();

                    bool[] permissaoParadaContagem = { false, false, false, false };
                    bool[] AcionamentoNotificado = { false, false, false, false };
                    UInt16 bufferOcupacaoL1 = 0, bufferOcupacaoL2 = 0, bufferOcupacaoL3 = 0, bufferOcupacaoL4 = 0;


                    while (threadCam2Rodando)
                    {
                        byte[] bytes = listener.Receive(ref groupEP); //Bloqueia até Receber o Dado via UDP  

                        indicePacoteCam2++;

                        UInt32 temp = Convert.ToUInt32(bytes[0]); //Guarda o valor do dado em uma variável inteira temporária para manipulação binária
                        UInt32 aux = 0;

                        aux = temp & 0x01;
                        boolTemp[0] = (aux != 0) ? true : false;
                        bufferOcupacaoL1 = (UInt16)(bufferOcupacaoL1 << 1); //Desloca buffer para receber novo valor

                        if (boolTemp[0])
                        {
                            bufferOcupacaoL1 = (UInt16)(bufferOcupacaoL1 | 0x0001);
                            if (cronometrosCamera[0].IsRunning && permissaoParadaContagem[0])
                            {
                                cronometrosCamera[0].Stop();
                                TempoAcionamento[0] = cronometrosCamera[0].ElapsedMilliseconds;
                                strTemp1[0] = TempoAcionamento[0].ToString();
                            }
                            else
                            {
                                cronometrosCamera[0].Restart();
                                permissaoParadaContagem[0] = false;
                            }
                            if (!AcionamentoNotificado[0])
                            {
                                AcionamentoNotificado[0] = true;
                                contaAcionamentosC2[0]++;
                            }
                        }
                        else
                        {
                            AcionamentoNotificado[0] = false;
                            if (cronometrosCamera[0].IsRunning)
                                permissaoParadaContagem[0] = true;

                        }


                        aux = temp & 0x02;
                        boolTemp[1] = (aux != 0) ? true : false;
                        bufferOcupacaoL2 = (UInt16)(bufferOcupacaoL2 << 1); //Desloca buffer para receber novo valor

                        if (boolTemp[1])
                        {
                            bufferOcupacaoL2 = (UInt16)(bufferOcupacaoL2 | 0x0001);
                            if (cronometrosCamera[1].IsRunning && permissaoParadaContagem[1])
                            {
                                cronometrosCamera[1].Stop();
                                TempoAcionamento[1] = cronometrosCamera[1].ElapsedMilliseconds;
                                strTemp1[1] = TempoAcionamento[1].ToString();
                            }
                            else
                            {
                                cronometrosCamera[1].Restart();
                                permissaoParadaContagem[1] = false;
                            }
                            if (!AcionamentoNotificado[1])
                            {
                                AcionamentoNotificado[1] = true;
                                contaAcionamentosC2[1]++;
                            }
                        }
                        else
                        {
                            AcionamentoNotificado[1] = false;
                            if (cronometrosCamera[1].IsRunning)
                                permissaoParadaContagem[1] = true;

                        }

                        aux = temp & 0x04;
                        boolTemp[2] = (aux != 0) ? true : false;
                        bufferOcupacaoL3 = (UInt16)(bufferOcupacaoL3 << 1); //Desloca buffer para receber novo valor

                        if (boolTemp[2])
                        {
                            bufferOcupacaoL3 = (UInt16)(bufferOcupacaoL3 | 0x0001);
                            if (cronometrosCamera[2].IsRunning && permissaoParadaContagem[2])
                            {
                                cronometrosCamera[2].Stop();
                                TempoAcionamento[2] = cronometrosCamera[2].ElapsedMilliseconds;
                                strTemp1[2] = TempoAcionamento[2].ToString();
                            }
                            else
                            {
                                cronometrosCamera[2].Restart();
                                permissaoParadaContagem[2] = false;
                            }
                            if (!AcionamentoNotificado[2])
                            {
                                AcionamentoNotificado[2] = true;
                                contaAcionamentosC2[2]++;
                            }
                        }
                        else
                        {
                            AcionamentoNotificado[3] = false;
                            if (cronometrosCamera[3].IsRunning)
                                permissaoParadaContagem[3] = true;

                        }

                        aux = temp & 0x08;
                        boolTemp[3] = (aux != 0) ? true : false;
                        bufferOcupacaoL4 = (UInt16)(bufferOcupacaoL4 << 1); //Desloca buffer para receber novo valor

                        if (boolTemp[3])
                        {
                            bufferOcupacaoL4 = (UInt16)(bufferOcupacaoL4 | 0x0001);
                            if (cronometrosCamera[3].IsRunning && permissaoParadaContagem[3])
                            {
                                cronometrosCamera[3].Stop();
                                TempoAcionamento[3] = cronometrosCamera[3].ElapsedMilliseconds;
                                strTemp1[3] = TempoAcionamento[3].ToString();
                            }
                            else
                            {
                                cronometrosCamera[3].Restart();
                                permissaoParadaContagem[3] = false;
                            }
                            if (!AcionamentoNotificado[3])
                            {
                                AcionamentoNotificado[3] = true;
                                contaAcionamentosC2[3]++;
                            }
                        }
                        else
                        {
                            AcionamentoNotificado[3] = false;
                            if (cronometrosCamera[3].IsRunning)
                                permissaoParadaContagem[3] = true;

                        }

                        param1[0] = cbTemp;
                        param1[1] = boolTemp;
                        BeginInvoke(pModificaCheckBox, param1); //Atualiza Checkbox


                        param2[0] = tbTemp1;
                        param2[1] = strTemp1;
                        BeginInvoke(pModificaTextBoxAcionamento, param2); //Atualiza Textbox Tempo acionamento


                        param3[0] = tbTemp2;
                        param3[1] = strTemp2;
                        strTemp2[0] = Convert.ToString((100 * bufferOcupacaoL1) / 65535); //Define a Taxa de ocupação e coloca em uma string
                        strTemp2[1] = Convert.ToString((100 * bufferOcupacaoL2) / 65535);
                        strTemp2[2] = Convert.ToString((100 * bufferOcupacaoL3) / 65535);
                        strTemp2[3] = Convert.ToString((100 * bufferOcupacaoL4) / 65535);
                        BeginInvoke(pModificaTextBoxOcupacao, param3); //Atualiza TextBox Ocupação

                        param4[0] = tbPacoteC2;
                        param4[1] = indicePacoteCam2.ToString();
                        BeginInvoke(pModificaTextBoxContagem, param4); //Atualiza TextBox Contagem

                        param5[0] = tbTemp3;
                        param5[1] = strTemp3;
                        strTemp3[0] = contaAcionamentosC2[0].ToString(); //Define a Taxa de ocupação e coloca em uma string
                        strTemp3[1] = contaAcionamentosC2[1].ToString();
                        strTemp3[2] = contaAcionamentosC2[2].ToString();
                        strTemp3[3] = contaAcionamentosC2[3].ToString();
                        BeginInvoke(pModificaTextBoxNoAcionamento, param5); //Atualiza TextBox Ocupação

                    }

                }

            }
            catch
            { }
            finally
            {
                listener.Close();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        void LimpaDadosCamera(int cam)
        {
            if (cam == 1)
            {
                indicePacoteCam1 = 0;
                TaxaOcupacaoC1[0] = 0;
                TaxaOcupacaoC1[1] = 0;
                TaxaOcupacaoC1[2] = 0;
                TaxaOcupacaoC1[3] = 0;
                contaAcionamentosC1[0] = 0;
                contaAcionamentosC1[1] = 0;
                contaAcionamentosC1[2] = 0;
                contaAcionamentosC1[3] = 0;
            }
            else
            {
                indicePacoteCam2 = 0;
                TaxaOcupacaoC2[0] = 0;
                TaxaOcupacaoC2[1] = 0;
                TaxaOcupacaoC2[2] = 0;
                TaxaOcupacaoC2[3] = 0;
                contaAcionamentosC2[0] = 0;
                contaAcionamentosC2[1] = 0;
                contaAcionamentosC2[2] = 0;
                contaAcionamentosC2[3] = 0;
            }

        }

        /// <summary>
        /// Evento: Inicia captura dos pacotes UDP da CAM1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btConectaCam1_Click(object sender, EventArgs e)
        {
            if (btConectaCam1.Text == "Capturar") //Inicia Thread para capturar pacotes UDP                
            {
                LimpaDadosCamera(1);
                tbPacoteC1.Text = indicePacoteCam1.ToString();
                btConectaCam1.Text = "Finalizar";
                threadCam1Rodando = true;
                threadIniciaCaptura1 = new Thread(() => CapturaPacotes(1, int.Parse(tbPortUDP1.Text)));
                threadIniciaCaptura1.Start();

            }
            else //Finaliza Thread de captura de pacotes
            {
                threadCam1Rodando = false;
                btConectaCam1.Text = "Capturar";
            }
        }

        /// <summary>
        /// Evento: Inicia captura dos pacotes UDP da CAM2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btConectaCam2_Click_1(object sender, EventArgs e)
        {
            if (btConectaCam2.Text == "Capturar") //Inicia Thread para capturar pacotes UDP                
            {
                LimpaDadosCamera(2);
                tbPacoteC2.Text = indicePacoteCam2.ToString();
                btConectaCam2.Text = "Finalizar";
                threadCam2Rodando = true;
                threadIniciaCaptura2 = new Thread(() => CapturaPacotes(2, int.Parse(tbPortUDP2.Text)));
                threadIniciaCaptura2.Start();
            }
            else //Finaliza Thread de captura de pacotes
            {
                threadCam2Rodando = false;
                btConectaCam2.Text = "Capturar";
            }
        }
    }
}