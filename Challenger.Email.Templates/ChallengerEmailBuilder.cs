using QuantumCore.Email.Builders;

namespace Challenger.Email.Templates
{
    public class ChallengerEmailBuilder : EmailBuilder
    {
        public override string BuildEmailSubject(string emailType)
        {
            string emailSubject = "";
            string culture = "en-US";

            if (culture == "pl")
            {
                switch (emailType)
                {
                    case "ResetPassword":
                        emailSubject = $"Challenger - Potwierdzenie zmiany hasła";
                        break;
                    case "Registration":
                        emailSubject = $"Challenger - Potwierdzenie rejestracji";
                        break;
                }
            }
            else if (culture == "en-US")
            {
                switch (emailType)
                {
                    case "ResetPassword":
                        emailSubject = $"Challenger - Password reset confirmation";
                        break;
                    case "Registration":
                        emailSubject = $"Challenger - Registration confirmation";
                        break;
                }
            }

            return emailSubject;
        }
    }
}