# sample-intune-maui-ios
Both applications are a demonstration of the [Microsoft Intune App SDK for MAUI.iOS](https://www.nuget.org/packages/Microsoft.Intune.Maui.Essentials.iOS). A developer guide to the SDK is available [here](https://learn.microsoft.com/intune/intune-service/developer/app-sdk-ios-phase1). 

They use the [Microsoft Authentication Library](https://github.com/AzureAD/microsoft-authentication-library-for-objc) to authenticate users.

The IntuneMAMiOSMauiSample is an example of a .NET MAUI application.
The IntuneMAMiOSDotNetSample is an example of a .NET iOS application.

## Steps to run the app
In order to deploy this sample you will need an Intune subscription. Free trials are sufficient for this demo.

These steps will need to be completed for each sample app.

### Step 1: Setting up AAD registration
To sign in with MSAL, you will need to register with Azure Active Directory (AAD) to create a client ID, redirect URI, and request your app permission to access the Mobile Application Management (MAM) service for your tenant.
1. Create an AAD app registration by following the steps in our developer guide [here](https://learn.microsoft.com/intune/intune-service/developer/app-sdk-ios-phase2#set-up-and-configure-a-microsoft-entra-app-registration).
1. Replace the three placeholder values in the Info.plist with the AAD client ID, redirect URI (ex: msauth.com.maui.microsoftintunemamsample://auth), and redirect URI scheme (ex: msauth.com.maui.microsoftintunemamsample).
1. Follow the last step in the [Configure MSAL settings for the Intune App SDK](https://learn.microsoft.com/intune/intune-service/developer/app-sdk-ios-phase2#configure-msal-settings-for-the-intune-app-sdk) section of our developer guide to request your app permission to the MAM service. You may need to grant your tenant's permission with an admin account after doing this.

### Step 2: Setting up Intune
You will need at least one user assigned to a user group. You can see how to create new users [here](https://learn.microsoft.com/intune/intune-service/fundamentals/users-add) and user groups [here](https://learn.microsoft.com/intune/intune-service/fundamentals/groups-add). Be sure to assign Intune licenses to your users [here](https://learn.microsoft.com/intune/intune-service/fundamentals/licenses-assign).

### Step 3: Create and Deploy App Protection Policy (APP)
To enable MAM without device enrollment (MAM-WE), we must create a new App Protection Policy with Intune. Instructions for creating and deploying a new APP can be found [here](https://learn.microsoft.com/intune/intune-service/apps/app-protection-policies). 
1. To create an APP targeting the sample app, click on **Create policy** in the "App protection policies" pane. 
1. In the "Create policy" pane specify the protection policy name, description, and platform. Click on **Select required apps**.
1. At the top of the pane click **More apps** and scroll to the bottom of the pane.
1. Enter the Bundle ID of your app and click **Add**. The bundle ID can be found by in the Info.plist (ex: com.maui.microsoftintunemamsample).
1. Hit **Select** at the bottom of the "Apps" pane.
1. Click the **Settings** button in the "Create policy" pane and set the policy settings you would like to apply to a user group for your app.
1. Once you have selected the settings click **OK** at the bottom of the Settings pane and then click **Create** at the bottom of the "Create policy" pane. Your app should now appear in the "App protection policies" pane.

### Step 4: Launch the App & Sign-In
The sample app should now be properly configured with Intune. Enter the email of one of the users in the group used in Step 2 or Step 3 and select Login to trigger the enrollment.
