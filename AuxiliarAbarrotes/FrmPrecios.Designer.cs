namespace AuxiliarAbarrotes
{
    partial class FrmPrecios
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrecios));
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gbCambio = new System.Windows.Forms.GroupBox();
            this.nudValor = new System.Windows.Forms.NumericUpDown();
            this.btnCambiar = new System.Windows.Forms.Button();
            this.rbMonto = new System.Windows.Forms.RadioButton();
            this.rbPorcentaje = new System.Windows.Forms.RadioButton();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.tbFiltro = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbCategorias = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.DEPARTAMENTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CODIGO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DESCRIPCION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PVENTA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.gbCambio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudValor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(867, 53);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.AutoSize = false;
            this.toolStripButton1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(100, 50);
            this.toolStripButton1.Text = "Grabar";
            this.toolStripButton1.ToolTipText = "Precios";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.AutoSize = false;
            this.toolStripButton2.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(100, 50);
            this.toolStripButton2.Text = "&Salir";
            this.toolStripButton2.ToolTipText = "Precios";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(867, 126);
            this.panel1.TabIndex = 5;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gbCambio);
            this.panel2.Controls.Add(this.btnBuscar);
            this.panel2.Controls.Add(this.tbFiltro);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.cbCategorias);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(61, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(763, 117);
            this.panel2.TabIndex = 10;
            // 
            // gbCambio
            // 
            this.gbCambio.Controls.Add(this.nudValor);
            this.gbCambio.Controls.Add(this.btnCambiar);
            this.gbCambio.Controls.Add(this.rbMonto);
            this.gbCambio.Controls.Add(this.rbPorcentaje);
            this.gbCambio.Location = new System.Drawing.Point(66, 48);
            this.gbCambio.Name = "gbCambio";
            this.gbCambio.Size = new System.Drawing.Size(624, 66);
            this.gbCambio.TabIndex = 5;
            this.gbCambio.TabStop = false;
            this.gbCambio.Text = "Cambiar Precio";
            // 
            // nudValor
            // 
            this.nudValor.Location = new System.Drawing.Point(244, 27);
            this.nudValor.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudValor.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.nudValor.Name = "nudValor";
            this.nudValor.Size = new System.Drawing.Size(120, 22);
            this.nudValor.TabIndex = 6;
            this.nudValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnCambiar
            // 
            this.btnCambiar.Location = new System.Drawing.Point(381, 21);
            this.btnCambiar.Name = "btnCambiar";
            this.btnCambiar.Size = new System.Drawing.Size(119, 31);
            this.btnCambiar.TabIndex = 5;
            this.btnCambiar.Text = "&Cambiar";
            this.btnCambiar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCambiar.UseVisualStyleBackColor = true;
            this.btnCambiar.Click += new System.EventHandler(this.btnCambiar_Click);
            // 
            // rbMonto
            // 
            this.rbMonto.AutoSize = true;
            this.rbMonto.Location = new System.Drawing.Point(125, 39);
            this.rbMonto.Name = "rbMonto";
            this.rbMonto.Size = new System.Drawing.Size(60, 18);
            this.rbMonto.TabIndex = 3;
            this.rbMonto.TabStop = true;
            this.rbMonto.Text = "Monto";
            this.rbMonto.UseVisualStyleBackColor = true;
            // 
            // rbPorcentaje
            // 
            this.rbPorcentaje.AutoSize = true;
            this.rbPorcentaje.Location = new System.Drawing.Point(125, 15);
            this.rbPorcentaje.Name = "rbPorcentaje";
            this.rbPorcentaje.Size = new System.Drawing.Size(82, 18);
            this.rbPorcentaje.TabIndex = 2;
            this.rbPorcentaje.TabStop = true;
            this.rbPorcentaje.Text = "Porcentaje";
            this.rbPorcentaje.UseVisualStyleBackColor = true;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(631, 14);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(105, 31);
            this.btnBuscar.TabIndex = 4;
            this.btnBuscar.Text = "&Buscar";
            this.btnBuscar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // tbFiltro
            // 
            this.tbFiltro.Location = new System.Drawing.Point(366, 19);
            this.tbFiltro.Name = "tbFiltro";
            this.tbFiltro.Size = new System.Drawing.Size(259, 22);
            this.tbFiltro.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(317, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Filtrar:";
            // 
            // cbCategorias
            // 
            this.cbCategorias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategorias.FormattingEnabled = true;
            this.cbCategorias.Location = new System.Drawing.Point(82, 19);
            this.cbCategorias.Name = "cbCategorias";
            this.cbCategorias.Size = new System.Drawing.Size(224, 22);
            this.cbCategorias.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Categorías:";
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            this.dgvDatos.AllowUserToDeleteRows = false;
            this.dgvDatos.AllowUserToResizeRows = false;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DEPARTAMENTO,
            this.CODIGO,
            this.DESCRIPCION,
            this.PVENTA});
            this.dgvDatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDatos.Location = new System.Drawing.Point(0, 179);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(867, 314);
            this.dgvDatos.TabIndex = 8;
            // 
            // DEPARTAMENTO
            // 
            this.DEPARTAMENTO.HeaderText = "Departamento";
            this.DEPARTAMENTO.Name = "DEPARTAMENTO";
            this.DEPARTAMENTO.ReadOnly = true;
            this.DEPARTAMENTO.Width = 250;
            // 
            // CODIGO
            // 
            this.CODIGO.HeaderText = "Codigo";
            this.CODIGO.Name = "CODIGO";
            this.CODIGO.ReadOnly = true;
            this.CODIGO.Width = 120;
            // 
            // DESCRIPCION
            // 
            this.DESCRIPCION.HeaderText = "Descripcion";
            this.DESCRIPCION.Name = "DESCRIPCION";
            this.DESCRIPCION.ReadOnly = true;
            this.DESCRIPCION.Width = 240;
            // 
            // PVENTA
            // 
            this.PVENTA.HeaderText = "Precio Vta.";
            this.PVENTA.Name = "PVENTA";
            this.PVENTA.ReadOnly = true;
            this.PVENTA.Width = 140;
            // 
            // FrmPrecios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 493);
            this.Controls.Add(this.dgvDatos);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip2);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmPrecios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmPrecios";
            this.Load += new System.EventHandler(this.FrmPrecios_Load);
            this.Resize += new System.EventHandler(this.FrmPrecios_Resize);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.gbCambio.ResumeLayout(false);
            this.gbCambio.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudValor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox tbFiltro;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbCategorias;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.GroupBox gbCambio;
        private System.Windows.Forms.Button btnCambiar;
        private System.Windows.Forms.RadioButton rbMonto;
        private System.Windows.Forms.RadioButton rbPorcentaje;
        private System.Windows.Forms.NumericUpDown nudValor;
        private System.Windows.Forms.DataGridViewTextBoxColumn DEPARTAMENTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODIGO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DESCRIPCION;
        private System.Windows.Forms.DataGridViewTextBoxColumn PVENTA;
    }
}