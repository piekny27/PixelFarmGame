using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP_project.Support
{
	public static class PopupDialog
	{
		private const string TAG = "PopupDialog";

		public delegate void PopupDialogActionDelegate();
		public async static Task ShowPopupDialog(string question, string yes, PopupDialogActionDelegate actionYes, string no, PopupDialogActionDelegate actionNo, bool defaultYes)
		{
			var dialog = new Windows.UI.Popups.MessageDialog(question == null ? "<NO TEXT AVAILABLE>" : question);

			dialog.Commands.Add(new Windows.UI.Popups.UICommand(yes == null ? "Yes" : yes) { Id = 0 });
			dialog.Commands.Add(new Windows.UI.Popups.UICommand(no == null ? "No" : no) { Id = 1 });


			if (defaultYes)
			{
				dialog.DefaultCommandIndex = 0;
			}
			else
			{
				dialog.DefaultCommandIndex = 1;
			}
			dialog.CancelCommandIndex = 1;

			switch ((int)(await dialog.ShowAsync()).Id)
			{
				case 0:
					Log.info(TAG, "User has confirmed his choice in the popup dialog");
					if (actionYes != null)
					{
						actionYes();
					}
					break;
				case 1:
				default:
					Log.info(TAG, "User has cancelled his choice in the popup dialog");
					if (actionNo != null)
					{
						actionNo();
					}
					break;
			}
		}

		public async static Task ShowPopupDialog(string question, string yes, PopupDialogActionDelegate actionYes, string no, PopupDialogActionDelegate actionNo)
		{
			await ShowPopupDialog(question, yes, actionYes, no, actionNo, false);
		}
	}
}
