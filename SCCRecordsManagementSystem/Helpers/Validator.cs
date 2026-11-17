using System.Net.Mail;
using System.Text.RegularExpressions;

namespace SCCRecordsManagementSystem
{
    // Reusable input checks. Each method returns true when the text is valid.
    public static class Validator
    {
        public static bool IsEmpty(string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        // Letters, spaces, dots, hyphens and apostrophes (for example "Dela Cruz", "O'Neil", "Jr.").
        public static bool IsValidName(string text)
        {
            return Regex.IsMatch(text.Trim(), @"^[\p{L}][\p{L} .'\-]*$");
        }

        // Letters, numbers and hyphens, 3 to 20 characters (for example "2026-001").
        public static bool IsValidStudentNumber(string text)
        {
            return Regex.IsMatch(text.Trim(), @"^[A-Za-z0-9\-]{3,20}$");
        }

        public static bool IsValidEmail(string text)
        {
            try
            {
                MailAddress address = new MailAddress(text.Trim());
                return address.Address == text.Trim() && text.Contains(".");
            }
            catch
            {
                return false;
            }
        }

        // Digits only (an optional + at the start), 7 to 15 digits. Spaces and hyphens are allowed.
        public static bool IsValidPhone(string text)
        {
            string cleaned = text.Trim().Replace(" ", "").Replace("-", "");
            return Regex.IsMatch(cleaned, @"^\+?[0-9]{7,15}$");
        }

        // Letters, numbers, dot and underscore, 4 to 30 characters.
        public static bool IsValidUsername(string text)
        {
            return Regex.IsMatch(text.Trim(), @"^[A-Za-z0-9._]{4,30}$");
        }

        public static bool IsValidPassword(string text)
        {
            return text != null && text.Length >= 6;
        }
    }
}
