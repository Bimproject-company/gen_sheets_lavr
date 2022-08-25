// Decompiled with JetBrains decompiler
// Type: SheetGen.Commands.Generate
// Assembly: ukon-sheet-gen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0A3EC11A-00F5-49C6-84D3-14D6114F312F
// Assembly location: C:\Users\bimproject\Desktop\ukon-sheet-gen_all ver\ukon-sheet-gen_19_final\ukon-sheet-gen.dll

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autofac;
using SheetGen.Drawings;
using SheetGen.Drawings.Factory;
using SheetGen.Drawings.Transformation;
using SheetGen.Selector;
using SheetGen.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SheetGen.Commands
{
  [Transaction(TransactionMode.Manual)]
  [Regeneration(RegenerationOption.Manual)]
  public class Generate : IExternalCommand
  {
    public Result Execute(
      ExternalCommandData commandData,
      ref string message,
      ElementSet elements)
    {
      DateTime.Parse("26-08-2022");
      if (DateTime.Now.Day > 26 || DateTime.Now.Month > 8 || DateTime.Now.Year > 2022)
      {
        message = "Демо-версия окончена";
        return Result.Failed;
      }
      foreach (Element element in Generate.GetElementsFromSelection(commandData))
      {
        DrawingBuilder drawingBuilder = new DrawingBuilder(element);
        ContainerBuilder builder = new ContainerBuilder();
        FamilyInstance instance = element as FamilyInstance;
        ParameterMap parametersMap = instance.Symbol.ParametersMap;
        if (element.Name.ToLower().Contains("потолок"))
          builder.RegisterType<CeilingSheetFactory>().As<IFactory>();
        else if (instance.ParametersMap.Contains("Ukon_Марка элемента"))
        {
          if (instance.ParametersMap.get_Item("Ukon_Марка элемента").AsString() == "4")
            builder.RegisterType<ToppingSheetFactory>().As<IFactory>();
          else
            builder.RegisterType<CarniceSheetFactory>().As<IFactory>();
        }
        else
          builder.RegisterType<CarniceSheetFactory>().As<IFactory>();
        if (string.IsNullOrEmpty(parametersMap.get_Item("Описание").AsString()) && Generate.PassesVolumeFilter(instance))
          builder.RegisterType<ArcStoneTransformation>().As<ITransformation>();
        else if (string.IsNullOrEmpty(parametersMap.get_Item("Описание").AsString()) && !Generate.PassesVolumeFilter(instance))
          builder.RegisterType<ContextStoneTransformation>().As<ITransformation>();
        else
          builder.RegisterType<RegularStoneTranformation>().As<ITransformation>();
        using (ILifetimeScope context = builder.Build().BeginLifetimeScope())
        {
          IFactory factory = context.Resolve<IFactory>();
          ITransformation transformation = context.Resolve<ITransformation>();
          drawingBuilder.BuildAssemblies();
          string assemblyName = drawingBuilder.GetElement().Document.GetElement(drawingBuilder.GetElement().AssemblyInstanceId).Name;
          if (new FilteredElementCollector(drawingBuilder.GetElement().Document).OfClass(typeof (AssemblyInstance)).Cast<AssemblyInstance>().Where<AssemblyInstance>((Func<AssemblyInstance, bool>) (a => a.AssemblyTypeName == assemblyName)).Count<AssemblyInstance>() == 1)
          {
            drawingBuilder.SetAssembliesOrigin(transformation);
            drawingBuilder.BuildSheetByModel(factory).BuildViews().PlaceViewsOnSheets();
          }
        }
      }
      return Result.Succeeded;
    }

    private static bool PassesVolumeFilter(FamilyInstance instance) => UnitUtils.ConvertFromInternalUnits((instance.get_Geometry(new Options()).First<GeometryObject>() as GeometryInstance).GetInstanceGeometry().Cast<Solid>().First<Solid>((Func<Solid, bool>) (o => o.Volume > 0.0)).Volume, DisplayUnitType.DUT_CUBIC_METERS) >= 0.04;

    private static List<Element> GetElementsFromSelection(
      ExternalCommandData commandData,
      ISelectionFilter filter = null)
    {
      List<Element> elementsFromSelection = new List<Element>();
      AddinSettings instance = AddinSettings.GetInstance();
      ContainerBuilder builder = new ContainerBuilder();
      switch (instance.SelectionType)
      {
        case SelectionType.AllElements:
          builder.RegisterType<AllElementsSelection>().As<ISelection>();
          break;
        case SelectionType.SingleElement:
          builder.RegisterType<SingleSelection>().As<ISelection>();
          break;
        case SelectionType.MultipleElements:
          builder.RegisterType<MultipleSelection>().As<ISelection>();
          break;
        case SelectionType.Cropbox:
          builder.RegisterType<CropboxSelection>().As<ISelection>();
          break;
        default:
          builder.RegisterType<MultipleSelection>().As<ISelection>();
          break;
      }
      using (ILifetimeScope context = builder.Build().BeginLifetimeScope())
      {
        ISelection selection = context.Resolve<ISelection>();
        elementsFromSelection = filter == null ? selection.GetElements(commandData, BuiltInCategory.OST_GenericModel).ToList<Element>() : selection.GetFilteredElements(commandData, filter, BuiltInCategory.OST_GenericModel).ToList<Element>();
      }
      return elementsFromSelection;
    }
  }
}
