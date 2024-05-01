using System;
using Godot;

namespace wmg
{
    public partial class DecisionCard : Node
    {
        [Export]
        private TextureRect Art = null;

        [Export]
        private Label CategoryLabel = null;

        [Export]
        private Label HeaderLabel = null;

        [Export]
        private Label DescriptionLabel = null;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready() { }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta) { }

        public void SetDecision(Decision decision)
        {
            GD.Print("Setting decision: " + decision.Header);

            CategoryLabel.Text = decision.Category;
            HeaderLabel.Text = decision.Header;
            DescriptionLabel.Text = decision.Description;

            var artTexture = GD.Load<Texture2D>(decision.ArtPath);
            Art.Texture = artTexture;
        }
    }
}
