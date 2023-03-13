
namespace EsDnevnik
{
    partial class Forme
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
            this.idd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Predmet = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Razred = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Smer = new System.Windows.Forms.DataGridViewComboBoxColumn();
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
            this.idd,
            this.Predmet,
            this.Razred,
            this.Smer,
            this.Brisi,
            this.Menjaj,
            this.Dodaj});
            this.dataGridView1.Location = new System.Drawing.Point(22, 28);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1263, 486);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // idd
            // 
            this.idd.HeaderText = "id";
            this.idd.MinimumWidth = 6;
            this.idd.Name = "idd";
            this.idd.ReadOnly = true;
            this.idd.Width = 125;
            // 
            // Predmet
            // 
            this.Predmet.HeaderText = "predmet";
            this.Predmet.Items.AddRange(new object[] {
            "Baze podataka 2",
            "Programiranje",
            "Srpski jezik",
            "Matematika",
            "Paradigme",
            "Engleski jezik",
            "Fizika",
            "Operativni sistemi",
            "Baze podataka 1"});
            this.Predmet.MinimumWidth = 6;
            this.Predmet.Name = "Predmet";
            this.Predmet.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Predmet.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Predmet.Width = 125;
            // 
            // Razred
            // 
            this.Razred.HeaderText = "razred";
            this.Razred.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.Razred.MinimumWidth = 6;
            this.Razred.Name = "Razred";
            this.Razred.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Razred.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Razred.Width = 125;
            // 
            // Smer
            // 
            this.Smer.HeaderText = "smer";
            this.Smer.Items.AddRange(new object[] {
            "Prirodni",
            "Drustveni",
            "Informaticki",
            "Jezicki"});
            this.Smer.MinimumWidth = 6;
            this.Smer.Name = "Smer";
            this.Smer.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Smer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Smer.Width = 125;
            // 
            // Brisi
            // 
            this.Brisi.HeaderText = "Brisi";
            this.Brisi.MinimumWidth = 6;
            this.Brisi.Name = "Brisi";
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
            // Forme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1516, 558);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Forme";
            this.Text = "Forme";
            this.Load += new System.EventHandler(this.Forme_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idd;
        private System.Windows.Forms.DataGridViewComboBoxColumn Predmet;
        private System.Windows.Forms.DataGridViewComboBoxColumn Razred;
        private System.Windows.Forms.DataGridViewComboBoxColumn Smer;
        private System.Windows.Forms.DataGridViewButtonColumn Brisi;
        private System.Windows.Forms.DataGridViewButtonColumn Menjaj;
        private System.Windows.Forms.DataGridViewButtonColumn Dodaj;
    }
}