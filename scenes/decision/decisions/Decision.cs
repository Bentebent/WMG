using System;
using Godot;

namespace wmg
{
    public partial class Decision : Resource
    {
        [Export]
        public string CardTemplatePath { get; set; }

        [Export]
        public string ArtPath { get; set; }

        [Export]
        public string Category { get; set; }

        [Export]
        public string Header { get; set; }

        [Export]
        public string Description { get; set; }
    }
}
