// Decompiled with JetBrains decompiler
// Type: SheetGen.Drawings.Factory.CarniceSheetFactory
// Assembly: ukon-sheet-gen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0A3EC11A-00F5-49C6-84D3-14D6114F312F
// Assembly location: C:\Users\bimproject\Desktop\ukon-sheet-gen_all ver\ukon-sheet-gen_19_final\ukon-sheet-gen.dll

using Autodesk.Revit.DB;
using SheetGen.Drawings.Models;
using System;
using System.Collections.Generic;

namespace SheetGen.Drawings.Factory
{
  public class CarniceSheetFactory : IFactory
  {
    public SheetModel GetSheetModel(Element element)
    {
      FamilyInstance familyInstance = (FamilyInstance) element;
      ParameterMap parametersMap = familyInstance.Symbol.ParametersMap;
      SheetModel sheetModel = new SheetModel()
      {
        Number = 1,
        StampTeplateName = "Форма 6_С примечанием_Карниз",
        RevitAssemblyInstance = element.Document.GetElement(element.AssemblyInstanceId) as AssemblyInstance,
        ViewModels = new List<ViewModel>()
        {
          new ViewModel()
          {
            ViewType = typeof (View3D),
            TemplateName = "КМД_О_3D",
            ViewName = "3D",
            ViewAnnotation = " ",
            Coordinates = new XYZ(-0.470753786112789, 0.780697385404636, -49.6672565796797)
          },
          new ViewModel()
          {
            ViewType = typeof (View),
            TemplateName = "КМД_О_Проекция",
            ViewName = "Вид сверху",
            ViewAnnotation = "Top view",
            MoveLeftView = true,
            Coordinates = new XYZ(-0.141857224226564, 0.767312823654761, -0.600000000000001),
            ViewOrientation = AssemblyDetailViewOrientation.ElevationTop
          },
          new ViewModel()
          {
            ViewType = typeof (View),
            TemplateName = "КМД_О_Проекция",
            ViewName = "Вид слева",
            ViewAnnotation = "Left view",
            Coordinates = new XYZ(-0.484223597872426, 0.493305431368229, -601.0 / 1000.0),
            ViewOrientation = AssemblyDetailViewOrientation.ElevationLeft
          },
          new ViewModel()
          {
            ViewType = typeof (View),
            TemplateName = "КМД_О_Проекция",
            ViewName = "Вид спереди",
            PlaceArrowAnnotation = true,
            ViewAnnotation = "Front view",
            Coordinates = new XYZ(-0.159307155650783, 0.504058706145376, -601.0 / 1000.0),
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
