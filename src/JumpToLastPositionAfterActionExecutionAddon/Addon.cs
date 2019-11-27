using JumpToLastPositionAfterActionExecution.Properties;
using pdftron.PDF;
using SwissAcademic.Citavi.Shell;
using SwissAcademic.Citavi.Shell.Controls.Preview;
using SwissAcademic.Controls;
using SwissAcademic.Pdf;
using System;
using System.Windows.Forms;

namespace JumpToLastPositionAfterActionExecution
{
    public class Addon : CitaviAddOn<MainForm>
    {
        #region Constants

        const string Keys_Button_JumpToLastPosition = "PDFActionObserver.Button.JumpToLastPosition";

        #endregion

        #region Fields

        readonly ActionSources _actionSources;

        #endregion

        #region Constructors

        public Addon()
        {
            _actionSources = new ActionSources();
        }

        #endregion

        #region Methods

        public override void OnHostingFormLoaded(MainForm mainForm)
        {
            mainForm.FormClosed += MainForm_FormClosed;

            var button = mainForm.GetPreviewCommandbar(MainFormPreviewCommandbarId.Toolbar).GetCommandbarMenu(MainFormPreviewCommandbarMenuId.Tools).InsertCommandbarButton(7, Keys_Button_JumpToLastPosition, Resources.Button_JumpToLastPosition);
            button.Shortcut = Shortcut.AltF3;
            button.HasSeparator = true;

            mainForm.PreviewControl.ActiveUriChanged += PreviewControl_ActiveUriChanged;

            var viewer = mainForm.PreviewControl.GetPdftronViewer();
            viewer.OnAction += Viewer_OnAction;

            _actionSources.Add(new ActionSource(mainForm, viewer));

            base.OnHostingFormLoaded(mainForm);
        }

        public override void OnApplicationIdle(MainForm mainForm)
        {
            if (mainForm.GetPreviewCommandbar(MainFormPreviewCommandbarId.Toolbar).GetCommandbarMenu(MainFormPreviewCommandbarMenuId.Tools).GetCommandbarButton(Keys_Button_JumpToLastPosition) is CommandbarButton button)
            {
                button.Visible = mainForm.PreviewControl.ActivePreviewType == PreviewType.Pdf && _actionSources.GetSourceEntryPoint(mainForm) != null;
            }
            base.OnApplicationIdle(mainForm);
        }

        public override void OnBeforePerformingCommand(MainForm mainForm, BeforePerformingCommandEventArgs e)
        {
            if (e.Key.Equals(Keys_Button_JumpToLastPosition, StringComparison.OrdinalIgnoreCase))
            {
                if (mainForm.PreviewControl.ActivePreviewType == PreviewType.Pdf && _actionSources.GetSourceEntryPoint(mainForm) is EntryPoint point)
                {
                    mainForm.PreviewControl.GetPdfViewer()?.SetEntryPoint(point);
                }
                e.Handled = true;
            }

            base.OnBeforePerformingCommand(mainForm, e);
        }

        public override void OnLocalizing(MainForm mainForm)
        {
            if (mainForm.GetPreviewCommandbar(MainFormPreviewCommandbarId.Toolbar).GetCommandbarMenu(MainFormPreviewCommandbarMenuId.Tools).GetCommandbarButton(Keys_Button_JumpToLastPosition) is CommandbarButton button)
            {
                button.Text = Resources.Button_JumpToLastPosition;
            }
            base.OnLocalizing(mainForm);
        }

        #endregion

        #region EventHandlers

        void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is MainForm mainForm)
            {
                mainForm.FormClosed -= MainForm_FormClosed;
                mainForm.PreviewControl.ActiveUriChanged -= PreviewControl_ActiveUriChanged;
                var viewer = mainForm.PreviewControl.GetPdftronViewer();
                viewer.OnAction -= Viewer_OnAction;

                _actionSources.RemoveAt(mainForm);
            }
        }

        void Viewer_OnAction(PDFViewWPF viewer, PDFViewWPF.ActionEventArgs e)
        {

            if (_actionSources.GetActionSource(viewer) is ActionSource actionSource)
            {
                actionSource.EntryPoint = actionSource.Form.PreviewControl.GetPdfViewer()?.GetActualEntryPoint();
            }
        }

        void PreviewControl_ActiveUriChanged(object sender, EventArgs e)
        {
            if (sender is PreviewControl previewControl && previewControl.FindForm() is MainForm mainForm)
            {
                _actionSources.ResetEntryPoint(mainForm);
            }
        }

        #endregion
    }
}