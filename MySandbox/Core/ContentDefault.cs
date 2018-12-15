using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MySandbox.Core.Tilemaps;
using System;

namespace MySandbox.Core
{
    class ContentDefault
    {
        public static Tile[] tiles;
        public static Texture2DSheet ui_sheet;
        public static SpriteFont font;
        public static Texture2DSheet player_texture;
        public static Texture2DSheet empty;

        public static Texture2DSheet[] tilesets;

        public static void LoadContent()
        {
            player_texture = new Texture2DSheet("Player");
            player_texture.GenerateElements(16, 26, 3, 1);

            font = Core.content.Load<SpriteFont>("FFF");

            tilesets = new Texture2DSheet[3];
            tilesets[0] = new Texture2DSheet("Tilemap");
            tilesets[0].GenerateElements(16, 16, 6, 2,2,1,1);
            tilesets[1] = new Texture2DSheet("WaterTileAnimated");
            tilesets[1].GenerateElements(16, 16, 3, 1, 2, 1, 1);
            tilesets[2] = new Texture2DSheet("DarkWaterAnimated");
            tilesets[2].GenerateElements(16, 16, 3, 1, 2, 1, 1);

            tiles = new Tile[] {
                new Tile(0,0),
                new Tile(0,1),
                new Tile(0,2),
                new Tile(0,3),
                new AnimatedTile(new int[]{0,1,2 }, 1),
                new AnimatedTile(new int[]{ 0,1,2,1},2),
                new Tile(0,6),
                new Tile(0,7),
                new Tile(0,8),
                new Tile(0,9),
                new Tile(0,10),
                new Tile(0,11)
            };


            ui_sheet = new Texture2DSheet("ui", new Rectangle[] { new Rectangle(0, 0, 15, 15), new Rectangle(15, 0, 15, 15), new Rectangle(30, 0, 15, 15) , new Rectangle(45,0,2,6), new Rectangle(47,0,2,2) });

            empty = new Texture2DSheet("Empty", new Rectangle[] { new Rectangle() });
        }
    }
}
