using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;

namespace MySandbox.Core
{
    [Serializable]
    public class Texture2DSheet
    {
        [JsonIgnore]
        public Texture2D texture;

        public Rectangle[] elements = new Rectangle[] { };

        public string TexturePath { get; set; }

        [JsonConstructor]
        private Texture2DSheet()
        {

        }
        /// <summary>
        /// Load texture from path
        /// </summary>
        /// <param name="path">path</param>
        /// <param name="elements">default elements</param>
        public Texture2DSheet(string path  , Rectangle[] elements = null)
        {
            TexturePath = path;
            texture = Core.content.Load<Texture2D>(TexturePath);
            this.elements = elements;
        }
        /// <summary>
        /// Generate elements by cell
        /// </summary>
        /// <param name="el_width">Element width</param>
        /// <param name="el_height">Element height</param>
        /// <param name="total_x">Elements x</param>
        /// <param name="total_y">Elements y</param>
        /// <param name="spacing">Spacing behind elements</param>
        /// <param name="left_padding">Left padding</param>
        /// <param name="top_padding">Top padding</param>
        public void GenerateElements( int el_width, int el_height, int total_x, int total_y, int spacing = 1, int left_padding = 0, int top_padding = 0)
        {
            elements = new Rectangle[total_x * total_y];

            int total = 0;

            for (int ty = 0; ty < total_y; ty++)
            {
                for (int tx = 0; tx < total_x; tx++)
                {
                    int beginx = el_width * tx + spacing * tx + left_padding;
                    int beginy = el_height * ty + spacing * ty + top_padding;

                    elements[tx + total] = new Rectangle(beginx,beginy,el_width, el_height);
                }
                total+= total_x;
            }
        }
    }
}
