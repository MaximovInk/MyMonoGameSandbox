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

        /// <summary>
        /// Layers for collision detection
        /// </summary>
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
        /// <summary>
        /// Construct layered tilemap
        /// </summary>
        /// <param name="layers">List of layers</param>
        /// <param name="update">Update</param>
        public LayeredTilemap(List<Tilemap> layers, bool update = true)
        {
            this.UpdateThis = update;

            for (int i = 0; i < layers.Count; i++)
            {
                AddLayer(layers[i]);
            }
        }
        /// <summary>
        /// Add layer
        /// </summary>
        /// <param name="layer">Layer</param>
        public void AddLayer(Tilemap layer)
        {
            layers.Add(layer);
            Childrens.Add(layer);
        }
        /// <summary>
        /// Remove layer
        /// </summary>
        /// <param name="layer">Layer</param>
        public void RemoveLayer(Tilemap layer)
        {
            layers.Remove(layer);
            Childrens.Remove(layer);
        }
        /// <summary>
        /// Update current chunk
        /// </summary>
        /// <param name="pos">Player position</param>
        public void UpdateCurrChunk(Vector2 pos)
        {
            for (int i = 0; i < layers.Count; i++)
            {
                layers[i].UpdateCurrChunk(pos);
            }
        }
        /// <summary>
        /// Draw
        /// </summary>
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
        /// <summary>
        /// Check collision
        /// </summary>
        /// <param name="pos">Body position</param>
        /// <param name="size">Body size</param>
        /// <returns></returns>
        public bool BodyColliding(Vector2 pos, Vector2 size)
        {
            for (int i = 0; i < physics_layers.Length; i++)
            {
                if (layers.Count > physics_layers[i])
                {
                    
                    return layers[physics_layers[i]].CheckCollision(pos, size);
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
