using System.IO;

namespace ForumDal.Interface.Models
{
    public class DalImage : DalEntity
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public int Size { get; set; }

        public Stream Content { get; set; }
    }
}
