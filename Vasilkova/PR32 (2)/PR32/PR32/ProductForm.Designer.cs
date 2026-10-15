namespace PR32
{
    partial class ProductForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductForm));
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.unitProduct = new System.Windows.Forms.ComboBox();
            this.nameProduct = new System.Windows.Forms.TextBox();
            this.priceProduct = new System.Windows.Forms.TextBox();
            this.categoryProduct = new System.Windows.Forms.ComboBox();
            this.manufProduct = new System.Windows.Forms.TextBox();
            this.salleryProduct = new System.Windows.Forms.ComboBox();
            this.maxAmount = new System.Windows.Forms.TextBox();
            this.amountProduct = new System.Windows.Forms.TextBox();
            this.decriptionProduct = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.countProduct = new System.Windows.Forms.TextBox();
            this.labelCount = new System.Windows.Forms.Label();
            this.imageProduct = new System.Windows.Forms.PictureBox();
            this.btnViewOrder = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(255)))), ((int)(((byte)(102)))));
            this.label1.Location = new System.Drawing.Point(677, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "g";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(1, 60);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1082, 366);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(255)))), ((int)(((byte)(204)))));
            this.textBox1.Location = new System.Drawing.Point(12, 435);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(455, 34);
            this.textBox1.TabIndex = 2;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(255)))), ((int)(((byte)(204)))));
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(399, 435);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(324, 34);
            this.comboBox1.TabIndex = 3;
            // 
            // comboBox2
            // 
            this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(255)))), ((int)(((byte)(204)))));
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(655, 435);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(324, 34);
            this.comboBox2.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(255)))), ((int)(((byte)(204)))));
            this.button1.Location = new System.Drawing.Point(809, 634);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(242, 45);
            this.button1.TabIndex = 5;
            this.button1.Text = "Выйти";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 526);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 26);
            this.label2.TabIndex = 6;
            this.label2.Text = "Название:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 563);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 26);
            this.label3.TabIndex = 7;
            this.label3.Text = "Единица:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 607);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 26);
            this.label4.TabIndex = 8;
            this.label4.Text = "Цена:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(439, 604);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 26);
            this.label5.TabIndex = 9;
            this.label5.Text = "Описание:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 694);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(165, 26);
            this.label6.TabIndex = 10;
            this.label6.Text = "Производитель:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(439, 482);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 26);
            this.label7.TabIndex = 11;
            this.label7.Text = "Поставщик:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 644);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 26);
            this.label8.TabIndex = 12;
            this.label8.Text = "Категория:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(439, 519);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(230, 26);
            this.label9.TabIndex = 13;
            this.label9.Text = "Максимальная скидка:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(439, 563);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 26);
            this.label10.TabIndex = 14;
            this.label10.Text = "Скидка:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(437, 673);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 26);
            this.label11.TabIndex = 15;
            this.label11.Text = "Фото:";
            // 
            // unitProduct
            // 
            this.unitProduct.FormattingEnabled = true;
            this.unitProduct.Location = new System.Drawing.Point(119, 563);
            this.unitProduct.Name = "unitProduct";
            this.unitProduct.Size = new System.Drawing.Size(290, 34);
            this.unitProduct.TabIndex = 16;
            // 
            // nameProduct
            // 
            this.nameProduct.Location = new System.Drawing.Point(119, 523);
            this.nameProduct.Name = "nameProduct";
            this.nameProduct.Size = new System.Drawing.Size(290, 34);
            this.nameProduct.TabIndex = 17;
            // 
            // priceProduct
            // 
            this.priceProduct.Location = new System.Drawing.Point(119, 605);
            this.priceProduct.Name = "priceProduct";
            this.priceProduct.Size = new System.Drawing.Size(290, 34);
            this.priceProduct.TabIndex = 18;
            // 
            // categoryProduct
            // 
            this.categoryProduct.FormattingEnabled = true;
            this.categoryProduct.Location = new System.Drawing.Point(128, 645);
            this.categoryProduct.Name = "categoryProduct";
            this.categoryProduct.Size = new System.Drawing.Size(281, 34);
            this.categoryProduct.TabIndex = 19;
            // 
            // manufProduct
            // 
            this.manufProduct.Location = new System.Drawing.Point(170, 691);
            this.manufProduct.Name = "manufProduct";
            this.manufProduct.Size = new System.Drawing.Size(239, 34);
            this.manufProduct.TabIndex = 20;
            // 
            // salleryProduct
            // 
            this.salleryProduct.FormattingEnabled = true;
            this.salleryProduct.Location = new System.Drawing.Point(573, 479);
            this.salleryProduct.Name = "salleryProduct";
            this.salleryProduct.Size = new System.Drawing.Size(208, 34);
            this.salleryProduct.TabIndex = 21;
            // 
            // maxAmount
            // 
            this.maxAmount.Location = new System.Drawing.Point(675, 516);
            this.maxAmount.Name = "maxAmount";
            this.maxAmount.Size = new System.Drawing.Size(106, 34);
            this.maxAmount.TabIndex = 22;
            // 
            // amountProduct
            // 
            this.amountProduct.Location = new System.Drawing.Point(534, 560);
            this.amountProduct.Name = "amountProduct";
            this.amountProduct.Size = new System.Drawing.Size(247, 34);
            this.amountProduct.TabIndex = 23;
            // 
            // decriptionProduct
            // 
            this.decriptionProduct.Location = new System.Drawing.Point(547, 597);
            this.decriptionProduct.Name = "decriptionProduct";
            this.decriptionProduct.Size = new System.Drawing.Size(234, 34);
            this.decriptionProduct.TabIndex = 24;
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(255)))), ((int)(((byte)(204)))));
            this.addButton.Location = new System.Drawing.Point(809, 482);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(242, 45);
            this.addButton.TabIndex = 26;
            this.addButton.Text = "Добавить";
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.updateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(255)))), ((int)(((byte)(204)))));
            this.updateButton.Location = new System.Drawing.Point(809, 533);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(242, 45);
            this.updateButton.TabIndex = 28;
            this.updateButton.Text = "Редактировать";
            this.updateButton.UseVisualStyleBackColor = false;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(439, 639);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(86, 26);
            this.label12.TabIndex = 29;
            this.label12.Text = "Кол-во:";
            // 
            // countProduct
            // 
            this.countProduct.Location = new System.Drawing.Point(547, 636);
            this.countProduct.Name = "countProduct";
            this.countProduct.Size = new System.Drawing.Size(234, 34);
            this.countProduct.TabIndex = 30;
            // 
            // labelCount
            // 
            this.labelCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCount.AutoSize = true;
            this.labelCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(255)))), ((int)(((byte)(102)))));
            this.labelCount.Location = new System.Drawing.Point(86, 20);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(23, 26);
            this.labelCount.TabIndex = 31;
            this.labelCount.Text = "g";
            // 
            // imageProduct
            // 
            this.imageProduct.Location = new System.Drawing.Point(443, 703);
            this.imageProduct.Name = "imageProduct";
            this.imageProduct.Size = new System.Drawing.Size(109, 71);
            this.imageProduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageProduct.TabIndex = 27;
            this.imageProduct.TabStop = false;
            this.imageProduct.Click += new System.EventHandler(this.imageProduct_Click_1);
            // 
            // btnViewOrder
            // 
            this.btnViewOrder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(255)))), ((int)(((byte)(204)))));
            this.btnViewOrder.Location = new System.Drawing.Point(809, 584);
            this.btnViewOrder.Name = "btnViewOrder";
            this.btnViewOrder.Size = new System.Drawing.Size(242, 45);
            this.btnViewOrder.TabIndex = 32;
            this.btnViewOrder.Text = "Просмотр заказа";
            this.btnViewOrder.UseVisualStyleBackColor = false;
            // 
            // ProductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 853);
            this.Controls.Add(this.btnViewOrder);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.countProduct);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.imageProduct);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.decriptionProduct);
            this.Controls.Add(this.amountProduct);
            this.Controls.Add(this.maxAmount);
            this.Controls.Add(this.salleryProduct);
            this.Controls.Add(this.manufProduct);
            this.Controls.Add(this.categoryProduct);
            this.Controls.Add(this.priceProduct);
            this.Controls.Add(this.nameProduct);
            this.Controls.Add(this.unitProduct);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ProductForm";
            this.Text = "ООО \"Рулевой\"";
            this.Load += new System.EventHandler(this.ProductForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageProduct)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox unitProduct;
        private System.Windows.Forms.TextBox nameProduct;
        private System.Windows.Forms.TextBox priceProduct;
        private System.Windows.Forms.ComboBox categoryProduct;
        private System.Windows.Forms.TextBox manufProduct;
        private System.Windows.Forms.ComboBox salleryProduct;
        private System.Windows.Forms.TextBox maxAmount;
        private System.Windows.Forms.TextBox amountProduct;
        private System.Windows.Forms.TextBox decriptionProduct;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.PictureBox imageProduct;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox countProduct;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.Button btnViewOrder;
    }
}