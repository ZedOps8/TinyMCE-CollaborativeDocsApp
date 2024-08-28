using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TextEditor.Models
{
    // The Doc class represents a document entity in the application.
    public class Doc
    {
        // Primary key for the Doc entity
        public int Id { get; set; }

        // Title of the document
        public string Title { get; set; }

        // Content of the document
        public string Content { get; set; }

        // UserId represents the foreign key relationship with the IdentityUser table
        [Required] // Ensures that UserId cannot be null
        public string? UserId { get; set; }

        // Navigation property for the relationship between Doc and IdentityUser
        // The ForeignKey attribute specifies that UserId is the foreign key
        [ForeignKey("UserId")]
        public IdentityUser? User { get; set; }
    }
}
