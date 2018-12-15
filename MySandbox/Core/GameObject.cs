using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MySandbox.Core
{
    [Serializable]
    public class GameObject
    {
        public Texture2DSheet Sprite{ get; set; }
        
        protected Rectangle current_element;

        public Vector2 Position { get; set; }
        
        public Color color = Color.White;

        public float Rotation { get; set; }
        [JsonIgnore]
        public bool FlipX
        { get { return flip_x; } set{ flip_x = value; UpdateEffects(); } }
        [JsonIgnore]
        protected bool flip_x = false;
        [JsonIgnore]
        public bool FlipY
        { get { return flip_y; } set{ flip_y = value; UpdateEffects(); } }
        [JsonIgnore]
        protected bool flip_y = false;
        
        public float Scale = 1;
        [JsonIgnore]
        public Vector2 Bounds
        { get { return new Vector2(current_element.Width * Scale, current_element.Height * Scale); } }

        public bool DrawThis = true;

        protected SpriteEffects effects;

        protected List<Component> components { get; protected set; } = new List<Component>();

        public virtual List<GameObject> Childrens { get; protected set; } = new List<GameObject>();

        public bool UpdateThis = false;

        /// <summary>
        /// Update current effects
        /// </summary>
        protected void UpdateEffects()
        {
            effects = (FlipX ? SpriteEffects.FlipHorizontally : SpriteEffects.None) | (FlipY ? SpriteEffects.FlipVertically : SpriteEffects.None);
        }

        [JsonConstructor]
        protected GameObject()
        {
        }
        /// <summary>
        /// Set default texture
        /// </summary>
        public void UpdateTexture()
        {
            if (Sprite == null || Sprite.TexturePath == null)
                return;
                Sprite.texture = Core.content.Load<Texture2D>(Sprite.TexturePath);
            SetElement(0);
        }
        /// <summary>
        /// Create gameobject
        /// </summary>
        /// <param name="atlas">Texture atlas</param>
        /// <param name="position">Start position</param>
        /// <param name="rotation">Rotation</param>
        /// <param name="update">Update components and child objects</param>
        public GameObject(Texture2DSheet atlas, Vector2 position, float rotation = 0 , bool update = false)
        {
            Sprite = atlas;
            Position = position;
            SetElement(0);
            this.UpdateThis = update;
        }
        /// <summary>
        /// Add component
        /// </summary>
        /// <param name="component">Component</param>
        public Component AddComponent(Component component)
        {
            components.Add(component);
            component.Construct(this);
            return component;
        }
        /// <summary>
        /// Remove component
        /// </summary>
        /// <param name="component">Component</param>
        public void RemoveComponent(Component component)
        {
            components.Remove(component);
        }
        /// <summary>
        /// Remove component by index
        /// </summary>
        /// <param name="index">List index</param>
        public void RemoveComponent(int index)
        {
            components.RemoveAt(index);
        }
        /// <summary>
        /// Set sprite element
        /// </summary>
        /// <param name="index">elements index</param>
        public void SetElement(int index)
        {
            if (Sprite == null || Sprite.elements.Length <= index)
                return;

            current_element = Sprite.elements[index];
        }
        /// <summary>
        /// Draw object
        /// </summary>
        public virtual void Draw()
        {
            if (!DrawThis)
                return;
            if (Sprite != null && Sprite.texture != null)
                Core.spriteBatch.Draw(Sprite.texture, Position, current_element, color, Rotation, Vector2.Zero, Scale, effects, 0);

            for (int i = 0; i < Childrens.Count; i++)
            {
                Childrens[i].Draw();
            }
        }
        /// <summary>
        /// Update object
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public void Update(GameTime gameTime)
        {
            if (!UpdateThis)
                return;
            for (int i = 0; i < components.Count; i++)
            {
                    components[i].Update(gameTime);
            }
            for (int i = 0; i < Childrens.Count; i++)
            {
                Childrens[i].Update(gameTime);
            }
            OnUpdate();
        }
        /// <summary>
        /// OnUpdate
        /// </summary>
        protected virtual void OnUpdate()
        {

        }
        /// <summary>
        /// To string
        /// </summary>
        public override string ToString()
        {
            return "GameObject [Pos:" + Position.X +  "," + Position.Y +"]" + "; Sprite element :" + current_element;
        }
        /// <summary>
        /// Return center position of sprite
        /// </summary>
        public Vector2 GetCentredPosition()
        {
            return new Vector2(Position.X + Bounds.X/2, Position.Y+Bounds.Y/2);
        }
        /// <summary>
        /// Return component by type
        /// </summary>
        /// <typeparam name="T">Component type</typeparam>
        public T GetComponent<T>() where T : Component
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components is T)
                {
                    return components[i] as T;
                }
            }

            return null;
        }
    }
}
