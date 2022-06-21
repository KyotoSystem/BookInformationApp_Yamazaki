
namespace BookAppFrom.Sales
{
    partial class Sale
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
            this.BooksList = new System.Windows.Forms.ListView();
            this.Names = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Prie = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Quantity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LoginStrip = new System.Windows.Forms.StatusStrip();
            this.IDLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.NameLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.CodeText = new System.Windows.Forms.TextBox();
            this.Code = new System.Windows.Forms.Label();
            this.CodeButton = new System.Windows.Forms.Button();
            this.QuantityButton = new System.Windows.Forms.Button();
            this.Quantitylabel = new System.Windows.Forms.Label();
            this.QuantityText = new System.Windows.Forms.TextBox();
            this.TotalFeeLabel = new System.Windows.Forms.Label();
            this.TotalFeeText = new System.Windows.Forms.TextBox();
            this.PaymentButton = new System.Windows.Forms.Button();
            this.PaymentLabel = new System.Windows.Forms.Label();
            this.PaymentText = new System.Windows.Forms.TextBox();
            this.ChangeLabel = new System.Windows.Forms.Label();
            this.ChangeText = new System.Windows.Forms.TextBox();
            this.LoginStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // BooksList
            // 
            this.BooksList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Names,
            this.Prie,
            this.Quantity});
            this.BooksList.HideSelection = false;
            this.BooksList.Location = new System.Drawing.Point(12, 38);
            this.BooksList.Name = "BooksList";
            this.BooksList.Size = new System.Drawing.Size(458, 327);
            this.BooksList.TabIndex = 0;
            this.BooksList.UseCompatibleStateImageBehavior = false;
            this.BooksList.View = System.Windows.Forms.View.Details;
            this.BooksList.SelectedIndexChanged += new System.EventHandler(this.BooksList_SelectedIndexChanged);
            // 
            // Names
            // 
            this.Names.Text = "商品名";
            this.Names.Width = 183;
            // 
            // Prie
            // 
            this.Prie.Text = "価格";
            this.Prie.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Prie.Width = 118;
            // 
            // Quantity
            // 
            this.Quantity.Text = "数量";
            this.Quantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Quantity.Width = 153;
            // 
            // LoginStrip
            // 
            this.LoginStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.IDLabel,
            this.NameLabel});
            this.LoginStrip.Location = new System.Drawing.Point(0, 406);
            this.LoginStrip.Name = "LoginStrip";
            this.LoginStrip.Size = new System.Drawing.Size(800, 22);
            this.LoginStrip.TabIndex = 1;
            this.LoginStrip.Text = "statusStrip1";
            // 
            // IDLabel
            // 
            this.IDLabel.Name = "IDLabel";
            this.IDLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // NameLabel
            // 
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // CodeText
            // 
            this.CodeText.Location = new System.Drawing.Point(592, 40);
            this.CodeText.Name = "CodeText";
            this.CodeText.Size = new System.Drawing.Size(182, 19);
            this.CodeText.TabIndex = 2;
            // 
            // Code
            // 
            this.Code.Location = new System.Drawing.Point(486, 38);
            this.Code.Name = "Code";
            this.Code.Size = new System.Drawing.Size(100, 23);
            this.Code.TabIndex = 3;
            this.Code.Text = "商品コード：";
            this.Code.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CodeButton
            // 
            this.CodeButton.Location = new System.Drawing.Point(683, 78);
            this.CodeButton.Name = "CodeButton";
            this.CodeButton.Size = new System.Drawing.Size(91, 23);
            this.CodeButton.TabIndex = 4;
            this.CodeButton.Text = "商品コード";
            this.CodeButton.UseVisualStyleBackColor = true;
            this.CodeButton.Click += new System.EventHandler(this.CodeButton_Click);
            // 
            // QuantityButton
            // 
            this.QuantityButton.Location = new System.Drawing.Point(683, 164);
            this.QuantityButton.Name = "QuantityButton";
            this.QuantityButton.Size = new System.Drawing.Size(91, 23);
            this.QuantityButton.TabIndex = 7;
            this.QuantityButton.Text = "数量";
            this.QuantityButton.UseVisualStyleBackColor = true;
            this.QuantityButton.Click += new System.EventHandler(this.QuantityButton_Click);
            // 
            // Quantitylabel
            // 
            this.Quantitylabel.Location = new System.Drawing.Point(486, 124);
            this.Quantitylabel.Name = "Quantitylabel";
            this.Quantitylabel.Size = new System.Drawing.Size(100, 23);
            this.Quantitylabel.TabIndex = 6;
            this.Quantitylabel.Text = "数量：";
            this.Quantitylabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // QuantityText
            // 
            this.QuantityText.Location = new System.Drawing.Point(592, 126);
            this.QuantityText.Name = "QuantityText";
            this.QuantityText.Size = new System.Drawing.Size(182, 19);
            this.QuantityText.TabIndex = 5;
            // 
            // TotalFeeLabel
            // 
            this.TotalFeeLabel.Location = new System.Drawing.Point(486, 210);
            this.TotalFeeLabel.Name = "TotalFeeLabel";
            this.TotalFeeLabel.Size = new System.Drawing.Size(100, 23);
            this.TotalFeeLabel.TabIndex = 9;
            this.TotalFeeLabel.Text = "合計：";
            this.TotalFeeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TotalFeeText
            // 
            this.TotalFeeText.Location = new System.Drawing.Point(592, 212);
            this.TotalFeeText.Name = "TotalFeeText";
            this.TotalFeeText.Size = new System.Drawing.Size(182, 19);
            this.TotalFeeText.TabIndex = 8;
            // 
            // PaymentButton
            // 
            this.PaymentButton.Location = new System.Drawing.Point(683, 302);
            this.PaymentButton.Name = "PaymentButton";
            this.PaymentButton.Size = new System.Drawing.Size(91, 23);
            this.PaymentButton.TabIndex = 12;
            this.PaymentButton.Text = "会計";
            this.PaymentButton.UseVisualStyleBackColor = true;
            this.PaymentButton.Click += new System.EventHandler(this.PaymentButton_Click);
            // 
            // PaymentLabel
            // 
            this.PaymentLabel.Location = new System.Drawing.Point(486, 262);
            this.PaymentLabel.Name = "PaymentLabel";
            this.PaymentLabel.Size = new System.Drawing.Size(100, 23);
            this.PaymentLabel.TabIndex = 11;
            this.PaymentLabel.Text = "支払い：";
            this.PaymentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PaymentText
            // 
            this.PaymentText.Location = new System.Drawing.Point(592, 264);
            this.PaymentText.Name = "PaymentText";
            this.PaymentText.Size = new System.Drawing.Size(182, 19);
            this.PaymentText.TabIndex = 10;
            // 
            // ChangeLabel
            // 
            this.ChangeLabel.Location = new System.Drawing.Point(486, 344);
            this.ChangeLabel.Name = "ChangeLabel";
            this.ChangeLabel.Size = new System.Drawing.Size(100, 23);
            this.ChangeLabel.TabIndex = 14;
            this.ChangeLabel.Text = "お釣り：";
            this.ChangeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChangeText
            // 
            this.ChangeText.Location = new System.Drawing.Point(592, 346);
            this.ChangeText.Name = "ChangeText";
            this.ChangeText.Size = new System.Drawing.Size(182, 19);
            this.ChangeText.TabIndex = 13;
            // 
            // Sale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 428);
            this.Controls.Add(this.ChangeLabel);
            this.Controls.Add(this.ChangeText);
            this.Controls.Add(this.PaymentButton);
            this.Controls.Add(this.PaymentLabel);
            this.Controls.Add(this.PaymentText);
            this.Controls.Add(this.TotalFeeLabel);
            this.Controls.Add(this.TotalFeeText);
            this.Controls.Add(this.QuantityButton);
            this.Controls.Add(this.Quantitylabel);
            this.Controls.Add(this.QuantityText);
            this.Controls.Add(this.CodeButton);
            this.Controls.Add(this.Code);
            this.Controls.Add(this.CodeText);
            this.Controls.Add(this.LoginStrip);
            this.Controls.Add(this.BooksList);
            this.Name = "Sale";
            this.Text = "売買画面";
            this.Load += new System.EventHandler(this.Sale_Load);
            this.LoginStrip.ResumeLayout(false);
            this.LoginStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView BooksList;
        private System.Windows.Forms.StatusStrip LoginStrip;
        private System.Windows.Forms.ToolStripStatusLabel IDLabel;
        private System.Windows.Forms.ToolStripStatusLabel NameLabel;
        private System.Windows.Forms.ColumnHeader Names;
        private System.Windows.Forms.ColumnHeader Prie;
        private System.Windows.Forms.ColumnHeader Quantity;
        private System.Windows.Forms.TextBox CodeText;
        private System.Windows.Forms.Label Code;
        private System.Windows.Forms.Button CodeButton;
        private System.Windows.Forms.Button QuantityButton;
        private System.Windows.Forms.Label Quantitylabel;
        private System.Windows.Forms.TextBox QuantityText;
        private System.Windows.Forms.Label TotalFeeLabel;
        private System.Windows.Forms.TextBox TotalFeeText;
        private System.Windows.Forms.Button PaymentButton;
        private System.Windows.Forms.Label PaymentLabel;
        private System.Windows.Forms.TextBox PaymentText;
        private System.Windows.Forms.Label ChangeLabel;
        private System.Windows.Forms.TextBox ChangeText;
    }
}