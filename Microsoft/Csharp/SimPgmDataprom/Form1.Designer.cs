namespace SimPgmDataprom
{
    partial class Form1
    {
        //static ME maquinaEstados = ME.None;

        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btConectar = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbAuto = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tbIterRX = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbQtRX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbChaveRemPub = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbSegredo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbChaveLocalPriv = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbChaveLocalPub = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbAESkey = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.tbIKM = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbCriptoRX = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.lbSessão = new System.Windows.Forms.Label();
            this.tbTagRX = new System.Windows.Forms.TextBox();
            this.tbSessaoRX = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbCriptoTX = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.tbTagTX = new System.Windows.Forms.TextBox();
            this.tbSessaoTX = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbQtTX = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbIterTX = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbCodigo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tbTabela = new System.Windows.Forms.TextBox();
            this.tbSw = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbDescricao = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbProtocolo = new System.Windows.Forms.TextBox();
            this.btDataHora = new System.Windows.Forms.Button();
            this.btPubKey = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cbRepete = new System.Windows.Forms.CheckBox();
            this.tbRede = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tbArea = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label44 = new System.Windows.Forms.Label();
            this.tbTcpPort = new System.Windows.Forms.TextBox();
            this.btIniciar = new System.Windows.Forms.Button();
            this.cbAutoAnt = new System.Windows.Forms.CheckBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.cbRepeteAnt = new System.Windows.Forms.CheckBox();
            this.btPubKeyAnt = new System.Windows.Forms.Button();
            this.btDataHoraAnt = new System.Windows.Forms.Button();
            this.tbRedeAnt = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.tbAreaAnt = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.tbProtocoloAnt = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.tbCodigoAnt = new System.Windows.Forms.TextBox();
            this.tbDescricaoAnt = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.tbSWAnt = new System.Windows.Forms.TextBox();
            this.tbTabelaAnt = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.cbCriptoTXAnt = new System.Windows.Forms.CheckBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.tbTagTXAnt = new System.Windows.Forms.TextBox();
            this.tbSessaoTXAnt = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.tbQtTXAnt = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.tbIterTXAnt = new System.Windows.Forms.TextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.cbCriptoRXAnt = new System.Windows.Forms.CheckBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.tbTagRXAnt = new System.Windows.Forms.TextBox();
            this.tbSessaoRXAnt = new System.Windows.Forms.TextBox();
            this.tbIterRXAnt = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.tbQtRXAnt = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tbAESkeyAnt = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.tbIKMAnt = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.tbChaveRemPubAnt = new System.Windows.Forms.TextBox();
            this.tbChaveLocalPrivAnt = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.tbSegredoAnt = new System.Windows.Forms.TextBox();
            this.tbChaveLocalPubAnt = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.tbPacotesTX = new System.Windows.Forms.TextBox();
            this.tbPacotesRX = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.btLimpar = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // btConectar
            // 
            this.btConectar.Location = new System.Drawing.Point(111, 19);
            this.btConectar.Name = "btConectar";
            this.btConectar.Size = new System.Drawing.Size(85, 23);
            this.btConectar.TabIndex = 0;
            this.btConectar.Text = "Conectar";
            this.btConectar.UseVisualStyleBackColor = true;
            this.btConectar.Click += new System.EventHandler(this.btConectar_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 653);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1224, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslStatus
            // 
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(82, 17);
            this.tsslStatus.Text = "Desconectado";
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 19200;
            this.serialPort1.PortName = "COM8";
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbAuto);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.btConectar);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 71);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Comunicação";
            // 
            // cbAuto
            // 
            this.cbAuto.AutoSize = true;
            this.cbAuto.Location = new System.Drawing.Point(15, 49);
            this.cbAuto.Margin = new System.Windows.Forms.Padding(2);
            this.cbAuto.Name = "cbAuto";
            this.cbAuto.Size = new System.Drawing.Size(164, 17);
            this.cbAuto.TabIndex = 2;
            this.cbAuto.Text = "Troca de Chaves Automática";
            this.cbAuto.UseVisualStyleBackColor = true;
            this.cbAuto.CheckedChanged += new System.EventHandler(this.cbAuto_CheckedChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(15, 21);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(81, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.Click += new System.EventHandler(this.comboBox1_Click);
            // 
            // tbIterRX
            // 
            this.tbIterRX.Enabled = false;
            this.tbIterRX.Location = new System.Drawing.Point(68, 21);
            this.tbIterRX.Margin = new System.Windows.Forms.Padding(2);
            this.tbIterRX.Name = "tbIterRX";
            this.tbIterRX.Size = new System.Drawing.Size(70, 20);
            this.tbIterRX.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Iterador:";
            // 
            // tbQtRX
            // 
            this.tbQtRX.Enabled = false;
            this.tbQtRX.Location = new System.Drawing.Point(69, 46);
            this.tbQtRX.Name = "tbQtRX";
            this.tbQtRX.Size = new System.Drawing.Size(70, 20);
            this.tbQtRX.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Qt Dados:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Chave Rem Publica:";
            // 
            // tbChaveRemPub
            // 
            this.tbChaveRemPub.Enabled = false;
            this.tbChaveRemPub.Location = new System.Drawing.Point(127, 81);
            this.tbChaveRemPub.Margin = new System.Windows.Forms.Padding(2);
            this.tbChaveRemPub.Name = "tbChaveRemPub";
            this.tbChaveRemPub.Size = new System.Drawing.Size(1043, 20);
            this.tbChaveRemPub.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(68, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Segredo:";
            // 
            // tbSegredo
            // 
            this.tbSegredo.Enabled = false;
            this.tbSegredo.Location = new System.Drawing.Point(127, 106);
            this.tbSegredo.Margin = new System.Windows.Forms.Padding(2);
            this.tbSegredo.Name = "tbSegredo";
            this.tbSegredo.Size = new System.Drawing.Size(1043, 20);
            this.tbSegredo.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Chave Local Privada:";
            // 
            // tbChaveLocalPriv
            // 
            this.tbChaveLocalPriv.Enabled = false;
            this.tbChaveLocalPriv.Location = new System.Drawing.Point(127, 46);
            this.tbChaveLocalPriv.Margin = new System.Windows.Forms.Padding(2);
            this.tbChaveLocalPriv.Name = "tbChaveLocalPriv";
            this.tbChaveLocalPriv.Size = new System.Drawing.Size(530, 20);
            this.tbChaveLocalPriv.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Chave Local Publica:";
            // 
            // tbChaveLocalPub
            // 
            this.tbChaveLocalPub.Enabled = false;
            this.tbChaveLocalPub.Location = new System.Drawing.Point(127, 21);
            this.tbChaveLocalPub.Margin = new System.Windows.Forms.Padding(2);
            this.tbChaveLocalPub.Name = "tbChaveLocalPub";
            this.tbChaveLocalPub.Size = new System.Drawing.Size(1043, 20);
            this.tbChaveLocalPub.TabIndex = 11;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbAESkey);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.tbIKM);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tbChaveRemPub);
            this.groupBox2.Controls.Add(this.tbChaveLocalPriv);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tbSegredo);
            this.groupBox2.Controls.Add(this.tbChaveLocalPub);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(6, 92);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(1194, 199);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ECDH";
            // 
            // tbAESkey
            // 
            this.tbAESkey.Enabled = false;
            this.tbAESkey.Location = new System.Drawing.Point(127, 173);
            this.tbAESkey.Margin = new System.Windows.Forms.Padding(2);
            this.tbAESkey.Name = "tbAESkey";
            this.tbAESkey.Size = new System.Drawing.Size(1043, 20);
            this.tbAESkey.TabIndex = 18;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(84, 176);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(31, 13);
            this.label18.TabIndex = 17;
            this.label18.Text = "AES:";
            // 
            // tbIKM
            // 
            this.tbIKM.Enabled = false;
            this.tbIKM.Location = new System.Drawing.Point(127, 144);
            this.tbIKM.Margin = new System.Windows.Forms.Padding(2);
            this.tbIKM.Name = "tbIKM";
            this.tbIKM.Size = new System.Drawing.Size(1043, 20);
            this.tbIKM.TabIndex = 16;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(84, 147);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(29, 13);
            this.label14.TabIndex = 15;
            this.label14.Text = "IKM:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbCriptoRX);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.lbSessão);
            this.groupBox3.Controls.Add(this.tbTagRX);
            this.groupBox3.Controls.Add(this.tbSessaoRX);
            this.groupBox3.Controls.Add(this.tbIterRX);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.tbQtRX);
            this.groupBox3.Location = new System.Drawing.Point(378, 308);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(356, 129);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "RX";
            // 
            // cbCriptoRX
            // 
            this.cbCriptoRX.AutoSize = true;
            this.cbCriptoRX.Enabled = false;
            this.cbCriptoRX.Location = new System.Drawing.Point(284, 24);
            this.cbCriptoRX.Name = "cbCriptoRX";
            this.cbCriptoRX.Size = new System.Drawing.Size(53, 17);
            this.cbCriptoRX.TabIndex = 21;
            this.cbCriptoRX.Text = "Cripto";
            this.cbCriptoRX.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(29, 103);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 13);
            this.label15.TabIndex = 18;
            this.label15.Text = "Tag:";
            // 
            // lbSessão
            // 
            this.lbSessão.AutoSize = true;
            this.lbSessão.Location = new System.Drawing.Point(18, 74);
            this.lbSessão.Name = "lbSessão";
            this.lbSessão.Size = new System.Drawing.Size(45, 13);
            this.lbSessão.TabIndex = 17;
            this.lbSessão.Text = "Sessão:";
            // 
            // tbTagRX
            // 
            this.tbTagRX.Enabled = false;
            this.tbTagRX.Location = new System.Drawing.Point(68, 100);
            this.tbTagRX.Margin = new System.Windows.Forms.Padding(2);
            this.tbTagRX.Name = "tbTagRX";
            this.tbTagRX.Size = new System.Drawing.Size(270, 20);
            this.tbTagRX.TabIndex = 16;
            // 
            // tbSessaoRX
            // 
            this.tbSessaoRX.Enabled = false;
            this.tbSessaoRX.Location = new System.Drawing.Point(69, 71);
            this.tbSessaoRX.Margin = new System.Windows.Forms.Padding(2);
            this.tbSessaoRX.Name = "tbSessaoRX";
            this.tbSessaoRX.Size = new System.Drawing.Size(69, 20);
            this.tbSessaoRX.TabIndex = 15;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbCriptoTX);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.tbTagTX);
            this.groupBox4.Controls.Add(this.tbSessaoTX);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.tbQtTX);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.tbIterTX);
            this.groupBox4.Location = new System.Drawing.Point(6, 308);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(353, 129);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "TX";
            // 
            // cbCriptoTX
            // 
            this.cbCriptoTX.AutoSize = true;
            this.cbCriptoTX.Enabled = false;
            this.cbCriptoTX.Location = new System.Drawing.Point(285, 21);
            this.cbCriptoTX.Name = "cbCriptoTX";
            this.cbCriptoTX.Size = new System.Drawing.Size(53, 17);
            this.cbCriptoTX.TabIndex = 20;
            this.cbCriptoTX.Text = "Cripto";
            this.cbCriptoTX.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(28, 102);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(29, 13);
            this.label17.TabIndex = 19;
            this.label17.Text = "Tag:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 73);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(45, 13);
            this.label16.TabIndex = 18;
            this.label16.Text = "Sessão:";
            // 
            // tbTagTX
            // 
            this.tbTagTX.Enabled = false;
            this.tbTagTX.Location = new System.Drawing.Point(71, 99);
            this.tbTagTX.Margin = new System.Windows.Forms.Padding(2);
            this.tbTagTX.Name = "tbTagTX";
            this.tbTagTX.Size = new System.Drawing.Size(267, 20);
            this.tbTagTX.TabIndex = 15;
            // 
            // tbSessaoTX
            // 
            this.tbSessaoTX.Enabled = false;
            this.tbSessaoTX.Location = new System.Drawing.Point(71, 70);
            this.tbSessaoTX.Margin = new System.Windows.Forms.Padding(2);
            this.tbSessaoTX.Name = "tbSessaoTX";
            this.tbSessaoTX.Size = new System.Drawing.Size(69, 20);
            this.tbSessaoTX.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Qt Dados:";
            // 
            // tbQtTX
            // 
            this.tbQtTX.Enabled = false;
            this.tbQtTX.Location = new System.Drawing.Point(71, 46);
            this.tbQtTX.Margin = new System.Windows.Forms.Padding(2);
            this.tbQtTX.Name = "tbQtTX";
            this.tbQtTX.Size = new System.Drawing.Size(69, 20);
            this.tbQtTX.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Iterador:";
            // 
            // tbIterTX
            // 
            this.tbIterTX.Enabled = false;
            this.tbIterTX.Location = new System.Drawing.Point(71, 19);
            this.tbIterTX.Margin = new System.Windows.Forms.Padding(2);
            this.tbIterTX.Name = "tbIterTX";
            this.tbIterTX.Size = new System.Drawing.Size(69, 20);
            this.tbIterTX.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(475, 28);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Código:";
            // 
            // tbCodigo
            // 
            this.tbCodigo.Enabled = false;
            this.tbCodigo.Location = new System.Drawing.Point(522, 24);
            this.tbCodigo.Margin = new System.Windows.Forms.Padding(2);
            this.tbCodigo.Name = "tbCodigo";
            this.tbCodigo.Size = new System.Drawing.Size(48, 20);
            this.tbCodigo.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(854, 28);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Tabela:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1120, 27);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(28, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "SW:";
            // 
            // tbTabela
            // 
            this.tbTabela.Enabled = false;
            this.tbTabela.Location = new System.Drawing.Point(900, 24);
            this.tbTabela.Margin = new System.Windows.Forms.Padding(2);
            this.tbTabela.Name = "tbTabela";
            this.tbTabela.Size = new System.Drawing.Size(48, 20);
            this.tbTabela.TabIndex = 22;
            // 
            // tbSw
            // 
            this.tbSw.Enabled = false;
            this.tbSw.Location = new System.Drawing.Point(1152, 24);
            this.tbSw.Margin = new System.Windows.Forms.Padding(2);
            this.tbSw.Name = "tbSw";
            this.tbSw.Size = new System.Drawing.Size(48, 20);
            this.tbSw.TabIndex = 23;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(463, 60);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 13);
            this.label12.TabIndex = 24;
            this.label12.Text = "Descrição:";
            // 
            // tbDescricao
            // 
            this.tbDescricao.Enabled = false;
            this.tbDescricao.Location = new System.Drawing.Point(522, 57);
            this.tbDescricao.Margin = new System.Windows.Forms.Padding(2);
            this.tbDescricao.Name = "tbDescricao";
            this.tbDescricao.Size = new System.Drawing.Size(678, 20);
            this.tbDescricao.TabIndex = 25;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(967, 28);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(55, 13);
            this.label13.TabIndex = 26;
            this.label13.Text = "Protocolo:";
            // 
            // tbProtocolo
            // 
            this.tbProtocolo.Enabled = false;
            this.tbProtocolo.Location = new System.Drawing.Point(1026, 24);
            this.tbProtocolo.Margin = new System.Windows.Forms.Padding(2);
            this.tbProtocolo.Name = "tbProtocolo";
            this.tbProtocolo.Size = new System.Drawing.Size(48, 20);
            this.tbProtocolo.TabIndex = 27;
            // 
            // btDataHora
            // 
            this.btDataHora.Enabled = false;
            this.btDataHora.Location = new System.Drawing.Point(6, 65);
            this.btDataHora.Name = "btDataHora";
            this.btDataHora.Size = new System.Drawing.Size(75, 23);
            this.btDataHora.TabIndex = 28;
            this.btDataHora.Text = "Data e Hora";
            this.btDataHora.UseVisualStyleBackColor = true;
            this.btDataHora.Click += new System.EventHandler(this.btDataHora_Click);
            // 
            // btPubKey
            // 
            this.btPubKey.Enabled = false;
            this.btPubKey.Location = new System.Drawing.Point(6, 36);
            this.btPubKey.Name = "btPubKey";
            this.btPubKey.Size = new System.Drawing.Size(75, 23);
            this.btPubKey.TabIndex = 29;
            this.btPubKey.Text = "Public Key";
            this.btPubKey.UseVisualStyleBackColor = true;
            this.btPubKey.Click += new System.EventHandler(this.btPubKey_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbRepete);
            this.groupBox5.Controls.Add(this.btPubKey);
            this.groupBox5.Controls.Add(this.btDataHora);
            this.groupBox5.Location = new System.Drawing.Point(754, 309);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(143, 129);
            this.groupBox5.TabIndex = 30;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Comandos";
            // 
            // cbRepete
            // 
            this.cbRepete.AutoSize = true;
            this.cbRepete.Location = new System.Drawing.Point(7, 106);
            this.cbRepete.Name = "cbRepete";
            this.cbRepete.Size = new System.Drawing.Size(50, 17);
            this.cbRepete.TabIndex = 30;
            this.cbRepete.Text = "Loop";
            this.cbRepete.UseVisualStyleBackColor = true;
            // 
            // tbRede
            // 
            this.tbRede.Enabled = false;
            this.tbRede.Location = new System.Drawing.Point(648, 24);
            this.tbRede.Margin = new System.Windows.Forms.Padding(2);
            this.tbRede.Name = "tbRede";
            this.tbRede.Size = new System.Drawing.Size(48, 20);
            this.tbRede.TabIndex = 34;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(611, 28);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(36, 13);
            this.label19.TabIndex = 33;
            this.label19.Text = "Rede:";
            // 
            // tbArea
            // 
            this.tbArea.Enabled = false;
            this.tbArea.Location = new System.Drawing.Point(774, 24);
            this.tbArea.Margin = new System.Windows.Forms.Padding(2);
            this.tbArea.Name = "tbArea";
            this.tbArea.Size = new System.Drawing.Size(48, 20);
            this.tbArea.TabIndex = 32;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(738, 28);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(32, 13);
            this.label20.TabIndex = 31;
            this.label20.Text = "Área:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1222, 470);
            this.tabControl1.TabIndex = 35;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.tbRede);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.label19);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.tbArea);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.label20);
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Controls.Add(this.tbProtocolo);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.tbCodigo);
            this.tabPage1.Controls.Add(this.tbDescricao);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.tbSw);
            this.tabPage1.Controls.Add(this.tbTabela);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1214, 444);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Programador";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label44);
            this.tabPage2.Controls.Add(this.tbTcpPort);
            this.tabPage2.Controls.Add(this.btIniciar);
            this.tabPage2.Controls.Add(this.cbAutoAnt);
            this.tabPage2.Controls.Add(this.groupBox9);
            this.tabPage2.Controls.Add(this.tbRedeAnt);
            this.tabPage2.Controls.Add(this.label35);
            this.tabPage2.Controls.Add(this.tbAreaAnt);
            this.tabPage2.Controls.Add(this.label36);
            this.tabPage2.Controls.Add(this.tbProtocoloAnt);
            this.tabPage2.Controls.Add(this.label37);
            this.tabPage2.Controls.Add(this.label38);
            this.tabPage2.Controls.Add(this.tbCodigoAnt);
            this.tabPage2.Controls.Add(this.tbDescricaoAnt);
            this.tabPage2.Controls.Add(this.label39);
            this.tabPage2.Controls.Add(this.label40);
            this.tabPage2.Controls.Add(this.label41);
            this.tabPage2.Controls.Add(this.tbSWAnt);
            this.tabPage2.Controls.Add(this.tbTabelaAnt);
            this.tabPage2.Controls.Add(this.groupBox7);
            this.tabPage2.Controls.Add(this.groupBox8);
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1214, 444);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Antares";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(32, 9);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(35, 13);
            this.label44.TabIndex = 53;
            this.label44.Text = "Porta:";
            // 
            // tbTcpPort
            // 
            this.tbTcpPort.Location = new System.Drawing.Point(29, 25);
            this.tbTcpPort.Name = "tbTcpPort";
            this.tbTcpPort.Size = new System.Drawing.Size(69, 20);
            this.tbTcpPort.TabIndex = 52;
            this.tbTcpPort.Text = "20560";
            this.tbTcpPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btIniciar
            // 
            this.btIniciar.Location = new System.Drawing.Point(119, 23);
            this.btIniciar.Name = "btIniciar";
            this.btIniciar.Size = new System.Drawing.Size(75, 23);
            this.btIniciar.TabIndex = 51;
            this.btIniciar.Text = "Iniciar";
            this.btIniciar.UseVisualStyleBackColor = true;
            this.btIniciar.Click += new System.EventHandler(this.btIniciar_Click);
            // 
            // cbAutoAnt
            // 
            this.cbAutoAnt.AutoSize = true;
            this.cbAutoAnt.Location = new System.Drawing.Point(28, 55);
            this.cbAutoAnt.Margin = new System.Windows.Forms.Padding(2);
            this.cbAutoAnt.Name = "cbAutoAnt";
            this.cbAutoAnt.Size = new System.Drawing.Size(164, 17);
            this.cbAutoAnt.TabIndex = 50;
            this.cbAutoAnt.Text = "Troca de Chaves Automática";
            this.cbAutoAnt.UseVisualStyleBackColor = true;
            this.cbAutoAnt.CheckedChanged += new System.EventHandler(this.cbAutoAnt_CheckedChanged);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.cbRepeteAnt);
            this.groupBox9.Controls.Add(this.btPubKeyAnt);
            this.groupBox9.Controls.Add(this.btDataHoraAnt);
            this.groupBox9.Location = new System.Drawing.Point(752, 288);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(143, 129);
            this.groupBox9.TabIndex = 49;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Comandos";
            // 
            // cbRepeteAnt
            // 
            this.cbRepeteAnt.AutoSize = true;
            this.cbRepeteAnt.Location = new System.Drawing.Point(7, 106);
            this.cbRepeteAnt.Name = "cbRepeteAnt";
            this.cbRepeteAnt.Size = new System.Drawing.Size(50, 17);
            this.cbRepeteAnt.TabIndex = 30;
            this.cbRepeteAnt.Text = "Loop";
            this.cbRepeteAnt.UseVisualStyleBackColor = true;
            // 
            // btPubKeyAnt
            // 
            this.btPubKeyAnt.Enabled = false;
            this.btPubKeyAnt.Location = new System.Drawing.Point(6, 36);
            this.btPubKeyAnt.Name = "btPubKeyAnt";
            this.btPubKeyAnt.Size = new System.Drawing.Size(75, 23);
            this.btPubKeyAnt.TabIndex = 29;
            this.btPubKeyAnt.Text = "Public Key";
            this.btPubKeyAnt.UseVisualStyleBackColor = true;
            this.btPubKeyAnt.Click += new System.EventHandler(this.btPubKeyAnt_Click);
            // 
            // btDataHoraAnt
            // 
            this.btDataHoraAnt.Enabled = false;
            this.btDataHoraAnt.Location = new System.Drawing.Point(6, 65);
            this.btDataHoraAnt.Name = "btDataHoraAnt";
            this.btDataHoraAnt.Size = new System.Drawing.Size(75, 23);
            this.btDataHoraAnt.TabIndex = 28;
            this.btDataHoraAnt.Text = "Data e Hora";
            this.btDataHoraAnt.UseVisualStyleBackColor = true;
            this.btDataHoraAnt.Click += new System.EventHandler(this.btDataHoraAnt_Click);
            // 
            // tbRedeAnt
            // 
            this.tbRedeAnt.Enabled = false;
            this.tbRedeAnt.Location = new System.Drawing.Point(646, 19);
            this.tbRedeAnt.Margin = new System.Windows.Forms.Padding(2);
            this.tbRedeAnt.Name = "tbRedeAnt";
            this.tbRedeAnt.Size = new System.Drawing.Size(48, 20);
            this.tbRedeAnt.TabIndex = 48;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(609, 23);
            this.label35.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(36, 13);
            this.label35.TabIndex = 47;
            this.label35.Text = "Rede:";
            // 
            // tbAreaAnt
            // 
            this.tbAreaAnt.Enabled = false;
            this.tbAreaAnt.Location = new System.Drawing.Point(772, 19);
            this.tbAreaAnt.Margin = new System.Windows.Forms.Padding(2);
            this.tbAreaAnt.Name = "tbAreaAnt";
            this.tbAreaAnt.Size = new System.Drawing.Size(48, 20);
            this.tbAreaAnt.TabIndex = 46;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(736, 23);
            this.label36.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(32, 13);
            this.label36.TabIndex = 45;
            this.label36.Text = "Área:";
            // 
            // tbProtocoloAnt
            // 
            this.tbProtocoloAnt.Enabled = false;
            this.tbProtocoloAnt.Location = new System.Drawing.Point(1024, 19);
            this.tbProtocoloAnt.Margin = new System.Windows.Forms.Padding(2);
            this.tbProtocoloAnt.Name = "tbProtocoloAnt";
            this.tbProtocoloAnt.Size = new System.Drawing.Size(48, 20);
            this.tbProtocoloAnt.TabIndex = 44;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(473, 23);
            this.label37.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(43, 13);
            this.label37.TabIndex = 35;
            this.label37.Text = "Código:";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(965, 23);
            this.label38.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(55, 13);
            this.label38.TabIndex = 43;
            this.label38.Text = "Protocolo:";
            // 
            // tbCodigoAnt
            // 
            this.tbCodigoAnt.Enabled = false;
            this.tbCodigoAnt.Location = new System.Drawing.Point(520, 19);
            this.tbCodigoAnt.Margin = new System.Windows.Forms.Padding(2);
            this.tbCodigoAnt.Name = "tbCodigoAnt";
            this.tbCodigoAnt.Size = new System.Drawing.Size(48, 20);
            this.tbCodigoAnt.TabIndex = 36;
            // 
            // tbDescricaoAnt
            // 
            this.tbDescricaoAnt.Enabled = false;
            this.tbDescricaoAnt.Location = new System.Drawing.Point(520, 52);
            this.tbDescricaoAnt.Margin = new System.Windows.Forms.Padding(2);
            this.tbDescricaoAnt.Name = "tbDescricaoAnt";
            this.tbDescricaoAnt.Size = new System.Drawing.Size(678, 20);
            this.tbDescricaoAnt.TabIndex = 42;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(852, 23);
            this.label39.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(43, 13);
            this.label39.TabIndex = 37;
            this.label39.Text = "Tabela:";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(461, 55);
            this.label40.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(58, 13);
            this.label40.TabIndex = 41;
            this.label40.Text = "Descrição:";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(1118, 22);
            this.label41.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(28, 13);
            this.label41.TabIndex = 38;
            this.label41.Text = "SW:";
            // 
            // tbSWAnt
            // 
            this.tbSWAnt.Enabled = false;
            this.tbSWAnt.Location = new System.Drawing.Point(1150, 19);
            this.tbSWAnt.Margin = new System.Windows.Forms.Padding(2);
            this.tbSWAnt.Name = "tbSWAnt";
            this.tbSWAnt.Size = new System.Drawing.Size(48, 20);
            this.tbSWAnt.TabIndex = 40;
            // 
            // tbTabelaAnt
            // 
            this.tbTabelaAnt.Enabled = false;
            this.tbTabelaAnt.Location = new System.Drawing.Point(898, 19);
            this.tbTabelaAnt.Margin = new System.Windows.Forms.Padding(2);
            this.tbTabelaAnt.Name = "tbTabelaAnt";
            this.tbTabelaAnt.Size = new System.Drawing.Size(48, 20);
            this.tbTabelaAnt.TabIndex = 39;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.cbCriptoTXAnt);
            this.groupBox7.Controls.Add(this.label27);
            this.groupBox7.Controls.Add(this.label28);
            this.groupBox7.Controls.Add(this.tbTagTXAnt);
            this.groupBox7.Controls.Add(this.tbSessaoTXAnt);
            this.groupBox7.Controls.Add(this.label29);
            this.groupBox7.Controls.Add(this.tbQtTXAnt);
            this.groupBox7.Controls.Add(this.label30);
            this.groupBox7.Controls.Add(this.tbIterTXAnt);
            this.groupBox7.Location = new System.Drawing.Point(8, 287);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox7.Size = new System.Drawing.Size(353, 129);
            this.groupBox7.TabIndex = 19;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "TX";
            // 
            // cbCriptoTXAnt
            // 
            this.cbCriptoTXAnt.AutoSize = true;
            this.cbCriptoTXAnt.Enabled = false;
            this.cbCriptoTXAnt.Location = new System.Drawing.Point(285, 21);
            this.cbCriptoTXAnt.Name = "cbCriptoTXAnt";
            this.cbCriptoTXAnt.Size = new System.Drawing.Size(53, 17);
            this.cbCriptoTXAnt.TabIndex = 20;
            this.cbCriptoTXAnt.Text = "Cripto";
            this.cbCriptoTXAnt.UseVisualStyleBackColor = true;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(28, 102);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(29, 13);
            this.label27.TabIndex = 19;
            this.label27.Text = "Tag:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(12, 73);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(45, 13);
            this.label28.TabIndex = 18;
            this.label28.Text = "Sessão:";
            // 
            // tbTagTXAnt
            // 
            this.tbTagTXAnt.Enabled = false;
            this.tbTagTXAnt.Location = new System.Drawing.Point(71, 99);
            this.tbTagTXAnt.Margin = new System.Windows.Forms.Padding(2);
            this.tbTagTXAnt.Name = "tbTagTXAnt";
            this.tbTagTXAnt.Size = new System.Drawing.Size(267, 20);
            this.tbTagTXAnt.TabIndex = 15;
            // 
            // tbSessaoTXAnt
            // 
            this.tbSessaoTXAnt.Enabled = false;
            this.tbSessaoTXAnt.Location = new System.Drawing.Point(71, 70);
            this.tbSessaoTXAnt.Margin = new System.Windows.Forms.Padding(2);
            this.tbSessaoTXAnt.Name = "tbSessaoTXAnt";
            this.tbSessaoTXAnt.Size = new System.Drawing.Size(69, 20);
            this.tbSessaoTXAnt.TabIndex = 14;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(5, 49);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(55, 13);
            this.label29.TabIndex = 13;
            this.label29.Text = "Qt Dados:";
            // 
            // tbQtTXAnt
            // 
            this.tbQtTXAnt.Enabled = false;
            this.tbQtTXAnt.Location = new System.Drawing.Point(71, 46);
            this.tbQtTXAnt.Margin = new System.Windows.Forms.Padding(2);
            this.tbQtTXAnt.Name = "tbQtTXAnt";
            this.tbQtTXAnt.Size = new System.Drawing.Size(69, 20);
            this.tbQtTXAnt.TabIndex = 12;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(17, 23);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(46, 13);
            this.label30.TabIndex = 11;
            this.label30.Text = "Iterador:";
            // 
            // tbIterTXAnt
            // 
            this.tbIterTXAnt.Enabled = false;
            this.tbIterTXAnt.Location = new System.Drawing.Point(71, 19);
            this.tbIterTXAnt.Margin = new System.Windows.Forms.Padding(2);
            this.tbIterTXAnt.Name = "tbIterTXAnt";
            this.tbIterTXAnt.Size = new System.Drawing.Size(69, 20);
            this.tbIterTXAnt.TabIndex = 10;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.cbCriptoRXAnt);
            this.groupBox8.Controls.Add(this.label31);
            this.groupBox8.Controls.Add(this.label32);
            this.groupBox8.Controls.Add(this.tbTagRXAnt);
            this.groupBox8.Controls.Add(this.tbSessaoRXAnt);
            this.groupBox8.Controls.Add(this.tbIterRXAnt);
            this.groupBox8.Controls.Add(this.label33);
            this.groupBox8.Controls.Add(this.label34);
            this.groupBox8.Controls.Add(this.tbQtRXAnt);
            this.groupBox8.Location = new System.Drawing.Point(365, 287);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox8.Size = new System.Drawing.Size(356, 129);
            this.groupBox8.TabIndex = 18;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "RX";
            // 
            // cbCriptoRXAnt
            // 
            this.cbCriptoRXAnt.AutoSize = true;
            this.cbCriptoRXAnt.Enabled = false;
            this.cbCriptoRXAnt.Location = new System.Drawing.Point(284, 24);
            this.cbCriptoRXAnt.Name = "cbCriptoRXAnt";
            this.cbCriptoRXAnt.Size = new System.Drawing.Size(53, 17);
            this.cbCriptoRXAnt.TabIndex = 21;
            this.cbCriptoRXAnt.Text = "Cripto";
            this.cbCriptoRXAnt.UseVisualStyleBackColor = true;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(29, 103);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(29, 13);
            this.label31.TabIndex = 18;
            this.label31.Text = "Tag:";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(18, 74);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(45, 13);
            this.label32.TabIndex = 17;
            this.label32.Text = "Sessão:";
            // 
            // tbTagRXAnt
            // 
            this.tbTagRXAnt.Enabled = false;
            this.tbTagRXAnt.Location = new System.Drawing.Point(68, 100);
            this.tbTagRXAnt.Margin = new System.Windows.Forms.Padding(2);
            this.tbTagRXAnt.Name = "tbTagRXAnt";
            this.tbTagRXAnt.Size = new System.Drawing.Size(270, 20);
            this.tbTagRXAnt.TabIndex = 16;
            // 
            // tbSessaoRXAnt
            // 
            this.tbSessaoRXAnt.Enabled = false;
            this.tbSessaoRXAnt.Location = new System.Drawing.Point(69, 71);
            this.tbSessaoRXAnt.Margin = new System.Windows.Forms.Padding(2);
            this.tbSessaoRXAnt.Name = "tbSessaoRXAnt";
            this.tbSessaoRXAnt.Size = new System.Drawing.Size(69, 20);
            this.tbSessaoRXAnt.TabIndex = 15;
            // 
            // tbIterRXAnt
            // 
            this.tbIterRXAnt.Enabled = false;
            this.tbIterRXAnt.Location = new System.Drawing.Point(68, 21);
            this.tbIterRXAnt.Margin = new System.Windows.Forms.Padding(2);
            this.tbIterRXAnt.Name = "tbIterRXAnt";
            this.tbIterRXAnt.Size = new System.Drawing.Size(70, 20);
            this.tbIterRXAnt.TabIndex = 3;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(17, 24);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(46, 13);
            this.label33.TabIndex = 4;
            this.label33.Text = "Iterador:";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(5, 49);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(55, 13);
            this.label34.TabIndex = 6;
            this.label34.Text = "Qt Dados:";
            // 
            // tbQtRXAnt
            // 
            this.tbQtRXAnt.Enabled = false;
            this.tbQtRXAnt.Location = new System.Drawing.Point(69, 46);
            this.tbQtRXAnt.Name = "tbQtRXAnt";
            this.tbQtRXAnt.Size = new System.Drawing.Size(70, 20);
            this.tbQtRXAnt.TabIndex = 5;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.tbAESkeyAnt);
            this.groupBox6.Controls.Add(this.label21);
            this.groupBox6.Controls.Add(this.tbIKMAnt);
            this.groupBox6.Controls.Add(this.label22);
            this.groupBox6.Controls.Add(this.label23);
            this.groupBox6.Controls.Add(this.label24);
            this.groupBox6.Controls.Add(this.tbChaveRemPubAnt);
            this.groupBox6.Controls.Add(this.tbChaveLocalPrivAnt);
            this.groupBox6.Controls.Add(this.label25);
            this.groupBox6.Controls.Add(this.tbSegredoAnt);
            this.groupBox6.Controls.Add(this.tbChaveLocalPubAnt);
            this.groupBox6.Controls.Add(this.label26);
            this.groupBox6.Location = new System.Drawing.Point(8, 84);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox6.Size = new System.Drawing.Size(1194, 199);
            this.groupBox6.TabIndex = 16;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "ECDH";
            // 
            // tbAESkeyAnt
            // 
            this.tbAESkeyAnt.Enabled = false;
            this.tbAESkeyAnt.Location = new System.Drawing.Point(127, 173);
            this.tbAESkeyAnt.Margin = new System.Windows.Forms.Padding(2);
            this.tbAESkeyAnt.Name = "tbAESkeyAnt";
            this.tbAESkeyAnt.Size = new System.Drawing.Size(1043, 20);
            this.tbAESkeyAnt.TabIndex = 18;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(84, 176);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(31, 13);
            this.label21.TabIndex = 17;
            this.label21.Text = "AES:";
            // 
            // tbIKMAnt
            // 
            this.tbIKMAnt.Enabled = false;
            this.tbIKMAnt.Location = new System.Drawing.Point(127, 144);
            this.tbIKMAnt.Margin = new System.Windows.Forms.Padding(2);
            this.tbIKMAnt.Name = "tbIKMAnt";
            this.tbIKMAnt.Size = new System.Drawing.Size(1043, 20);
            this.tbIKMAnt.TabIndex = 16;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(84, 147);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(29, 13);
            this.label22.TabIndex = 15;
            this.label22.Text = "IKM:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(14, 24);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(108, 13);
            this.label23.TabIndex = 12;
            this.label23.Text = "Chave Local Publica:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(15, 49);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(109, 13);
            this.label24.TabIndex = 14;
            this.label24.Text = "Chave Local Privada:";
            // 
            // tbChaveRemPubAnt
            // 
            this.tbChaveRemPubAnt.Enabled = false;
            this.tbChaveRemPubAnt.Location = new System.Drawing.Point(127, 81);
            this.tbChaveRemPubAnt.Margin = new System.Windows.Forms.Padding(2);
            this.tbChaveRemPubAnt.Name = "tbChaveRemPubAnt";
            this.tbChaveRemPubAnt.Size = new System.Drawing.Size(1043, 20);
            this.tbChaveRemPubAnt.TabIndex = 7;
            // 
            // tbChaveLocalPrivAnt
            // 
            this.tbChaveLocalPrivAnt.Enabled = false;
            this.tbChaveLocalPrivAnt.Location = new System.Drawing.Point(127, 46);
            this.tbChaveLocalPrivAnt.Margin = new System.Windows.Forms.Padding(2);
            this.tbChaveLocalPrivAnt.Name = "tbChaveLocalPrivAnt";
            this.tbChaveLocalPrivAnt.Size = new System.Drawing.Size(530, 20);
            this.tbChaveLocalPrivAnt.TabIndex = 13;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(14, 84);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(104, 13);
            this.label25.TabIndex = 8;
            this.label25.Text = "Chave Rem Publica:";
            // 
            // tbSegredoAnt
            // 
            this.tbSegredoAnt.Enabled = false;
            this.tbSegredoAnt.Location = new System.Drawing.Point(127, 106);
            this.tbSegredoAnt.Margin = new System.Windows.Forms.Padding(2);
            this.tbSegredoAnt.Name = "tbSegredoAnt";
            this.tbSegredoAnt.Size = new System.Drawing.Size(1043, 20);
            this.tbSegredoAnt.TabIndex = 9;
            // 
            // tbChaveLocalPubAnt
            // 
            this.tbChaveLocalPubAnt.Enabled = false;
            this.tbChaveLocalPubAnt.Location = new System.Drawing.Point(127, 21);
            this.tbChaveLocalPubAnt.Margin = new System.Windows.Forms.Padding(2);
            this.tbChaveLocalPubAnt.Name = "tbChaveLocalPubAnt";
            this.tbChaveLocalPubAnt.Size = new System.Drawing.Size(1043, 20);
            this.tbChaveLocalPubAnt.TabIndex = 11;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(68, 109);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(50, 13);
            this.label26.TabIndex = 10;
            this.label26.Text = "Segredo:";
            // 
            // tbPacotesTX
            // 
            this.tbPacotesTX.Location = new System.Drawing.Point(12, 490);
            this.tbPacotesTX.MaxLength = 10000000;
            this.tbPacotesTX.Multiline = true;
            this.tbPacotesTX.Name = "tbPacotesTX";
            this.tbPacotesTX.ReadOnly = true;
            this.tbPacotesTX.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbPacotesTX.Size = new System.Drawing.Size(591, 130);
            this.tbPacotesTX.TabIndex = 36;
            // 
            // tbPacotesRX
            // 
            this.tbPacotesRX.Location = new System.Drawing.Point(622, 490);
            this.tbPacotesRX.MaxLength = 10000000;
            this.tbPacotesRX.Multiline = true;
            this.tbPacotesRX.Name = "tbPacotesRX";
            this.tbPacotesRX.ReadOnly = true;
            this.tbPacotesRX.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbPacotesRX.Size = new System.Drawing.Size(582, 130);
            this.tbPacotesRX.TabIndex = 37;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(619, 474);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(100, 13);
            this.label42.TabIndex = 38;
            this.label42.Text = "Pacotes Recebidos";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(15, 474);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(93, 13);
            this.label43.TabIndex = 39;
            this.label43.Text = "Pacotes Enviados";
            // 
            // btLimpar
            // 
            this.btLimpar.Location = new System.Drawing.Point(1129, 626);
            this.btLimpar.Name = "btLimpar";
            this.btLimpar.Size = new System.Drawing.Size(75, 23);
            this.btLimpar.TabIndex = 40;
            this.btLimpar.Text = "Limpar";
            this.btLimpar.UseVisualStyleBackColor = true;
            this.btLimpar.Click += new System.EventHandler(this.btLimpar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 675);
            this.Controls.Add(this.btLimpar);
            this.Controls.Add(this.label43);
            this.Controls.Add(this.label42);
            this.Controls.Add(this.tbPacotesRX);
            this.Controls.Add(this.tbPacotesTX);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Emulador Servidor Antares";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btConectar;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox tbIterRX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbQtRX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbChaveRemPub;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbSegredo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbChaveLocalPriv;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbChaveLocalPub;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbIterTX;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbQtTX;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbCodigo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbTabela;
        private System.Windows.Forms.TextBox tbSw;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbDescricao;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbProtocolo;
        private System.Windows.Forms.CheckBox cbAuto;
        private System.Windows.Forms.Button btDataHora;
        private System.Windows.Forms.Button btPubKey;
        private System.Windows.Forms.TextBox tbIKM;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox tbSessaoTX;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lbSessão;
        private System.Windows.Forms.TextBox tbTagRX;
        private System.Windows.Forms.TextBox tbSessaoRX;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbTagTX;
        private System.Windows.Forms.CheckBox cbCriptoRX;
        private System.Windows.Forms.CheckBox cbCriptoTX;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox cbRepete;
        private System.Windows.Forms.TextBox tbAESkey;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tbRede;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tbArea;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox tbRedeAnt;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.TextBox tbAreaAnt;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox tbProtocoloAnt;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.TextBox tbCodigoAnt;
        private System.Windows.Forms.TextBox tbDescricaoAnt;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.TextBox tbSWAnt;
        private System.Windows.Forms.TextBox tbTabelaAnt;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox cbCriptoTXAnt;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox tbTagTXAnt;
        private System.Windows.Forms.TextBox tbSessaoTXAnt;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox tbQtTXAnt;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox tbIterTXAnt;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.CheckBox cbCriptoRXAnt;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox tbTagRXAnt;
        private System.Windows.Forms.TextBox tbSessaoRXAnt;
        private System.Windows.Forms.TextBox tbIterRXAnt;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox tbQtRXAnt;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox tbAESkeyAnt;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox tbIKMAnt;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox tbChaveRemPubAnt;
        private System.Windows.Forms.TextBox tbChaveLocalPrivAnt;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox tbSegredoAnt;
        private System.Windows.Forms.TextBox tbChaveLocalPubAnt;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox tbPacotesTX;
        private System.Windows.Forms.TextBox tbPacotesRX;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.CheckBox cbRepeteAnt;
        private System.Windows.Forms.Button btPubKeyAnt;
        private System.Windows.Forms.Button btDataHoraAnt;
        private System.Windows.Forms.Button btLimpar;
        private System.Windows.Forms.CheckBox cbAutoAnt;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.TextBox tbTcpPort;
        private System.Windows.Forms.Button btIniciar;
    }
}

