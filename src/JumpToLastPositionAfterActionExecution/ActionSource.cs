using pdftron.PDF;
using SwissAcademic.Citavi.Shell;
using SwissAcademic.Pdf;

namespace JumpToLastPositionAfterActionExecution
{
    public class ActionSource
    {
        public ActionSource(MainForm form, PDFViewWPF viewer)
        {
            Form = form;
            Viewer = viewer;
        }

        public MainForm Form { get; }

        public PDFViewWPF Viewer { get; }

        public EntryPoint EntryPoint { get; set; }
    }
}
