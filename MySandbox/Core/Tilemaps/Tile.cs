using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;

namespace MySandbox.Core.Tilemaps
{
    public class Tile
    {
        [JsonIgnore]
        public Texture2DSheet Sprite { get; set; }
        [JsonIgnore]
        public Rectangle CurrentElement { get; protected set; }
        [JsonIgnore]
        public Tilemap Tilemap { get { return _tilemap; } set { _tilemap = value;
                if(Core.currentScene != null)
                tilemap_index = Core.currentScene.gameObjects.IndexOf(value);
            } }
        [JsonIgnore]
        private Tilemap _tilemap;
        [JsonProperty]
        protected int tilemap_index;
        [JsonProperty]
        protected int tileset_index;
        [JsonProperty]
        protected int element_index;

        public int x;
        public int y;
        public Color Color = Color.White;
        public float Scale = 1;
        public Vector2 Position
        {
            get
            {
                return new Vector2(0);
            }
        }

        [JsonConstructor]
        protected Tile(int tilemap_index , int tileset_index, int element_index)
        {
            this.tilemap_index = tilemap_index;
            this.tileset_index = tileset_index;
            this.element_index = element_index;

            try
            {
                Tilemap = Core.currentScene.gameObjects[tilemap_index] as Tilemap;
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Set tilemap [" + tilemap_index + "] + to tile failed | Exception :" + ex);
            }

            try
            {
                Sprite = ContentDefault.tilesets[tileset_index];
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Set tileset [" + tileset_index + "] + to tile failed | Exception :" + ex);
            }
            SetElement(element_index);
        }

        public Tile(int ContentDefualtIndex = 0 , int index = 0)
        {
            tileset_index = ContentDefualtIndex;
            Sprite = ContentDefault.tilesets[tileset_index];
            element_index = index;
            SetElement(element_index);
            
        }

        public void SetElement(int index)
        {
            CurrentElement = Sprite.elements[index];
        }

        public void Update()
        {
            OnUpdate();
        }

        protected virtual void OnUpdate()
        {

        }
    }
}
