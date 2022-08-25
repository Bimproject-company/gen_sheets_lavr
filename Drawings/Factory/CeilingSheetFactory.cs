// Decompiled with JetBrains decompiler
// Type: SheetGen.Drawings.Factory.CeilingSheetFactory
// Assembly: ukon-sheet-gen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0A3EC11A-00F5-49C6-84D3-14D6114F312F
// Assembly location: C:\Users\bimproject\Desktop\ukon-sheet-gen_all ver\ukon-sheet-gen_19_final\ukon-sheet-gen.dll

using Autodesk.Revit.DB;
using SheetGen.Drawings.Models;
using System;
using System.Collections.Generic;

namespace SheetGen.Drawings.Factory
{
  public class CeilingSheetFactory : IFactory
  {
    public SheetModel GetSheetModel(Element element)
    {
      FamilyInstance familyInstance = (FamilyInstance) element;
      ParameterMap parametersMap = familyInstance.Symbol.ParametersMap;
      SheetModel sheetModel = new SheetModel()
      {
        Number = 1,
        StampTeplateName = "Форма 6_С примечаниеим",
        RevitAssemblyInstance = element.Document.GetElement(element.AssemblyInstanceId) as AssemblyInstance,
        ViewModels = new List<ViewModel>()
        {
          new ViewModel()
          {
            ViewType = typeof (View3D),
            TemplateName = "КМД_О_3D",
            ViewName = "3D",
            ViewAnnotation = " ",
            Coordinates = new XYZ(-0.159115906283893, 0.640899773818219, -28.2216763581152)
          },
          new ViewModel()
          {
            ViewType = typeof (View),
            TemplateName = "КМД_О_Проекция",
            ViewName = "Вид сверху (тыльная сторона)",
            PlaceArrowAnnotation = true,
            ViewAnnotation = "Top view (back side)",
            Coordinates = new XYZ(-0.441081348195042, 0.715699376338001, -1.2),
            ViewOrientation = AssemblyDetailViewOrientation.ElevationTop
          },
          new ViewModel()
          {
            ViewType = typeof (View),
            TemplateName = "КМД_О_Проекция",
            ViewName = "Вид спереди",
            ViewAnnotation = "Front view",
            PlaceLinesAnnotation = true,
            Coordinates = new XYZ(-0.441081348195043, 0.508984873930244, -1.202),
            ViewOrientation = AssemblyDetailViewOrientation.ElevationFront
          }
        },
        ParameterModels = new List<ParameterModel>()
        {
          new ParameterModel()
          {
            Name = "ADSK_Примечание",
            Value = "Деталировочный чертёж"
          }
        }
      };
      try
      {
        sheetModel.ParameterModels.Add(new ParameterModel()
        {
          Name = "ADSK_Штамп Раздел проекта",
          Value = "РД-ДК-К1. КМД. " + parametersMap.get_Item("Ukon_Порода камня").AsString() + parametersMap.get_Item("Ukon_Марка элемента").AsString() + " этаж " + element.ParametersMap.get_Item("Ukon_Этаж").AsString()
        });
      }
      catch (Exception ex)
      {
        sheetModel.ParameterModels.Add(new ParameterModel()
        {
          Name = "ADSK_Штамп Раздел проекта",
          Value = "РД-ДК-К1. КМД. " + parametersMap.get_Item("Ukon_Порода камня").AsString() + familyInstance.ParametersMap.get_Item("Ukon_Марка элемента").AsString() + " этаж " + element.ParametersMap.get_Item("Ukon_Этаж").AsString()
        });
      }
      return sheetModel;
    }
  }
}
