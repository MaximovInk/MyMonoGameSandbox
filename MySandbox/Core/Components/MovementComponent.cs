using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace MySandbox.Core.Components
{
    [Serializable]
    public class MovementComponent : Component
    {
        public Keys Up = Keys.W;
        public Keys Down = Keys.S;
        public Keys Left = Keys.A;
        public Keys Right = Keys.D;

        public delegate void OnPositionChanged( Vector2 pos);

        public virtual event OnPositionChanged onMove;

        protected Vector2 last_position;

        public float Speed => MySandbox.speed_hack ? speed * 10 : speed;

        protected float speed = 50f;

        protected Vector2 direction = Vector2.Zero;

        public override void OnConstruct()
        {
            
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Core.Input.GetKey(Up))
            {
                direction.Y-= 1* elapsed * Speed;
            }
            if (Core.Input.GetKey(Down))
            {
                direction.Y+= 1 * elapsed * Speed;
            }
            if (Core.Input.GetKey(Left))
            {
                direction.X-= 1 * elapsed * Speed;
            }
            if (Core.Input.GetKey(Right))
            {
                direction.X+= 1* elapsed * Speed;
            }
            @object.Position += direction;


            if (@object.Position != last_position)
            {

                if (onMove != null)
                    onMove(@object.GetCentredPosition());
                last_position = @object.Position;
            }

            direction = Vector2.Zero;
        }
    }
}
