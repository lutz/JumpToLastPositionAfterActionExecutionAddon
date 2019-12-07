using pdftron.PDF;
using SwissAcademic.Citavi.Shell;
using SwissAcademic.Pdf;

namespace JumpToLastPositionAfterActionExecution
{
    public class ActionSource
    {
        #region Constructors

        public ActionSource(MainForm form, PDFViewWPF viewer)
        {
            Form = form;
            Viewer = viewer;
        }

        #endregion

        #region Properties

        public MainForm Form { get; }

        public PDFViewWPF Viewer { get; }

        public EntryPoint EntryPoint { get; set; }

        #endregion
    }
}
