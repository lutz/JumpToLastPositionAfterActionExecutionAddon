using SwissAcademic.Citavi.Controls.Wpf;
using SwissAcademic.Citavi.Shell.Controls.Preview;
using System.Collections.Generic;
using System.Reflection;
using pdftron.PDF;

namespace JumpToLastPositionAfterActionExecution
{
    public static class Extensions
    {
        #region System.Collections.Generic.Dictionary<TKey,TValue>

        public static void AddOrUpdateSafe<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        public static void RemoveSafe<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary.Remove(key);
            }
        }

        #endregion

        #region SwissAcademic.Citavi.Shell.Controls.Preview.PreviewControl

        public static PdfViewControl GetPdfViewer(this PreviewControl previewControl)
        {
            return previewControl.GetType().GetProperty("PdfViewControl", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(previewControl) as PdfViewControl;
        }

        public static PDFViewWPF GetPdftronViewer(this PreviewControl previewControl)
        {
            var pdfViewControl = previewControl.GetPdfViewer();

            return pdfViewControl.GetType().GetProperty("Viewer", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(pdfViewControl) as PDFViewWPF;
        }

        #endregion
    }
}
