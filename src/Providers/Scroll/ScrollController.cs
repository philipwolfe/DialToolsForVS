﻿using Microsoft.VisualStudio.Text.Editor;
using System.Windows.Forms;
using EnvDTE;

namespace DialToolsForVS
{
    internal class ScrollController : IDialController
    {
        public string Moniker => ScrollControllerProvider.Moniker;
        public bool CanHandleClick => true;
        public bool CanHandleRotate => true;

        public bool OnClick()
        {
            if (VsHelpers.DTE.ActiveWindow.IsDocument())
            {
                IWpfTextView view = VsHelpers.GetCurentTextView();

                if (view != null && view.HasAggregateFocus)
                    SendKeys.Send("+{F10}");
                else
                    SendKeys.Send("{ENTER}");
            }
            else if (VsHelpers.DTE.ActiveWindow.IsSolutionExplorer())
            {
                var selectedItems = VsHelpers.DTE.ToolWindows.SolutionExplorer.SelectedItems as UIHierarchyItem[];

                if (selectedItems == null || selectedItems.Length != 1)
                    return false;

                if (selectedItems[0].UIHierarchyItems.Expanded)
                {
                    SendKeys.Send("{LEFT}");
                }
                else
                {
                    SendKeys.Send("{RIGHT}");
                }
            }
            else
            {
                SendKeys.Send("{ENTER}");
            }

            return true;
        }

        public bool OnRotate(RotationDirection direction)
        {
            bool handled = false;
            if (VsHelpers.DTE.ActiveWindow.IsDocument())
            {
                IWpfTextView view = VsHelpers.GetCurentTextView();

                if (view != null && view.HasAggregateFocus)
                {
                    string cmd = direction == RotationDirection.Left ? "Edit.ScrollLineUp" : "Edit.ScrollLineDown";
                    handled = VsHelpers.ExecuteCommand(cmd);
                }
            }

            if (!handled)
            {
                string key = direction == RotationDirection.Left ? "{UP}" : "{DOWN}";
                SendKeys.Send(key);
            }

            return true;
        }
    }
}