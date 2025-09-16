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
            this.label18 = new System.Windows.Forms.Label();
            this.tbAESkey = new System.Windows.Forms.TextBox();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 436);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1214, 22);
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
            this.serialPort1.BaudRate = 115200;
            this.serialPort1.PortName = "COM8";
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbAuto);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.btConectar);
            this.groupBox1.Location = new System.Drawing.Point(10, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 71);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Comunicação";
            // 
            // cbAuto
            // 
            this.cbAuto.AutoSize = true;
            this.cbAuto.Checked = true;
            this.cbAuto.CheckState = System.Windows.Forms.CheckState.Checked;
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
            this.groupBox2.Location = new System.Drawing.Point(11, 87);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(1194, 199);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ECDH";
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
            this.groupBox3.Location = new System.Drawing.Point(390, 305);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(356, 129);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Serial RX";
            // 
            // cbCriptoRX
            // 
            this.cbCriptoRX.AutoSize = true;
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
            this.groupBox4.Location = new System.Drawing.Point(10, 300);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(353, 129);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Serial TX";
            // 
            // cbCriptoTX
            // 
            this.cbCriptoTX.AutoSize = true;
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
            this.label9.Location = new System.Drawing.Point(248, 24);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Código:";
            // 
            // tbCodigo
            // 
            this.tbCodigo.Enabled = false;
            this.tbCodigo.Location = new System.Drawing.Point(293, 22);
            this.tbCodigo.Margin = new System.Windows.Forms.Padding(2);
            this.tbCodigo.Name = "tbCodigo";
            this.tbCodigo.Size = new System.Drawing.Size(48, 20);
            this.tbCodigo.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(377, 24);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Tabela:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(652, 24);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(28, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "SW:";
            // 
            // tbTabela
            // 
            this.tbTabela.Enabled = false;
            this.tbTabela.Location = new System.Drawing.Point(422, 22);
            this.tbTabela.Margin = new System.Windows.Forms.Padding(2);
            this.tbTabela.Name = "tbTabela";
            this.tbTabela.Size = new System.Drawing.Size(48, 20);
            this.tbTabela.TabIndex = 22;
            // 
            // tbSw
            // 
            this.tbSw.Enabled = false;
            this.tbSw.Location = new System.Drawing.Point(680, 20);
            this.tbSw.Margin = new System.Windows.Forms.Padding(2);
            this.tbSw.Name = "tbSw";
            this.tbSw.Size = new System.Drawing.Size(48, 20);
            this.tbSw.TabIndex = 23;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(234, 58);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 13);
            this.label12.TabIndex = 24;
            this.label12.Text = "Descrição:";
            // 
            // tbDescricao
            // 
            this.tbDescricao.Enabled = false;
            this.tbDescricao.Location = new System.Drawing.Point(293, 55);
            this.tbDescricao.Margin = new System.Windows.Forms.Padding(2);
            this.tbDescricao.Name = "tbDescricao";
            this.tbDescricao.Size = new System.Drawing.Size(434, 20);
            this.tbDescricao.TabIndex = 25;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(496, 24);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(55, 13);
            this.label13.TabIndex = 26;
            this.label13.Text = "Protocolo:";
            // 
            // tbProtocolo
            // 
            this.tbProtocolo.Enabled = false;
            this.tbProtocolo.Location = new System.Drawing.Point(551, 22);
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
            this.groupBox5.Location = new System.Drawing.Point(772, 305);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(172, 129);
            this.groupBox5.TabIndex = 30;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Comandos Serial";
            // 
            // cbRepete
            // 
            this.cbRepete.AutoSize = true;
            this.cbRepete.Enabled = false;
            this.cbRepete.Location = new System.Drawing.Point(7, 106);
            this.cbRepete.Name = "cbRepete";
            this.cbRepete.Size = new System.Drawing.Size(50, 17);
            this.cbRepete.TabIndex = 30;
            this.cbRepete.Text = "Loop";
            this.cbRepete.UseVisualStyleBackColor = true;
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
            // tbAESkey
            // 
            this.tbAESkey.Enabled = false;
            this.tbAESkey.Location = new System.Drawing.Point(127, 173);
            this.tbAESkey.Margin = new System.Windows.Forms.Padding(2);
            this.tbAESkey.Name = "tbAESkey";
            this.tbAESkey.Size = new System.Drawing.Size(1043, 20);
            this.tbAESkey.TabIndex = 18;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 458);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.tbProtocolo);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.tbDescricao);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tbSw);
            this.Controls.Add(this.tbTabela);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbCodigo);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
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
    }
}

