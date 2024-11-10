using System.Text.RegularExpressions;

namespace Backend.Core.Domain.ValueObjects;

public class Email
{
    public string Address { get; }

    public Email(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Email address cannot be empty");

        if (!IsValidEmailAddress(address))
            throw new ArgumentException("Invalid email address format");

        Address = address;
    }

    public static bool IsValidEmailAddress(string address)
    {
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        Regex regex = new(pattern, RegexOptions.IgnoreCase);
        return regex.IsMatch(address);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Email other) return false;
        return Address == other.Address;
    }

    public override int GetHashCode()
        => Address.GetHashCode();

    public override string ToString()
        => Address;
}
