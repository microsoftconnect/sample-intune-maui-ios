using System;
using Microsoft.Intune.MAM;

namespace IntuneMAMiOSMauiSample
{
	public class MainPageEnrollmentDelegate : IntuneMAMEnrollmentDelegate
	{
		MainPage MainPageInstance { get; }

		public MainPageEnrollmentDelegate(MainPage PageInstance)
		{
			this.MainPageInstance = PageInstance;
            Console.WriteLine("Page instance was set to MainPage passed in >>>?????");
        }
        //this is still a work in progress, I don't even know if this will work

        public override void EnrollmentRequestWithStatus(IntuneMAMEnrollmentStatus status)
        {
            Console.WriteLine("Checking status of enrollment request. ????????");

            if (status.DidSucceed)
            {
                this.MainPageInstance.LoginSucceeded();
            }
            else if (IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusLoginCanceled != status.StatusCode)
            {
                this.MainPageInstance.LoginFailed();
            }
        }
    }
}

