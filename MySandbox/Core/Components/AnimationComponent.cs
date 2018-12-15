using System;
using Microsoft.Xna.Framework;

namespace MySandbox.Core.Components
{
    [Serializable]
    public class AnimationComponent : Component
    {
        public int Wait = 12;

        public int Start_frame = 1;
        public int End_frame = 2;

        private int frame = 0;

        private int current;
        public bool IsPlay { get; private set; }

        public int Step = 1;

        public bool Loop;

        public override void OnConstruct()
        {
            frame = Wait + 1;
            Start();
        }

        public void Start()
        {
            IsPlay = true;
        }

        public void Stop()
        {
            IsPlay = false;
            frame = Wait+1;
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            if (!IsPlay)
                return;

            frame++;

            if (frame > Wait)
            {
                current+= Step;
                if (current > End_frame)
                {
                    current = Start_frame;
                    if (!Loop)
                        Stop();
                }

                if (current < Start_frame)
                {
                    current = End_frame;
                    if (!Loop)
                        Stop();
                }
                    
                @object.SetElement(current);
                frame = 0;
            }
        }

        public void PlayOneShot()
        {
            Loop = false;
            Start();
        }
    }
}
