// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace ScratchTicket
{
	[Register ("ScratchTicketViewController")]
	partial class ScratchTicketViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ImageFromViewButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		ScratchTicketView scratchView { get; set; }

		[Action ("OnTouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void OnTouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (ImageFromViewButton != null) {
				ImageFromViewButton.Dispose ();
				ImageFromViewButton = null;
			}
			if (scratchView != null) {
				scratchView.Dispose ();
				scratchView = null;
			}
		}
	}
}
