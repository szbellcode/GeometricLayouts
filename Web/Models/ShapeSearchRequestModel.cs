using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Web.Models
{
    public class ShapeSearchRequestModel : IValidatableObject
    {
        [Required]
        public string SearchBy { get; set; }
        
        public string Name { get; set; }

        public string Vertex1 { get; set; }
        public string Vertex2 { get; set; }
        public string Vertex3 { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (SearchBy.Equals("name", System.StringComparison.InvariantCultureIgnoreCase))
            {
                if (string.IsNullOrEmpty(Name))
                {
                    yield return new ValidationResult("Please specify a search Name.", new[] { nameof(Name) });
                }
            }
            else if (SearchBy.Equals("coordinates", System.StringComparison.InvariantCultureIgnoreCase))
            {
                string InvalidVertexMessage = "Please specify vertext coordinates.";
                if (!IsVertexValid(Vertex1))
                    yield return new ValidationResult(InvalidVertexMessage, new[] { nameof(Vertex1) });
                if (!IsVertexValid(Vertex2))
                    yield return new ValidationResult(InvalidVertexMessage, new[] { nameof(Vertex2) });
                if (!IsVertexValid(Vertex3))
                    yield return new ValidationResult(InvalidVertexMessage, new[] { nameof(Vertex3) });
            }
            else {
                yield return new ValidationResult("Please specify a valid search type.", new[] { nameof(SearchBy) });
            }
        }

        private bool IsVertexValid(string vertex)
        {
            if (string.IsNullOrWhiteSpace(vertex))
            {
                return false;
            }

            var regEx = new Regex(@"^\d+,\d+$");
            return regEx.IsMatch(vertex);
        }
    }
}
