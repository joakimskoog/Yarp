using System;

namespace Yarp.Plugins.Api
{
    public class YarpPluginMetadata
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Author { get; private set; }
        public string Description { get; private set; }
        public string Version { get; private set; }
        public string Category { get; private set; }
        public string Website { get; private set; }

        public YarpPluginMetadata(string id, string name, string author, string description, string version, string category, string website = "")
        {
            if (id == null) throw new ArgumentNullException("id");
            if (name == null) throw new ArgumentNullException("name");
            if (author == null) throw new ArgumentNullException("author");
            if (description == null) throw new ArgumentNullException("description");
            if (category == null) throw new ArgumentNullException("category");
            if (website == null) throw new ArgumentNullException("website");
            Id = id;
            Name = name;
            Author = author;
            Description = description;
            Version = version;
            Category = category;
            Website = website;
        }
    }
}