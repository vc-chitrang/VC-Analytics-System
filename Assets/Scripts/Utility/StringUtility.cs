using System.IO.Compression;
using System.IO;
using System.Text;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Linq;

public class StringUtility : MonoBehaviour
{
    public static byte[] CompressString(string inputString)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(inputString);

        using (MemoryStream outputStream = new MemoryStream())
        {
            using (GZipStream gzipStream = new GZipStream(outputStream, CompressionMode.Compress))
            {
                gzipStream.Write(inputBytes, 0, inputBytes.Length);
            }
            return outputStream.ToArray();
        }
    }

    public static string DecompressString(byte[] compressedBytes)
    {
        using (MemoryStream inputStream = new MemoryStream(compressedBytes))
        {
            using (GZipStream gzipStream = new GZipStream(inputStream, CompressionMode.Decompress))
            {
                using (StreamReader streamReader = new StreamReader(gzipStream, Encoding.UTF8))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }

    public static bool IsValidFileNameForDate(string value)
    {
        DateTime dateTime;
        bool isValidFileName = false;

        if (value.Length < 15)
        {
            return isValidFileName;
        }

        try
        {
            isValidFileName = DateTime.TryParseExact(value.Substring(0, 15), "ddMMyyyy_HHmmss", null, System.Globalization.DateTimeStyles.None, out dateTime);
        }
        catch (FormatException ex)
        {
            Debug.LogError("ERROR: parsing date: " + ex.Message);
        }
        return isValidFileName;
    }

    public static string GetReadableFileName(string value)
    {
        DateTime dateTime = DateTime.ParseExact(value, "ddMMyyyy_HHmmss", null);
        string readableDate = dateTime.ToString("MMMM-dd-yyyy h-mm-ss tt");
        return readableDate;
    }

    public static string GetFileNameWithoutSpace(string url)
    {
        return Path.GetFileName(url).Replace("%20", " ");
    }

    public static string OptimizeJson(string json)
    {
        StringBuilder optimizedJson = new StringBuilder();
        bool isInString = false;
        bool isWhiteSpace = false;

        foreach (char c in json)
        {
            if (c == '\"')
            {
                isInString = !isInString;
            }

            if (Char.IsWhiteSpace(c) && !isInString)
            {
                isWhiteSpace = true;
            }
            else
            {
                if (isWhiteSpace)
                {
                    optimizedJson.Append(' ');
                    isWhiteSpace = false;
                }
                optimizedJson.Append(c);
            }
        }

        return optimizedJson.ToString();
    }

    public static string ReplaceUnderScoreAndDashWithSpace(string value)
    {
        //remove "_" and "-" charachters from string
        return value.Replace("_", " ").Replace("-", " ").Trim();
    }

    public static string ReplaceSpaceWithUnderscore(string value)
    {
        return value.Replace(" ", "_");
    }

    public static string RemoveNumbers(string value)
    {
        //remove any numbers if any persent in string
        return Regex.Replace(value, @"\d", string.Empty).Trim();
    }

    public static string ConvertToTitleCase(string value)
    {
        //convert string into Title Case
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        return textInfo.ToTitleCase(value.ToLower());
    }


    public static void CopyToClipboard(string content)
    {
        GUIUtility.systemCopyBuffer = content;
    }

    public static bool IsValidateMobileNumber(string phoneNumber)
    {
        // Remove any non-digit characters from the phone number
        string cleanedNumber = Regex.Replace(phoneNumber, @"[^\d]", "");

        // Check if the cleaned number has exactly 10 digits
        return cleanedNumber.Length == 10;
    }

    public static (bool isValid, string message) IsValidateEmail(string email)
    {
        // Regular expression pattern for validating email addresses
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        // Check if the email is empty
        if (string.IsNullOrEmpty(email))
        {
            return (false, "Email cannot be empty.");
        }

        // Check if the email matches the pattern
        if (!Regex.IsMatch(email, emailPattern))
        {
            return (false, "Invalid email format. Please enter a valid email.");
        }

        // If the email is valid
        return (true, "Email is valid.");
    }

    public static (bool isValid, string status) IsValidPassword(string password)
    {
        // Minimum password length requirement
        int minPasswordLength = 8;

        // Check if password meets the length requirement
        if (password.Length < minPasswordLength)
        {
            return (false, $"Password must be at least {minPasswordLength} characters long.");
        }

        // Check for at least one uppercase letter
        if (!Regex.IsMatch(password, @"[A-Z]"))
        {
            return (false, "Password must contain at least one uppercase letter (A-Z).");
        }

        // Check for at least one lowercase letter
        if (!Regex.IsMatch(password, @"[a-z]"))
        {
            return (false, "Password must contain at least one lowercase letter (a-z).");
        }

        // Check for at least one digit
        if (!Regex.IsMatch(password, @"\d"))
        {
            return (false, "Password must contain at least one digit (0-9).");
        }

        // Check for at least one special character
        if (!Regex.IsMatch(password, @"[@$!%*?&#]"))
        {
            return (false, "Password must contain at least one special character (@$!%*?&#).");
        }

        // If all conditions are met, return success
        return (true, "Password is valid.");
    }

    public static string CleanResponse(string response)
    {
        // First, replace hyphens with spaces
        response = response.Replace("-", " ");
        response = response.Replace("_", " ");

        // Define a regex pattern to remove all non-alphabetical characters except spaces
        string pattern = @"[^a-zA-Z ]";

        // Remove all unwanted characters except spaces
        string cleanedResponse = Regex.Replace(response, pattern, "");

        // Trim leading and trailing spaces if needed
        cleanedResponse = cleanedResponse.Trim();

        // Optionally, handle multiple spaces (if needed)
        cleanedResponse = Regex.Replace(cleanedResponse, @"\s+", " ");

        return cleanedResponse;
    }

    // Method to sanitize a path segment according to Windows directory naming rules
    public static string SanitizePathSegment(string segment)
    {
        // Define invalid characters for Windows paths
        char[] invalidChars = Path.GetInvalidFileNameChars();

        // Remove invalid characters defined by Windows
        segment = string.Concat(segment.Where(c => !invalidChars.Contains(c)));

        // Remove additional special characters using regex to keep only alphanumeric, underscores, and hyphens
        segment = Regex.Replace(segment, @"[^a-zA-Z0-9_\-]", "");

        // Replace reserved names if they occur at the start or end
        string[] reservedNames = { "CON", "PRN", "AUX", "NUL", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9",
                               "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9" };

        if (reservedNames.Contains(segment.ToUpperInvariant()))
        {
            segment = "_" + segment; // Prefix with an underscore to avoid reserved names
        }

        // Trim trailing periods and spaces
        segment = segment.TrimEnd('.', ' ').TrimStart();

        return segment;
    }
}//StringCompression class end.
