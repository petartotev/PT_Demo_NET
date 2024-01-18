using System.Collections.Frozen;
using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace NET8;

// .NET8 uses C#12
internal class Program
{
    static void Main()
    {
        #region Methods for working with randomness
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

        #endregion

        #region Performance-focused types
        // https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8#performance-focused-types

        var myDictionary = new Dictionary<string, string>
        {
            { "key1", "value1" },
            { "key2", "value2" },
            { "key3", "value3" },
            { "key4", "value4" },
            { "key5", "value5" }
        };

        // "Frozen-" types don't allow any changes to keys and values once a collection created.
        // That requirement allows faster read operations (for example, TryGetValue()).
        // These types useful for collections populated on first use and then persisted for the duration of a long-lived service.

        var myFrozenDictionary = myDictionary.ToFrozenDictionary();
        var myFrozenSet = myDictionary.ToFrozenSet();

        Console.WriteLine(myFrozenDictionary.Count);
        Console.WriteLine(myFrozenSet.Count);

        #endregion

        #region Data validation

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

        #endregion

        #region Cryptography

        var stringToHash = "data to hash";
        var dataToHash = Encoding.UTF8.GetBytes(stringToHash);

        if (SHA3_256.IsSupported)
        {
            byte[] hash = SHA3_256.HashData(dataToHash);

            Console.WriteLine(hash.Length);
        }
        else
        {
            Console.WriteLine("SHA3_256 is not supported!");
        }

        #endregion

        #region Stream-based ZipFile methods

        // https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8#stream-based-zipfile-methods

        ZipFile.CreateFromDirectory(".", "C://Users/petar/test1.zip");

        #endregion

        #region Test

        var myExample = new Example();

        /*
         var methodResult = typeof(Example)
            .GetMethod("Method", BindingFlags.Instance | BindingFlags.NonPublic)!
            .Invoke(myExample, ArrayEmpty<object>());
         */

        var methodResult = Caller.GetExampleMethod(myExample);

        Console.WriteLine(methodResult);

        var fieldResult = Caller.GetExampleField(myExample);

        Console.WriteLine(fieldResult);

        Caller.GetExampleField(myExample) = "field value changed from outside";

        var fieldResultChanged = Caller.GetExampleField(myExample);

        Console.WriteLine(fieldResultChanged);

        #endregion
    }
}