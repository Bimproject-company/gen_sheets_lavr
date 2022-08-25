// Decompiled with JetBrains decompiler
// Type: SheetGen.Commands.Settings
// Assembly: ukon-sheet-gen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0A3EC11A-00F5-49C6-84D3-14D6114F312F
// Assembly location: C:\Users\bimproject\Desktop\ukon-sheet-gen_all ver\ukon-sheet-gen_19_final\ukon-sheet-gen.dll

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using SheetGen.UI.ProjectEnvironment;
using System.Windows.Forms;

namespace SheetGen.Commands
{
  [Transaction(TransactionMode.Manual)]
  [Regeneration(RegenerationOption.Manual)]
  internal class Settings : IExternalCommand
  {
    public Result Execute(
      ExternalCommandData commandData,
      ref string message,
      ElementSet elements)
    {
      SettingsGUI settingsGui = new SettingsGUI();
      if (settingsGui.ShowDialog() == DialogResult.OK || settingsGui.ShowDialog() == DialogResult.Cancel)
        return settingsGui.settingsResult;
      string str = "Что - то пошло не так";
      message = str;
      return Result.Failed;
    }
  }
}
