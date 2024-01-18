using System.ComponentModel.DataAnnotations;

namespace NET8;

// ==================== Data validation ====================
// https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8#data-validation
public class DataValidationObject
{
    // RangeAttribute.MinimumIsExclusive & RangeAttribute.MaximumIsExclusive
    // Specifies whether bounds are included in the allowable range.
    [Range(10, 20, MinimumIsExclusive = false, MaximumIsExclusive = false)]
    public int RangedInt { get; set; }

    // System.ComponentModel.DataAnnotations.LengthAttribute
    // Specifies both lower and upper bounds for strings or collections.
    // For example, [Length(10, 20)] requires at least 10 elements and at most 20 elements in a collection.
    [Length(10, 20)]
    public string MinMaxLengthString { get; set; }

    // System.ComponentModel.DataAnnotations.Base64StringAttribute
    // Validates that a string is a valid Base64 representation.
    [Base64String]
    public string ValidBase64 { get; set; }

    // System.ComponentModel.DataAnnotations.AllowedValuesAttribute
    // Specify allow lists and deny lists, respectively. For example, [AllowedValues("apple", "banana", "mango")].
    [AllowedValues("apple", "banana", "mango")]
    public string AllowedValuesString { get; set; }

    // System.ComponentModel.DataAnnotations.DeniedValuesAttribute
    // Specify allow lists and deny lists, respectively. For example, [AllowedValues("apple", "banana", "mango")]. 
    [DeniedValues("apple", "banana", "mango")]
    public string DeniedValuesString { get; set; }
}
