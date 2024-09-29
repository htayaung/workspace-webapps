using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Web.Pages.RandomPassword;

public class IndexModel : BasePageModel
{
    private const int MIN_PASSWORD_LENGTH = 1;
    private const int MAX_PASSWORD_LENGTH = 30;
    private const string LETTERS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string NUMBERS = "0123456789";
    private const string SPECIAL_CHARACTERS = " !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";

    public IndexModel(
        IUser user,
        ILogger<IndexModel> logger) : base(user, logger) { }

    [BindProperty(SupportsGet = true)]
    public InputModel Input { get; set; }

    public class InputModel
    {
        [Range(MIN_PASSWORD_LENGTH, MAX_PASSWORD_LENGTH)]
        [DisplayName("Password Length")]
        public int PasswordLength { get; set; }

        public int MinPasswordLength { get; set; }

        public int MaxPasswordLength { get; set; }

        [DisplayName("ABCabc")]
        public bool IncludeCharacters { get; set; }

        [DisplayName("123")]
        public bool IncludeNumbers { get; set; }

        [DisplayName("#$&")]
        public bool IncludeSpecialCharacters { get; set; }
    }

    public class OutputModel
    {
        public string Status { get; set; } = string.Empty;

        public string StatusClass { get; set; } = string.Empty;

        public string GeneratedPassword { get; set; }
    }

    public void OnGet()
    {
        Input.MinPasswordLength = MIN_PASSWORD_LENGTH;
        Input.MaxPasswordLength = MAX_PASSWORD_LENGTH;
        Input.PasswordLength = 15;
        Input.IncludeCharacters = true;
        Input.IncludeNumbers = true;
        Input.IncludeSpecialCharacters = true;
    }

    public IActionResult OnPostGenerate(InputModel input)
    {
        if (Input == null)
        {
            Input = new InputModel();
        }

        Input.MinPasswordLength = MIN_PASSWORD_LENGTH;
        Input.MaxPasswordLength = MAX_PASSWORD_LENGTH;
        Input.PasswordLength = input.PasswordLength;
        Input.IncludeCharacters = input.IncludeCharacters;
        Input.IncludeNumbers = input.IncludeNumbers;
        Input.IncludeSpecialCharacters = input.IncludeSpecialCharacters;

        var password = Generate();
        var status = SetStatus();
        var output = new OutputModel
        {
            GeneratedPassword = password,
            Status = status.Item1,
            StatusClass = status.Item2
        };

        return new JsonResult(output);
    }

    private void ValidateInput()
    {
        if (Input.MinPasswordLength < MIN_PASSWORD_LENGTH || Input.MinPasswordLength > MAX_PASSWORD_LENGTH)
        {
            throw new ArgumentException("Minimum password length is invalid.");
        }

        if (Input.MaxPasswordLength < MIN_PASSWORD_LENGTH || Input.MaxPasswordLength > MAX_PASSWORD_LENGTH)
        {
            throw new ArgumentException("Maximum password length is invalid.");
        }

        if (Input.MinPasswordLength > Input.MaxPasswordLength)
        {
            throw new ArgumentException("Password length is invalid.");
        }

        if (!Input.IncludeCharacters && !Input.IncludeNumbers && !Input.IncludeSpecialCharacters)
        {
            Input.IncludeCharacters = true;
        }
    }

    private string Generate()
    {
        ValidateInput();

        string chars = "";
        if (Input.IncludeCharacters)
            chars += LETTERS;

        if (Input.IncludeNumbers)
            chars += NUMBERS;

        if (Input.IncludeSpecialCharacters)
            chars += SPECIAL_CHARACTERS;

        StringBuilder sb = new StringBuilder();
        Random rnd = new Random();

        for (int i = 0; i < Input.PasswordLength; i++)
        {
            int index = rnd.Next(chars.Length);
            sb.Append(chars[index]);
        }

        return sb.ToString();
    }

    private (string, string) SetStatus()
    {
        string status, statusClass = "";
        if (Input.PasswordLength < 5)
        {
            status = "Very weak";
            statusClass = "bg-danger";
        }
        else if (Input.PasswordLength < 10)
        {
            status = "Weak";
            statusClass = "bg-warning";
        }
        else if (Input.PasswordLength < 15)
        {
            status = "Strong";
            statusClass = "bg-success";
        }
        else
        {
            status = "Very strong";
            statusClass = "bg-success";
        }

        return (status, statusClass);
    }
}
