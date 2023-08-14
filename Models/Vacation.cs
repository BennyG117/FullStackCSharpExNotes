#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

namespace FullStackCSharpExNotes.Models;

public class Vacation
{
    //*KEY*
    [Key]
    // VacationId =========================
    public int VacationId {get; set;}



//! UPDATE EVERYTHIING BELOW: ===============================
//! UPDATE EVERYTHIING BELOW: ===============================


// Destination, Description, ImageUrl, Summer, Spring, Fall, Winter



    // Destination ========================= 
    [Required]
    [MinLength(3, ErrorMessage = "Must be at least 3 characters long")]
    public string Destination {get; set;}



    // Description ========================= 
    [Required]
    [MinLength(10, ErrorMessage = "Must be at least 10 characters long")]
    [MaxLength(50, ErrorMessage = "No longer than 50 characters long")]
    public string Description {get; set;}



    // ImageUrl ========================= 
    [Required]
    public string ImageUrl {get; set;}
    


    // Summer ======================== 
    public bool Summer {get; set;} = false;


    // Spring ======================== 
    public bool Spring {get; set;} = false;


    // Fall ======================== 
    public bool Fall {get; set;} = false;


    // Winter ======================== 
    public bool Winter {get; set;} = false;



    // CreatedAt ======================== 
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    


    // UpdatedAt ======================== 
    public DateTime UpdatedAt { get; set; } = DateTime.Now;



    // foreign key  - OUR ONE TO MANY*============================
    public int UserId {get; set;}


    public User? Creator {get; set;}


    //! TBD - adding many to many - user to vacations linking

    // public List<UserPostLike> PostLikes {get; set;} = new List<UserPostLike>();

}
