using Microsoft.Xna.Framework;
using System;

namespace MySandbox.Core
{
    [Serializable]
    public abstract class Component
    {
        public GameObject @object { get; protected set; }

        public bool is_enabled = true;

        /// <summary>
        /// Construct component
        /// </summary>
        /// <param name="object">Attached object</param>
        public void Construct(GameObject @object)
        {
            this.@object = @object;
            if(is_enabled)
                OnConstruct();
        }
        
        public abstract void OnConstruct();
        /// <summary>
        /// Update component
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public void Update(GameTime gameTime)
        {
            if (is_enabled)
                OnUpdate(gameTime);
        }

        protected abstract void OnUpdate(GameTime gameTime);
    }
}
