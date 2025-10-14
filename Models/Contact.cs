using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Contact_Manger.Models
{
    /// <summary>
    /// Contact model - represents individual contact information
    /// 
    /// VARIABLE REFERENCE FOR TEAMMATES:
    /// - ContactId: int (Primary key, auto-generated)
    /// - Firstname: string (First name, required, max 50 chars)
    /// - Lastname: string (Last name, required, max 50 chars)
    /// - Phone: string (Phone number, required, max 20 chars)
    /// - Email: string (Email address, required, max 100 chars)
    /// - Organization: string (Organization, optional, max 100 chars)
    /// - CategoryId: int (Foreign key to Category, required, must be > 0)
    /// - DateAdded: DateTime (Auto-set when contact created)
    /// - Slug: string (Read-only, URL-friendly name like "john-doe")
    /// - Category: Category (Navigation property to related Category)
    /// </summary>
    public class Contact
    {
        // Primary Key - EF Core recognizes ContactId as primary key
        // Database auto-generates this value (identity column)
        public int ContactId { get; set; }

        // First Name - Required
        [Required(ErrorMessage = "Please enter a first name.")]
        [StringLength(50)]
        public string Firstname { get; set; } = string.Empty;

        // Last Name - Required
        [Required(ErrorMessage = "Please enter a last name.")]
        [StringLength(50)]
        public string Lastname { get; set; } = string.Empty;

        // Phone - Required with phone format validation
        [Required(ErrorMessage = "Please enter a phone number.")]
        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        // Email - Required with email format validation
        [Required(ErrorMessage = "Please enter an email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        // Organization - Optional field
        [StringLength(100)]
        public string? Organization { get; set; }

        // Foreign Key - Links to Category table
        // EF Core recognizes CategoryId as foreign key because:
        // 1. Property name matches pattern [RelatedEntity]Id
        // 2. Navigation property Category exists below
        // Range validation ensures user selects a valid category (must be > 0)
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }

        // DateAdded - Automatically set when contact is created
        // NOT included in Add/Edit forms per project specs
        public DateTime DateAdded { get; set; } = DateTime.Now;

        // Slug - Read-only property for user-friendly URLs
        // Converts "John Doe" to "john-doe"
        // Used in routing: /contact/details/1/john-doe
        public string Slug =>
            $"{Firstname?.Replace(' ', '-')}-{Lastname?.Replace(' ', '-')}".ToLower();

        // Navigation Property - Provides access to related Category
        // Allows eager loading: context.Contacts.Include(c => c.Category)
        // ValidateNever because we only validate the CategoryId (foreign key)
        [ValidateNever]
        public Category? Category { get; set; }
    }
}