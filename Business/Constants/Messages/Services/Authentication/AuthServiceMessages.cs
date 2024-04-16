namespace Business.Constants.Messages.Services.Authentication;

internal static class AuthServiceMessages
{
    internal const string NotCreator = "Logged in user is not the creator of the application.";
    internal const string VerificationCodeMailNotSent = "Verification code failed to send.";
    internal const string LogoutSuccessful = "Successfully logged out.";
    internal const string NotFound = "Email or password is wrong.";
    internal const string WrongPassword = "Email or password is wrong.";
    internal const string LoginSuccessful = "Successfully logged in.";
    internal const string UsernameAlreadyRegistered = "Username already registered.";
    internal const string RegisterSuccessful = "Successfully registered.";
    internal const string EmailAlreadyRegistered = "Email already registered.";
    internal const string DefaultLanguageNotFound = "Default language not found.";
    internal const string WrongVerificationCode = "Invalid verification code.";
    internal const string VerificationCodeExpired = "Invalid verification code.";
    internal const string MfaRequired = "Multi-factor authentication required.";
}