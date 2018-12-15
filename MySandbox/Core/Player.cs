using Microsoft.Xna.Framework;
using MySandbox.Core.Components;
using Newtonsoft.Json;


namespace MySandbox.Core
{
    public class Player : GameObject
    {
        public MovementComponent movement;
        public AnimationComponent animation;

        private Vector2 last_position = Vector2.Zero;

        public Player(Texture2DSheet atlas, Vector2 position, float rotation = 0, bool update = false) : base(atlas, position, rotation,update)
        {

        }
        [JsonConstructor]
        private Player()
        {

        }

        protected override void OnUpdate()
        {
            if (Position != last_position)
            {
                FlipX = !(Position.X - last_position.X > 0);
                last_position = Position;

                if (!animation.IsPlay)
                    animation.PlayOneShot();
            }
            else if (animation.IsPlay)
            {
                animation.Stop();
            }
            else
            {
                SetElement(0);
            }
        }
    }
}
