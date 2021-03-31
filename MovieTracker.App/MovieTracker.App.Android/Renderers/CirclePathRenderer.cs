using Android.Content;
using Android.Graphics;
using MovieTracker.App.Droid.Renderers;
using MovieTracker.App.Views.CustomViews;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CirclePathView), typeof(CirclePathRenderer))]
namespace MovieTracker.App.Droid.Renderers
{
    public class CirclePathRenderer : ViewRenderer
    {
        private bool disposed = false;
        public bool IsDisposed
        {
            get { return disposed; }
        }

        public CirclePathRenderer(Context context) : base(context)
        {
            SetWillNotDraw(false); //Allow the view to call the Draw method.
        }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            //Hook into the Action on the CirclePathView element
            if (e.NewElement != null)
                ((CirclePathView)e.NewElement).CurrentProgressChanged = OnCurrentProgressChanged;
            //If the old element exists, remove that hook.
            if (e.OldElement != null)
                ((CirclePathView)e.OldElement).CurrentProgressChanged = null;
        }

        private void OnCurrentProgressChanged()
        {
            if (!IsDisposed)
                Invalidate(); //Forces redraw.
        }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);
            var paintObj = new Paint();
            var view = Element as CirclePathView;
            //Set pathwidth from DP. This should help with varying screen densities.
            var pathWidth = Context.ToPixels(4);

            paintObj.SetStyle(Paint.Style.Stroke);
            paintObj.StrokeWidth = pathWidth;
            paintObj.Color = view.LineColor.ToAndroid();

            //We're representing the rating as a percentage. To display it as a degree, we multiply our percentage by 360.
            var progressPercent = (float)((view.CurrentProgress / 100) * 360f);
            canvas.DrawArc(pathWidth, pathWidth, canvas.Width - pathWidth, canvas.Height - pathWidth, 0, progressPercent, false, paintObj);
        }

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    // free only managed resources here
                    base.Dispose(disposing);
                }

                // free unmanaged resources here
                disposed = true;
            }
        }
    }
}