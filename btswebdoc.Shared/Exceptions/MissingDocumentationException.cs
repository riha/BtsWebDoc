using System;

namespace btswebdoc.Shared.Exceptions
{
    public class MissingDocumentationException : Exception
    {
        public string Path { get; set; }
        public MissingDocumentationException() { }
        public MissingDocumentationException(string message) : base(message) { }
    }
}
