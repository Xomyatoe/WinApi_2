namespace Clock
{
    partial class AlarmList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlarmList));
            this.listBoxAlarms = new System.Windows.Forms.ListBox();
            this.buttonAddAlarm = new System.Windows.Forms.Button();
            this.buttonDeleteAlarm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxAlarms
            // 
            this.listBoxAlarms.FormattingEnabled = true;
            this.listBoxAlarms.Location = new System.Drawing.Point(13, 33);
            this.listBoxAlarms.Name = "listBoxAlarms";
            this.listBoxAlarms.Size = new System.Drawing.Size(500, 238);
            this.listBoxAlarms.TabIndex = 0;
            this.listBoxAlarms.DoubleClick += new System.EventHandler(this.listBoxAlarms_DoubleClick);
            // 
            // buttonAddAlarm
            // 
            this.buttonAddAlarm.Location = new System.Drawing.Point(519, 33);
            this.buttonAddAlarm.Name = "buttonAddAlarm";
            this.buttonAddAlarm.Size = new System.Drawing.Size(75, 23);
            this.buttonAddAlarm.TabIndex = 1;
            this.buttonAddAlarm.Text = "Добавить";
            this.buttonAddAlarm.UseVisualStyleBackColor = true;
            this.buttonAddAlarm.Click += new System.EventHandler(this.buttonAddAlarm_Click);
            // 
            // buttonDeleteAlarm
            // 
            this.buttonDeleteAlarm.Location = new System.Drawing.Point(519, 63);
            this.buttonDeleteAlarm.Name = "buttonDeleteAlarm";
            this.buttonDeleteAlarm.Size = new System.Drawing.Size(75, 23);
            this.buttonDeleteAlarm.TabIndex = 2;
            this.buttonDeleteAlarm.Text = "Удалить";
            this.buttonDeleteAlarm.UseVisualStyleBackColor = true;
            // 
            // AlarmList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 287);
            this.Controls.Add(this.buttonDeleteAlarm);
            this.Controls.Add(this.buttonAddAlarm);
            this.Controls.Add(this.listBoxAlarms);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AlarmList";
            this.Text = "AlarmList";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxAlarms;
        private System.Windows.Forms.Button buttonAddAlarm;
        private System.Windows.Forms.Button buttonDeleteAlarm;
    }
}