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
        public Texture2D sheet;

        public Rectangle[] elements = new Rectangle[] { };

        public string TexturePath { get; set; }

        [JsonConstructor]
        private Texture2DSheet()
        {

        }

        public Texture2DSheet(string path  , Rectangle[] elements = null)
        {
            TexturePath = path;
            sheet = Core.content.Load<Texture2D>(TexturePath);
            Init( elements);
        }

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
            Init( elements);
        }

        private void Init( Rectangle[] elements)
        {
            
            this.elements = elements;
        }
    }
}
