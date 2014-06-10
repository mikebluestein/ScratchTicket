using System;
using System.ComponentModel;
using System.Drawing;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.IO;

namespace ScratchTicket
{
    public enum Shape
    {
        Rectangle,
        Circle
    }

	[Register("ScratchTicketView")]
	public class ScratchTicketView : UIView
	{
		CGPath path;
		PointF initialPoint;
		PointF latestPoint;
		bool startNewPath = false;
		string defaultImageFileName = "FillTexture.png";
		string imageFileName;

        Shape shape = Shape.Rectangle;

        [Export("ScratchViewShape"), Browsable(true)]
        public Shape ScratchViewShape {
            get {
                return shape;
            }
            set {
                shape = value;
                SetNeedsDisplay ();
            }
        }

        [Export("ImageFileName"), Browsable(true)]
		public string ImageFileName {
			get {                  
				if (String.IsNullOrWhiteSpace (imageFileName)) 

					return defaultImageFileName;

				else if (UIImage.FromBundle(imageFileName) != null) 

					return imageFileName;
				else
					return defaultImageFileName;
			}
			set {
				imageFileName = value;
				SetNeedsDisplay ();
			}
		}

		public ScratchTicketView (IntPtr p) : base(p)
		{
		}

		public ScratchTicketView ()
		{
			Initialize ();
		}

        public override void AwakeFromNib ()
        {
            Initialize ();
        }

		void Initialize ()
        {
			BackgroundColor = UIColor.Clear;
			Opaque = false;
			path = new CGPath ();
		}

		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			base.TouchesBegan (touches, evt);

			var touch = touches.AnyObject as UITouch;

			if (touch != null) {
				initialPoint = touch.LocationInView (this);
			}
		}

		public override void TouchesMoved (NSSet touches, UIEvent evt)
		{
			base.TouchesMoved (touches, evt);

			var touch = touches.AnyObject as UITouch;

			if (touch != null) {
				latestPoint = touch.LocationInView (this);
				SetNeedsDisplay ();
			}
		}

		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			base.TouchesEnded (touches, evt);
			startNewPath = true;
		}

		public override void Draw (RectangleF rect)
		{
			base.Draw (rect);

			using (CGContext g = UIGraphics.GetCurrentContext ()) {

				g.SetFillColor ((UIColor.FromPatternImage (UIImage.FromBundle (ImageFileName)).CGColor));

                if (ScratchViewShape == Shape.Rectangle) {
                    g.FillRect (rect);
                } else {
                    g.AddArc (rect.Width / 2, rect.Height / 2, rect.Width / 2, 0, (float)Math.PI * 2.0f, true);
                    g.FillPath ();
                }

				if (!initialPoint.IsEmpty) {
					g.SetLineWidth (20);
					g.SetBlendMode (CGBlendMode.Clear);
					UIColor.Clear.SetColor ();

					if (path.IsEmpty || startNewPath) {
						path.AddLines (new PointF[] { initialPoint, latestPoint });
						startNewPath = false;
					} else {
						path.AddLineToPoint (latestPoint);
					}

					g.SetLineCap (CGLineCap.Round);
					g.AddPath (path);		
					g.DrawPath (CGPathDrawingMode.Stroke);
				}
			}
		}
	}
}

