// Decompiled with JetBrains decompiler
// Type: SheetGen.App
// Assembly: ukon-sheet-gen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0A3EC11A-00F5-49C6-84D3-14D6114F312F
// Assembly location: C:\Users\bimproject\Desktop\ukon-sheet-gen_all ver\ukon-sheet-gen_19_final\ukon-sheet-gen.dll

using Autodesk.Revit.UI;
using SheetGen.Properties;
using SheetGen.UI;
using System;
using System.Collections.Generic;

namespace SheetGen
{
  public class App : IExternalApplication
  {
    public Result OnShutdown(UIControlledApplication application) => Result.Succeeded;

    public Result OnStartup(UIControlledApplication application)
    {
      try
      {
        UIBuilder builder = new UIBuilder(application);
        builder.BuildRibbon("Automation", "Carnice sheets").BuildButtons(new List<Button>()
        {
          new Button(builder, Resources.s_createDrawings.ToBitmap(), "SheetGen.Commands.Generate", "Generate some sheets", "1. Создать листы (простой камень)"),
          new Button(builder, Resources.s_createDrawings.ToBitmap(), "SheetGen.Commands.GenerateAssembliesOnly", "Generate assemblies", "2. Создать сборки (индивид. камень)"),
          new Button(builder, Resources.s_createDrawings.ToBitmap(), "SheetGen.Commands.GenerateSheetsOnly", "Generate some ass sheets", "3. Создать листы для сборки (индивид. камень)"),
          new Button(builder, Resources.settings.ToBitmap(), "SheetGen.Commands.Settings", "DoSomeSettings", "Настройки")
        });
        return Result.Succeeded;
      }
      catch (Exception ex)
      {
        return Result.Failed;
      }
    }
  }
}
