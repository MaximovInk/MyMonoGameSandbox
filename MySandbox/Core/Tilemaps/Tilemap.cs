using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MySandbox.Core.Tilemaps
{
    public class Tilemap : GameObject
    {
        public short[,] structure;

        public List<Tile> tiles = new List<Tile>();

        public byte TileX = 16;

        public byte TileY = 16;
        [JsonIgnore]
        public byte CurrentX_chunk { get { return c_ch_x; } set { c_ch_x = Math.Clamp(value, (byte)0, (byte)(structure.GetUpperBound(0) / max_chunk_size)); } }
        [JsonIgnore]
        public byte CurrentY_chunk { get { return c_ch_y; } set { c_ch_y = Math.Clamp(value, (byte)0, (byte)(structure.GetUpperBound(1)/ max_chunk_size)); } }
        private byte c_ch_x = 0;
        private byte c_ch_y = 0;

        public byte max_chunk_size = 100;

        public byte smooth = 20;
        [JsonIgnore]
        protected Vector2 one_chunk_size => new Vector2(max_chunk_size, max_chunk_size);

        public Tilemap(short width, short height, Vector2 position, float rotation = 0, bool update = false) : base(ContentDefault.empty, position, rotation, update)
        {
            structure = new short[width, height];
            Clear();
        }
        [JsonConstructor]
        protected Tilemap(short[,] structure, List<Tile> tiles)
        {
            this.structure = structure;
            this.tiles = tiles;
            Console.WriteLine(tiles.Count);
            Init();
            UpdateCurrChunk(Core.Player.Position);
        }

        public void UpdateCurrChunk(Vector2 pos)
        {
            CurrentX_chunk = (byte)(pos.X / max_chunk_size / TileX);
            CurrentY_chunk = (byte)(pos.Y / max_chunk_size / TileY);
        }

        private void Init()
        {
            for (int x = 0; x < structure.GetUpperBound(0); x++)
            {
                for (int y = 0; y < structure.GetUpperBound(1); y++)
                {
                    if(structure[x, y] != -1)
                        SetTile(tiles[structure[x,y]], x, y);
                }
            }
        }

        public void Clear()
        {
            for (int x = 0; x < structure.GetUpperBound(0); x++)
            {
                for (int y = 0; y < structure.GetUpperBound(1); y++)
                {
                    structure[x, y] = -1;
                }
            }
        }

        public void SetTile(Tile tile, int x ,int y)
        {
            if (tile == null)
            {
                x = Math.Clamp(x, 0, structure.GetUpperBound(0));
                y = Math.Clamp(y, 0, structure.GetUpperBound(1));

                structure[x, y] = -1;

                return;
            }

            for (int i = 0; i < tiles.Count; i++)
            {
                if (tiles[i] == tile)
                    break;

                if (i == tiles.Count - 1)
                {
                    tiles.Add(tile);
                }
            }
            if (tiles.Count == 0)
            {
                tiles.Add(tile);
            }

            if (x > 0 && x < structure.GetUpperBound(0) && y > 0 && y < structure.GetUpperBound(1))
            {
                tile.Tilemap = this;

                structure[x, y] = (short)tiles.IndexOf(tile);
                
            }

        }

        protected override void OnUpdate()
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                tiles[i].Update();
            }
        }

        public override void Draw()
        {
            int min_x = CurrentX_chunk * (int)one_chunk_size.X ;
            int min_y = CurrentY_chunk * (int)one_chunk_size.Y ;
            
            int max_x = Math.Clamp(min_x + (int)one_chunk_size.X + smooth ,0, structure.GetUpperBound(0));
            int max_y = Math.Clamp(min_y + (int)one_chunk_size.Y + smooth,0, structure.GetUpperBound(0));

            for (int x = Math.Clamp(min_x - smooth,0,structure.GetUpperBound(0)); x < max_x; x++)
            {
                for (int y = Math.Clamp(min_y-smooth, 0, structure.GetUpperBound(1)); y < max_y; y++)
                {
                    if (structure[x, y] != -1)
                    {
                        Texture2DSheet sheet = tiles[structure[x, y]].Sprite;
                        Rectangle element = tiles[structure[x, y]].CurrentElement;

                        Core.spriteBatch.Draw(sheet.sheet, new Vector2(x * TileX, y * TileY),element, Color.White, 0 , Vector2.Zero, 1, SpriteEffects.None, 0);

                    }
                }
            }
        }

        public bool CheckCollision(Vector2 pos , Vector2 size)
        {
            bool colliding = false;

            int tile_x_size = (int) size.X/ TileX;
            int tile_y_size = (int)System.Math.Ceiling( size.Y/ TileY);

            Vector2 bounds = new Vector2(structure.GetUpperBound(0), structure.GetUpperBound(1));

            if (((pos.X + size.X )/ TileX) < bounds.X && (int)((pos.X + size.X) / TileX) >= 0 && ((pos.Y + size.Y) / TileY) < bounds.Y && (int)((pos.Y + size.Y) / TileY) >= 0)
            {
                for (int x = 0; x <= tile_x_size; x++)
                {
                    for (int y = 1; y <= tile_y_size+1; y++)
                    {
                        
                        if((int)((pos.X / TileX) + x) > 0 && (int)(pos.Y + (size.Y / y)) > 0)
                        {
                            colliding = colliding == true ? true : (structure[(int)((pos.X) / TileX + x), (int)((pos.Y + size.Y/y) / TileY)] != -1);
                        }

                        if ((int)(pos.X / TileX) > 0 && (int)((pos.Y + (size.Y / y)) / TileY) > 0)
                        {
                            colliding = colliding == true ? true : (structure[(int)(pos.X / TileX), (int)((pos.Y + size.Y/y) / TileY)] != -1);
                        }

                        if ((int)((pos.X / TileX) + x) > 0 && (int)(pos.Y / TileY) > 0)
                        {
                            colliding = colliding == true ? true : (structure[(int)((pos.X) / TileX + x) , (int)(pos.Y / TileY)] != -1);
                        }
                    }
                }

                
            }


            return colliding;
        }
    }
}
