using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MySandbox.Core;
using MySandbox.Core.Components;
using MySandbox.Core.Debug;
using MySandbox.Core.Tilemaps;
using System.Collections.Generic;
using MySandbox.Core.UI;
using System;

namespace MySandbox
{
    /// <summary>
    /// Main game class
    /// </summary>
    class MySandbox : Core.Core
    {
        public int current_tool { get; set; }  
        private int current_tile;               
        private Tile tile;                      
        private UIButton change_tile_button;    
        public static GameTime time;

        public static LayeredTilemap current_tilemap;

        public static bool speed_hack { get; private set; }

        private static PhysicsMovementComponent control;

        protected override void Initialize()
        {
            base.Initialize();

            Camera.main = new Camera(GraphicsDevice.Viewport);

            Player = new Player(ContentDefault.player_texture, Vector2.Zero);
            Player.UpdateThis = true;
            control = new PhysicsMovementComponent();

            Player.animation = Player.AddComponent(new AnimationComponent()) as AnimationComponent;
            Player.AddComponent(control);

            UIManager.elements.Add(new FpsCounterText(ContentDefault.font, new Vector2(0, 1)));

            var pickaxe_button = new UIButton(ContentDefault.ui_sheet, new Vector2(0, 10 * 3), 0, 3, 3);
            pickaxe_button.onMouseRelease += () => { current_tool = 0; };

            var building_button = new UIButton(ContentDefault.ui_sheet, new Vector2(0, 26 * 3), 1, 3, 3);
            building_button.onMouseRelease += () => { current_tool = 1; };

            change_tile_button = new UIButton(ContentDefault.tilesets[0], new Vector2(0, 42 * 3), 2, 3, 3);
            change_tile_button.onMouseRelease += () => { current_tile++; TileChanged(); };

            UIManager.elements.Add(pickaxe_button);
            UIManager.elements.Add(building_button);
            UIManager.elements.Add(change_tile_button);

            UIManager.OnMove(Vector2.Zero);

            building_button.Selectable = true;
            pickaxe_button.Selectable = true;

            pickaxe_button.Select();

            building_button.onSelect += pickaxe_button.Deselect;
            pickaxe_button.onSelect += building_button.Deselect;

            control.onMove +=  Camera.main.SetPosition;

            Camera.main.SetPosition(Player.GetCentredPosition());
            Camera.main.onMove += UIManager.OnMove;
            Camera.main.onZoom += () => { UIManager.OnMove(Camera.main.Position); };

            TileChanged();
            CurrentScene = new Scene();
            CurrentScene.gameObjects.Add(current_tilemap);
            UpdateTilemapEvents();
        }

        private void TileChanged()
        {
            if (current_tile > 11)
                current_tile = 0;

            change_tile_button.SetElement(current_tile);

            tile = ContentDefault.tiles[current_tile];
            
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            Console.WriteLine("Creating tilemap...");
            Tilemap layer0 = new Tilemap(500, 500, Vector2.Zero);
            Tilemap layer1 = new Tilemap(500, 500, Vector2.Zero);

            current_tilemap = new LayeredTilemap(new List<Tilemap>() { layer0, layer1 });

            for (int x = 0; x < current_tilemap.layers[0].structure.GetUpperBound(0); x++)
            {
                for (int y = 0; y < current_tilemap.layers[0].structure.GetUpperBound(1); y++)
                {
                    current_tilemap.layers[0].SetTile(ContentDefault.tiles[0], x, y);
                }
            }

        }

        public static void UpdateTilemapEvents()
        {
            control.onMove += current_tilemap.UpdateCurrChunk;
            
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            time = gameTime;
            Camera.main.UpdateCamera(GraphicsDevice.Viewport);
            float elapsed = gameTime.ElapsedGameTime.Milliseconds / 16;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Input.GetKeyDown(Keys.Escape))
                Exit();

            if (Input.GetKeyDown(Keys.F12))
            {
                graphics.ToggleFullScreen();
            }

            if (Input.GetKeyDown(Keys.F1))
            {
                speed_hack = !speed_hack;
            }

            if (Input.GetKeyDown(Keys.C))
                CurrentScene.Save();
            if (Input.GetKeyDown(Keys.R))
                CurrentScene.Load();


            if (Input.GetMouseButton(0) && current_tilemap != null && !UIManager.IsOverUI())
            {
                Vector2 m_p = Camera.main.GetWorldPosition(Input.MousePosition);
                current_tilemap.layers[1].SetTile(current_tool == 1 ? tile : null, (int)m_p.X/ current_tilemap.TileX, (int)m_p.Y/ current_tilemap.TileY);
            }
        }
       
    }
}
