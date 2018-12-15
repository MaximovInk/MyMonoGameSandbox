using Microsoft.Xna.Framework;
using MySandbox.Core.Components;
using Newtonsoft.Json;


namespace MySandbox.Core
{
    public class Player : GameObject
    {
        public AnimationComponent animation;

        private Vector2 last_position = Vector2.Zero;
        /// <summary>
        /// Create player
        /// </summary>
        /// <param name="atlas">Texture atlas</param>
        /// <param name="position">Start position</param>
        /// <param name="rotation">Rotation</param>
        /// <param name="update">Update</param>
        public Player(Texture2DSheet atlas, Vector2 position, float rotation = 0, bool update = true) : base(atlas, position, rotation,update)
        {

        }
        [JsonConstructor]
        private Player()
        {

        }
        /// <summary>
        /// Update animation component
        /// </summary>
        protected override void OnUpdate()
        {
            if (UpdateThis)
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
}
