using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MySandbox.Core.Tilemaps
{
    public class LayeredTilemap : GameObject
    {
        public List<Tilemap> layers { get; protected set; } = new List<Tilemap>();
        [JsonIgnore]
        public byte CurrentY_chunk {
            get {
                return layers.Count > 0 ? layers[0].CurrentY_chunk : (byte)0;
            }
            set {
                for (int i = 0; i < layers.Count; i++)
                {
                    layers[i].CurrentY_chunk = value;
                }
            }
        }
        [JsonIgnore]
        public byte CurrentX_chunk
        {
            get
            {
                return layers.Count > 0 ? layers[0].CurrentX_chunk : (byte)0;
            }
            set
            {
                for (int i = 0; i < layers.Count; i++)
                {
                    layers[i].CurrentX_chunk = value;
                }
            }
        }

        public TilemapDrawType drawType;

        public byte TileX => layers.Count > 0 ? layers[0].TileX : (byte)0;
        public byte TileY => layers.Count > 0 ? layers[0].TileY : (byte)0;
        [JsonIgnore]
        public override List<GameObject> Childrens { get => base.Childrens; protected set => base.Childrens = value; }

        public int[] physics_layers = new int[] { 1 };

        [JsonConstructor]
        protected LayeredTilemap(List<Tilemap> layers)
        {
            MySandbox.current_tilemap = this;
            MySandbox.UpdateTilemapEvents();
            for (int i = 0; i < layers.Count; i++)
            {
                AddLayer(layers[i]);
            }
        }

        public LayeredTilemap(List<Tilemap> layers, bool update = true)
        {
            this.update = update;

            for (int i = 0; i < layers.Count; i++)
            {
                AddLayer(layers[i]);
            }
        }

        public void AddLayer(Tilemap layer)
        {
            layers.Add(layer);
            Childrens.Add(layer);
        }

        public void RemoveLayer(Tilemap layer)
        {
            layers.Remove(layer);
            Childrens.Remove(layer);
        }

        public void UpdateCurrChunk(Vector2 pos)
        {
            for (int i = 0; i < layers.Count; i++)
            {
                layers[i].UpdateCurrChunk(pos);
            }
        }

        public override void Draw()
        {
            if (drawType == TilemapDrawType.BackToFront)
            {
                for (int i = layers.Count - 1; i >= 0; i--)
                {
                    layers[i].Draw();
                }
            }
            else
            {
                for (int i = 0; i < layers.Count; i++)
                {
                    layers[i].Draw();
                }
            }
        }

        public bool BodyColliding(Vector2 pos, Vector2 half_szie)
        {
            for (int i = 0; i < physics_layers.Length; i++)
            {
                if (layers.Count > physics_layers[i])
                {
                    
                    return layers[physics_layers[i]].CheckCollision(pos, half_szie);
                }
            }
            return false;
        }
    }

    public enum TilemapDrawType
    {
        FrontToBack,
        BackToFront
    }
}
