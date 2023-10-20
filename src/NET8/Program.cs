using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace NET8;

internal class Program
{
    static void Main()
    {
        // ==================== Methods for working with randomness ====================
        // https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8#methods-for-working-with-randomness

        // --------------- GetItems<T>() ---------------

        var colors = new[] { "red", "green", "blue", "cyan", "magenta", "yellow", "black", "white" };

        // Random.Shared.GetItems() creates an array populated with items chosen at random from the provided set of choices.
        var colorsSelectedRandomly = Random.Shared.GetItems<string>(choices: colors, length: 3);

        // magenta, magenta, blue
        // N.B. Can be repeated!
        foreach (var item in colorsSelectedRandomly) { Console.WriteLine(item); }

        Console.WriteLine(new string('-', 10));

        // --------------- Shuffle<T>() ---------------

        var animals = new[] { "ant", "bee", "cat", "dog", "elephant", "fox", "goat" };

        // Random.Shared.Shuffle() performs an in-place shuffle of an array.
        Random.Shared.Shuffle<string>(animals);

        foreach (var animal in animals) { Console.WriteLine(animal); }

        Console.WriteLine(new string('-', 10));

        // ==================== Data validation ====================
        var validDataValidationObject = new DataValidationObject
        {
            RangedInt = 10,
            MinMaxLengthString = "1234567890",
            ValidBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes("This is a valid string that will be converted to Base64.")),
            AllowedValuesString = "apple",
            DeniedValuesString = "car"
        };

        ValidateObject(validDataValidationObject, nameof(validDataValidationObject));

        var invalidDataValidationObject = new DataValidationObject
        {
            RangedInt = 9,
            MinMaxLengthString = "123456789",
            ValidBase64 = "invalidVGhpcyBpcyBhIHZhbGlkIHN0cmluZyB0aGF0IHdpbGwgYmUgY29udmVydGVkIHRvIEJhc2U2NC4=",
            AllowedValuesString = "car",
            DeniedValuesString = "apple"
        };

        ValidateObject(invalidDataValidationObject, nameof(invalidDataValidationObject));

        static void ValidateObject(object objectToValidate, string nameOfObject)
        {
            var validationResult = new List<ValidationResult>();

            var result = Validator.TryValidateObject(objectToValidate, new ValidationContext(objectToValidate), validationResult, true);

            Console.WriteLine($"The validation of object '{nameOfObject}' returned '{result}'.");

            if (!result && validationResult != null && validationResult.Count != 0)
            {
                foreach (var item in validationResult) { Console.WriteLine(item.ErrorMessage); }
            }
        }
    }
}