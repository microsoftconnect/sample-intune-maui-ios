using Foundation;
using UIKit;

namespace IntuneMAMiOSMauiSample;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate { 
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public override void BuildMenu(IUIMenuBuilder builder)
    {
        if (this.Window != null)
        {
            base.BuildMenu(builder);
        }      
    }

}

