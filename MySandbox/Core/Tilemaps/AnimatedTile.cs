using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace MySandbox.Core.Tilemaps
{
    class AnimatedTile : Tile
    {
        public int Wait = 12;
        public Rectangle[] elements;
        public int Step = 1;
        public bool IsPlay { get; protected set; }

        public bool Loop { get; set; }

        private int frame = 0;
        private int current;

        public AnimatedTile(int[] indexes, int ContentDefualtIndex = 0)
        {
            tileset_index = ContentDefualtIndex;
            Sprite = ContentDefault.tilesets[tileset_index];
            if (indexes.Length < 0)
                return;

            elements = new Rectangle[indexes.Length];

            for (int i = 0; i < indexes.Length; i++)
            {
                elements[i] = Sprite.elements[indexes[i]];
            }

            SetElement(indexes[0]);
            Start(true);
        }
        [JsonConstructor]
        protected AnimatedTile(int tilemap_index, int tileset_index, int element_index) : base(tilemap_index, tileset_index, element_index)
        {
            Start(true);
        }
        /// <summary>
        /// Start animation
        /// </summary>
        /// <param name="loop">Is loop</param>
        public void Start(bool loop)
        {
            Loop = loop;
            IsPlay = true;
        }
        /// <summary>
        /// Stop animation
        /// </summary>
        public void Stop()
        {
            IsPlay = false;
            frame = Wait + 1;
        }
        /// <summary>
        /// On update
        /// </summary>
        protected override void OnUpdate()
        {
            if (!IsPlay)
                return;

            frame++;

            if (frame > Wait)
            {
                current += Step;
                if (current >= elements.Length)
                {
                    current = 0;
                    if (!Loop)
                        Stop();
                }

                if (current < 0)
                {
                    current = elements.Length - 1;
                    if (!Loop)
                        Stop();
                }

                CurrentElement = elements[current];
                frame = 0;
            }
        }
    }
}
