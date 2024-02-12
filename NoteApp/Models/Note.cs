namespace NoteApp.Models
{
    public class Note { 
    
            public long Id { get; set; }
            public string? Name { get; set; }
            public string? Description { get; set; }
            public DateTime? DueDate { get; set; }
            public DateTime? CompletionDate { get; set; }
        
    }
}
