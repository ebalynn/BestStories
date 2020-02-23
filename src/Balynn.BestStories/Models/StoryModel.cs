using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Balynn.BestStories.Models
{
    public class StoryModel
    {
        public string By { get; set; }
        
        public int Descendants { get; set; }

        [Key] 
        public int Id { get; set; }
        
        public List<int> Kids { get; set; }

        // Since we are relying on this field for sorting we need to ensure that it's present 
        [Required] 
        public int Score { get; set; }
        
        public int Time { get; set; }
        
        public string Title { get; set; }
        
        public string Type { get; set; }

        [Url]
        public string Url { get; set; }
    }
}
