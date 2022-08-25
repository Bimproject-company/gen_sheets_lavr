// Decompiled with JetBrains decompiler
// Type: SheetGen.Settings.AddinSettings
// Assembly: ukon-sheet-gen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0A3EC11A-00F5-49C6-84D3-14D6114F312F
// Assembly location: C:\Users\bimproject\Desktop\ukon-sheet-gen_all ver\ukon-sheet-gen_19_final\ukon-sheet-gen.dll

namespace SheetGen.Settings
{
  internal class AddinSettings
  {
    private static AddinSettings Instance;

    public SelectionType SelectionType { get; set; }

    private AddinSettings() => this.SelectionType = SelectionType.MultipleElements;

    public static AddinSettings GetInstance()
    {
      if (AddinSettings.Instance == null)
        AddinSettings.Instance = new AddinSettings();
      return AddinSettings.Instance;
    }
  }
}
