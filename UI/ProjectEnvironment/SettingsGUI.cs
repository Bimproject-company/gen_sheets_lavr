// Decompiled with JetBrains decompiler
// Type: SheetGen.UI.ProjectEnvironment.SettingsGUI
// Assembly: ukon-sheet-gen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0A3EC11A-00F5-49C6-84D3-14D6114F312F
// Assembly location: C:\Users\bimproject\Desktop\ukon-sheet-gen_all ver\ukon-sheet-gen_19_final\ukon-sheet-gen.dll

using Autodesk.Revit.UI;
using SheetGen.Settings;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SheetGen.UI.ProjectEnvironment
{
  public class SettingsGUI : Form
  {
    private static AddinSettings Settings;
    private SelectionType temp_SelectionType;
    internal Result settingsResult;
    private IContainer components = (IContainer) null;
    private Panel panel1;
    private RadioButton SelectSingleButton;
    private RadioButton SelectMultipleButton;
    private RadioButton CropBoxButton;
    private RadioButton SelectAllButton;
    private Label label1;
    private System.Windows.Forms.Button SaveSettingsButton;
    private System.Windows.Forms.Button CancelButton;

    public SettingsGUI()
    {
      this.InitializeComponent();
      this.InitializeFields();
      this.InitializeSelectionTypeCheckboxes();
    }

    private void InitializeFields()
    {
      SettingsGUI.Settings = AddinSettings.GetInstance();
      this.temp_SelectionType = SettingsGUI.Settings.SelectionType;
    }

    private void InitializeSelectionTypeCheckboxes()
    {
      switch (SettingsGUI.Settings.SelectionType)
      {
        case SelectionType.AllElements:
          this.SelectAllButton.Checked = true;
          this.CropBoxButton.Checked = false;
          this.SelectMultipleButton.Checked = false;
          this.SelectSingleButton.Checked = false;
          break;
        case SelectionType.SingleElement:
          this.SelectAllButton.Checked = false;
          this.CropBoxButton.Checked = false;
          this.SelectMultipleButton.Checked = false;
          this.SelectSingleButton.Checked = true;
          break;
        case SelectionType.MultipleElements:
          this.SelectAllButton.Checked = false;
          this.CropBoxButton.Checked = false;
          this.SelectMultipleButton.Checked = true;
          this.SelectSingleButton.Checked = false;
          break;
        case SelectionType.Cropbox:
          this.SelectAllButton.Checked = false;
          this.CropBoxButton.Checked = true;
          this.SelectMultipleButton.Checked = false;
          this.SelectSingleButton.Checked = false;
          break;
      }
    }

    private void SelectAllButton_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.SelectAllButton.Checked)
        return;
      this.temp_SelectionType = SelectionType.AllElements;
      this.CropBoxButton.Checked = false;
      this.SelectMultipleButton.Checked = false;
      this.SelectSingleButton.Checked = false;
    }

    private void CropBoxButton_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.CropBoxButton.Checked)
        return;
      this.temp_SelectionType = SelectionType.Cropbox;
      this.SelectAllButton.Checked = false;
      this.SelectMultipleButton.Checked = false;
      this.SelectSingleButton.Checked = false;
    }

    private void SelectMultipleButton_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.SelectMultipleButton.Checked)
        return;
      this.temp_SelectionType = SelectionType.MultipleElements;
      this.SelectAllButton.Checked = false;
      this.CropBoxButton.Checked = false;
      this.SelectSingleButton.Checked = false;
    }

    private void SelectSingleButton_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.SelectSingleButton.Checked)
        return;
      this.temp_SelectionType = SelectionType.SingleElement;
      this.SelectAllButton.Checked = false;
      this.CropBoxButton.Checked = false;
      this.SelectMultipleButton.Checked = false;
    }

    private void SaveSettingsButton_MouseEnter(object sender, EventArgs e)
    {
      if (this.DialogResult != DialogResult.None)
        return;
      this.SaveSettingsButton.BackColor = Color.FromKnownColor(KnownColor.ControlDark);
    }

    private void SaveSettingsButton_MouseLeave(object sender, EventArgs e)
    {
      if (this.DialogResult != DialogResult.None)
        return;
      this.SaveSettingsButton.BackColor = Color.FromKnownColor(KnownColor.ControlLight);
    }

    private void CancelButton_MouseEnter(object sender, EventArgs e)
    {
      if (this.DialogResult != DialogResult.None)
        return;
      this.CancelButton.BackColor = Color.FromKnownColor(KnownColor.ControlDark);
    }

    private void CancelButton_MouseLeave(object sender, EventArgs e)
    {
      if (this.DialogResult != DialogResult.None)
        return;
      this.CancelButton.BackColor = Color.FromKnownColor(KnownColor.ControlLight);
    }

    private void SaveSettingsButton_Click(object sender, EventArgs e)
    {
      SettingsGUI.Settings.SelectionType = this.temp_SelectionType;
      this.settingsResult = Result.Succeeded;
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void CancelButton_Click(object sender, EventArgs e)
    {
      this.temp_SelectionType = SettingsGUI.Settings.SelectionType;
      this.settingsResult = Result.Cancelled;
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void panel2_Click(object sender, EventArgs e)
    {
    }

    private void SettingsGUI_MouseClick(object sender, MouseEventArgs e)
    {
    }

    private void SettingsGUI_Load(object sender, EventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.panel1 = new Panel();
      this.SelectSingleButton = new RadioButton();
      this.SelectMultipleButton = new RadioButton();
      this.CropBoxButton = new RadioButton();
      this.SelectAllButton = new RadioButton();
      this.label1 = new Label();
      this.SaveSettingsButton = new System.Windows.Forms.Button();
      this.CancelButton = new System.Windows.Forms.Button();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      this.panel1.Controls.Add((Control) this.SelectSingleButton);
      this.panel1.Controls.Add((Control) this.SelectMultipleButton);
      this.panel1.Controls.Add((Control) this.CropBoxButton);
      this.panel1.Controls.Add((Control) this.SelectAllButton);
      this.panel1.Location = new Point(12, 32);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(201, 131);
      this.panel1.TabIndex = 0;
      this.SelectSingleButton.AutoSize = true;
      this.SelectSingleButton.Location = new Point(5, 82);
      this.SelectSingleButton.Name = "SelectSingleButton";
      this.SelectSingleButton.Size = new Size(97, 17);
      this.SelectSingleButton.TabIndex = 3;
      this.SelectSingleButton.Text = "Один элемент";
      this.SelectSingleButton.UseVisualStyleBackColor = true;
      this.SelectSingleButton.CheckedChanged += new EventHandler(this.SelectSingleButton_CheckedChanged);
      this.SelectMultipleButton.AutoSize = true;
      this.SelectMultipleButton.Location = new Point(5, 58);
      this.SelectMultipleButton.Name = "SelectMultipleButton";
      this.SelectMultipleButton.Size = new Size(139, 17);
      this.SelectMultipleButton.TabIndex = 2;
      this.SelectMultipleButton.Text = "Несколько элементов";
      this.SelectMultipleButton.UseVisualStyleBackColor = true;
      this.SelectMultipleButton.CheckedChanged += new EventHandler(this.SelectMultipleButton_CheckedChanged);
      this.CropBoxButton.AutoSize = true;
      this.CropBoxButton.Location = new Point(5, 34);
      this.CropBoxButton.Name = "CropBoxButton";
      this.CropBoxButton.Size = new Size(99, 17);
      this.CropBoxButton.TabIndex = 1;
      this.CropBoxButton.Text = "Выбор рамкой";
      this.CropBoxButton.UseVisualStyleBackColor = true;
      this.CropBoxButton.CheckedChanged += new EventHandler(this.CropBoxButton_CheckedChanged);
      this.SelectAllButton.AutoSize = true;
      this.SelectAllButton.Checked = true;
      this.SelectAllButton.Location = new Point(5, 11);
      this.SelectAllButton.Name = "SelectAllButton";
      this.SelectAllButton.Size = new Size(96, 17);
      this.SelectAllButton.TabIndex = 0;
      this.SelectAllButton.TabStop = true;
      this.SelectAllButton.Text = "Выбирать все";
      this.SelectAllButton.UseVisualStyleBackColor = true;
      this.SelectAllButton.CheckedChanged += new EventHandler(this.SelectAllButton_CheckedChanged);
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label1.Location = new Point(8, 9);
      this.label1.Name = "label1";
      this.label1.Size = new Size(166, 20);
      this.label1.TabIndex = 4;
      this.label1.Text = "Выбирать элементы";
      this.SaveSettingsButton.BackColor = SystemColors.ControlLight;
      this.SaveSettingsButton.Location = new Point(115, 185);
      this.SaveSettingsButton.Name = "SaveSettingsButton";
      this.SaveSettingsButton.Size = new Size(98, 32);
      this.SaveSettingsButton.TabIndex = 6;
      this.SaveSettingsButton.Text = "Сохранить";
      this.SaveSettingsButton.UseVisualStyleBackColor = false;
      this.SaveSettingsButton.Click += new EventHandler(this.SaveSettingsButton_Click);
      this.SaveSettingsButton.MouseEnter += new EventHandler(this.SaveSettingsButton_MouseEnter);
      this.SaveSettingsButton.MouseLeave += new EventHandler(this.SaveSettingsButton_MouseLeave);
      this.CancelButton.BackColor = SystemColors.ControlLight;
      this.CancelButton.Location = new Point(11, 185);
      this.CancelButton.Name = "CancelButton";
      this.CancelButton.Size = new Size(98, 32);
      this.CancelButton.TabIndex = 7;
      this.CancelButton.Text = "Отмена";
      this.CancelButton.UseVisualStyleBackColor = false;
      this.CancelButton.Click += new EventHandler(this.CancelButton_Click);
      this.CancelButton.MouseEnter += new EventHandler(this.CancelButton_MouseEnter);
      this.CancelButton.MouseLeave += new EventHandler(this.CancelButton_MouseLeave);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(242, 240);
      this.Controls.Add((Control) this.panel1);
      this.Controls.Add((Control) this.CancelButton);
      this.Controls.Add((Control) this.SaveSettingsButton);
      this.Controls.Add((Control) this.label1);
      this.Name = nameof (SettingsGUI);
      this.Text = "Настройки";
      this.Load += new EventHandler(this.SettingsGUI_Load);
      this.MouseClick += new MouseEventHandler(this.SettingsGUI_MouseClick);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
