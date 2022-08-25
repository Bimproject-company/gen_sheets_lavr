// Decompiled with JetBrains decompiler
// Type: SheetGen.Utils.TransactionSettings
// Assembly: ukon-sheet-gen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0A3EC11A-00F5-49C6-84D3-14D6114F312F
// Assembly location: C:\Users\bimproject\Desktop\ukon-sheet-gen_all ver\ukon-sheet-gen_19_final\ukon-sheet-gen.dll

using Autodesk.Revit.DB;
using System.Collections.Generic;

namespace SheetGen.Utils
{
  public static class TransactionSettings
  {
    public static void SetFailuresPreprocessor(Transaction transaction)
    {
      IFailuresPreprocessor preprocessor = (IFailuresPreprocessor) new TransactionSettings.WarningDiscard();
      FailureHandlingOptions failureHandlingOptions = transaction.GetFailureHandlingOptions();
      failureHandlingOptions.SetFailuresPreprocessor(preprocessor);
      transaction.SetFailureHandlingOptions(failureHandlingOptions);
    }

    public class WarningDiscard : IFailuresPreprocessor
    {
      FailureProcessingResult IFailuresPreprocessor.PreprocessFailures(
        FailuresAccessor failuresAccessor)
      {
        IList<FailureMessageAccessor> failureMessages = failuresAccessor.GetFailureMessages();
        if (failureMessages.Count == 0)
          return FailureProcessingResult.Continue;
        bool flag = false;
        foreach (FailureMessageAccessor failure in (IEnumerable<FailureMessageAccessor>) failureMessages)
        {
          if (failure.HasResolutions())
          {
            failuresAccessor.ResolveFailure(failure);
            flag = true;
          }
          if (failure.GetSeverity() == FailureSeverity.Warning)
          {
            failuresAccessor.DeleteWarning(failure);
            flag = true;
          }
        }
        return flag ? FailureProcessingResult.ProceedWithCommit : FailureProcessingResult.Continue;
      }
    }
  }
}
