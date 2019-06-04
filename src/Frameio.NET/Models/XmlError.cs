using System.Xml.Serialization;

namespace Frameio.NET.Models
{

    [XmlRoot("Error", IsNullable = false)]
    public class XmlError {

        public string Code { get; set; }

        public string Message { get; set; }
    }
}
