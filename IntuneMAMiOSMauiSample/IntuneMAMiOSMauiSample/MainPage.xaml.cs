using System;
using Microsoft.Intune.MAM;

namespace IntuneMAMiOSMauiSample;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        SignedInStatus.Text = "";
        SemanticScreenReader.Announce(SignedInStatus.Text);       

        IntuneMAMEnrollmentManager.Instance.Delegate = new MainPageEnrollmentDelegate(this);

        UpdateLoginState();
    }

    static bool HasEnrolledAccount()
    {
        return !(IntuneMAMEnrollmentManager.Instance.EnrolledAccount == null || IntuneMAMEnrollmentManager.Instance.EnrolledAccount.Length == 0);
    }

    void UpdateLoginState()
    {
        bool Enrolled = HasEnrolledAccount();
        EmailEntry.IsReadOnly = Enrolled;
        EmailEntry.Text = Enrolled ? IntuneMAMEnrollmentManager.Instance.EnrolledAccount : "";
        LoginBtn.Text = Enrolled ? "Logout" : "Login";
    }

    void OnOpenBtnClicked(System.Object sender, System.EventArgs e)
    {
        string uriString = OpenUrlEntry.Text;
        if (string.IsNullOrWhiteSpace(uriString))
        {
            uriString = "https://learn.microsoft.com/dotnet/maui/";
        }
        Uri uri = new(uriString);
        Browser.Default.OpenAsync(uri, BrowserLaunchMode.External);
    }

    void OnShareContentBtnClicked(System.Object sender, System.EventArgs e)
    {
        string text = "Test Content Sharing from Intune Sample App";
        Share.Default.RequestAsync(new ShareTextRequest
        {
            Text = text
        });
    }

    void OnSaveFileBtnClicked(System.Object sender, System.EventArgs e)
    {
        // Apps are responsible for enforcing Save-As policy
        if (!IntuneMAMPolicyManager.Instance.Policy.IsSaveToAllowedForLocation(IntuneMAMSaveLocation.LocalDrive, IntuneMAMEnrollmentManager.Instance.EnrolledAccount))
        {
            SaveFileBtn.Text = "Save file - Failed!";
            IntuneMAMUIHelper.ShowSharingBlockedMessage(delegate
            {
                Console.WriteLine("Dismissed alert");
            });
        }

        string fileName = "intune-test.txt";
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), fileName);

        try
        {
            File.WriteAllText(path, "Test Save to Personal");
            SaveFileBtn.Text = "Save file - Success!";
        }
        catch (Exception)
        {
            SaveFileBtn.Text = "Save file - Failed!";
        }
    }

    void OnLoginBtnClicked(System.Object sender, System.EventArgs e)
    {
        if (!HasEnrolledAccount())
        {
            IntuneMAMEnrollmentManager.Instance.LoginAndEnrollAccount(EmailEntry.Text);
            SignedInStatus.Text = "Logging in...";
            SemanticScreenReader.Announce(SignedInStatus.Text);
        }
        else
        {
            IntuneMAMEnrollmentManager.Instance.DeRegisterAndUnenrollAccount(IntuneMAMEnrollmentManager.Instance.EnrolledAccount, true);
            SignedInStatus.Text = "Logging out...";
            SemanticScreenReader.Announce(SignedInStatus.Text);
        }
    }

    public void LoginFailed()
    {
        SignedInStatus.Text = "Login failed!";
        SemanticScreenReader.Announce(SignedInStatus.Text);

    }

    public void LoginSucceeded()
    {
        SignedInStatus.Text = "Login succeeded!";
        SemanticScreenReader.Announce(SignedInStatus.Text);
        UpdateLoginState();
    }
}


