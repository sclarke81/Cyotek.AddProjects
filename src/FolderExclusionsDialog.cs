﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

// TODO: Rename this to be a settings dialog

namespace Cyotek.VisualStudioExtensions.AddProjects
{
    using System.Linq;

    public partial class FolderExclusionsDialog : BaseForm
  {
    #region Constructors

    public FolderExclusionsDialog()
    {
      this.InitializeComponent();
    }

    public FolderExclusionsDialog(ExtensionSettingsProjectCollection exclusions, ExtensionSettingsProjectCollection projectTypes)
      : this()
    {
      if (exclusions.Count != 0)
      {
        StringBuilder sb;

        sb = new StringBuilder();

        foreach (string exclusion in exclusions)
        {
          sb.AppendLine(exclusion);
        }

        folderExclusionsTextBox.Text = sb.ToString();
      }
        if (projectTypes.Count != 0)
        {
            var sb = new StringBuilder();
            foreach (var projectType in projectTypes)
            {
                sb.AppendLine(projectType);
            }
            projectTypesTextBox.Text = sb.ToString();
        }
    }

    #endregion

    #region Properties

    [Browsable(false)]
    public string[] ExcludedFolders
    {
      get
      {
        List<string> results;

        results = new List<string>();

        // ReSharper disable once LoopCanBePartlyConvertedToQuery
        foreach (string line in folderExclusionsTextBox.Lines)
        {
          if (!string.IsNullOrWhiteSpace(line) && !results.Contains(line))
          {
            results.Add(line);
          }
        }

        return results.ToArray();
      }
    }

    [Browsable(false)]
    public string[] ProjectTypes
       => projectTypesTextBox.Lines.Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().ToArray();

    #endregion

    #region Methods

        private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void resetToDefaultLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      if (MessageBox.Show(this, "Are you sure you want to reset exclusion and project type settings to their default values?", "Reset Settings", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
      {
        folderExclusionsTextBox.Text = "\\bower_components\\\r\n\\node_modules\\\r\n\\bin\\\r\n\\obj\\";
        projectTypesTextBox.Text = "C# Projects|*.csproj\r\nVisual Basic Projects|*.vbproj\r\nC++ Projects|*.vcproj;*.vcxproj\r\nF# Projects|*.fsproj\r\nNuGet Packager Projects|*.nuproj";
      }
    }

    #endregion
  }
}
