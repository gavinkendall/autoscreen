using System.Windows.Forms;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
        private void RunTriggersOfConditionType(TriggerConditionType conditionType)
        {
            foreach (Trigger trigger in formTrigger.TriggerCollection)
            {
                // Don't show the interface on startup if we're running from the command line.
                if (ScreenCapture.RunningFromCommandLine &&
                    trigger.ConditionType == TriggerConditionType.ApplicationStartup &&
                    trigger.ActionType == TriggerActionType.ShowInterface)
                {
                    continue;
                }

                if (trigger.ConditionType == conditionType)
                {
                    // These actions need to directly correspond with the TriggerActionType class.
                    switch (trigger.ActionType)
                    {
                        case TriggerActionType.ExitApplication:
                            ExitApplication();
                            break;

                        case TriggerActionType.HideInterface:
                            HideInterface();
                            break;

                        case TriggerActionType.RunEditor:
                            Editor editor = formEditor.EditorCollection.GetByName(trigger.Editor);
                            RunEditor(editor, TriggerActionType.RunEditor);
                            break;

                        case TriggerActionType.ShowInterface:
                            ShowInterface();
                            break;

                        case TriggerActionType.StartScreenCapture:
                            StartScreenCapture();
                            break;

                        case TriggerActionType.StopScreenCapture:
                            StopScreenCapture();
                            break;

                        case TriggerActionType.EmailScreenshot:
                            EmailScreenshot(TriggerActionType.EmailScreenshot);
                            break;
                    }
                }
            }
        }
    }
}