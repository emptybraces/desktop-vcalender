namespace app_vertical_calender
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.c_listbox_calender = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // c_listbox_calender
            // 
            this.c_listbox_calender.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_listbox_calender.Font = new System.Drawing.Font("Yu Gothic UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.c_listbox_calender.HideSelection = false;
            this.c_listbox_calender.LabelWrap = false;
            this.c_listbox_calender.Location = new System.Drawing.Point(0, 0);
            this.c_listbox_calender.MultiSelect = false;
            this.c_listbox_calender.Name = "c_listbox_calender";
            this.c_listbox_calender.Size = new System.Drawing.Size(271, 645);
            this.c_listbox_calender.TabIndex = 0;
            this.c_listbox_calender.UseCompatibleStateImageBehavior = false;
            this.c_listbox_calender.View = System.Windows.Forms.View.Details;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 645);
            this.Controls.Add(this.c_listbox_calender);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView c_listbox_calender;
    }
}

