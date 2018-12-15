using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MySandbox.Core.Components
{
    [Serializable]
    class PhysicsMovementComponent : Component
    {
        public Keys Up = Keys.W;
        public Keys Down = Keys.S;
        public Keys Left = Keys.A;
        public Keys Right = Keys.D;

        public delegate void OnPositionChanged(Vector2 pos);

        public virtual event OnPositionChanged onMove;

        protected Vector2 last_position;

        public float Speed => MySandbox.speed_hack ? speed * 10 : speed;

        protected float speed = 50f;

        protected Vector2 direction = Vector2.Zero;

        protected Vector2 size;

        public override void OnConstruct()
        {
            UpdateSize();
        }

        public void UpdateSize()
        {
            size =  new Vector2( @object.Bounds.X, @object.Bounds.Y);
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (MySandbox.current_tilemap.BodyColliding(@object.Position, size))
            {
                direction.X -= 1 * elapsed * speed;
                direction.Y -= 1 * elapsed * speed;
            }

            if (Core.Input.IsKey(Up))
            {
                if (!MySandbox.current_tilemap.BodyColliding(new Vector2(@object.Position.X, @object.Position.Y - 1 * elapsed * Speed),size))
                        direction.Y -= 1 * elapsed * Speed;
            }
            if (Core.Input.IsKey(Down))
            {
                if (!MySandbox.current_tilemap.BodyColliding(new Vector2(@object.Position.X, @object.Position.Y + 1 * elapsed * Speed),size))
                    direction.Y += 1 * elapsed * Speed;
            }
            if (Core.Input.IsKey(Left))
            {
                if (!MySandbox.current_tilemap.BodyColliding(new Vector2(@object.Position.X - 1 * elapsed * Speed , @object.Position.Y),size))
                    direction.X -= 1 * elapsed * Speed;
            }
            if (Core.Input.IsKey(Right))
            {
                if (!MySandbox.current_tilemap.BodyColliding(new Vector2(@object.Position.X + 1 * elapsed * Speed , @object.Position.Y),size))
                    direction.X += 1 * elapsed * Speed;
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
