using pdftron.PDF;
using SwissAcademic.Citavi.Shell;
using SwissAcademic.Pdf;
using System.Collections.Generic;
using System.Linq;

namespace JumpToLastPositionAfterActionExecution
{
    public class ActionSources : List<ActionSource>
    {
        #region Methods

        public EntryPoint GetSourceEntryPoint(MainForm form) => this.FirstOrDefault(obj => obj.Form.Equals(form))?.EntryPoint;

        public ActionSource GetActionSource(MainForm mainForm) => this.FirstOrDefault(obj => obj.Form.Equals(mainForm));

        public ActionSource GetActionSource(PDFViewWPF viewer) => this.FirstOrDefault(obj => obj.Viewer.Equals(viewer));

        public void RemoveAt(MainForm form)
        {
            var obj = GetActionSource(form);
            if (obj != null) Remove(obj);
        }

        public void ResetEntryPoint(MainForm form)
        {
            var obj = GetActionSource(form);
            if (obj != null) obj.EntryPoint = null;
        }

        #endregion
    }
}
