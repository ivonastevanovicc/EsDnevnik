﻿
namespace EsDnevnik
{
    partial class Raspodela
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nastavnik = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Godina = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Predmet = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Odeljenje = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Brisi = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Menjaj = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Dodaj = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Nastavnik,
            this.Godina,
            this.Predmet,
            this.Odeljenje,
            this.Brisi,
            this.Menjaj,
            this.Dodaj});
            this.dataGridView1.Location = new System.Drawing.Point(2, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1218, 508);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Id
            // 
            this.Id.HeaderText = "id";
            this.Id.MinimumWidth = 6;
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Width = 125;
            // 
            // Nastavnik
            // 
            this.Nastavnik.HeaderText = "nastavnik";
            this.Nastavnik.MinimumWidth = 6;
            this.Nastavnik.Name = "Nastavnik";
            this.Nastavnik.Width = 125;
            // 
            // Godina
            // 
            this.Godina.HeaderText = "godina";
            this.Godina.MinimumWidth = 6;
            this.Godina.Name = "Godina";
            this.Godina.Width = 125;
            // 
            // Predmet
            // 
            this.Predmet.HeaderText = "predmet";
            this.Predmet.MinimumWidth = 6;
            this.Predmet.Name = "Predmet";
            this.Predmet.Width = 125;
            // 
            // Odeljenje
            // 
            this.Odeljenje.HeaderText = "odeljenje";
            this.Odeljenje.MinimumWidth = 6;
            this.Odeljenje.Name = "Odeljenje";
            this.Odeljenje.Width = 125;
            // 
            // Brisi
            // 
            this.Brisi.HeaderText = "Brisi";
            this.Brisi.MinimumWidth = 6;
            this.Brisi.Name = "Brisi";
            this.Brisi.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Brisi.Text = "Brisi";
            this.Brisi.UseColumnTextForButtonValue = true;
            this.Brisi.Width = 125;
            // 
            // Menjaj
            // 
            this.Menjaj.HeaderText = "Menjaj";
            this.Menjaj.MinimumWidth = 6;
            this.Menjaj.Name = "Menjaj";
            this.Menjaj.Text = "Menjaj";
            this.Menjaj.UseColumnTextForButtonValue = true;
            this.Menjaj.Width = 125;
            // 
            // Dodaj
            // 
            this.Dodaj.HeaderText = "Dodaj";
            this.Dodaj.MinimumWidth = 6;
            this.Dodaj.Name = "Dodaj";
            this.Dodaj.Text = "Dodaj";
            this.Dodaj.UseColumnTextForButtonValue = true;
            this.Dodaj.Width = 125;
            // 
            // Raspodela
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1431, 549);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Raspodela";
            this.Text = "Raspodela";
            this.Load += new System.EventHandler(this.Raspodela_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewComboBoxColumn Nastavnik;
        private System.Windows.Forms.DataGridViewComboBoxColumn Godina;
        private System.Windows.Forms.DataGridViewComboBoxColumn Predmet;
        private System.Windows.Forms.DataGridViewComboBoxColumn Odeljenje;
        private System.Windows.Forms.DataGridViewButtonColumn Brisi;
        private System.Windows.Forms.DataGridViewButtonColumn Menjaj;
        private System.Windows.Forms.DataGridViewButtonColumn Dodaj;
    }
}