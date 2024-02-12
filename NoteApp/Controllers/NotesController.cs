using Microsoft.AspNetCore.Mvc;

namespace NoteApp.Controllers
{
    public class Note
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompletionDate { get; set; }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private static List<Note> _notes = new List<Note> { // CAUTION: Do not use that approach in production code!
            new Note { Id = 1, Name="Homework", Description = "Do some homework for M295." }
        };

        public NotesController()
        {
        }

        /// <summary>
        /// GET: api/notes/5
        /// </summary>
        /// <example>
        ///  <code>
        ///  async function getNote() {
        ///    const request = await fetch('http://localhost:4200/api/notes/1', {
        ///      headers: {
        ///        'Accept': 'application/json'
        ///        },
        ///      method: 'GET'
        ///    });
        ///    const data = await request.json();
        ///    console.log(data);
        ///  }
        ///  </code>
        /// </example>
        /// <param name="id">ID of the note to retrieve.</param>
        /// <returns>Returns the note associated to the given id.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            foreach (var note in _notes)
            {
                if (note.Id == id)
                {
                    return Ok(note);
                }
            }
            return NotFound();
        }


        /// <summary>
        /// GET: api/notes
        /// </summary>
        /// <example>
        ///  <code>
        ///  async function getNotes() {
        ///    const request = await fetch('http://localhost:4200/api/notes/', {
        ///      headers: {
        ///        'Accept': 'application/json'
        ///        },
        ///      method: 'GET'
        ///    });
        ///    const data = await request.json();
        ///    console.log(data);
        ///  }
        ///  </code>
        /// </example>
        /// <returns>Returns all notes in the database.</returns>
        [HttpGet()]
        public IActionResult GetAll()
        {
            return Ok(_notes);
        }


        /// <summary>
        /// POST: api/notes
        /// </summary>
        /// <example>
        ///  <code>
        ///  async function addNote() {
        ///    const request = await fetch('http://localhost:4200/api/notes/', {
        ///      headers: {
        ///        'Accept': 'application/json',
        ///        'Content-Type': 'application/json'
        ///        },
        ///      method: 'POST',
        ///      body: JSON.stringify({ todo: 'new todo', dueDate: '2023-08-19T18:31:35.294Z', completionDate: '2023-08-19T18:31:35.294Z' }),
        ///    });
        ///    const data = await request.json();
        ///    console.log(data);
        ///  }
        ///  </code>
        /// </example>
        /// <param name="note">Note data to be stored.</param>
        /// <returns>Returns the created note.</returns>
        [HttpPost()]
        public IActionResult Post(Note note)
        {
            note.Id = new Random().Next(10000000);
            _notes.Add(note);
            return Ok(note);
        }

        /// <summary>
        /// PUT: api/notes/5
        /// </summary>
        /// <example>
        ///  <code>
        ///  async function updateNote() {
        ///    const request = await fetch('http://localhost:4200/api/notes/1', {
        ///      headers: {
        ///        'Accept': 'application/json',
        ///        'Content-Type': 'application/json'
        ///        },
        ///      method: 'PUT',
        ///      body: JSON.stringify({ id: 1, todo: 'overwrite todo', dueDate: '2023-08-20T18:31:35.294Z', completionDate: '2023-08-20T18:31:35.294Z' }),
        ///    });
        ///    const data = request.status;
        ///    console.log(data);
        ///  }
        ///  </code>
        /// </example>
        /// <param name="id">ID of the note to be edited.</param>
        /// <param name="note">Note data to be stored.</param>
        /// <returns>Nothing, see HTTP status code.</returns>
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] long id, [FromBody] Note note)
        {
            foreach (var storedNote in _notes)
            {
                if (storedNote.Id == id)
                {
                    storedNote.CompletionDate = note.CompletionDate;
                    storedNote.DueDate = note.DueDate;
                    storedNote.Name = note.Name;
                    storedNote.Description = note.Description;
                    return Ok();
                }
            }
            return NotFound();
        }


        /// <summary>
        /// DELETE: api/notes/5
        /// </summary>
        /// <example>
        ///  <code>
        ///  async function deleteNote() {
        ///    const request = await fetch('http://localhost:4200/api/notes/1', {
        ///      headers: {
        ///        'Accept': 'application/json',
        ///        'Content-Type': 'application/json'
        ///        },
        ///      method: 'DELETE'
        ///    });
        ///    const data = request.status;
        ///    console.log(data);
        ///  }
        ///  </code>
        /// </example>
        /// <param name="id">ID of the note to be deleted.</param>
        /// <returns>Nothing, see HTTP status code.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            Note toDelete = null;
            
            foreach (var note in _notes)
            {
                if (note.Id == id)
                {
                    toDelete = note;
                }
            }

            if (toDelete != null)
            {
                _notes.Remove(toDelete);
                return Ok();
            }
            return NotFound();
        }
    }
}