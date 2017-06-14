using System.IO;

namespace ForumBll.Interface.Models
{
    public class BllImage
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public int Size { get; set; }

        public Stream Content { get; set; }
    }
}
